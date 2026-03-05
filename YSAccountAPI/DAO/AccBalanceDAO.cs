using System;
using System.Collections.Generic;
using System.Data;
using Models;
using Helpers;

namespace DAO
{
    public class AccBalanceDAO : BaseClass
    {
        public static rs_AccBalanceAdd rsAccBalance(int code, string msg, AccBalanceID AccBalance_id = null)
        {
            if (AccBalance_id != null)
            return new rs_AccBalanceAdd()
            {
                result = new rsItem() { retCode = code, retMsg = msg },
                data = new AccBalanceID() 
                { 
                     
                    ACBL_COMPID = AccBalance_id.ACBL_COMPID,
                    ACBL_YEAR = AccBalance_id.ACBL_YEAR,
                    ACBL_MONTH = AccBalance_id.ACBL_MONTH,
                    ACBL_CODE = AccBalance_id.ACBL_CODE
                    
                }
            };
            else
                return new rs_AccBalanceAdd()
                {
                    result = new rsItem() { retCode = code, retMsg = msg },
                    data = null
                };
        }

        public DataTable haveAccBalance(AccBalanceID AccBalance_id)
        {
            //string s = new JavaScriptSerializer().Serialize(AccBalance_id);
            //Log.Info("haveAccBalance AccBalance_id : " + s);

            string sql = "SELECT * FROM ACC_BALANCE WHERE ACBL_COMPID = '" + AccBalance_id.ACBL_COMPID + 
                "' AND ACBL_YEAR= " + AccBalance_id.ACBL_YEAR + " " +
                "AND ACBL_MONTH= " + AccBalance_id.ACBL_MONTH + " AND ACBL_CODE= '" + AccBalance_id.ACBL_CODE + "'";

            return comm.DB.RunSQL(sql, new object[] {});
        }

        public rs_AccBalanceAdd addAccBalance(AccBalanceAdd data)
        {
            //string s = new JavaScriptSerializer().Serialize(data);
            //Log.Info("dataIn string : " + s);
            if (string.IsNullOrEmpty(data.data.ACBL_COMPID))
                return rsAccBalance(1, "未輸入公司代號");
            if (data.data.ACBL_YEAR == 0)
                return rsAccBalance(1, "未輸入會計年度");
            if (data.data.ACBL_MONTH == 0)
                return rsAccBalance(1, "未輸入會計月份");
            if (string.IsNullOrEmpty(data.data.ACBL_CODE))
                return rsAccBalance(1, "未輸入會計科目代號");
            AccBalanceID id = new AccBalanceID()
            {
                ACBL_COMPID = data.data.ACBL_COMPID,
                ACBL_YEAR = data.data.ACBL_YEAR,
                ACBL_MONTH = data.data.ACBL_MONTH,
                ACBL_CODE = data.data.ACBL_CODE
            };
            //s = new JavaScriptSerializer().Serialize(id);
            //Log.Info("before haveAccBalance s : " + s);
            DataTable dt = haveAccBalance(id);
            //Log.Info("after haveAccBalance 000 !!");
            if (dt != null && dt.Rows.Count != 0)
            {
                //Log.Info("dt NOT NULL!!");
                return rsAccBalance(1, "會計科目代號, 已經存在");
            }
            //Log.Info("after haveAccBalance 111 !!");
            string sql = $@"INSERT INTO ACC_BALANCE(
ACBL_COMPID, ACBL_YEAR, ACBL_MONTH, ACBL_CODE, ACBL_DBAL, ACBL_CBAL, ACBL_DAMT1, ACBL_DAMT2,
ACBL_CAMT1, ACBL_CAMT2, ACBL_DCNT, ACBL_CCNT) VALUES (
@ACBL_COMPID, @ACBL_YEAR, @ACBL_MONTH, @ACBL_CODE, @ACBL_DBAL, @ACBL_CBAL, @ACBL_DAMT1, @ACBL_DAMT2,
@ACBL_CAMT1, @ACBL_CAMT2, @ACBL_DCNT, @ACBL_CCNT ) ";
            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.ACBL_COMPID,
                data.data.ACBL_YEAR,
                data.data.ACBL_MONTH,
                data.data.ACBL_CODE,
                data.data.ACBL_DBAL,
                data.data.ACBL_CBAL,
                data.data.ACBL_DAMT1,
                data.data.ACBL_DAMT2,
                data.data.ACBL_CAMT1,
                data.data.ACBL_CAMT2,
                data.data.ACBL_DCNT,
                data.data.ACBL_CCNT
            });
            if (bOK)
                return rsAccBalance(0, "成功");

            return rsAccBalance(1, "失敗");
        }

        public rs updateAccBalance(AccBalanceAdd data)
        {
            if (string.IsNullOrEmpty(data.data.ACBL_COMPID))
                return rsAccBalance(1, "未輸入公司代號");
            if (data.data.ACBL_YEAR == 0)
                return rsAccBalance(1, "未輸入會計年度");
            if (data.data.ACBL_MONTH == 0)
                return rsAccBalance(1, "未輸入會計月份");
            if (string.IsNullOrEmpty(data.data.ACBL_CODE))
                return rsAccBalance(1, "未輸入會計科目代號");
            AccBalanceID id = new AccBalanceID()
            {
                ACBL_COMPID = data.data.ACBL_COMPID,
                ACBL_YEAR = data.data.ACBL_YEAR,
                ACBL_MONTH = data.data.ACBL_MONTH,
                ACBL_CODE = data.data.ACBL_CODE
            };
            DataTable dt = haveAccBalance(id);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, " 會計科目代號不存在");

            string sql = $@"UPDATE ACC_BALANCE SET 
