using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccMonthAccountDAO : BaseClass
    {
        public rs_AccMonthAccount_query check(AccMonthAccount_query data)
        {
            if (string.IsNullOrEmpty(data.data.MOAC_COMPID))
                return (rs_AccMonthAccount_query)CommDAO.getRs(1, "未輸入公司代碼");

            if (data.data.MOAC_YEAR == null)
                return (rs_AccMonthAccount_query)CommDAO.getRs(1, "會計年月的年");

            if (data.data.MOAC_MONTH == null)
                return (rs_AccMonthAccount_query)CommDAO.getRs(1, "會計年月的月");

            string sql = $@"SELECT MOAC_CURRID,MAX(ISNULL(EXRA_RATE_E,0)) as EXRA_RATE_E
FROM ACC_MONTH_ACCOUNT
LEFT JOIN ACC_EXRATE ON MOAC_YEAR = EXRA_YEAR AND MOAC_MONTH=EXRA_MONTH AND
MOAC_CURRID = EXRA_CURRID
WHERE MOAC_COMPID =@MOAC_COMPID
AND MOAC_YEAR =@MOAC_YEAR
AND MOAC_MONTH =@MOAC_MONTH
AND MOAC_CURRID <> 'NTD'
AND MOAC_BAL + MOAC_D_AMT - MOAC_C_AMT > 0
GROUP BY MOAC_CURRID";

            DataTable dt = comm.DB.RunSQL(sql, new object[] {
            data.data.MOAC_COMPID,
            data.data.MOAC_YEAR,
            data.data.MOAC_MONTH});

            List<rs_AccMonthAccount_query_item> rs = dt.ToList<rs_AccMonthAccount_query_item>();

            return new rs_AccMonthAccount_query
            {
                result = CommDAO.getRsItem(),
                data = rs
            };
        }


    }
}