using System;
using System.Collections.Generic;
using System.Data;
using Models;
using Helpers;

namespace DAO
{
    public class AccAccKindDAO : BaseClass
    {
        public rs_AccAccKindAdd rsAccAccKind(int code, string msg, string AccAccKind_id = "")
        {
            return new rs_AccAccKindAdd()
            {
                result = new rsItem() { retCode = code, retMsg = msg },
                data = new AccAccKindID() { ACKD_ID = AccAccKind_id }
            };
        }

        public DataTable haveAccAccKind(string AccAccKind_id)
        {
            string sql = "SELECT * FROM ACC_ACC_KIND WHERE ACKD_ID = @AccAccKind_id ";

            return comm.DB.RunSQL(sql, new object[] { AccAccKind_id });
        }

        // 新增
        public bool _addAccAccKind(AccAccKind data, string employeeNo, string name, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(data.ACKD_ID))
            {
                Log.Info("_addAccAccKind: 未輸入會計大分類代號");
                return false;
            }

            DataTable dt = haveAccAccKind(data.ACKD_ID);
            if (dt != null && dt.Rows.Count != 0)
            {
                Log.Info("_addAccAccKind: 會計大分類代號, 已經存在");
                return false;
            }

            string sql = $@"INSERT INTO ACC_ACC_KIND(
ACKD_ID,ACKD_C_NAME,ACKD_J_NAME,ACKD_E_NAME,ACKD_A_USER_ID,ACKD_A_USER_NM,ACKD_A_DATE) VALUES (
@ACKD_ID,@ACKD_C_NAME,@ACKD_J_NAME,@ACKD_E_NAME,@ACKD_A_USER_ID,@MEMO_A_USER_NM,GetDate() ) ";

            object[] obj = new object[] {
                data.ACKD_ID,
                data.ACKD_C_NAME,
                data.ACKD_J_NAME,
                data.ACKD_E_NAME,
                employeeNo,
                name
            };
            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }
        public rs_AccAccKindAdd addAccAccKind(AccAccKindAdd data)
        {
            if (string.IsNullOrEmpty(data.data.ACKD_ID))
                return rsAccAccKind(1, "未輸入會計大分類代號");

            DataTable dt = haveAccAccKind(data.data.ACKD_ID);
            if (dt != null && dt.Rows.Count != 0)
                return rsAccAccKind(1, "會計大分類代號, 已經存在");

            bool bOK = _addAccAccKind(data.data, data.baseRequest.employeeNo, data.baseRequest.name);

            if (bOK)
                return rsAccAccKind(0, "成功");

            return rsAccAccKind(1, "失敗");
        }

        // 修改
        public bool _updateAccAccKind(AccAccKind data, string employeeNo, string name, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(data.ACKD_ID))
            {
                Log.Info("_updateAccAccKind: 未輸入會計大分類代號");
                return false;
            }

            DataTable dt = haveAccAccKind(data.ACKD_ID);
            if (dt == null || dt.Rows.Count == 0)
            {
                Log.Info("_updateAccAccKind: 會計大分類代號不存在");
            }

            string sql = $@"UPDATE ACC_ACC_KIND SET 
ACKD_C_NAME=@ACKD_C_NAME, ACKD_J_NAME=@ACKD_J_NAME, ACKD_E_NAME=@ACKD_E_NAME, 
ACKD_U_USER_ID=@ACKD_U_USER_ID, ACKD_U_USER_NM=@ACKD_U_USER_NM, ACKD_U_DATE = GetDate()
WHERE ACKD_ID='{data.ACKD_ID}' ";

            object[] obj = new object[] {
                data.ACKD_C_NAME,
                data.ACKD_J_NAME,
                data.ACKD_E_NAME,
                employeeNo,
                name
            };
            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rs updateAccAccKind(AccAccKindAdd data)
        {
            if (string.IsNullOrEmpty(data.data.ACKD_ID))
                return rsAccAccKind(1, "未輸入會計大分類代號");

            DataTable dt = haveAccAccKind(data.data.ACKD_ID);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, " 會計大分類代號不存在");

            bool bOK = _updateAccAccKind(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        // 刪除
        public bool _deleteAccAccKind(AccAccKindIDs data, CommDAO dao = null)
        {
            if (data.ACKD_ID.Count == 0)
            {
                Log.Info("_deleteAccAccKind: 未輸入摘要代號");
                return false;
            }

            string AccAccKind_id = "";
            foreach (string id in data.ACKD_ID)
            {
                AccAccKind_id += "'" + id + "',";
            }
            AccAccKind_id = AccAccKind_id.Substring(0, AccAccKind_id.Length - 1);

            string sql = $"DELETE ACC_ACC_KIND WHERE ACKD_ID IN (" + AccAccKind_id + ")";

            if (dao == null)
                return comm.DB.ExecSQL(sql);
            else
                return dao.DB.ExecSQL_T(sql);
        }

        public bool _deleteAccAccKind_ByItem(string ACKD_ID)
        {
            if (string.IsNullOrEmpty( ACKD_ID ))
            {
                Log.Info("_deleteAccAccKind: 未輸入摘要代號");
                return false;
            }

            string sql = $"DELETE ACC_ACC_KIND WHERE ACKD_ID=@ACKD_ID";

            return comm.DB.ExecSQL(sql, new object[] { ACKD_ID });
        }

        public rs deleteAccAccKind(AccAccKindDelete data)
        {
            if (data.data.ACKD_ID.Count == 0)
                return CommDAO.getRs(1, "未輸入摘要代號");

            bool bOK = _deleteAccAccKind(data.data);

            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs_AccAccKindQuery queryAccAccKind(AccAccKindQuery dataIn)
        {
            int pageNumber=0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "";
            AccAccKindIDs ida = null;
            Log.Info("sql : " + sql);
            if (dataIn.data != null)
            {
                ida = dataIn.data;
            }
            if (ida == null)
            {
                //  改成沒有找到 ACKD_ID 就不給查詢了。
                return new rs_AccAccKindQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入摘要代號" },
                };
            }
            else
            {
                if (ida.ACKD_ID == null)
                {
                    //  改成沒有找到 MEMO_ID 就不給查詢了。
                    return new rs_AccAccKindQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入摘要代號" },
                    };
                }
                else if (ida.ACKD_ID.Count == 0)
                    sql = "SELECT * FROM ACC_ACC_KIND ";
                else
                {
                    sql = "SELECT * FROM ACC_ACC_KIND WHERE ";
                    String str_id = "";
                    List<String> ids = ida.ACKD_ID;

                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = ids[i];
                        str_id = str_id + "'" + id + "',";
                    }
                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" ACKD_ID IN ({str_id});";
                }
            }

            sql += " ORDER BY ACKD_ID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);

            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccAccKindSimple> rs = dt.ToList<AccAccKindSimple>();
            return new rs_AccAccKindQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber!=0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }
    }
}