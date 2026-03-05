using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccReceiveD
    {
        public string RECD_COMPID { get; set; }
        public string RECD_NO { get; set; }
        public Int16? RECD_SEQ { get; set; }
        public string RECD_ITEMID { get; set; }
        public string RECD_ACCD { get; set; }
        public decimal? RECD_NT_AMT { get; set; }
        public string RECD_EXPENSE { get; set; }
        public string RECD_MEMO { get; set; }
    }

    public class AccReceiveD_item : AccReceiveD
    {
        public int AutoId { get; set; }
        public string State { get; set; }  //AUD
        public string errMsg { get; set; }
    }
}