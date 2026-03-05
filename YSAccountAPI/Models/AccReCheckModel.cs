using System;
using System.Collections.Generic;

namespace Models
{
    public class AccReCheckModel
    {
    }

    #region 定義
    //  完整定義。
    public class AccReCheck
    {
        //  公司代號
        public string RECK_COMPID { get; set; }
        //  票據號碼
        public string RECK_NO { get; set; }
        //  收票日
        public DateTime? RECK_REC_DATE { get; set; }
        //  到期日
        public DateTime? RECK_DUE_DATE1 { get; set; }
        //  客戶代號
        public string RECK_CUSTID { get; set; }
        //  票據金額
        public decimal? RECK_AMT { get; set; }
        //  付款行
        public string RECK_DUE_BANK { get; set; }
        //  付款行帳號
        public string RECK_ACNO { get; set; }
        //  託收區分
        public string RECK_SAV_FLG { get; set; }
        //  託收日期
        public DateTime? RECK_SAV_DATE { get; set; }
        //  託收行會計科目代號
        public string RECK_SAV_BANK { get; set; }
        //  地區別
        public string RECK_AREA_FLG { get; set; }
        //  預定抵用日
        public DateTime? RECK_DUE_DATE2 { get; set; }
        //  兌現區分
        public string RECK_DUE_FLG { get; set; }
        //  實際兌現日
        public DateTime? RECK_DUE_DATE3 { get; set; }
        //  兌現開立傳票日期
        public DateTime? RECK_VOU_DATE { get; set; }
        //  兌現傳票號碼
        public string RECK_VOUNO { get; set; }
        //  立帳傳票號碼
        public string RECK_C_VOUNO { get; set; }
        //  立帳傳票明細序號
        public int RECK_C_SEQ { get; set; }
        //  登錄者代號
        public string RECK_A_USER_ID { get; set; }
        //  登錄者姓名
        public string RECK_A_USER_NM { get; set; }
        //  登錄日期
        public DateTime? RECK_A_DATE { get; set; }
        //  修改者代號
        public string RECK_U_USER_ID { get; set; }
        //  修改者姓名
        public string RECK_U_USER_NM { get; set; }
        //  修改日期
        public DateTime? RECK_U_DATE { get; set; }
    }

    public class AccReCheck_item : AccReCheck
    {
        public int AutoId { get; set; }
        public string State { get; set; }   // AUD
        public string errMsg { get; set; }
    }

    //  查詢回傳需要的內容定義。
    public class AccReCheckSimple
    {
        //  公司代號
        public string RECK_COMPID { get; set; }
        //  票據號碼
        public string RECK_NO { get; set; }
        //  收票日
        public DateTime? RECK_REC_DATE { get; set; }
        //  到期日
        public DateTime? RECK_DUE_DATE1 { get; set; }
        //  不知道文件上沒有但是資料庫有
        public string RECK_DEPTID { get; set; }
        //  客戶代號
        public string RECK_CUSTID { get; set; }
        //  票據金額
        public decimal RECK_AMT { get; set; }
        //  付款行
        public string RECK_DUE_BANK { get; set; }
        //  付款行帳號
        public string RECK_ACNO { get; set; }
        //  託收區分
        public string RECK_SAV_FLG { get; set; }
        //  託收日期
        public DateTime? RECK_SAV_DATE { get; set; }
        //  託收行會計科目代號
        public string RECK_SAV_BANK { get; set; }
        //  地區別
        public string RECK_AREA_FLG { get; set; }
        //  預定抵用日
        public DateTime? RECK_DUE_DATE2 { get; set; }
        //  兌現區分
        public string RECK_DUE_FLG { get; set; }
        //  實際兌現日
        public DateTime? RECK_DUE_DATE3 { get; set; }
        //  兌現開立傳票日期
        public DateTime? RECK_VOU_DATE { get; set; }
        //  兌現傳票號碼
        public string RECK_VOUNO { get; set; }
        //  立帳傳票號碼
        public string RECK_C_VOUNO { get; set; }
        //  立帳傳票明細序號
        public int RECK_C_SEQ { get; set; }
        // 立帳人
        public string RECK_A_USER_NM { get; set; }

        public string ACNM_C_NAME { get; set; }
    }

