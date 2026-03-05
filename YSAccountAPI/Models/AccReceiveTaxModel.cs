using System;
using System.Collections.Generic;

namespace Models
{
    public class AccReceiveTax
    {
        public string RECT_COMPID { get; set; }
        public string RECT_NO { get; set; }
        public Int16? RECT_SEQ { get; set; }
        public string RECT_FORMAT { get; set; }
        public string RECT_DATA_YM { get; set; }
        public DateTime RECT_INV_DATE { get; set; }
        public string RECT_INVNO { get; set; }
        public string RECT_S_UNNO { get; set; }
        public string RECT_P_UNNO { get; set; }
        public string RECT_TAXCD { get; set; }
        public string RECT_TAXCD1 { get; set; }
        public int RECT_NET_AMT { get; set; }
        public int RECT_TAX { get; set; }
        public int RECT_AMT { get; set; }
        public string RECT_PCODE { get; set; }
        public string RECT_TOT_FLG { get; set; }
        public Int16? RECT_QTY { get; set; }
    }
    public class AccReceiveTax_item : AccReceiveTax
    {
        public int AutoId { get; set; }
        public string State { get; set; }   // AUD
        public string errMsg { get; set; }
    }
    //---------------------------------------------------


    public class AccReceiveTax_ins : BaseIn
    {
        public AccReceiveTax data { get; set; }
    }
    public class AccReceiveTax_qry : BaseIn
    {
        public AccReceiveTax_item data { get; set; }
    }

    public class rsAccReceiveTax_qry : rs
    {
        public List<AccReceiveTax> data { get; set; }
    }
}