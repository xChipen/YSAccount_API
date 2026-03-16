using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;

namespace DAO
{
    public class AccAccNameDAO : BaseClass
    {
        public DataTable _Query(string ACCD4)
        {
            string sql = $@"SELECT * from ACC_ACCNAME where 1=1
AND ACNM_ID1='{ACCD4}' 
AND isNull(ACNM_ID2,'')=''
AND isNull(ACNM_ID3,'')=''
";
            return comm.DB.RunSQL(sql);
        }

        public DataTable _Query2(string ACCD4)
        {
            string sql = $@"SELECT * from ACC_ACCNAME where 1=1
AND isNull(ACNM_ID1,'')+isNull(ACNM_ID2,'')+isNull(ACNM_ID3,'')='{ACCD4}'";
            return comm.DB.RunSQL(sql);
        }


        public static rs_AccAccNameAdd rsAccAccName(int code, string msg, AccAccNameID AccAccName_id = null)
        {
            if (AccAccName_id != null)
                return new rs_AccAccNameAdd()
                {
                    result = new rsItem() { retCode = code, retMsg = msg },
                    data = new AccAccNameID()
                    {
                        ACNM_COMPID = AccAccName_id.ACNM_COMPID,
                        ACNM_ID1 = AccAccName_id.ACNM_ID1,
                        ACNM_ID2 = AccAccName_id.ACNM_ID2.Trim(),
                        ACNM_ID3 = AccAccName_id.ACNM_ID3.Trim()
                    }
                };
            else
                return new rs_AccAccNameAdd()
                {
                    result = new rsItem() { retCode = code, retMsg = msg },
                    data = null
                };
        }

        public DataTable haveAccAccName(AccAccNameID AccAccName_id)
        {
            string sql = "SELECT * FROM ACC_ACCNAME WHERE ACNM_COMPID = '" + AccAccName_id.ACNM_COMPID +
                "' AND ACNM_ID1 = '" + AccAccName_id.ACNM_ID1 +
                "' AND ACNM_ID2 = '" + AccAccName_id.ACNM_ID2 +
                "' AND ACNM_ID3 = '" + AccAccName_id.ACNM_ID3 + "' ";

            return comm.DB.RunSQL(sql, new object[] { AccAccName_id });
        }

