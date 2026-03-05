using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccVouCounter
    {
        public string VCNT_COMPID { get; set; }
        public DateTime? VCNT_DATE { get; set; }
        public int VCNT_NO { get; set; }
    }
    public class AccVouCounter_item: AccVouCounter
    {
        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
}