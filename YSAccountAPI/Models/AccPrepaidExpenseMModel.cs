using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPrepaidExpenseM
    {
        public string PEXM_COMPID { get; set; }
        public string PEXM_NO { get; set; }
        public string PEXM_TYPE { get; set; }
        public DateTime? PEXM_DATE { get; set; }
        public string PEXM_APPLY_EMPLID { get; set; }
        public string PEXM_APPLY_NAME { get; set; }
        public string PEXM_DEPTID { get; set; }
        public string PEXM_CURRID { get; set; }
        public decimal? PEXM_NT_AMT { get; set; }
        public string PEXM_MEMO { get; set; }
        public DateTime? PEXM_PAY_DATE { get; set; }
        public string PEXM_PAY_TYPE { get; set; }
        public string PEXM_VOUNO { get; set; }
        public string PEXM_A_USER_ID { get; set; }
        public string PEXM_A_USER_NM { get; set; }
        public DateTime? PEXM_A_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }

    public class AccPrepaidExpenseM_ins : BaseIn
    {
        public AccPrepaidExpenseM data { get; set; }
    }
    public class AccPrepaidExpenseM_qry
    {
        public BaseRequest baseRequest { get; set; }
        public AccPrepaidExpenseM data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rsAccPrepaidExpenseM_qry : rs
    {
        public List<AccPrepaidExpenseM> data { get; set; }
    }

}