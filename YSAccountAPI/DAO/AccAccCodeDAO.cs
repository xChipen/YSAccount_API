using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;

namespace DAO
{
    public class AccAccCodeDAO : BaseClass
    {
        public rs_AccAccCodeAdd rsAccAccCode(int code, string msg, AccAccCodeID AccAccCode_id = null)
        {
            if (AccAccCode_id != null)
                return new rs_AccAccCodeAdd()
                {
                    result = new rsItem() { retCode = code, retMsg = msg },
                    data = new AccAccCodeID()
                    {
                        ACCD_KIND = AccAccCode_id.ACCD_KIND,
                        ACCD_ID = AccAccCode_id.ACCD_ID
                    }
                };
            else
                return new rs_AccAccCodeAdd()
                {
                    result = new rsItem() { retCode = code, retMsg = msg },
                    data = null
                };
        }

        public DataTable haveAccAccCode(AccAccCode AccAccCode_id)
        {
            string sql = "SELECT * FROM ACC_ACC_CODE WHERE ACCD_KIND = '" + AccAccCode_id.ACCD_KIND +
                "' AND ACCD_ID= '" + AccAccCode_id.ACCD_ID + "' ";

            return comm.DB.RunSQL(sql, new object[] { });
        }

        // 新增
        public bool _addAccAccCode(AccAccCode data, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(data.ACCD_KIND))
            {
                Log.Info("_addAccAccCode: 未輸入會計大分類代號");
                return false;
            }
            if (string.IsNullOrEmpty(data.ACCD_ID))
            {
                Log.Info("_addAccAccCode: 未輸入會計中分類代號");
                return false;
            }

            string sql = $@"INSERT INTO ACC_ACC_CODE(
ACCD_KIND, ACCD_ID, ACCD_C_NAME, ACCD_J_NAME, ACCD_E_NAME) VALUES (
@ACCD_KIND, @ACCD_ID, @ACCD_C_NAME, @ACCD_J_NAME, @ACCD_E_NAME ) ";

            object[] obj = new object[] {
                data.ACCD_KIND,
                data.ACCD_ID,
                data.ACCD_C_NAME,
                data.ACCD_J_NAME,
                data.ACCD_E_NAME
            };
            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public  rs_AccAccCodeAdd addAccAccCode(AccAccCodeAdd data)
        {
            if (string.IsNullOrEmpty(data.data.ACCD_KIND))
                return rsAccAccCode(1, "未輸入會計大分類代號");

            if (string.IsNullOrEmpty(data.data.ACCD_ID))
                return rsAccAccCode(1, "未輸入會計中分類代號");

            AccAccCode id = new AccAccCode()
            {
                ACCD_KIND = data.data.ACCD_KIND,
                ACCD_ID = data.data.ACCD_ID
            };

            DataTable dt = haveAccAccCode(id);
            if (dt != null && dt.Rows.Count != 0)
            {
                return rsAccAccCode(1, "會計中分類代號, 已經存在");
            }

            bool bOK = _addAccAccCode(data.data);
            if (bOK)
                return rsAccAccCode(0, "成功");

            return rsAccAccCode(1, "失敗");
        }

        // 修改
        public bool _updateAccAccCode(AccAccCode data, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(data.ACCD_KIND))
            {
                Log.Info("_updateAccAccCode: 未輸入會計大分類代號");
                return false;
            }
            if (string.IsNullOrEmpty(data.ACCD_ID)) {
                Log.Info("_updateAccAccCode: 未輸入會計中分類代號");
                return false;
            }

            string sql = $@"UPDATE ACC_ACC_CODE SET 
ACCD_C_NAME = @ACCD_C_NAME, ACCD_J_NAME = @ACCD_J_NAME, ACCD_E_NAME = @ACCD_E_NAME 
WHERE ACCD_KIND='{data.ACCD_KIND}' AND ACCD_ID='{data.ACCD_ID}' ";

