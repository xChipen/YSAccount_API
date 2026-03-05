using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccMonthValuation
    {
        public string MOVA_COMPID { get; set; }
        public int? MOVA_YEAR { get; set; }
        public int? MOVA_MONTH { get; set; }

        public string MOVA_ACNMID { get; set; }
        public string MOVA_DEPTID { get; set; }
        public string MOVA_TRANID { get; set; }
        public string MOVA_INVNO { get; set; }
        public string MOVA_NAME { get; set; }
        public DateTime? MOVA_INV_DATE { get; set; }
        public string MOVA_CURRID { get; set; }
        public decimal? MOVA_EXRATE { get; set; }
        public decimal? MOVA_BAL { get; set; }
        public decimal? MOVA_NT_BAL { get; set; }
        public decimal? MOAA_VA_EXRATE { get; set; }
        public decimal? MOVA_VA_NT_BAL { get; set; }
        public string MOVA_MEMO { get; set; }
        public string MOVA_A_USER_ID { get; set; }
        public string MOVA_A_USER_NM { get; set; }
        public DateTime? MOVA_A_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }

    public class AccMonthValuation_qry_max:BaseIn {
        public AccMonthValuation data { get; set; }
    }

    public class rsAccMonthValuation_query:rs
    {
        public List<AccMonthValuation_query_item> data { get; set; }
    }

    public class AccMonthValuation_query_item
    {
        public string MOVA_ACNMID { get; set; }
        public string ACNM_C_NAME { get; set; }
    }




}