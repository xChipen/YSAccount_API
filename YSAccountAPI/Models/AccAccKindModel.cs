using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAccKindModel
    {
    }

    #region 定義
    public class AccAccKind
    {
        //  會計大分類代號
        public string ACKD_ID { get; set; }
        //  中文名稱
        public string ACKD_C_NAME { get; set; }
        //  日文名稱
        public string ACKD_J_NAME { get; set; }
        //  英文名稱
        public string ACKD_E_NAME { get; set; }
        //  登錄者代號
        public string ACKD_A_USER_ID { get; set; }
        //  登錄者姓名
        public string ACKD_A_USER_NM { get; set; }
        //  登錄日期
        public DateTime ACKD_A_DATE { get; set; }
        //  修改者代號
        public string ACKD_U_USER_ID { get; set; }
        //  修改者姓名
        public string ACKD_U_USER_NM { get; set; }
        //  修改日期
        public DateTime ACKD_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }

    //  查詢回傳需要的內容定義。
    public class AccAccKindSimple
    {
        //  會計大分類代號
        public string ACKD_ID { get; set; }
        //  中文名稱
        public string ACKD_C_NAME { get; set; }
        //  日文名稱
        public string ACKD_J_NAME { get; set; }
        //  英文名稱
        public string ACKD_E_NAME { get; set; }
    }

    public class AccAccKindIDs
    {
        public List<String> ACKD_ID { get; set; }
    }

    public class AccAccKindID
    {
        public string ACKD_ID { get; set; }
    }

    #endregion

    #region 新增
    // AccMemo
    public class AccAccKindAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccAccKind data { get; set; }
    }

    public class AccAccKindQuery
    {
        public BaseRequest baseRequest { get; set; }
        public AccAccKindIDs data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rs_AccAccKindAdd : rs
    {
        public AccAccKindID data { get; set; }
    }
    #endregion

    #region 刪除
    // AccMemo Delete
    public class AccAccKindDelete
    {
        public BaseRequest baseRequest { get; set; }
        public AccAccKindIDs data { get; set; }

    }
    #endregion

    #region 查詢
    public class rs_AccAccKindQuery : rs
    {
        public List<AccAccKindSimple> data { get; set; }
    }
    #endregion

}