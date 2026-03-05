using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccVoumstTax
    {
        public string VOMT_COMPID { get; set; }
        public string VOMT_NO { get; set; }
        public Int16? VOMT_SEQ { get; set; }
        public string VOMT_FORMAT { get; set; }
        public string VOMT_DATA_YM { get; set; }
        public DateTime? VOMT_INV_DATE { get; set; }
        public string VOMT_INVNO { get; set; }
        public string VOMT_S_UNNO { get; set; }
        public string VOMT_P_UNNO { get; set; }
        public string VOMT_TAXCD { get; set; }
        public int VOMT_AMT { get; set; }
        public int VOMT_TAX { get; set; }
        public string VOMT_PCODE { get; set; }
        public string VOMT_TAXCD1 { get; set; }
        public int VOMT_NET_AMT { get; set; }
        public string VOMT_TOT_FLG { get; set; }
        public Int16 VOMT_QTY { get; set; }
    }
    public class AccVoumstTax_item: AccVoumstTax
    {
        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    //---------------------------------------------------


    public class AccVoumstTax_ins : BaseIn
    {
        public AccVoumstTax data { get; set; }
    }
    public class AccVoumstTax_qry : BaseIn
    {
        public AccVoumstTax_item data { get; set; }
    }

    public class rsAccVoumstTax_qry : rs
    {
        public List<AccVoumstTax> data { get; set; }
    }
}