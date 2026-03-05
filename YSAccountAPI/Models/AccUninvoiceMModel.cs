using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccUninvoiceM
    {
        public string UNIM_COMPID { get; set; }
        public string UNIM_NO { get; set; }
        public string UNIM_FORMAT { get; set; }
        public DateTime? UNIM_DATE { get; set; }
        public string UNIM_S_UNNO { get; set; }
        public string UNIM_P_UNNO { get; set; }
        public decimal? UNIM_NET_AMT { get; set; }
        public decimal? UNIM_TAX_AMT { get; set; }
        public decimal? UNIM_TOT_AMT { get; set; }
        public string UNIM_TAXCD { get; set; }
    }
    public class AccUninvoiceM_item: AccUninvoiceM
    {
        public string UNIM_A_USER_ID { get; set; }
        public string UNIM_A_USER_NM { get; set; }
        public DateTime? UNIM_A_DATE { get; set; }
        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }


}