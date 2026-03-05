using System;
using System.Collections.Generic;

namespace Models
{
    public class AccJouralDModel
    {
    }

    #region 定義
    public class AccJouralD
    {
        //  公司代號
        public string JOUD_COMPID { get; set; }
        //  常用分錄代號
        public string JOUD_CODE { get; set; }
        //  明細序號
        public int? JOUD_SEQ { get; set; }
        //  會計科目代號
        public string JOUD_ACCD { get; set; }
        //  摘要
        public string JOUD_MEMO { get; set; }
        //  部門代號
        public string JOUD_DEPTID { get; set; }
        //  對象代號
        public string JOUD_TRANID { get; set; }
        //  憑證號碼
        public string JOUD_INVNO { get; set; }
        //  借方金額
        public decimal JOUD_DAMT { get; set; }
        //  貸方金額
        public decimal JOUD_CAMT { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    public class AccJouralDKey
    {
        //  公司代號
        public string JOUD_COMPID { get; set; }
        //  常用分錄代號
        public string JOUD_CODE { get; set; }
        //  明細序號
        public int? JOUD_SEQ { get; set; }
    }

    #endregion
    #region 新增,修改,刪除,查詢
    // AccJouralD
    public class AccJouralDAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccJouralD data { get; set; }
    }
    #endregion

    public class AccJouralD_del
    {
        public BaseRequest baseRequest { get; set; }
        public AccJouralD_del_item data { get; set; }
    }

    public class AccJouralD_del_item
    {
        //  公司代號
        public string JOUD_COMPID { get; set; }
        //  常用分錄代號
        public string JOUD_CODE { get; set; }
        //  明細序號
        public List<int> JOUD_SEQ { get; set; }
        //  會計科目代號
        public string JOUD_ACCD { get; set; }
        //  摘要
        public string JOUD_MEMO { get; set; }
        //  部門代號
        public string JOUD_DEPTID { get; set; }
        //  對象代號
        public string JOUD_TRANID { get; set; }
        //  憑證號碼
        public string JOUD_INVNO { get; set; }
        //  借方金額
        public decimal JOUD_DAMT { get; set; }
        //  貸方金額
        public decimal JOUD_CAMT { get; set; }
    }

    public class AccJouralDQuery
    {
        public List<AccJouralDKey> data { get; set; }
    }

    public class AccJouralD_ins
    {
        public BaseRequest baseRequest { get; set; }
        public AccJouralDKey data { get; set; }
        public Pagination pagination { get; set; }
    }


    #region 查詢
    public class rs_AccJouralDQuery : rs
    {
        public List<AccJouralD> data { get; set; }
    }
    #endregion
}