    public class AccReCheckIDs
    {
        public string RECK_COMPID { get; set; }
        //  票據號碼
        public List<String> RECK_NO { get; set; }

        public DateTime? RECK_REC_DATE { get; set; } // 收票日 起訖  like ‘%%’
        public DateTime? RECK_DUE_DATE1 { get; set; } // 到期日 起訖 like ‘%%’
        public string RECK_CUSTID { get; set; } // 客戶代號 like ‘%%’
        public string RECK_DUE_BANK { get; set; } // 付款行 like ‘%%’
        //託收
        public DateTime? RECK_SAV_DATE { get; set; } // 託收日期 起訖 like ‘%%’
        public string RECK_SAV_BANK { get; set; } // 託收行代號 起訖 like ‘%%’
        //轉傳票
        public DateTime? RECK_DUE_DATE3 { get; set; } // 兌現日 like ‘%%’

        public string RECK_DUE_FLG { get; set; } // 兌現區分 like ‘%%’

        public string RECK_SAV_FLG { get; set; }   // 託收區分
        public string RECK_VOUNO { get; set; } 
    }

    public class AccReCheckID
    {
        public string RECK_COMPID { get; set; }
        public string RECK_NO { get; set; }
    }

    public class AccReCheck_qry_item
    {
        //  公司代號
        public string RECK_COMPID { get; set; }

        //  票據號碼
        public string RECK_NO { get; set; }

        //  收票日
        public DateTime? RECK_REC_DATE_B { get; set; }
        public DateTime? RECK_REC_DATE_E { get; set; }

        //  到期日
        public DateTime? RECK_DUE_DATE1_B { get; set; }
        public DateTime? RECK_DUE_DATE1_E { get; set; }

        //  客戶代號
        public string RECK_CUSTID { get; set; }

        //  付款行
        public string RECK_DUE_BANK { get; set; }

        //  託收日期
        public DateTime? RECK_SAV_DATE_B { get; set; }
        public DateTime? RECK_SAV_DATE_E { get; set; }

        //  託收行會計科目代號
        public string RECK_SAV_BANK_B { get; set; }
        public string RECK_SAV_BANK_E { get; set; }

        public string RECK_DUE_FLG { get; set; } // 兌現區分
        public string RECK_SAV_FLG { get; set; }

        public DateTime? RECK_DUE_DATE3 { get; set; }
        public string RECK_VOUNO { get; set; }

    }

    #endregion

    #region 新增
    // AccReCheck
    public class AccReCheckAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccReCheck data { get; set; }
    }
    public class AccReCheck_ins
    {
        public BaseRequest baseRequest { get; set; }
        public List<AccReCheck> data { get; set; }
    }

    public class AccReCheckQuery
    {
        public BaseRequest baseRequest { get; set; }
        public AccReCheckIDs data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class AccReCheckQuery2
    {
        public BaseRequest baseRequest { get; set; }
        public AccReCheck_qry_item data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rs_AccReCheckAdd : rs
    {
        public AccReCheckID data { get; set; }
    }
    #endregion

    #region 刪除
    public class AccReCheckDeleteItem
    {
        public string RECK_COMPID { get; set; }
        //  票據號碼
        public List<String> RECK_NO { get; set; }
    }
    // AccReCheck Delete
    public class AccReCheckDelete
    {
        public BaseRequest baseRequest { get; set; }
        public AccReCheckDeleteItem data { get; set; }

    }
    #endregion

    #region 查詢
    public class rs_AccReCheckQuery : rs
    {
        public List<AccReCheckSimple> data { get; set; }
    }
    #endregion

    // ACG040B Update
    public class ACG040B_update_ins : BaseIn
    {
        public ACG040B_update data { get; set; }
    }
    public class ACG040B_update {
        public int KIND { get; set; }   // 1: 兌現 0: 兌現取消
        public string RECK_COMPID { get; set; }
        public string RECK_NO { get; set; }
        public DateTime? RECK_DUE_DATE3 { get; set; }
    }

    // ACG070B Query

    public class rsACG070B_qry : rs
    {
        public ACG070B_qry data { get; set; }
    }
    public class ACG070B_qry
    {
        public string RECK_NO { get; set; }
        public string RECK_SAV_BANK { get; set; }
        public string ACNM_C_NAME { get; set; }
        public decimal? RECK_AMT { get; set; }
    }




}