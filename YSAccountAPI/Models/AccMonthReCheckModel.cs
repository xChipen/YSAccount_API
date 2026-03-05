using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccMonthReCheck
    {
        public string MREK_COMPID { get; set; }
        public int? MREK_YEAR { get; set; }
        public int? MREK_MONTH { get; set; }
        public string MREK_CUSTID { get; set; }
        public string MREK_INVNO { get; set; }
        public decimal? MREK_AMT { get; set; }
        public DateTime? MREK_REC_DATE { get; set; }
        public DateTime? MREK_DUE_DATE { get; set; }
        public string MREK_DUE_BANK { get; set; }
        public string MREK_ACNO { get; set; }
        public string MREK_SAV_BANK { get; set; }
        public string MREK_DUE_FLG { get; set; }
        public DateTime? MREK_DUE_DATE2 { get; set; }
        public string MREK_MEMO { get; set; }
    }
    public class AccMonthReCheck_item: AccMonthReCheck
    {
        public string MREK_A_USER_ID { get; set; }
        public string MREK_A_USER_NM { get; set; }
        public DateTime? MREK_A_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }

}