ACBL_DBAL = @ACBL_DBAL, ACBL_CBAL = @ACBL_CBAL, ACBL_DAMT1 = @ACBL_DAMT1, ACBL_DAMT2 = @ACBL_DAMT2,
ACBL_CAMT1 = @ACBL_CAMT1, ACBL_CAMT2 = @ACBL_CAMT2, ACBL_DCNT = @ACBL_DCNT, ACBL_CCNT = @ACBL_CCNT 
WHERE ACBL_COMPID='{data.data.ACBL_COMPID}' AND ACBL_YEAR={data.data.ACBL_YEAR} AND
ACBL_MONTH={data.data.ACBL_MONTH} AND ACBL_CODE='{data.data.ACBL_CODE}' ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.ACBL_DBAL,
                data.data.ACBL_CBAL,
                data.data.ACBL_DAMT1,
                data.data.ACBL_DAMT2,
                data.data.ACBL_CAMT1,
                data.data.ACBL_CAMT2,
                data.data.ACBL_DCNT,
                data.data.ACBL_CCNT
            });
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs deleteAccBalance(AccBalanceDelete data)
        {
            AccBalanceIDs ids = data.data;
            string AccBalance_id = "";
            if (ids.ACBL_COMPID == "")
                return CommDAO.getRs(1, "未輸入公司代號");
            if (ids.ACBL_YEAR == 0)
                return CommDAO.getRs(1, "未輸入會計年度");
            if (ids.ACBL_MONTH == 0)
                return CommDAO.getRs(1, "未輸入會計月份");
            if (ids.ACBL_CODE.Count == 0)
                return CommDAO.getRs(1, "未輸入會計科目代號");
            

            foreach (string id in ids.ACBL_CODE)
            {
                AccBalance_id += "'" + id + "',";
            }
            AccBalance_id = AccBalance_id.Substring(0, AccBalance_id.Length - 1);

            string sql = $"DELETE ACC_BALANCE WHERE ACBL_COMPID='{ids.ACBL_COMPID}' AND ACBL_YEAR={ids.ACBL_YEAR} AND " + 
                $"ACBL_MONTH ={ids.ACBL_MONTH} AND ACBL_CODE IN (" + AccBalance_id + ")";

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public rs_AccBalanceQuery queryAccBalance(AccBalanceQuery dataIn)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "";
            AccBalanceIDs ida = null;

            if (dataIn.data != null)
            {
                ida = dataIn.data;
            }
            if (ida == null)
            {
                //  改成沒有找到 MEMO_ID 就不給查詢了。
                return new rs_AccBalanceQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入會計科目代號" },
                };
            }
            else
            {
                sql = "SELECT * FROM ACC_BALANCE WHERE ";

                if (ida.ACBL_COMPID == null)
                {
                    //  改成沒有找到 ACBL_COMPID 就不給查詢了。
                    return new rs_AccBalanceQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                    };
                }
                sql = sql + "ACBL_COMPID = '" + ida.ACBL_COMPID + "' ";
                if (ida.ACBL_YEAR == 0)
                {
                    //  改成沒有找到 ACBL_COMPID 就不給查詢了。
                    return new rs_AccBalanceQuery()
                    {
                        result = new rsItem() { retCode = 1, retMsg = "未輸入會計年度" },
                    };
                }
                sql = sql + "AND ACBL_YEAR = " + ida.ACBL_YEAR + " ";
                if (ida.ACBL_MONTH != 0)
                {
                    sql = sql + "AND ACBL_MONTH = " + ida.ACBL_MONTH + " ";
                }
                if (ida.ACBL_CODE.Count != 0)
                {
                    String str_id = "";
                    List<String> ids = ida.ACBL_CODE;
                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = ids[i];
                        //Log.Info("ids[" + i + "] : " + id);
                        str_id = str_id + "'" + id + "',";
                    }
                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" AND ACBL_CODE IN ({str_id});";
                }
            }

            sql += " ORDER BY ACBL_COMPID,ACBL_YEAR,ACBL_MONTH,ACBL_CODE ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccBalance> rs = dt.ToList<AccBalance>();
                    
            return new rs_AccBalanceQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null
            };
        }

    }
}