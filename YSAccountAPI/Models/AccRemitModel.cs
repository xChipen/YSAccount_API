using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccRemitModel
    {
    }
    #region 欄位定義
    public class AccRemit
    {
        //  公司代號
        public string RECT_COMPID { get; set; }
        //  匯款銀行存款科目代號
        public string RECT_ACNMID { get; set; }
        //  匯款銀行代號
        public string RECT_BANKID { get; set; }
        //  匯款帳號
        public string RECT_ACNO { get; set; }

    }
    #endregion

    #region 新增,修改,刪除,查詢
    // AccRemit
    public class AccRemitAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccRemit data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class AccRemitAdd2
    {
        public BaseRequest baseRequest { get; set; }
        public List<AccRemit> data { get; set; }
        public Pagination pagination { get; set; }
    }
    #endregion

    #region 查詢
    public class rs_AccRemitQuery : rs
    {
        public List<AccRemit> data { get; set; }
    }
    #endregion
}