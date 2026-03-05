using System;
using System.Collections.Generic;

namespace Models
{
    public class AccBankModel
    {
    }

    #region 定義
    //  完整定義。
    public class AccBank
    {
        //  銀行代號
        public string BANK_ID { get; set; }
        //  銀行名稱
        public string BANK_NAME { get; set; }
        //  銀行簡稱
        public string BANK_ABBR { get; set; }
        //  登錄者代號
        public string BANK_A_USER_ID { get; set; }
        //  登錄者姓名
        public string BANK_A_USER_NM { get; set; }
        //  登錄日期
        public DateTime BANK_A_DATE { get; set; }
        //  修改者代號
        public string BANK_U_USER_ID { get; set; }
        //  修改者姓名
        public string BANK_U_USER_NM { get; set; }
        //  修改日期
        public DateTime BANK_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    //  查詢回傳需要的內容定義。
    public class AccBankSimple
    {
        //  銀行代號
        public string BANK_ID { get; set; }
        //  銀行名稱
        public string BANK_NAME { get; set; }
        //  銀行簡稱
        public string BANK_ABBR { get; set; }
    }

    public class AccBankIDs
    {
        public List<String> BANK_ID { get; set; }
        public string BANK_NAME { get; set; }
    }

    public class AccBank_id
    {
        public string BANK_ID { get; set; }
    }
    #endregion

    #region 新增
    // AccBank
    public class AccBankAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccBank data { get; set; }
    }

    public class AccBankQuery
    {
        public BaseRequest baseRequest { get; set; }
        public AccBankIDs data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class AccBankQuery2
    {
        public BaseRequest baseRequest { get; set; }
        public AccBank data { get; set; }
        public Pagination pagination { get; set; }
    }


    public class rs_AccBankAdd : rs
    {
        public AccBank_id data { get; set; }
    }
    #endregion

    #region 刪除
    public class AccBankDeleteItem
    {
        public List<string> BANK_ID { get; set; }
    }
    // AccBank Delete
    public class AccBankDelete
    {
        public BaseRequest baseRequest { get; set; }
        public AccBankDeleteItem data { get; set; }

    }
    #endregion

    #region 查詢
    public class rs_AccBankQuery : rs
    {
        public List<AccBankSimple> data { get; set; }
    }
    #endregion
}