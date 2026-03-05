using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;

namespace DAO
{
    public class AccBankDAO : BaseClass
    {
        public static rs_AccBankAdd rsAccBank(int code, string msg, string AccBank_id = "")
        {
            return new rs_AccBankAdd()
            {
                result = new rsItem() { retCode = code, retMsg = msg },
                data = new AccBank_id() { BANK_ID = AccBank_id }
            };
        }

        public DataTable haveAccBank(string AccBank_id)
        {
            string sql = "SELECT * FROM ACC_BANK WHERE BANK_ID = @AccBank_id ";

            return comm.DB.RunSQL(sql, new object[] { AccBank_id });
        }

        public rs_AccBankAdd addAccBank(AccBankAdd data)
        {
            if (string.IsNullOrEmpty(data.data.BANK_ID))
                return rsAccBank(1, "未輸入銀行代號");

            DataTable dt = haveAccBank(data.data.BANK_ID);
            if (dt != null && dt.Rows.Count != 0)
                return rsAccBank(1, "銀行代號, 已經存在");

            string sql = $@"INSERT INTO ACC_BANK(
BANK_ID,BANK_NAME,BANK_ABBR,BANK_A_USER_ID,BANK_A_USER_NM,BANK_A_DATE) VALUES (
@BANK_ID,@BANK_NAME,@BANK_ABBR,@BANK_A_USER_ID,@BANK_A_USER_NM,GetDate() ) ";
            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.BANK_ID,
                data.data.BANK_NAME,
                data.data.BANK_ABBR,
                data.baseRequest.employeeNo,
                data.baseRequest.name
            });
            if (bOK)
                return rsAccBank(0, "成功");

            return rsAccBank(1, "失敗");
        }

        public rs updateAccBank(AccBankAdd data)
        {
            if (string.IsNullOrEmpty(data.data.BANK_ID))
                return rsAccBank(1, "未輸入銀行代號");

            DataTable dt = haveAccBank(data.data.BANK_ID);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, "銀行代號不存在");

            string sql = $@"UPDATE ACC_BANK SET 
BANK_NAME=@BANK_NAME, BANK_ABBR=@BANK_ABBR, BANK_U_USER_ID=@BANK_U_USER_ID, BANK_U_USER_NM=@BANK_U_USER_NM, BANK_U_DATE = GetDate()
WHERE BANK_ID='{data.data.BANK_ID}' ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.BANK_NAME,
                data.data.BANK_ABBR,
                data.baseRequest.employeeNo,
                data.baseRequest.name
            });
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs deleteAccBank(AccBankDelete data)
        {
            if (data.data == null)
                return CommDAO.getRs(1, "未輸入銀行代號");
            if (data.data.BANK_ID == null)
                return CommDAO.getRs(1, "未輸入銀行代號");
            if (data.data.BANK_ID.Count == 0)
                return CommDAO.getRs(1, "未輸入銀行代號");

            string AccBank_id = "";
            foreach (string id in data.data.BANK_ID)
            {
                AccBank_id += "'" + id + "',";
            }
            AccBank_id = AccBank_id.Substring(0, AccBank_id.Length - 1);

            string sql = $"DELETE ACC_BANK WHERE BANK_ID IN (" + AccBank_id + ")";

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }


        public rs_AccBankQuery queryAccBank(AccBankQuery dataIn)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "";
            AccBankIDs ida = null;
            Log.Info("sql : " + sql);
            if (dataIn.data != null)
            {
                ida = dataIn.data;
            }
            if (ida == null)
            {
                //  改成沒有找到 MEMO_ID 就不給查詢了。
                return new rs_AccBankQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入銀行代號" },
                };
            }
            else
            {
                if (ida.BANK_ID == null)
                {
                    //  改成沒有找到 MEMO_ID 就不給查詢了。
                    return new rs_AccBankQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入銀行代號" },
                    };
                }
                else if (ida.BANK_ID.Count == 0)
                    sql = "SELECT * FROM ACC_BANK where 1=1 ";
                else
                {
                    sql = "SELECT * FROM ACC_BANK WHERE ";

                    String str_id = "";
                    List<String> ids = ida.BANK_ID;

                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = ids[i];
                        str_id = str_id + "'" + id + "',";
                    }

                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" BANK_ID IN ({str_id}) ";
                }
                //20230912
                if (!string.IsNullOrEmpty(dataIn.data.BANK_NAME))
                {
                    sql += $" and BANK_NAME Like '%{dataIn.data.BANK_NAME}%'";
                }
            }

            sql += " ORDER BY BANK_ID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccBankSimple> rs = dt.ToList<AccBankSimple>();
            return new rs_AccBankQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null
            };
        }

        public rs_AccBankQuery queryAccBank_like(AccBankQuery2 dataIn)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "SELECT * FROM ACC_BANK where 1=1 ";

            if (!string.IsNullOrEmpty(dataIn.data.BANK_ID))
                sql += $" AND BANK_ID Like '{dataIn.data.BANK_ID}%'";

            if (!string.IsNullOrEmpty(dataIn.data.BANK_NAME))
                sql += $" AND BANK_NAME Like '%{dataIn.data.BANK_NAME}%'";

            sql += " ORDER BY BANK_ID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccBankSimple> rs = dt.ToList<AccBankSimple>();
            return new rs_AccBankQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }



    }
}