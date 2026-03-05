using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccMonthPaCheck
    {
        public string MPAK_COMPID { get; set; }
        public int? MPAK_YEAR { get; set; }
        public int? MPAK_MONTH { get; set; }
        public string MPAK_VENDID { get; set; }
        public string MPAK_INVNO { get; set; }
        public decimal? MPAK_AMT { get; set; }
        public DateTime? MPAK_OPEN_DATE { get; set; }
        public DateTime? MPAK_DUE_DATE1 { get; set; }
        public string MPAK_DUE_BANK { get; set; }
        public string MPAK_DUE_FLG { get; set; }
        public DateTime? MPAK_DUE_DATE2 { get; set; }
        public string MPAK_MEMO { get; set; }
    }
    public class AccMonthPaCheck_item: AccMonthPaCheck
    {
        public string MPAK_A_USER_ID { get; set; }
        public string MPAK_A_USER_NM { get; set; }
        public DateTime? MPAK_A_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }   // AUD
        public string errMsg { get; set; }
    }


}