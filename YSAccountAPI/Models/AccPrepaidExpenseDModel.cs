using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPrepaidExpenseD
    {
        public string PEXD_COMPID { get; set; }
        public string PEXD_NO { get; set; }
        public string PEXD_PAY_EMPLID { get; set; }
        public string PEXD_CURRID { get; set; }
        public decimal? PEXD_AMT { get; set; }
        public decimal? PEXD_EXRATE { get; set; }
        public decimal? PEXD_NT_AMT { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }

    public class AccPrepaidExpenseD_ins : BaseIn
    {
        public AccPrepaidExpenseD data { get; set; }
    }

    public class rsAccPrepaidExpenseD_qry : rs
    {
        public List<AccPrepaidExpenseD> data { get; set; }
    }


}