        public rs_AccAccNameAdd addAccAccName(AccAccNameAdd data)
        {
            // 因為 PK 的關係
            if (string.IsNullOrEmpty(data.data.ACNM_ID2))
                data.data.ACNM_ID2 = "";
            if (string.IsNullOrEmpty(data.data.ACNM_ID3))
                data.data.ACNM_ID3 = "";

            if (string.IsNullOrEmpty(data.data.ACNM_COMPID))
                return rsAccAccName(1, "未輸入公司代號");
            if (string.IsNullOrEmpty(data.data.ACNM_ID1))
                return rsAccAccName(1, "未輸入會計主科目代號");

            AccAccNameID id = new AccAccNameID()
            {
                ACNM_COMPID = data.data.ACNM_COMPID,
                ACNM_ID1 = data.data.ACNM_ID1,
                ACNM_ID2 = data.data.ACNM_ID2,
                ACNM_ID3 = data.data.ACNM_ID3
            };
            DataTable dt = haveAccAccName(id);
            if (dt != null && dt.Rows.Count != 0)
                return rsAccAccName(1, "公司代號, 已經存在");

            string sql = $@"INSERT INTO ACC_ACCNAME(
ACNM_COMPID,ACNM_ID1,ACNM_ID2,ACNM_ID3,ACNM_C_NAME,ACNM_J_NAME,ACNM_E_NAME,ACNM_KIND,ACNM_CODE,
ACNM_SAVE_FLG,ACNM_REMIT_FLG,
ACNM_AR_FLG,ACNM_AP_FLG,ACNM_NR_FLG,ACNM_PK_FLG,
ACNM_START_DATE,ACNM_VALID_DATE,
ACNM_A_USER_ID,ACNM_A_USER_NM,ACNM_A_DATE,
ACNM_FA_FLG,ACNM_TAX_FLG) VALUES (
@ACNM_COMPID,@ACNM_ID1,@ACNM_ID2,@ACNM_ID3,@ACNM_C_NAME,@ACNM_J_NAME,@ACNM_E_NAME,@ACNM_KIND,@ACNM_CODE,
@ACNM_SAVE_FLG,@ACNM_REMIT_FLG,
@ACNM_AR_FLG,@ACNM_AP_FLG,@ACNM_NR_FLG,@ACNM_PK_FLG,
@ACNM_START_DATE,@ACNM_VALID_DATE,
@ACNM_A_USER_ID,@ACNM_A_USER_NM,GetDate(),
@ACNM_FA_FLG,@ACNM_TAX_FLG ) ";
            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.ACNM_COMPID,
                data.data.ACNM_ID1,
                data.data.ACNM_ID2,
                data.data.ACNM_ID3,
                data.data.ACNM_C_NAME,
                data.data.ACNM_J_NAME,
                data.data.ACNM_E_NAME,
                data.data.ACNM_KIND,
                data.data.ACNM_CODE,
                data.data.ACNM_SAVE_FLG,
                data.data.ACNM_REMIT_FLG,
                data.data.ACNM_AR_FLG,
                data.data.ACNM_AP_FLG,
                data.data.ACNM_NR_FLG,
                data.data.ACNM_PK_FLG,
                data.data.ACNM_START_DATE,
                data.data.ACNM_VALID_DATE,
                data.baseRequest.employeeNo,
                data.baseRequest.name,
                data.data.ACNM_FA_FLG,
                data.data.ACNM_TAX_FLG
            });
            if (bOK)
                return rsAccAccName(0, "成功");

            return rsAccAccName(1, "失敗");
        }

        public rs updateAccAccName(AccAccNameAdd data)
        {
            // 因為 PK 的關係
            if (string.IsNullOrEmpty(data.data.ACNM_ID2))
                data.data.ACNM_ID2 = "";
            if (string.IsNullOrEmpty(data.data.ACNM_ID3))
                data.data.ACNM_ID3 = "";

            if (string.IsNullOrEmpty(data.data.ACNM_COMPID))
                return rsAccAccName(1, "未輸入公司代號");
            if (string.IsNullOrEmpty(data.data.ACNM_ID1))
                return rsAccAccName(1, "未輸入會計主科目代號");

            AccAccNameID id = new AccAccNameID()
            {
                ACNM_COMPID = data.data.ACNM_COMPID,
                ACNM_ID1 = data.data.ACNM_ID1.Trim(),
                ACNM_ID2 = data.data.ACNM_ID2.Trim(),
                ACNM_ID3 = data.data.ACNM_ID3.Trim()
            };

            DataTable dt = haveAccAccName(id);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, " 公司代號不存在");

            string sql = $@"UPDATE ACC_ACCNAME SET 
