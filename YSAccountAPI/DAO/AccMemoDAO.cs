using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;

namespace DAO
{
    public class AccMemoDAO : BaseClass
    {
        public static rs_AccMemoAdd rsAccMemo(int code, string msg, string AccMemo_id = "")
        {
            return new rs_AccMemoAdd()
            {
                result = new rsItem() { retCode = code, retMsg = msg },
                data = new AccMemo_id() { MEMO_ID = AccMemo_id }
            };
        }

        public DataTable haveAccMemo(string AccMemo_id)
        {
            string sql = "SELECT * FROM ACC_MEMO WHERE MEMO_ID = @AccMemo_id ";

            return comm.DB.RunSQL(sql, new object[] { AccMemo_id });
        }

        public rs_AccMemoAdd addAccMemo(AccMemoAdd data)
        {
            if (string.IsNullOrEmpty(data.data.MEMO_ID))
                return rsAccMemo(1, "未輸入摘要代號");

            DataTable dt = haveAccMemo(data.data.MEMO_ID);
            if (dt != null && dt.Rows.Count != 0)
                return rsAccMemo(1, "摘要代號, 已經存在");

            string sql = $@"INSERT INTO ACC_MEMO(
MEMO_ID,MEMO_NAME,MEMO_A_USER_ID,MEMO_A_USER_NM,MEMO_A_DATE) VALUES (
@MEMO_ID,@MEMO_NAME,@MEMO_A_USER_ID,@MEMO_A_USER_NM,GetDate() ) ";
            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.MEMO_ID,
                data.data.MEMO_NAME,
                data.baseRequest.employeeNo,
                data.baseRequest.name
            });
            if (bOK)
                return rsAccMemo(0, "成功");

            return rsAccMemo(1, "失敗");
        }

        public rs updateAccMemo(AccMemoAdd data)
        {
            if (string.IsNullOrEmpty(data.data.MEMO_ID))
                return rsAccMemo(1, "未輸入摘要代號");

            DataTable dt = haveAccMemo(data.data.MEMO_ID);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, " 摘要代號不存在");

            string sql = $@"UPDATE ACC_MEMO SET 
MEMO_NAME=@MEMO_NAME, MEMO_U_USER_ID=@MEMO_U_USER_ID, MEMO_U_USER_NM=@MEMO_U_USER_NM, MEMO_U_DATE = GetDate()
WHERE MEMO_ID='{data.data.MEMO_ID}' ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.MEMO_NAME,
                data.baseRequest.employeeNo,
                data.baseRequest.name
            });
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs deleteAccMemo(AccMemoDelete data)
        {
            if (data.data.MEMO_ID.Count == 0)
                return CommDAO.getRs(1, "未輸入摘要代號");

            string AccMemo_id = "";
            foreach (string id in data.data.MEMO_ID)
            {
                AccMemo_id += "'" + id + "',";
            }
            AccMemo_id = AccMemo_id.Substring(0, AccMemo_id.Length - 1);

            string sql = $"DELETE ACC_MEMO WHERE MEMO_ID IN (" + AccMemo_id + ")";

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs_AccMemoQuery queryAccMemo2(AccMemoQuery2 data)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "SELECT * from ACC_MEMO where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.MEMO_ID) && !string.IsNullOrEmpty(data.data.MEMO_ID_E))
            {
                sql += $" and MEMO_ID>='{data.data.MEMO_ID}' and MEMO_ID<='{data.data.MEMO_ID_E}'";
            }
            else
            {
                if (!string.IsNullOrEmpty(data.data.MEMO_ID))
                    sql += $" and MEMO_ID='{data.data.MEMO_ID}'";
            }
            // 20230920
            if (!string.IsNullOrEmpty(data.data.MEMO_NAME)) {
                sql += $" and MEMO_NAME LIKE '%{data.data.MEMO_ID}%' ";
            }

            sql += " ORDER BY MEMO_ID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccMemoSimple> rs = dt.ToList<AccMemoSimple>();
            return rsAccMemo(rs,
                pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null ,
                0, "成功");
        }

        public rs_AccMemoQuery queryAccMemo(AccMemoQuery dataIn)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "";
            AccMemoIDs ida = null;
            Log.Info("sql : " + sql);
            if (dataIn.data != null)
            {
                ida = dataIn.data;
            }
            if (ida == null)
            {
                //  改成沒有找到 MEMO_ID 就不給查詢了。
                return new rs_AccMemoQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入摘要代號" },
                };
                //Log.Info("first Count 0 : " + ida.id.Count);
                //sql = "SELECT * FROM ACC_MEMO;";
            }
            else
            {
                if (ida.MEMO_ID == null)
                {
                    //  改成沒有找到 MEMO_ID 就不給查詢了。
                    return new rs_AccMemoQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入摘要代號" },
                    };
                }
                else if (ida.MEMO_ID.Count == 0)
                    sql = "SELECT * FROM ACC_MEMO where 1=1 ";
                else
                {
                    //Log.Info("data Count : " + ida.id.Count);
                    sql = "SELECT * FROM ACC_MEMO WHERE ";
                    String str_id = "";
                    List<String> ids = ida.MEMO_ID;
                    //Log.Info("ids Count : " + ids.Count);
                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = ids[i];
                        str_id = str_id + "'" + id + "',";
                    }
                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" MEMO_ID IN ({str_id}) ";
                }

                if (!string.IsNullOrEmpty(dataIn.data.MEMO_NAME))
                {
                    sql += $"and MEMO_NAME LIKE '%{dataIn.data.MEMO_NAME}%' ";
                }
            }
            sql += " ORDER BY MEMO_ID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccMemoSimple> rs = dt.ToList<AccMemoSimple>();
            return new rs_AccMemoQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null
            };
        }

        public static rs_AccMemoQuery rsAccMemo(List<AccMemoSimple> data = null, Pagination pagination =null, int retCode = 1, string retMsg = "失敗")
        {
            return new rs_AccMemoQuery()
            {
                result = new rsItem() { retCode = retCode, retMsg = retMsg },
                data = data,
                pagination= pagination
            };
        }


    }
}