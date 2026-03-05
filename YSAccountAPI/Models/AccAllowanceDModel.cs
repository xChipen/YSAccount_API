using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAllowanceD
    {
        public string ALLD_COMPID { get; set; }
        public string ALLD_NO { get; set; }
        public Int16? ALLD_SEQ { get; set; }
        public string ALLD_ITEMID { get; set; }
        public string ALLD_ACCD { get; set; }
        public string ALLD_FORMAT { get; set; }
        public string ALLD_S_UNNO { get; set; }
        public string ALLD_INVNO { get; set; }
        public DateTime? ALLD_INV_DATE { get; set; }
        public decimal? ALLD_INV_AMT { get; set; }
        public decimal? ALLD_NET_AMT { get; set; }
        public decimal? ALLD_TAX_AMT { get; set; }
        public string ALLD_REMARK { get; set; }
    }
    public class AccAllowanceD_item: AccAllowanceD
    {
        public int AutoId { get; set; }
        public string errMsg { get; set; }
        public string State { get; set; }
    }


}