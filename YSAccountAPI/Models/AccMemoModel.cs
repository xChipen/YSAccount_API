using System;
using System.Collections.Generic;

namespace Models
{
    public class AccMemoModel
    {
    }

    #region 定義
    //  完整定義。
    public class AccMemo
    {
        //  摘要代號
        public string MEMO_ID { get; set; }
        //  摘要名稱
        public string MEMO_NAME { get; set; }
        //  登錄者代號
        public string MEMO_A_USER_ID { get; set; }
        //  登錄者姓名
        public string MEMO_A_USER_NM { get; set; }
        //  登錄日期
        public DateTime MEMO_A_DATE { get; set; }
        //  修改者代號
        public string MEMO_U_USER_ID { get; set; }
        //  修改者姓名
        public string MEMO_U_USER_NM { get; set; }
        //  修改日期
        public DateTime MEMO_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    //  查詢回傳需要的內容定義。
    public class AccMemoSimple
    {
        //  摘要代號
        public string MEMO_ID { get; set; }
        //  摘要名稱
        public string MEMO_NAME { get; set; }
    }

    public class AccMemoIDs
    {
        public List<String> MEMO_ID { get; set; }
        public string MEMO_NAME { get; set; }
    }

    public class AccMemoID_qry
    {
        public string MEMO_ID { get; set; }
        public string MEMO_ID_E { get; set; }
        public string MEMO_NAME { get; set; }
    }

    public class AccMemo_id
    {
        public string MEMO_ID { get; set; }
    }
    #endregion

    #region 新增
    // AccMemo
    public class AccMemoAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccMemo data { get; set; }
    }

    public class AccMemoQuery
    {
        public BaseRequest baseRequest { get; set; }
        public AccMemoIDs data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rs_AccMemoAdd : rs
    {
        public AccMemo_id data { get; set; }
    }
    #endregion

    #region 刪除
    public class AccMemoDeleteItem
    {
        public List<string> MEMO_ID { get; set; }
    }
    // AccMemo Delete
    public class AccMemoDelete
    {
        public BaseRequest baseRequest { get; set; }
        public AccMemoDeleteItem data { get; set; }

    }
    #endregion

    // 區間查詢
    public class AccMemoQuery2
    {
        public BaseRequest baseRequest { get; set; }
        public AccMemoID_qry data { get; set; }
        public Pagination pagination { get; set; }
    }

    #region 查詢
    public class rs_AccMemoQuery : rs
    {
        public List<AccMemoSimple> data { get; set; }
    } 
    #endregion
}