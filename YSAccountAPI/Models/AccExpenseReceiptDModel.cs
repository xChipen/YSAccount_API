using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccExpenseReceiptD
    {
        public string EXRD_COMPID { get; set; }
        public string EXRD_NO { get; set; }
        public Int16? EXRD_SEQ { get; set; }
        public string EXRD_CURRID { get; set; }
        public decimal? EXRD_AMT { get; set; }
        public decimal? EXRD_EXRATE { get; set; }
        public decimal? EXRD_NT_AMT { get; set; }
        public string EXRD_FORMAT { get; set; }
        public string EXRD_INVNO { get; set; }
        public string EXRD_UNNO { get; set; }
        public DateTime? EXRD_INV_DATE { get; set; }
        public decimal? EXRD_NET_AMT { get; set; }
        public decimal? EXRD_TAX_AMT { get; set; }
        public decimal? EXRD_TOT_AMT { get; set; }
        public decimal? EXRD_PAY_AMT { get; set; }
        public DateTime? EXRD_PAY_DATE { get; set; }
        public string EXRD_PAY_TYPE { get; set; }
        public string EXRD_PAY_EMPLID { get; set; }
        public string EXRD_DEPTID { get; set; }
        public string EXRD_EXPE_NAME { get; set; }
        public string EXRD_ACCD { get; set; }
        public string EXRD_MEMO { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }

    public class AccExpenseReceiptD_ins : BaseIn
    {
        public AccExpenseReceiptD data { get; set; }
    }

    public class rsAccExpenseReceiptD_qry : rs
    {
        public List<AccExpenseReceiptD> data { get; set; }
    }



}