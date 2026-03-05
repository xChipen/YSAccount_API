using System;
using System.Collections.Generic;

namespace Models
{
    public class AccPaCheck
    {
        public string PACK_COMPID { get; set; }
        public string PACK_NO { get; set; }
        public DateTime? PACK_OPEN_DATE { get; set; }
        public DateTime? PACK_DUE_DATE1 { get; set; }
        public string PACK_VENDID { get; set; }
        public decimal? PACK_AMT { get; set; }
        public string PACK_DUE_BANK { get; set; }
        public string PACK_DUE_FLG { get; set; }
        public DateTime? PACK_DUE_DATE2 { get; set; }
        public DateTime? PACK_VOU_DATE { get; set; }
        public string PACK_VOUNO { get; set; }
        public string PACK_C_VOUNO { get; set; }
        public string PACK_MEMO { get; set; }
    }
    public class AccPaCheck_item: AccPaCheck
    {
        public string PACK_A_USER_ID { get; set; }
        public string PACK_A_USER_NM { get; set; }
        public DateTime? PACK_A_DATE { get; set; }
        public string PACK_U_USER_ID { get; set; }
        public string PACK_U_USER_NM { get; set; }
        public DateTime? PACK_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    //-------------------------------------------------------------------------

    // 新增, 查詢回傳
    public class AccPaCheck_ins : BaseIn
    {
        public List<AccPaCheck_item> data { get; set; }
    }
    // 刪除
    public class AccPaCheck_del : BaseIn
    {
        public List<string> data { get; set; }
    }
    // 查詢
    public class AccPaCheck_qry : BaseIn
    {
        public AccPaCheck2 data { get; set; }
    }

    public class AccPaCheck2 : AccPaCheck
    {
        public DateTime? PACK_OPEN_DATE_E { get; set; } // 開票日
        public DateTime? PACK_DUE_DATE1_E { get; set; } // 票面到期日
    }

    public class rsAccPaCheck_qry : rs
    {
        public List<AccPaCheck> data { get; set; }
    }


    public class AccPaCheck_ReCreate : BaseIn
    {
        public AccPaCheck_ReCreate_in data { get; set; }
    }

    public class AccPaCheck_ReCreate_in
    {
        public string PACK_NO { get; set; }
    }



}