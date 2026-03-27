using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccQueryDAO : BaseClass
    {
        public rsACD030B_Query_result _ACD030B_Query(ACD030B_Query data)
        {
            if (string.IsNullOrEmpty(data.COMPID))
            {
                return new rsACD030B_Query_result { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            string sql = "";

            if (data.KIND == 1)
            {
                sql = $@"Select 
b.TRAN_ABBR as ABBR, a.PURM_NO as NO, a.PURM_DATE as DATE, 
a.PURM_CURRID as CURRID, a.PURM_NET_AMT as NT_AMT ,
PURM_FOR_TOT_AMT as FOR_AMT, 
PURM_TOT_AMT as AMT 
From ACC_PURCH_M a
left join vw_TRAIN b on a.PURM_VENDID = b.TRAN_ID
Where a.PURM_COMPID='{data.COMPID}' 
and PURM_VOUNO = '' AND PURM_VALID = 'Y' AND PURM_KIND = '1'
";

                if (data.BDATE != null && data.BDATE != DateTime.MinValue && data.EDATE != null && data.EDATE != DateTime.MinValue)
                {
                    sql += $@" and  convert(char(8), a.PURM_DATE ,112) >= '{string.Format("{0:yyyyMMdd}", data.BDATE)}' 
 and convert(char(8), a.PURM_DATE, 112) <= '{string.Format("{0:yyyyMMdd}", data.EDATE)}'";
                }
                if (!string.IsNullOrEmpty(data.VENDID))
                {
                    sql += $@" and a.PURM_VENDID='{data.VENDID}'";
                }
            }
            else if (data.KIND == 2)
            {
                sql = $@"Select
b.TRAN_ABBR as ABBR, c.TRAN_ABBR as ABBR2, a.IMPM_NO as NO, a.IMPM_DATE as DATE, 
a.IMPM_CURRID as CURRID, a.IMPM_NT_AMT as NT_AMT ,
IMPM_FOR_AMT as FOR_AMT, 
IsNull(IMPM_TARIFF,0)
+ IsNull(IMPM_BROKEAGE_FEE,0)
+ IsNull(IMPM_INSURANCE,0)
+ IsNull(IMPM_FREIGHT,0)
+ IsNull(IMPM_OTHER_FEE,0) as AMT 
From ACC_IMPORT_EXPENSE_M a
left join vw_TRAIN b on a.IMPM_VENDID = b.TRAN_ID
left join vw_TRAIN c on a.IMPM_BROKERID = c.TRAN_ID
Where a.IMPM_COMPID = '{data.COMPID}' 
and IMPM_VOUNO ='' AND IMPM_VALID = 'Y' AND IMPM_KIND = '1'
";

                if (data.BDATE != null && data.BDATE != DateTime.MinValue && data.EDATE != null && data.EDATE != DateTime.MinValue)
                {
                    sql += $@" and convert(char(8), a.IMPM_DATE, 112) >= '{string.Format("{0:yyyyMMdd}", data.BDATE)}'
 and convert(char(8), a.IMPM_DATE, 112) <= '{string.Format("{0:yyyyMMdd}", data.EDATE)}'";
                }
                if (!string.IsNullOrEmpty(data.VENDID))
                {
                    sql += $@" and a.IMPM_VENDID = '{data.VENDID}'";
                }
            }
            else if (data.KIND == 3)
            {
                sql = $@"Select
b.TRAN_ABBR as ABBR, c.TRAN_ABBR as ABBR2, a.IMPM_NO as NO, a.IMPM_DATE as DATE, 
a.IMPM_CURRID as CURRID, a.IMPM_NT_AMT as NT_AMT ,
IMPM_FOR_AMT as FOR_AMT, 
IsNull(IMPM_TARIFF,0)
+ IsNull(IMPM_BROKEAGE_FEE,0)
+ IsNull(IMPM_INSURANCE,0)
+ IsNull(IMPM_FREIGHT,0)
+ IsNull(IMPM_OTHER_FEE,0) as AMT 
From ACC_IMPORT_EXPENSE_M a
left join vw_TRAIN b on a.IMPM_VENDID = b.TRAN_ID
left join vw_TRAIN c on a.IMPM_BROKERID = c.TRAN_ID
Where a.IMPM_COMPID = '{data.COMPID}' 
and IMPM_VOUNO ='' AND IMPM_VALID = 'Y' AND IMPM_KIND = '2'
";

                if (data.BDATE != null && data.BDATE != DateTime.MinValue && data.EDATE != null && data.EDATE != DateTime.MinValue)
                {
                    sql += $@" and convert(char(8), a.IMPM_DATE, 112) >= '{string.Format("{0:yyyyMMdd}", data.BDATE)}'
 and convert(char(8), a.IMPM_DATE, 112) <= '{string.Format("{0:yyyyMMdd}", data.EDATE)}'";
                }
                if (!string.IsNullOrEmpty(data.VENDID))
                {
                    sql += $@" and a.IMPM_VENDID = '{data.VENDID}'";
                }
            }
            else
            {
                sql = $@"Select 
b.TRAN_ABBR as ABBR, a.PURM_NO as NO, a.PURM_DATE as DATE, 
a.PURM_CURRID as CURRID, a.PURM_NET_AMT as NT_AMT ,
PURM_FOR_TOT_AMT as FOR_AMT, 
PURM_TOT_AMT as AMT 
From ACC_PURCH_M a
left join vw_TRAIN b on a.PURM_VENDID = b.TRAN_ID
Where a.PURM_COMPID='{data.COMPID}' 
and PURM_VOUNO = '' AND PURM_VALID = 'Y' AND PURM_KIND = '2'
";

                if (data.BDATE != null && data.BDATE != DateTime.MinValue && data.EDATE != null && data.EDATE != DateTime.MinValue)
                {
                    sql += $@" and  convert(char(8), a.PURM_DATE ,112) >= '{string.Format("{0:yyyyMMdd}", data.BDATE)}' 
 and convert(char(8), a.PURM_DATE, 112) <= '{string.Format("{0:yyyyMMdd}", data.EDATE)}'";
                }
                if (!string.IsNullOrEmpty(data.VENDID))
                {
                    sql += $@" and a.PURM_VENDID='{data.VENDID}'";
                }
            }
            //Log.Info(sql);

        DataTable dt = comm.DB.RunSQL(sql);

            return new rsACD030B_Query_result {
                result = CommDAO.getRsItem(),
                data = dt.ToList<rsACD030B_Query>()
            };
        }

        // ACC_PURCH_VOUNO
        public rsACC_PURCH_VOUNO_result ACC_PURCH_VOUNO_Query(ACC_PURCH_VOUNO data)
        {
            string sql = $@"select * from ACC_PURCH_VOUNO where 1=1 ";

            if (!string.IsNullOrEmpty(data.PUVO_USER_ID))
            {
                sql += CommDAO.sql_ep(data.PUVO_USER_ID, "PUVO_USER_ID");
            }
            if (!string.IsNullOrEmpty(data.PUVO_PROGID))
            {
                sql += CommDAO.sql_ep(data.PUVO_PROGID, "PUVO_PROGID");
            }

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsACC_PURCH_VOUNO_result {

                result = CommDAO.getRsItem(),
                data = dt.ToList<ACC_PURCH_VOUNO>()
            };
        }


    }
}