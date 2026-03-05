using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccReceiveCheck
    {
        public string REAK_COMPID { get; set; }
        public string REAK_NO { get; set; }
        public Int16? REAK_SEQ { get; set; }
        public string REAK_CHKNO { get; set; }
        public DateTime? REAK_DUE_DATE { get; set; }
        public string REAK_BANKID { get; set; }
        public string REAK_ACNO { get; set; }
        public decimal? REAK_NT_AMT { get; set; }
    }

    public class AccReceiveCheck_item : AccReceiveCheck
    {
        public int AutoId { get; set; }
        public string State { get; set; }   // AUD
        public string errMsg { get; set; }
    }
}