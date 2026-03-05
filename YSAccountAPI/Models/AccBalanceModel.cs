using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccBalanceModel
    {
    }

    #region 定義
    public class AccBalance
    {
        //  公司代號
        public string ACBL_COMPID { get; set; }
        //  會計年度
        public int ACBL_YEAR { get; set; }
        //  會計月份
        public int ACBL_MONTH { get; set; }
        //  會計科目代號
        public string ACBL_CODE { get; set; }
        //  期初借方餘額
        public decimal ACBL_DBAL { get; set; }
        //  期初貸方餘額
        public decimal ACBL_CBAL { get; set; }
        //  本月借方金額1
        public decimal ACBL_DAMT1 { get; set; }
        //  本月借方金額2
        public decimal ACBL_DAMT2 { get; set; }
        //  本月貸方金額1
        public decimal ACBL_CAMT1 { get; set; }
        //  本月貸方金額2
        public decimal ACBL_CAMT2 { get; set; }
        //  本月借方筆數
        public int ACBL_DCNT { get; set; }
        //  本月貸方筆數
        public int ACBL_CCNT { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }

    public class AccBalanceIDs
    {
        public string ACBL_COMPID { get; set; }
        public int ACBL_YEAR { get; set; }
        public int ACBL_MONTH { get; set; }
        public List<String> ACBL_CODE { get; set; }
    }
    public class AccBalanceID
    {
        public string ACBL_COMPID { get; set; }
        public int ACBL_YEAR { get; set; }
        public int ACBL_MONTH { get; set; }
        public string ACBL_CODE { get; set; }
    }

    #endregion

    #region 新增
    // AccMemo
    public class AccBalanceAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccBalance data { get; set; }
    }

    public class AccBalanceQuery
    {
        public BaseRequest baseRequest { get; set; }
        public AccBalanceIDs data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rs_AccBalanceAdd : rs
    {
        public AccBalanceID data { get; set; }
    }
    #endregion

    #region 刪除
    // AccMemo Delete
    public class AccBalanceDelete
    {
        public BaseRequest baseRequest { get; set; }
        public AccBalanceIDs data { get; set; }

    }
    #endregion

    #region 查詢
    public class rs_AccBalanceQuery : rs
    {
        public List<AccBalance> data { get; set; }
    }
    #endregion

}