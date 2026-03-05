using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPayM
    {
        public string PAYM_COMPID { get; set; }
        public string PAYM_NO { get; set; }
        public DateTime? PAYM_DATE { get; set; }
        public string PAYM_VENDID { get; set; }
        public string PAYM_PAY_KIND { get; set; }
        public string PAYM_PAY_ACCD { get; set; }
        public string PAYM_CURRID { get; set; }
        public decimal PAYM_EXRATE { get; set; }
        public decimal PAYM_PAY_NT_AMT { get; set; }
        public decimal PAYM_PAY_FOR_AMT { get; set; }
        public decimal PAYM_FEE { get; set; }
        public string PAYM_BANKID { get; set; }
        public string PAYM_ACNO { get; set; }
        public string PAYM_CHKNO { get; set; }
        public DateTime? PAYM_DUE_DATE { get; set; }
        public string PAYM_VALID { get; set; }
        public string PAYM_VOUNO { get; set; }

    }
    public class AccPayM_item: AccPayM
    {
        public int AutoId { get; set; }
        public string State { get; set; }
        public string PAYM_A_USER_ID { get; set; }
        public string PAYM_A_USER_NM { get; set; }
        public DateTime? PAYM_A_DATE { get; set; }
        public string PAYM_U_USER_ID { get; set; }
        public string PAYM_U_USER_NM { get; set; }
        public DateTime? PAYM_U_DATE { get; set; }
    }

    public class AccPayM_ins : BaseIn
    {
        public AccPayM_item data { get; set; }
    }
    public class AccPayM_qry
    {
        public BaseRequest baseRequest { get; set; }
        public AccPayM_item data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rsAccPayM_qry : rs
    {
        public List<AccPayM> data { get; set; }
    }

    //--------------------------------------------------
    public class AccPayM_qry2
    {
        public DateTime? PAYM_DATE { get; set; }
        public string PYM_PAY_KIND { get; set; }
        public string PAYM_VALID { get; set; } = "Y";
        public string PAYM_VOUNO { get; set; } = "empty";
    }
    public class AccPayM_qry2_in : BaseIn
    {
        public AccPayM_qry2 data { get; set; }
    }
    public class AccPayM_qry2_rs
    {
        public string PAYM_VENDID { get; set; }
        public string TRAN_NAME { get; set; }
        public string PAYM_BANKID { get; set; }
        public string PAYM_ACNO { get; set; }
        public decimal? PAYM_PAY_NT_AMT { get; set; }
    }
    public class rsAccPayM_qry2_rs : rs
    {
        public List<AccPayM_qry2_rs> data { get; set; }
    }
    //--------------------------------------------------
    public class AccPayM_qry3
    {
        public DateTime? PAYM_DATE { get; set; }
        public string PAYM_VENDID { get; set; }
    }
    public class AccPayM_qry3_in : BaseIn
    {
        public AccPayM_qry3 data { get; set; }
    }
    public class AccPayM_qry3_rs
    {
        public DateTime? PAYD_INV_DATE { get; set; }
        public string PAYD_INVNO { get; set; }
        public decimal? PAYD_NT_AMT { get; set; }
    }
    public class rsAccPayM_qry3_rs : rs
    {
        public List<AccPayM_qry3_rs> data { get; set; }
    }
    //--------------------------------------------------

    public class AccPayM_ACD040M_qry_ins : BaseIn
    {
        public AccPayM_ACD040M_qry data { get; set; }
    }
    public class AccPayM_ACD040M_qry
    {
        public string COMPID { get; set; }
        public string PAY_KIND { get; set; }
        public DateTime? PAY_DATE { get; set; }
        public string VENDID { get; set; }
        public string STS { get; set; }
        public string CURRID { get; set; }
    }

    public class AccPayM_ACD040M_1  // 查詢 - 付款
    {
        public string VENDID { get; set; }
        public string NAME { get; set; }
        public string BANKID { get; set; }
        public string ACNO { get; set; }
        public decimal? NT_BAL { get; set; }
        public decimal? NT_BAL2 { get; set; }
        public string PAYM_CURRID { get; set; }
    }
    public class rsAccPayM_ACD040M_1_qry : rs
    {
        public List<AccPayM_ACD040M_1> data { get; set; }
    }

    //-------------------------------------------------------------------------

    public class rsAccPayM_ACD040M_qry : rs {
        public List<AccPayM_ACD040M_qry2> data { get; set; }
    }
    public class AccPayM_ACD040M_qry2
    {
        public DateTime? INV_DATE { get; set; }
        public string INVNO { get; set; }
        public decimal? NT_AMT { get; set; }
    }
    //-------------------------------------------------------------------------

    // 查詢畫面
    public class ACD120Q_qry_ins : BaseIn
    {
        public ACD120Q_qry data { get; set; }
    }
    public class ACD120Q_qry
    {
        public int KIND  { get; set; }              //查詢類別
        public string COMPID { get; set; }          //公司代號
        public string COMPID_NAME { get; set; }     //公司名稱
        public string VENDID { get; set; }          //廠商代號
        public string VEND_NAME { get; set; }       //廠商名稱
        public DateTime? INV_DATE { get; set; }     //發票日期(起~迄)
        public DateTime? INV_DATE_E { get; set; }   //
        public DateTime? PAY_DATE { get; set; }     //付款日期(起~迄)
        public DateTime? PAY_DATE_E { get; set; }   //
        public DateTime? PRE_DATE { get; set; }     //預計付款日期(起~迄)
        public DateTime? PRE_DATE_E { get; set; }   //
        public string BANKID { get; set; }          //匯款銀行
        public string TRAN_ACNO { get; set; }       //帳號
        public string ADDRESS { get; set; }         //郵寄地址
        public string INV_NO { get; set; }          //發票號碼
    }
    //---

    public class rsACD120Q_rs : rs
    {
        public List<ACD120Q_rs> data { get; set; }
    }
    public class ACD120Q_rs
    {
        public DateTime? INV_DATE { get; set; }
        public string INVNO { get; set; }
        public string TRAN_NAME { get; set; }
        public string VOUNO { get; set; }
        public DateTime? DUE_DATE { get; set; }
        public decimal? NT_TOT_AMT { get; set; }
        public decimal? AMT { get; set; }
        public DateTime? PAY_DATE { get; set; }
        public string VENDID { get; set; }
        public string C_NAME { get; set; }
        public string A_USER_NM { get; set; }
    }







}