using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;

namespace DAO
{
    public class AccBudgetDAO : BaseClass
    {
        public static rs_AccBudgetAdd rsAccBudget(int code, string msg, AccBudgetID AccBudget_id = null)
        {
            if (AccBudget_id != null)
                return new rs_AccBudgetAdd()
                {
                    result = new rsItem() { retCode = code, retMsg = msg },
                    data = new AccBudgetID()
                    {
                        BUDG_COMPID = AccBudget_id.BUDG_COMPID,
                        BUDG_YEAR = AccBudget_id.BUDG_YEAR,
                        BUDG_MONTH = AccBudget_id.BUDG_MONTH,
                        BUDG_DEPTID = AccBudget_id.BUDG_DEPTID,
                        BUDG_ACNMID = AccBudget_id.BUDG_ACNMID
                    }
                };
            else
                return new rs_AccBudgetAdd()
                {
                    result = new rsItem() { retCode = code, retMsg = msg },
                    data = null
                };
        }

        public DataTable haveAccBudget(AccBudget AccBudget_id)
        {
            string sql = "SELECT * FROM ACC_BUDGET WHERE BUDG_COMPID = '" + AccBudget_id.BUDG_COMPID +
                "' AND BUDG_YEAR = " + AccBudget_id.BUDG_YEAR +
                " AND BUDG_MONTH = " + AccBudget_id.BUDG_MONTH +
                " AND BUDG_DEPTID = '" + AccBudget_id.BUDG_DEPTID + "' " +
                " AND BUDG_ACNMID = '" + AccBudget_id.BUDG_ACNMID + "' ";

            return comm.DB.RunSQL(sql, new object[] { AccBudget_id });
        }

        public rs_AccBudgetAdd addAccBudget(AccBudgetAdd data)
        {
            if (string.IsNullOrEmpty(data.data.BUDG_COMPID))
                return rsAccBudget(1, "未輸入公司代號");
            if (data.data.BUDG_YEAR==0)
                return rsAccBudget(1, "未輸入年度");
            if (data.data.BUDG_MONTH==0)
                return rsAccBudget(1, "未輸入月份");
            if (string.IsNullOrEmpty(data.data.BUDG_DEPTID))
                return rsAccBudget(1, "未輸入部門代號");
            if (string.IsNullOrEmpty(data.data.BUDG_ACNMID))
                return rsAccBudget(1, "未輸入會計科目代號");

            DataTable dt = haveAccBudget(data.data);
            if (dt != null && dt.Rows.Count != 0)
                return rsAccBudget(1, "會計科目代號, 已經存在");

            bool bOK = insert(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            if (bOK)
                return rsAccBudget(0, "成功");

            return rsAccBudget(1, "失敗");
        }

        #region 批次匯入
        private bool insert(AccBudget data, string employeeNo, string name, CommDAO dao= null)
        {
            if (string.IsNullOrEmpty(data.BUDG_COMPID))
                return false;
            if (data.BUDG_YEAR == 0)
                return false;
            if (data.BUDG_MONTH == 0)
                return false;
            if (string.IsNullOrEmpty(data.BUDG_DEPTID))
                return false;
            if (string.IsNullOrEmpty(data.BUDG_ACNMID))
                return false;

            string sql = $@"INSERT INTO ACC_BUDGET(
BUDG_COMPID, BUDG_YEAR, BUDG_MONTH, BUDG_DEPTID, BUDG_ACNMID, BUDG_B_AMT, BUDG_R_AMT, BUDG_D_AMT1, BUDG_D_AMT2, 
BUDG_A_USER_ID,BUDG_A_USER_NM,BUDG_A_DATE) VALUES (
@BUDG_COMPID, @BUDG_YEAR, @BUDG_MONTH, @BUDG_DEPTID, @BUDG_ACNMID, @BUDG_B_AMT, @BUDG_R_AMT, @BUDG_D_AMT1, @BUDG_D_AMT2, 
@BUDG_A_USER_ID, @BUDG_A_USER_NM, GetDate() ) ";

            object[] obj = new object[] {
                data.BUDG_COMPID,
                data.BUDG_YEAR,
                data.BUDG_MONTH,
                data.BUDG_DEPTID,
                data.BUDG_ACNMID,
                data.BUDG_B_AMT,
                data.BUDG_R_AMT,
                data.BUDG_D_AMT1,
                data.BUDG_D_AMT2,
                employeeNo,
                name
            };
            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);

        }
        private bool deleteYearData(AccBudget data, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(data.BUDG_COMPID))
                return false;
            if (data.BUDG_YEAR == 0)
                return false;

