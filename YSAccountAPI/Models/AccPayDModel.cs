using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPayD
    {
        public int AutoId { get; set; }
        public string State { get; set; }   // AUD
        public string PAYD_COMPID { get; set; }
        public string PAYD_NO { get; set; }
        public Int16? PAYD_SEQ { get; set; }
        public string PAYD_ACCD { get; set; }
        public string PAYD_INVNO { get; set; }
        public DateTime? PAYD_INV_DATE { get; set; }
        public string PAYD_CURRID { get; set; }
        public decimal? PAYD_EXRATE { get; set; }
        public decimal? PAYD_NT_BAL { get; set; }
        public decimal? PAYD_FOR_BAL { get; set; }
        public decimal? PAYD_NT_AMT { get; set; }
        public decimal? PAYD_FOR_AMT { get; set; }
    }

    public class AccPayD_ins : BaseIn
    {
        public AccPayD data { get; set; }
    }
    public class AccPayD_qry : BaseIn
    {
        public AccPayD data { get; set; }
    }

    public class rsAccPayD_qry : rs
    {
        public List<AccPayD> data { get; set; }
    }
}