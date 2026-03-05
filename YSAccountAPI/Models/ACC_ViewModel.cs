using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    #region class ACCD
    public class ACCD
    {
        public string ACNM_COMPID { get; set; }
        public string ACNM_ID { get; set; }
        public string ACNM_C_NAME { get; set; }
        public string ACNM_J_NAME { get; set; }
        public string ACNM_E_NAME { get; set; }
        public string ACNM_SAVE_FLG { get; set; }
        public string ACNM_AP_FLG { get; set; }
        public string ACNM_REMIT_FLG { get; set; }
        public string ACNM_AR_FLG { get; set; }
        public string ACNM_NR_FLG { get; set; }
        public string ACNM_PK_FLG { get; set; }
        public string ACNM_TAX_FLG { get; set; } //20240418

        public string BACT_FLG { get; set; }
        public string BACT_DEPT_FLG { get; set; }
        public string BACT_TRANID_FLG { get; set; }
        public string BACT_INVNO_FLG { get; set; }
    }

    public class ACCD_ins
    {
        public BaseRequest baseRequest { get; set; }
        public Pagination pagination { get; set; }
        public ACCD data { get; set; }
    }

    public class rs_ACCD : rs
    {
        public List<ACCD> data { get; set; }
    }
    #endregion

    #region class TRAIN
    public class TRAIN
    {
        public string TRAN_COMPID { get; set; }
        public string TRAN_KIND { get; set; }
        public List<string> TRAN_ID { get; set; }
        public string TRAN_NAME { get; set; }
        public string TRAN_ABBR { get; set; }
        public string TRAN_BANKID { get; set; }
        public string TRAN_ACNO { get; set; }
        public string TRAN_ADDRESS { get; set; }
    }

    public class TRAIN_qry
    {
        public string TRAN_COMPID { get; set; }
        public List<string> TRAN_KIND { get; set; } 
        public string TRAN_ID { get; set; }
        public string TRAN_NAME { get; set; }
        public string TRAN_ABBR { get; set; }
        public string TRAN_BANKID { get; set; }
        public string TRAN_ACNO { get; set; }
        public string TRAN_ADDRESS { get; set; }
        public string TRAN_CODE { get; set; }
    }
    public class TRAIN_ins2
    {
        public BaseRequest baseRequest { get; set; }
        public Pagination pagination { get; set; }
        public TRAIN_qry data { get; set; }
    }

    public class TRAIN_ins
    {
        public BaseRequest baseRequest { get; set; }
        public Pagination pagination { get; set; }
        public TRAIN data { get; set; }
    }

    public class rsTRAIN_item
    {
        public string TRAN_COMPID { get; set; }
        public string TRAN_KIND { get; set; }
        public string TRAN_ID { get; set; }
        public string TRAN_NAME { get; set; }
        public string TRAN_ABBR { get; set; }
        public string TRAN_BANKID { get; set; }
        public string TRAN_ACNO { get; set; }
        public string TRAN_ADDRESS { get; set; }
        public string TRAN_CODE { get; set; }
    }

    public class rs_TRAIN : rs
    {
        public List<rsTRAIN_item> data { get; set; }
    }
    #endregion

    #region vw_USER
    public class USER {
        public string USER_COMPID { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
    }
    public class USER_ins
    {
        public BaseRequest baseRequest { get; set; }
        public Pagination pagination { get; set; }
        public USER data { get; set; }
    }
    public class rs_USER : rs
    {
        public List<USER> data { get; set; }
    }
    #endregion

    #region vwBalance
    public class BALANCE
    {
        public string BALC_COMPID { get; set; }
        public string BALC_CUSTID { get; set; }
        public string BALC_INVNO { get; set; }
        public DateTime? BALC_INV_DATE { get; set; }
        public DateTime? BALC_DUE_DATE { get; set; }
        public string BALC_DEPTID { get; set; }
        public string BALC_ACCD { get; set; }
        public string BALC_CURRID { get; set; }
        public decimal? BALC_EXRATE { get; set; }
        public decimal? BALC_NT_TOT_AMT { get; set; }
        public decimal? BALC_FOR_TOT_AMT { get; set; }
        public decimal? BALC_NT_BAL { get; set; }
        public decimal? BALC_FOR_BAL { get; set; }
        public string BALC_MEMO { get; set; }
    }
    public class BALANCE_qry:BaseIn
    {
        public BALANCE data { get; set; }
    }
    public class rsBALANCE_qry : rs
    {
        public List<BALANCE> data { get; set; }
    }
    #endregion


}