            object[] obj = new object[] {
                data.ACCD_C_NAME,
                data.ACCD_J_NAME,
                data.ACCD_E_NAME
            };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rs updateAccAccCode(AccAccCodeAdd data)
        {
            if (string.IsNullOrEmpty(data.data.ACCD_KIND))
                return rsAccAccCode(1, "未輸入會計大分類代號");
            if (string.IsNullOrEmpty(data.data.ACCD_ID))
                return rsAccAccCode(1, "未輸入會計中分類代號");

            AccAccCode id = new AccAccCode()
            {
                ACCD_KIND = data.data.ACCD_KIND,
                ACCD_ID = data.data.ACCD_ID
            };
            DataTable dt = haveAccAccCode(id);
            if (dt == null || dt.Rows.Count == 0)
            {
                return rsAccAccCode(1, "會計中分類代號不存在");
            }

            bool bOK = _updateAccAccCode(data.data);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        // 刪除
        public bool _deleteAccAccCode(AccAccCodeIDs data)
        {
            AccAccCodeIDs ids = data;
            string AccAccCode_id = "";
            if (ids.ACCD_KIND == "")
            {
                Log.Info("_deleteAccAccCode: 未輸入會計大分類代號");
                return false;
            }
            if (ids.ACCD_ID.Count == 0)
            {
                Log.Info("_deleteAccAccCode: 未輸入會計中分類代號");
                return false;
            }

            foreach (string id in ids.ACCD_ID)
            {
                AccAccCode_id += "'" + id + "',";
            }
            AccAccCode_id = AccAccCode_id.Substring(0, AccAccCode_id.Length - 1);

            string sql = $"DELETE ACC_ACC_CODE WHERE ACCD_KIND='{ids.ACCD_KIND}' AND ACCD_ID IN (" + AccAccCode_id + ")";

            return comm.DB.ExecSQL(sql);
        }

        public bool _deleteAccAccCode_ByItem(string ACCD_KIND, string ACCD_ID)
        {
            if ( string.IsNullOrEmpty( ACCD_KIND))
            {
                Log.Info("_deleteAccAccCode: 未輸入會計大分類代號");
                return false;
            }
            if ( string.IsNullOrEmpty( ACCD_ID ))
            {
                Log.Info("_deleteAccAccCode: 未輸入會計中分類代號");
                return false;
            }

            string sql = $"DELETE ACC_ACC_CODE WHERE ACCD_KIND=@ACCD_KIND AND ACCD_ID=@ACCD_ID";

            return comm.DB.ExecSQL(sql, new object[] { ACCD_KIND, ACCD_ID });
        }

        public rs deleteAccAccCode(AccAccCodeDelete data)
        {
            if (data.data.ACCD_KIND == "")
                return CommDAO.getRs(1, "未輸入會計大分類代號");
            if (data.data.ACCD_ID.Count == 0)
                return CommDAO.getRs(1, "未輸入會計中分類代號");

            bool bOK = _deleteAccAccCode(data.data);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        // 查詢 By ACCD_KIND
        public DataTable _queryAccAccCode_ByACCD_KIND(List<string> data)
        {
            string sql = "SELECT * FROM ACC_ACC_CODE WHERE 1=1";

            String str_id = "";
            if (data.Count != 0)
            {
                foreach (string ss in data)
                {
                    str_id = str_id + "'" + ss + "',";
                }
                str_id = str_id.Substring(0, str_id.Length - 1);
                sql += $" AND ACCD_KIND IN ({str_id}) ";
            }

            sql += " ORDER BY ACCD_KIND, ACCD_ID ";

            return comm.DB.RunSQL(sql);
        }
        public rs_AccAccCodeQuery queryAccAccCode(AccAccCodeQuery dataIn)
        {
            int pageNumber=0;
            int pageSize=0;
            int pageNumbers=0;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "";
            AccAccCodeIDs ida = null;

            if (dataIn.data != null)
            {
                ida = dataIn.data;
            }
            if (ida == null)
            {
                //  改成沒有找到 MEMO_ID 就不給查詢了。
                return new rs_AccAccCodeQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入會計大分類代號" },
                };
            }
            else
            {
                sql = "SELECT * FROM ACC_ACC_CODE WHERE ";

                if (ida.ACCD_KIND == null)
                {
                    //  改成沒有找到 ACBL_COMPID 就不給查詢了。
                    return new rs_AccAccCodeQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入會計中分類代號" },
                    };
                }
                else if (ida.ACCD_KIND != "")
                {
                    sql = sql + "ACCD_KIND = '" + ida.ACCD_KIND + "' ";
                }
                else
                {
                    sql = sql + " 1 = 1 ";
                }
                
                if (ida.ACCD_ID.Count != 0)
                {
                    String str_id = "";
                    List<String> ids = ida.ACCD_ID;
                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = ids[i];
                        //Log.Info("ids[" + i + "] : " + id);
                        str_id = str_id + "'" + id + "',";
                    }
                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" AND ACCD_ID IN ({str_id});";
                }
            }

            sql += " ORDER BY ACCD_KIND,ACCD_ID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt;
            if (comm.DB.RunSQL(out dt, sql))
            {
                List<AccAccCode> rs = dt.ToList<AccAccCode>();

                int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

                return new rs_AccAccCodeQuery()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = rs,
                    pagination = pageNumber!=0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
                };
            }

            return new rs_AccAccCodeQuery()
            {
                result = new rsItem() { retCode = 1, retMsg = "查詢失敗" },
            };
        }
    }
}