ACNM_C_NAME=@ACNM_C_NAME,ACNM_J_NAME=@ACNM_J_NAME,ACNM_E_NAME=@ACNM_E_NAME,ACNM_KIND=@ACNM_KIND,ACNM_CODE=@ACNM_CODE,
ACNM_SAVE_FLG=@ACNM_SAVE_FLG,ACNM_REMIT_FLG=@ACNM_REMIT_FLG,
ACNM_AR_FLG=@ACNM_AR_FLG,ACNM_AP_FLG=@ACNM_AP_FLG,ACNM_NR_FLG=@ACNM_NR_FLG,ACNM_PK_FLG=@ACNM_PK_FLG,
ACNM_START_DATE=@ACNM_START_DATE,ACNM_VALID_DATE=@ACNM_VALID_DATE,
ACNM_U_USER_ID=@ACNM_U_USER_ID, ACNM_U_USER_NM=@ACNM_U_USER_NM, ACNM_U_DATE = GetDate(),
ACNM_FA_FLG=@ACNM_FA_FLG, ACNM_TAX_FLG=@ACNM_TAX_FLG
WHERE ACNM_COMPID='{data.data.ACNM_COMPID}' AND ACNM_ID1='{data.data.ACNM_ID1}' 
AND ACNM_ID2='{data.data.ACNM_ID2}' AND ACNM_ID3='{data.data.ACNM_ID3}' ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.ACNM_C_NAME,
                data.data.ACNM_J_NAME,
                data.data.ACNM_E_NAME,
                data.data.ACNM_KIND,
                data.data.ACNM_CODE,
                data.data.ACNM_SAVE_FLG,
                data.data.ACNM_REMIT_FLG,
                data.data.ACNM_AR_FLG,
                data.data.ACNM_AP_FLG,
                data.data.ACNM_NR_FLG,
                data.data.ACNM_PK_FLG,
                data.data.ACNM_START_DATE,
                data.data.ACNM_VALID_DATE,
                data.baseRequest.employeeNo,
                data.baseRequest.name,
                data.data.ACNM_FA_FLG,
                data.data.ACNM_TAX_FLG
            });
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs deleteAccAccName(AccAccNameDelete data)
        {
            if (string.IsNullOrEmpty(data.data.ACNM_COMPID))
                return rsAccAccName(1, "未輸入公司代號");
            if (data.data.ACNM_ID1.Count == 0)
                return rsAccAccName(1, "未輸入會計主科目代號");
            if (data.data.ACNM_ID2.Count == 0)
                return rsAccAccName(1, "未輸入會計子科目代號-二階");
            if (data.data.ACNM_ID3.Count == 0)
                return rsAccAccName(1, "未輸入會計子科目代號-三階");

            string id1 = "";
            foreach (string id in data.data.ACNM_ID1)
            {
                id1 += "'" + id + "',";
            }
            id1 = id1.Substring(0, id1.Length - 1);

            string id2 = "";
            foreach (string id in data.data.ACNM_ID2)
            {
                id2 += "'" + id + "',";
            }
            id2 = id2.Substring(0, id2.Length - 1);

            string id3 = "";
            foreach (string id in data.data.ACNM_ID3)
            {
                id3 += "'" + id + "',";
            }
            id3 = id3.Substring(0, id3.Length - 1);

            string sql = $"DELETE ACC_ACCNAME WHERE ACNM_COMPID = '" + data.data.ACNM_COMPID + "' AND ACNM_ID1 IN (" + id1 + ")" +
                " AND ACNM_ID2 IN (" + id2 + ") AND ACNM_ID3 IN (" + id3 + ")";

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        // Add by Chipen
        public rs deleteAccAccName2(AccAccNameDelete2 data)
        {
            if (string.IsNullOrEmpty(data.data.ACNM_COMPID))
                return rsAccAccName(1, "未輸入公司代號");
            if (data.data.ACNM_ID.Count == 0)
                return rsAccAccName(1, "未輸入會計科目代號");

            string id1 = "";
            foreach (string id in data.data.ACNM_ID)
            {
                id1 += "'" + id + "',";
            }
            id1 = id1.Substring(0, id1.Length - 1);

            string sql = $"DELETE ACC_ACCNAME WHERE ACNM_COMPID = '" + data.data.ACNM_COMPID +
"' AND RTrim(ACNM_ID1)+RTrim(ACNM_ID2)+RTrim(ACNM_ID3) IN (" + id1 + ")"; 

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs_AccAccNameQuery queryAccAccName(AccAccNameQuery dataIn)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region SQL & Check property
            string sql = "";

            AccAccNameIDs ida = null;

            if (dataIn.data != null)
            {
                ida = dataIn.data;
            }
            if (ida == null)
            {
                //  改成沒有找到 ACNM_COMPID 就不給查詢了。
                return new rs_AccAccNameQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                };
            }
            else
            {
                #region 資料檢查
                if (ida.ACNM_COMPID == null || ida.ACNM_COMPID == "")
                {
                    //  改成沒有找到 ACNM_COMPID 就不給查詢了。
                    return new rs_AccAccNameQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                    };
                }
                #endregion

                //
                sql = $@"SELECT IsNull(ACNM_ID1,'')+IsNull(ACNM_ID2,'')+IsNull(ACNM_ID3,'') as ACNM_ID,
*, BACT_FLG,BACT_DEPT_FLG,BACT_TRANID_FLG,BACT_INVNO_FLG FROM ACC_ACCNAME 
LEFT JOIN ACC_BALANCE_CONTROL ON ACNM_ID1 = BACT_ACNMID
WHERE ACNM_COMPID = '" + ida.ACNM_COMPID + "' ";

                #region SQL

                string subStr = "";
                if (ida.ACNM_ID.Count != 0)
                {
                    foreach (var item in ida.ACNM_ID)
                    {
                        subStr += $" (IsNull(ACNM_ID1,'')+IsNull(ACNM_ID2,'')+IsNull(ACNM_ID3,'') = '{item}') OR ";
                    }
                    subStr = subStr.Substring(0, subStr.Length - 3);
                    sql += $" AND ({subStr})";
                }
                #endregion

                #region SQL Other
                if (!string.IsNullOrEmpty(dataIn.data.ACNM_C_NAME))
                {
                    sql += $" AND ACNM_C_NAME Like '%{dataIn.data.ACNM_C_NAME}%'";
                }
                if (!string.IsNullOrEmpty(dataIn.data.ACNM_J_NAME))
                {
                    sql += $" AND ACNM_J_NAME Like '%{dataIn.data.ACNM_J_NAME}%'";
                }
                if (!string.IsNullOrEmpty(dataIn.data.ACNM_E_NAME))
                {
                    sql += $" AND ACNM_E_NAME Like '%{dataIn.data.ACNM_E_NAME}%'";
                }
                #endregion
            }
            sql += " ORDER BY ACNM_COMPID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            Log.Info("sql : " + sql);

            DataTable dt = comm.DB.RunSQL(sql);

            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List <AccAccNameSimple> rs = dt.ToList<AccAccNameSimple>();
            return new rs_AccAccNameQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null
            };
        }

        public rs_AccAccNameQuery queryAccAccNameByRange(AccAccNameQueryByRange dataIn)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "";
            AccAccNameRangeIDs ida = null;
            if (dataIn.data != null)
            {
                ida = dataIn.data;
            }
            if (ida == null)
            {
                //  改成沒有找到 ACNM_COMPID 就不給查詢了。
                return new rs_AccAccNameQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                };
            }
            else
            {
                if (ida.ACNM_COMPID == null || ida.ACNM_COMPID == "")
                {
                    //  改成沒有找到 ACNM_COMPID 就不給查詢了。
                    return new rs_AccAccNameQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                    };
                }
                sql = "SELECT * FROM ACC_ACCNAME WHERE ACNM_COMPID = '" + ida.ACNM_COMPID + "' ";

                if (ida.ACNM_BEGIN_ID == null)
                {
                    return new rs_AccAccNameQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入(起)科目代號" },
                    };
                }
                if (ida.ACNM_END_ID == null)
                {
                    return new rs_AccAccNameQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入(訖)科目代號" },
                    };
                }

                if (!string.IsNullOrEmpty(ida.ACNM_KIND))
                {
                    sql = sql + " AND ACNM_KIND = '" + ida.ACNM_KIND + "' ";
                }

                if (!string.IsNullOrEmpty(ida.ACNM_CODE))
                {
                    sql = sql + " AND ACNM_CODE = '" + ida.ACNM_CODE + "' ";
                }

                if (ida.ACNM_QUERY_TYPE == 0)
                {
                    //  不用管
                }
                else if (ida.ACNM_QUERY_TYPE == 1)  //  0 or 1.主科目, 2.包含子科目-二階, 3.包含子科目-三階
                {
                    sql = sql + " AND ACNM_ID1 BETWEEN '" + ida.ACNM_BEGIN_ID + "' AND '" + ida.ACNM_END_ID + "' ";
                }
                else if (ida.ACNM_QUERY_TYPE == 2)
                {
                    sql = sql + " AND ACNM_ID1 + RTrim(ACNM_ID2) BETWEEN '" + ida.ACNM_BEGIN_ID + "' AND '" + ida.ACNM_END_ID + "' ";
                }
                else if (ida.ACNM_QUERY_TYPE == 3)
                {
                    sql = sql + " AND ACNM_ID1 + RTrim(ACNM_ID2) + RTrim(ACNM_ID3) BETWEEN '" + ida.ACNM_BEGIN_ID + "' AND '" + ida.ACNM_END_ID + "' ";
                }
                else
                {
                    return new rs_AccAccNameQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "無法辨識的(科目類型)參數。" },
                    };
                }

                if (ida.ACNM_QUERY_VALID == 0 || ida.ACNM_QUERY_VALID == 1)  //  0 or 1.全部, 2.有效, 3.無效
                {
                    //  不用管
                }
                else if (ida.ACNM_QUERY_VALID == 2)
                {
                    sql = sql + " AND ACNM_START_DATE <= GETDATE() AND ACNM_VALID_DATE >= GETDATE() ";
                }
                else if (ida.ACNM_QUERY_VALID == 3)
                {
                    sql = sql + " AND ACNM_START_DATE >= GETDATE() OR ACNM_VALID_DATE <= GETDATE() ";
                }
                else
                {
                    return new rs_AccAccNameQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "無法辨識的(全部，有效或無效區間)參數。" },
                    };
                }
                Log.Info("sql : " + sql);

                sql += " ORDER BY ACNM_COMPID ";
                if (pageNumber != 0)
                    sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

                DataTable dt = comm.DB.RunSQL(sql);

                int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

                List<AccAccNameSimple> rs = dt.ToList<AccAccNameSimple>();
                return new rs_AccAccNameQuery()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = rs,
                    pagination = pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null
                };
            }
        }

        //                                            託收行 
        private static string getSQL(int kind, string Remittingbank = "")
        {
            string sSQL = "";
            switch (kind) {
                case 2:
                    sSQL = " AND ACNM_REMIT_FLG='Y'";
                    break;
                case 3:
                    sSQL = " AND ACNM_ID2='' AND ACNM_ID3='' AND ACNM_ID1 <'3'";
                    break;
                case 4:
                    if (!string.IsNullOrEmpty(Remittingbank) )
                        sSQL = $" AND ACNM_ID1+ACNM_ID2+ACNM_ID3='{Remittingbank}'";

                    sSQL = $" AND ACNM_SAVE_FLG = 'Y' ";
                    break;
            }
            return sSQL;
        }
        // 下拉式 : 查詢
        public rs_AccAccNameQuery23 query23(AccAccNameQuery23 data, int kind = 0)
        {
            //
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region 必要條件檢查
            if (string.IsNullOrEmpty(data.data.ACNM_COMPID))
            {
                return new rs_AccAccNameQuery23()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                };
            }
            #endregion

            #region SQL 查詢式
            string sql = "SELECT IsNull(ACNM_ID1,'')+IsNull(ACNM_ID2,'')+IsNull(ACNM_ID3,'') as ACNM_ID1, ACNM_C_NAME FROM ACC_ACCNAME WHERE ACNM_COMPID = '" + data.data.ACNM_COMPID + "' ";

            //                  託收行
            sql += getSQL(kind, data.data.ACNM_ID);

            if (!string.IsNullOrEmpty(data.data.ACNM_ID))
                sql += $" AND ACNM_ID1+ACNM_ID2+ACNM_ID3 like '{data.data.ACNM_ID}%'";

            if (!string.IsNullOrEmpty(data.data.ACNM_C_NAME))
                sql += $" AND ACNM_C_NAME like '%{data.data.ACNM_C_NAME}%'";
            if (!string.IsNullOrEmpty(data.data.ACNM_J_NAME))
                sql += $" AND ACNM_J_NAME like '%{data.data.ACNM_J_NAME}%'";
            if (!string.IsNullOrEmpty(data.data.ACNM_E_NAME))
                sql += $" AND ACNM_E_NAME like '%{data.data.ACNM_E_NAME}%'";

            //
            sql += " ORDER BY ACNM_COMPID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccAccName_NO> rs = dt.ToList<AccAccName_NO>();
            return new rs_AccAccNameQuery23()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }


        // 20231106 ACNM_ID LIKE "", 
        // 20231120 ACNM_ID2 = ""
        // 20240322 add ACNM_FA_FLG
        public rs_AccAccNameQuery query5(AccAccNameQuery23 data)
        {
            string sql = "";
            if (data.data.ACNM_COMPID == null || data.data.ACNM_COMPID == "")
            {
                //  改成沒有找到 ACNM_COMPID 就不給查詢了。
                return new rs_AccAccNameQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                };
            }

            sql = $@"SELECT IsNull(ACNM_ID1,'')+IsNull(ACNM_ID2,'')+IsNull(ACNM_ID3,'') as ACNM_ID,* 
