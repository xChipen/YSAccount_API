using Models;
using System.Data;
using Helpers;
using System.Collections.Generic;

namespace DAO
{
    public class AccMonthValuationDAO : BaseClass
    {
        public rsAccMonthValuation_query AccMonthValuation_query_max(AccMonthValuation_qry_max data) {

            if (string.IsNullOrEmpty(data.data.MOVA_COMPID))
                return (rsAccMonthValuation_query)CommDAO.getRs(1, "未輸入公司代碼");

            if (data.data.MOVA_YEAR == null)
                return (rsAccMonthValuation_query)CommDAO.getRs(1, "會計年月的年");

            if (data.data.MOVA_MONTH == null)
                return (rsAccMonthValuation_query)CommDAO.getRs(1, "會計年月的月");

            string sql = $@"SELECT MOVA_ACNMID,MAX(ACNM_C_NAME) as ACNM_C_NAME
FROM ACC_MONTH_VALUATION, vw_ACCD
WHERE MOVA_COMPID=@MOVA_COMPID
AND   MOVA_YEAR =@MOVA_YEAR AND MOVA_MONTH =@MOVA_MONTH
AND   MOVA_ACNMID = ACNM_ID
GROUP BY MOVA_ACNMID";

            object[] obj = new object[] {
                data.data.MOVA_COMPID,
                data.data.MOVA_YEAR,
                data.data.MOVA_MONTH
            };

            DataTable dt = comm.DB.RunSQL(sql, obj);
            List<AccMonthValuation_query_item> rs = dt.ToList<AccMonthValuation_query_item>();

            return new rsAccMonthValuation_query {
                result = CommDAO.getRsItem(),
                data = rs
            };
        }

        public rsAccMonthValuation_query AccMonthValuation_check(AccMonthValuation_qry_max data)
        {

            if (string.IsNullOrEmpty(data.data.MOVA_COMPID))
                return (rsAccMonthValuation_query)CommDAO.getRs(1, "未輸入公司代碼");

            if (data.data.MOVA_YEAR == null)
                return (rsAccMonthValuation_query)CommDAO.getRs(1, "會計年月的年");

            if (data.data.MOVA_MONTH == null)
                return (rsAccMonthValuation_query)CommDAO.getRs(1, "會計年月的月");

            string sql = $@"SELECT * FROM ACC_MONTH_VALUATION
WHERE MOVA_COMPID=@MOVA_COMPID
AND   MOVA_YEAR =@MOVA_YEAR AND MOVA_MONTH =@MOVA_MONTH";

            object[] obj = new object[] {
                data.data.MOVA_COMPID,
                data.data.MOVA_YEAR,
                data.data.MOVA_MONTH
            };

            DataTable dt = comm.DB.RunSQL(sql, obj);
            List<AccMonthValuation_query_item> rs = dt.ToList<AccMonthValuation_query_item>();

            return new rsAccMonthValuation_query
            {
                result = CommDAO.getRsItem(),
                data = rs
            };
        }
    }
}