using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccReceiveWriteoff
    {
        public string RECW_COMPID { get; set; }
        public string RECW_NO { get; set; }
        public Int16? RECW_SEQ { get; set; }
        public string RECW_CUSTID { get; set; }
        public DateTime? RECW_INV_DATE { get; set; }
        public string RECW_INVNO { get; set; }
        public decimal? RECW_NT_AMT { get; set; }
    }

    public class AccReceiveWriteoff_item: AccReceiveWriteoff
    {
        public int AutoId { get; set; }
        public string State { get; set; }   // AUD
        public string errMsg { get; set; }
    }


}