FROM ACC_ACCNAME WHERE ACNM_COMPID = '" + data.data.ACNM_COMPID + "' ";

            if (!string.IsNullOrEmpty(data.data.ACNM_ID))
                sql += $@" AND (IsNull(ACNM_ID1,'')+IsNull(ACNM_ID2,'')+IsNull(ACNM_ID3,'') 
LIKE '{data.data.ACNM_ID}%') ";

            if (data.data.ACNM_ID1 != null)
                sql += CommDAO.sql_ep(data.data.ACNM_ID1, "ACNM_ID1");
            if (data.data.ACNM_ID2 != null)
                sql += CommDAO.sql_ep(data.data.ACNM_ID2, "ACNM_ID2");
            if (data.data.ACNM_ID3 != null)
                sql += CommDAO.sql_ep(data.data.ACNM_ID3, "ACNM_ID3");

            #region SQL Other
            if (!string.IsNullOrEmpty(data.data.ACNM_C_NAME))
            {
                sql += $" AND ACNM_C_NAME Like '%{data.data.ACNM_C_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_J_NAME))
            {
                sql += $" AND ACNM_J_NAME Like '%{data.data.ACNM_J_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_E_NAME))
            {
                sql += $" AND ACNM_E_NAME Like '%{data.data.ACNM_E_NAME}%'";
            }
            //20231204 ACE090R_銀行存款調節表 ACNM_PK_FLG = 'Y'
            sql += CommDAO.sql_ep(data.data.ACNM_PK_FLG, "ACNM_PK_FLG");
            sql += CommDAO.sql_ep(data.data.ACNM_AP_FLG, "ACNM_AP_FLG");
            //20240322
            sql += CommDAO.sql_ep(data.data.ACNM_FA_FLG, "ACNM_FA_FLG");
            #endregion

            sql += " AND LEN(ACNM_ID1) = 4 "; // 20260210

            sql += " ORDER BY ACNM_COMPID ";

            return query(sql, data.pagination);
        }

        // ACNM_COMPID = [螢幕] 公司代號AND
        // ACNM_ID1 LIKE ‘6%’ AND
        // LEN(ACNM_ID1) = 4
        // 20260210
        public rs_AccAccNameQuery query6(AccAccNameQuery23 data)
        {
            string sql = "";
            if (data.data.ACNM_COMPID == null || data.data.ACNM_COMPID == "")
            {
                //  改成沒有找到 ACNM_COMPID 就不給查詢了。
                return new rs_AccAccNameQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                };
            }

            sql = $@"SELECT IsNull(ACNM_ID1,'')+IsNull(ACNM_ID2,'')+IsNull(ACNM_ID3,'') as ACNM_ID,* 
FROM ACC_ACCNAME WHERE ACNM_COMPID = '" + data.data.ACNM_COMPID + "' ";

            sql += " AND ACNM_ID1 LIKE '6%' AND LEN(ACNM_ID1) = 4 AND ACNM_ID2='' ";
            sql += " ORDER BY ACNM_COMPID ";

            return query(sql, data.pagination);
        }

        public rs_AccAccNameQuery query5_2(AccAccNameQuery23 data)
        {
            string sql = "";
            if (data.data.ACNM_COMPID == null || data.data.ACNM_COMPID == "")
            {
                //  改成沒有找到 ACNM_COMPID 就不給查詢了。
                return new rs_AccAccNameQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                };
            }

            sql = $@"SELECT IsNull(ACNM_ID1,'')+IsNull(ACNM_ID2,'')+IsNull(ACNM_ID3,'') as ACNM_ID,* 
