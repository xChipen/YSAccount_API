using System;
using System.Collections.Generic;

namespace Models
{
    public class AccAccCodeModel
    {
    }

    #region 定義
    public class AccAccCode
    {
        //  會計大分類代號
        public string ACCD_KIND { get; set; }
        //  會計中分類代號
        public string ACCD_ID { get; set; }
        //  中文名稱
        public string ACCD_C_NAME { get; set; }
        //  日文名稱
        public string ACCD_J_NAME { get; set; }
        //  英文名稱
        public string ACCD_E_NAME { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }

    public class AccAccCodeIDs
    {
        public string ACCD_KIND { get; set; }
        public List<String> ACCD_ID { get; set; }
    }

    public class AccAccCodeID
    {
        public string ACCD_KIND { get; set; }
        public string ACCD_ID { get; set; }
    }

    #endregion

    #region 新增
    // AccMemo
    public class AccAccCodeAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccAccCode data { get; set; }
    }

    public class AccAccCodeQuery
    {
        public BaseRequest baseRequest { get; set; }
        public AccAccCodeIDs data { get; set; }
        public Pagination pagination { get; set; }

    }

    public class rs_AccAccCodeAdd : rs
    {
        public AccAccCodeID data { get; set; }
    }
    #endregion

    #region 刪除
    // AccMemo Delete
    public class AccAccCodeDelete
    {
        public BaseRequest baseRequest { get; set; }
        public AccAccCodeIDs data { get; set; }

    }
    #endregion

    #region 查詢
    public class rs_AccAccCodeQuery : rs
    {
        public List<AccAccCode> data { get; set; }
    }
    #endregion
}