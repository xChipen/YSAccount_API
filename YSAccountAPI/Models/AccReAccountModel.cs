using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccReAccount
    {
        public string REAC_COMPID { get; set; }
        public string REAC_CUSTID { get; set; }
        public string REAC_INVNO { get; set; }
        public DateTime? REAC_INV_DATE { get; set; }
        public DateTime? REAC_DUE_DATE { get; set; }
        public string REAC_VOUNO { get; set; }
        public string REAC_DEPTID { get; set; }
        public string REAC_ACCD { get; set; }
        public string REAC_CURRID { get; set; }
        public decimal? REAC_EXRATE { get; set; }
        public decimal? REAC_NT_TOT_AMT { get; set; }
        public decimal? REAC_FOR_TOT_AMT { get; set; }
        public decimal? REAC_NT_BAL { get; set; }
        public decimal? REAC_FOR_BAL { get; set; }
        public string REAC_MEMO { get; set; }
        public DateTime? REAC_REC_DATE { get; set; }
        public string CUGD_CUSTID_D { get; set; }
        public string TRAN_NAME { get; set; }
        public string REAC_SALNO { get; set; }
    }
    public class AccReAccount_item: AccReAccount
    {
        public string REAC_A_USER_ID { get; set; }
        public string REAC_A_USER_NM { get; set; }
        public DateTime? REAC_A_DATE { get; set; }
        public string REAC_U_USER_ID { get; set; }
        public string REAC_U_USER_NM { get; set; }
        public DateTime? REAC_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }   // AUD
        public string errMsg { get; set; }
    }

    // ACF030M_收款登錄
    public class AccReAccount_item2
    {
        public string COMPID { get; set; }
        public List<string> CUSTID { get; set; }
        //public string CUSTID1 { get; set; }
        public string REAC_CURRID { get; set; }
        public DateTime? REAC_INV_DATE_B { get; set; }
        public DateTime? REAC_INV_DATE_E { get; set; }
        public string REAC_REC_KIND { get; set; }
    }
    public class AccReAccount_item2_qry:BaseIn
    {
        public AccReAccount_item2 data { get; set; }
    }

    public class rsAccReAccount_qry:rs
    {
        public List<AccReAccount> data { get; set; }
    }


    public class AccReAccount_update_ins : BaseIn
    {
        public List<AccReAccount_update> data { get; set; }
    }
    public class AccReAccount_update: AccReAccount
    {
        public decimal? AMT { get; set; }
    }

}