FROM ACC_ACCNAME WHERE LEN(ACNM_ID1) = 4  AND ACNM_ID2='' AND ACNM_COMPID = '" + data.data.ACNM_COMPID + "' ";

            if (!string.IsNullOrEmpty(data.data.ACNM_ID))
                sql += $@" AND (IsNull(ACNM_ID1,'')+IsNull(ACNM_ID2,'')+IsNull(ACNM_ID3,'') 
LIKE '{data.data.ACNM_ID}%') ";

            if (data.data.ACNM_ID1 != null)
                sql += CommDAO.sql_ep(data.data.ACNM_ID1, "ACNM_ID1");
            if (data.data.ACNM_ID2 != null)
                sql += CommDAO.sql_ep(data.data.ACNM_ID2, "ACNM_ID2");
            if (data.data.ACNM_ID3 != null)
                sql += CommDAO.sql_ep(data.data.ACNM_ID3, "ACNM_ID3");

            #region SQL Other
            if (!string.IsNullOrEmpty(data.data.ACNM_C_NAME))
            {
                sql += $" AND ACNM_C_NAME Like '%{data.data.ACNM_C_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_J_NAME))
            {
                sql += $" AND ACNM_J_NAME Like '%{data.data.ACNM_J_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_E_NAME))
            {
                sql += $" AND ACNM_E_NAME Like '%{data.data.ACNM_E_NAME}%'";
            }
            //20231204 ACE090R_銀行存款調節表 ACNM_PK_FLG = 'Y'
            sql += CommDAO.sql_ep(data.data.ACNM_PK_FLG, "ACNM_PK_FLG");
            sql += CommDAO.sql_ep(data.data.ACNM_AP_FLG, "ACNM_AP_FLG");
            //20240322
            sql += CommDAO.sql_ep(data.data.ACNM_FA_FLG, "ACNM_FA_FLG");
            #endregion

            sql += " AND LEN(ACNM_ID1) = 4 "; // 20260210

            sql += " ORDER BY ACNM_COMPID ";

            return query(sql, data.pagination);
        }



        public rs_AccAccNameQuery query(string sql, Pagination pagination)
        {
            if (string.IsNullOrEmpty(sql))
                return new rs_AccAccNameQuery { result = new rsItem { retCode=1, retMsg="失敗"} }; 

            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(pagination, out pageNumber, out pageSize, out pageNumbers);

            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            Log.Info("sql : " + sql);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccAccNameSimple> rs = dt.ToList<AccAccNameSimple>();
            return new rs_AccAccNameQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

    }
}