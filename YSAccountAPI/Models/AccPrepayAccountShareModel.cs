using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPrepayAccountShare
    {
        public string PRAS_COMPID { get; set; }
        public string PRAS_NO { get; set; }
        public Int16? PRAS_SEQ { get; set; }
        public string PRAS_DEPTID { get; set; }
        public decimal? PRAS_NT_AMT { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }

    public class AccPrepayAccountShare_ins
    {
        public BaseRequest baseRequest { get; set; }
        public AccPrepayAccountShare data { get; set; }
    }

    public class AccPrepayAccountShare_rs : rs
    {
        public List<AccPrepayAccountShare> data { get; set; }
    }

    public class AccPrepayAccountShare_qry
    {
        public BaseRequest baseRequest { get; set; }
        public AccPrepayAccountShare data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rsAccPrepayAccountShare_qry : rs
    {
        public List<AccPrepayAccountShare> data { get; set; }
    }

}