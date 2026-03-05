using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPaAccount
    {
        public string PAAC_COMPID { get; set; }
        public string PAAC_VENDID { get; set; }
        public string PAAC_INVNO { get; set; }
        public string PAAC_ACCD { get; set; }
        public DateTime? PAAC_INV_DATE { get; set; }
        public DateTime? PAAC_DUE_DATE { get; set; }
        public string PAAC_VOUNO { get; set; }
        public string PAAC_CURRID { get; set; }
        public decimal? PAAC_EXRATE { get; set; }
        public decimal? PAAC_NT_TOT_AMT { get; set; }
        public decimal? PAAC_FOR_TOT_AMT { get; set; }
        public decimal? PAAC_NT_BAL { get; set; }
        public decimal? PAAC_FOR_BAL { get; set; }
        public string PAAC_STS { get; set; }
        public string PAAC_MEMO { get; set; }
        public DateTime? PAAC_PAY_DATE { get; set; }
        public string PAAC_PAY_KIND { get; set; }
    }
    public class AccPaAccount_item : AccPaAccount
    {
        public int AutoId { get; set; }
        public string State { get; set; }  // AUD
        public string errMsg { get; set; }

        public string PAAC_A_USER_ID { get; set; }
        public string PAAC_A_USER_NM { get; set; }
        public DateTime PAAC_A_DATE { get; set; }
        public string PAAC_U_USER_ID { get; set; }
        public string PAAC_U_USER_NM { get; set; }
        public DateTime PAAC_U_DATE { get; set; }
    }

    // 新增, 查詢回傳
    public class AccPaAccount_ins : BaseIn
    {
        public List<AccPaAccount_item> data { get; set; }
    }
    // 刪除
    public class AccPaAccount_del : BaseIn
    {
        public List<string> data { get; set; }
    }
    // 查詢
    public class AccPaAccount_qry : BaseIn
    {
        public AccPaAccount2 data { get; set; }
    }
    public class AccPaAccount2 : AccPaAccount
    {
        public DateTime? PAAC_INV_DATE_E { get; set; }
        public DateTime? PAAC_DUE_DATE_E { get; set; }
    }

    public class rsAccPaAccount_qry : rs
    {
        public List<AccPaAccount> data { get; set; }
    }

    //----------------------------------------------------
    public class AccPaAccount_qry2 : BaseIn
    {
        public AccPaAccount_query2 data { get; set; }
    }

    public class AccPaAccount_query2
    {
        public DateTime? PAAC_DUE_DATE { get; set; }
        public string PAAC_PAY_KIND { get; set; }
        public string PAAC_VENDID { get; set; }
        public string PAAC_STS { get; set; }
        public string TRAN_KIND { get; set; }
        public string PAAC_CURRID { get; set; }
    }
    public class AccPaAccount_query2_in : BaseIn
    {
        public AccPaAccount_query2 data { get; set; }
    }
    public class AccPaAccount_query2_rs
    {
        public string PAAC_VENDID { get; set; }
        public string TRAN_NAME { get; set; }
        public string TRAN_BANKID { get; set; }
        public string TRAN_ACNO { get; set; }
        public decimal? PAAC_NT_BAL { get; set; }
        public string PAAC_CURRID { get; set; }
        public decimal? PAAC_FOR_TOT_AMT { get; set; }
        public decimal? PAAC_FOR_BAL { get; set; }
        public DateTime PAAC_DUE_DATE { get; set; }
        public string PAAC_INVNO { get; set; }
        public string PAAC_VOUNO { get; set; }
    }
    public class rsAccPaAccount_query2_rs : rs
    {
        public List<AccPaAccount_query2_rs> data { get; set; }
    }

    public class AccPaAccount_qry3_rs
    {
        public string PAAC_INV_DATE { get; set; }
        public string PAAC_INVNO { get; set; }
        public decimal? PAAC_NT_BAL { get; set; }
        public decimal? PAAC_FOR_BAL { get; set; }
    }
    public class rsAccPaAccount_qry3_rs : rs
    {
        public List<AccPaAccount_qry3_rs> data { get; set; }
    }

    //----------------------------------------------------

    // IN
    public class AccPaAccount_QueryForm_ins : BaseIn
    {
        public AccPaAccount_QueryForm data { get; set; }
    }
    public class AccPaAccount_QueryForm
    {
        public int QueryType { get; set; }          // 查詢類別 : 1:未付款 2:已付款 3:暫不付款 4: 全部 5:未轉傳票
        public string COMPID { get; set; }          // 公司代號
        public string VENDID { get; set; }          // 廠商代號
        public string VENDID_NAME { get; set; }     // 廠商名稱
        public DateTime? INV_DATE_S { get; set; }   // 發票日期
        public DateTime? INV_DATE_E { get; set; }
        public DateTime? PAY_DATE_S { get; set; }   // 付款日期
        public DateTime? PAY_DATE_E { get; set; }
        public DateTime? PRE_DATE_S { get; set; }   // 預付款日期
        public DateTime? PRE_DATE_E { get; set; }
        public string BANKID { get; set; }          // vw_TRAIN
        public string INV_NO { get; set; }          // 發票日期
    }
    // OUT
    public class AccPaAccount_QueryForm_qry: rs
    {
        public List<rsAccPaAccount_QueryForm> data { get; set; }
    }
    public class rsAccPaAccount_QueryForm
    {
        public DateTime? INV_DATE { get; set; } // 發票日期
        public string INVNO { get; set; }       // 發票號碼
        public string TRAN_NAME { get; set; }   // 廠商名稱
        public string VOUNO { get; set; }       // 傳票編號
        public DateTime? DUE_DATE { get; set; }     // 預定付款日
        public decimal? NT_TOT_AMT { get; set; }    // 立帳金額
        public decimal? NT_AMT { get; set; }        // 收款金額
        public DateTime? ACT_DATE { get; set; }     // 實際付款日
        public string VENDID { get; set; }          // 廠商代號
        public string C_NAME { get; set; }          // 立帳科目
        public string A_USER_NM { get; set; }       // 登錄者
        public string TRAN_BANKID { get; set; }     // 匯款銀行
        public string TRAN_ACNO { get; set; }       // 帳號
        public string TRAN_ADDRESS { get; set; }    // 郵寄地址    
    }

    // ACD040M 廠商代號
    public class AccPaAccount_ACD040M_qry_ins : rs
    {
        public List<AccPaAccount_ACD040M_qry> data { get; set; }
    }
    public class AccPaAccount_ACD040M_qry
    {
        public DateTime? PAAC_INV_DATE { get; set; }
        public string PAAC_INVNO{ get; set; }
        public decimal? PAAC_NT_BAL { get; set; }
    }
    //-------------------------------------------------------------------------





}