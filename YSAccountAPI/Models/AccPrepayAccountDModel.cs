using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPrepayAccountD
    {
        public string PRAD_COMPID { get; set; }
        public string PRAD_NO { get; set; }
        public Int16? PRAD_YEAR { get; set; }
        public Int16? PRAD_MONTH { get; set; }
        public decimal? PRAD_NT_AMT { get; set; }
        public string PRAD_VOUNO { get; set; }
        public string PRAD_TR_FLG { get; set; }
        public decimal? PRAD_TAX { get; set; }
    }
    public class AccPrepayAccountD_item: AccPrepayAccountD
    {
        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    //-------------------------------------------------------------------------

    public class AccPrepayAccountD_ins
    {
        public BaseRequest baseRequest { get; set; }
        public AccPrepayAccountD data { get; set; }
    }

    public class AccPrepayAccountD_rs : rs
    {
        public List<AccPrepayAccountD> data { get; set; }
    }

    public class AccPrepayAccountD_qry
    {
        public BaseRequest baseRequest { get; set; }
        public AccPrepayAccountD_item data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rsAccPrepayAccountD_qry : rs
    {
        public List<AccPrepayAccountD> data { get; set; }
    }
}