            string sql = $@"DELETE ACC_BUDGET WHERE BUDG_COMPID =@BUDG_COMPID AND BUDG_YEAR=@BUDG_YEAR";

            object[] obj = new object[] { data.BUDG_COMPID, data.BUDG_YEAR};

            if (dao == null )
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }
        public rs_AccBudgetAdd batch(AccBudget_ins data)
        {
            bool bOK = true;
            if (data.data.Count != 0)
            {
                CommDAO dao = new CommDAO();
                dao.DB.BeginTransaction();
                try
                {
                    bOK = deleteYearData(data.data[0], dao);
                    if (bOK)
                    {
                        foreach (AccBudget item in data.data)
                        {
                            bOK = insert(item, data.baseRequest.employeeNo, data.baseRequest.name, dao);
                            if (bOK == false) break;
                        }
                    }
                    if (bOK)
                    {
                        dao.DB.Commit();
                        return new rs_AccBudgetAdd { result = new rsItem { retCode = 0, retMsg = "成功" } };
                    }
                    dao.DB.Rollback();
                }
                catch(Exception ex) {
                    Log.Info(ex.Message);
                    dao.DB.Rollback();
                }
            }
            return new rs_AccBudgetAdd { result = new rsItem { retCode = 1, retMsg = "失敗" } };
        }
        #endregion

        public rs updateAccBudget(AccBudgetAdd data)
        {
            if (string.IsNullOrEmpty(data.data.BUDG_COMPID))
                return rsAccBudget(1, "未輸入公司代號");
            if (data.data.BUDG_YEAR == 0)
                return rsAccBudget(1, "未輸入年度");
            if (data.data.BUDG_MONTH == 0)
                return rsAccBudget(1, "未輸入月份");
            if (string.IsNullOrEmpty(data.data.BUDG_DEPTID))
                return rsAccBudget(1, "未輸入部門代號");
            if (string.IsNullOrEmpty(data.data.BUDG_ACNMID))
                return rsAccBudget(1, "未輸入會計科目代號");

            DataTable dt = haveAccBudget(data.data);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, " 公司代號不存在");

            string sql = $@"UPDATE ACC_BUDGET SET " +
                $@"BUDG_B_AMT=@BUDG_B_AMT, BUDG_R_AMT=@BUDG_R_AMT, BUDG_D_AMT1=@BUDG_D_AMT1, BUDG_D_AMT2=@BUDG_D_AMT2 " +
                $@"WHERE BUDG_COMPID='{data.data.BUDG_COMPID}' AND BUDG_YEAR={data.data.BUDG_YEAR} " +
                $@"AND BUDG_MONTH={data.data.BUDG_MONTH} AND BUDG_DEPTID='{data.data.BUDG_DEPTID}' AND BUDG_ACNMID='{data.data.BUDG_ACNMID}' ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.BUDG_B_AMT,
                data.data.BUDG_R_AMT,
                data.data.BUDG_D_AMT1,
                data.data.BUDG_D_AMT2
            });
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs deleteAccBudget(AccBudgetDelete data)
        {
            if (string.IsNullOrEmpty(data.data.BUDG_COMPID))
                return rsAccBudget(1, "未輸入公司代號");
            if (data.data.BUDG_YEAR.Count == 0)
                return rsAccBudget(1, "未輸入年度");
            if (data.data.BUDG_MONTH.Count == 0)
                return rsAccBudget(1, "未輸入月份");
            if (data.data.BUDG_DEPTID.Count == 0)
                return rsAccBudget(1, "未輸入部門代號");
            if (data.data.BUDG_ACNMID.Count == 0)
                return rsAccBudget(1, "未輸入會計科目代號");

            string id1 = "";
            foreach (int id in data.data.BUDG_YEAR)
            {
                id1 += id + ",";
            }
            id1 = id1.Substring(0, id1.Length - 1);

            string id2 = "";
            foreach (int id in data.data.BUDG_MONTH)
            {
                id2 += id + ",";
            }
            id2 = id2.Substring(0, id2.Length - 1);

            string id3 = "";
            foreach (string id in data.data.BUDG_DEPTID)
            {
                id3 += "'" + id + "',";
            }
            id3 = id3.Substring(0, id3.Length - 1);

            string id4 = "";
            foreach (string id in data.data.BUDG_ACNMID)
            {
                id4 += "'" + id + "',";
            }
            id4 = id4.Substring(0, id4.Length - 1);

            string sql = $"DELETE ACC_BUDGET WHERE BUDG_COMPID = '" + data.data.BUDG_COMPID + "' AND BUDG_YEAR IN (" + id1 + ")" +
                " AND BUDG_MONTH IN (" + id2 + ") AND BUDG_DEPTID IN (" + id3 + ") AND BUDG_ACNMID IN (" + id4 + ") ";

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs_AccBudgetQuery queryAccBudget(AccBudgetQuery dataIn)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "";
            AccBudgetIDs ida = null;
            Log.Info("sql : " + sql);
            if (dataIn.data != null)
            {
                ida = dataIn.data;
            }
            if (ida == null)
            {
                //  改成沒有找到 ACNM_COMPID 就不給查詢了。
                return new rs_AccBudgetQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                };
            }
            else
            {
                if (ida.BUDG_COMPID == null || ida.BUDG_COMPID == "")
                {
                    //  改成沒有找到 ACNM_COMPID 就不給查詢了。
                    return new rs_AccBudgetQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                    };
                }
                if (ida.BUDG_YEAR == null)
                {
                    return new rs_AccBudgetQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入年度" },
                    };
                }
                if (ida.BUDG_MONTH == null)
                {
                    return new rs_AccBudgetQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入月份" },
                    };
                }
                if (ida.BUDG_DEPTID == null)
                {
                    return new rs_AccBudgetQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入部門代號" },
                    };
                }
                if (ida.BUDG_ACNMID == null)
                {
                    return new rs_AccBudgetQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入會計科目代號" },
                    };
                }
                sql = "SELECT * FROM ACC_BUDGET WHERE BUDG_COMPID = '" + ida.BUDG_COMPID + "' ";

                if (ida.BUDG_YEAR.Count != 0)
                {
                    String str_id = "";
                    List<int> ids = ida.BUDG_YEAR;
                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = "" + ids[i];
                        str_id = str_id + "" + id + ",";
                    }
                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" AND BUDG_YEAR IN ({str_id}) ";
                }
                if (ida.BUDG_MONTH.Count != 0)
                {
                    String str_id = "";
                    List<int> ids = ida.BUDG_MONTH;

                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = "" + ids[i];
                        str_id = str_id + "" + id + ",";
                    }
                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" AND BUDG_MONTH IN ({str_id}) ";
                }
                if (ida.BUDG_DEPTID.Count != 0)
                {
                    String str_id = "";
                    List<String> ids = ida.BUDG_DEPTID;

                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = ids[i];
                        str_id = str_id + "'" + id + "',";
                    }
                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" AND BUDG_DEPTID IN ({str_id}) ";
                }
                if (ida.BUDG_ACNMID.Count != 0)
                {

                    String str_id = "";
                    List<String> ids = ida.BUDG_ACNMID;

                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = ids[i];
                        str_id = str_id + "'" + id + "',";
                    }
                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" AND BUDG_ACNMID IN ({str_id}) ";
                }
            }

            sql += " ORDER BY BUDG_COMPID,BUDG_YEAR,BUDG_MONTH,BUDG_DEPTID,BUDG_ACNMID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccBudgetSimple> rs = dt.ToList<AccBudgetSimple>();
            return new rs_AccBudgetQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null
            };
        }
    }
}