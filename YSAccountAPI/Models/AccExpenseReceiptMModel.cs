using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccExpenseReceiptM
    {
        public string EXRM_COMPID { get; set; }
        public string EXRM_NO { get; set; }
        public string EXRM_TYPE { get; set; }
        public DateTime? EXRM_DATE { get; set; }
        public string EXRM_APPLY_EMPLID { get; set; }
        public string EXRM_PEXMNO { get; set; }
        public decimal? EXRM_WRITEOFF_AMT { get; set; }
        public decimal? EXRM_RETURN_AMT { get; set; }
        public string EXRM_CURRID { get; set; }
        public string EXRM_MEMO { get; set; }
        public string EXRM_VOUNO { get; set; }
        public string EXRM_A_USER_ID { get; set; }
        public string EXRM_A_USER_NM { get; set; }
        public DateTime? EXRM_A_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }

    public class AccExpenseReceiptM_ins : BaseIn
    {
        public AccExpenseReceiptM data { get; set; }
    }
    public class AccExpenseReceiptM_qry
    {
        public BaseRequest baseRequest { get; set; }
        public AccExpenseReceiptM data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rsAccExpenseReceiptM_qry : rs
    {
        public List<AccExpenseReceiptM> data { get; set; }
    }


}