using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAccNameModel
    {
    }

    #region 定義
    public class AccAccName
    {
        //  公司代號
        public string ACNM_COMPID { get; set; }
        //  會計主科目代號
        public string ACNM_ID1 { get; set; }
        //  會計子科目代號-二階
        public string ACNM_ID2 { get; set; }
        //  會計子科目代號-三階
        public string ACNM_ID3 { get; set; }
        //  中文名稱
        public string ACNM_C_NAME { get; set; }
        //  日文名稱
        public string ACNM_J_NAME { get; set; }
        //  英文名稱
        public string ACNM_E_NAME { get; set; }
        //  會計大分類代號
        public string ACNM_KIND { get; set; }
        //  會計中分類代號
        public string ACNM_CODE { get; set; }
        //  票據託收科目
        public string ACNM_SAVE_FLG { get; set; }
        //  銀行匯款科目
        public string ACNM_REMIT_FLG { get; set; }

        public string ACNM_AR_FLG { get; set; }
        public string ACNM_AP_FLG { get; set; }
        public string ACNM_NR_FLG { get; set; }
        public string ACNM_PK_FLG { get; set; }

        //  啟用日期 輸入格式 : YYYY/MM/DD
        public DateTime? ACNM_START_DATE { get; set; }
        //  有效日期 輸入格式 : YYYY/MM/DD
        public DateTime? ACNM_VALID_DATE { get; set; }
        //  登錄者代號
        public string ACNM_A_USER_ID { get; set; }
        //  登錄者姓名
        public string ACNM_A_USER_NM { get; set; }
        //  登錄日期
        public DateTime ACNM_A_DATE { get; set; }
        //  修改者代號
        public string ACNM_U_USER_ID { get; set; }
        //  修改者姓名
        public string ACNM_U_USER_NM { get; set; }
        //  修改日期
        public DateTime? ACNM_U_DATE { get; set; }
        // 固定資產科目
        public string ACNM_FA_FLG { get; set; }
        // 進項稅額科目
        public string ACNM_TAX_FLG { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }

    public class AccAccNameSimple
    {
        //  公司代號
        public string ACNM_COMPID { get; set; }

        public string ACNM_ID { get; set; }

        //  會計主科目代號
        public string ACNM_ID1 { get; set; }
        //  會計子科目代號-二階
        public string ACNM_ID2 { get; set; }
        //  會計子科目代號-三階
        public string ACNM_ID3 { get; set; }
        //  中文名稱
        public string ACNM_C_NAME { get; set; }
        //  日文名稱
        public string ACNM_J_NAME { get; set; }
        //  英文名稱
        public string ACNM_E_NAME { get; set; }
        //  會計大分類代號
        public string ACNM_KIND { get; set; }
        //  會計中分類代號
        public string ACNM_CODE { get; set; }
        //  票據託收科目
        public string ACNM_SAVE_FLG { get; set; }
        //  銀行匯款科目
        public string ACNM_REMIT_FLG { get; set; }

        public string ACNM_AR_FLG { get; set; }
        public string ACNM_AP_FLG { get; set; }
        public string ACNM_NR_FLG { get; set; }
        public string ACNM_PK_FLG { get; set; }

        //  啟用日期 輸入格式 : YYYY/MM/DD
        public DateTime? ACNM_START_DATE { get; set; }
        //  有效日期 輸入格式 : YYYY/MM/DD
        public DateTime? ACNM_VALID_DATE { get; set; }

        public string ACNM_FA_FLG { get; set; }
        public string ACNM_TAX_FLG { get; set; }

        public string BACT_FLG { get; set; }
        public string BACT_DEPT_FLG { get; set; }
        public string BACT_TRANID_FLG { get; set; }
        public string BACT_INVNO_FLG { get; set; }
    }

    public class AccAccNameRangeIDs
    {
        //  查詢範圍 0 or 1.主科目, 2.包含子科目-二階, 3.包含子科目-三階
        public int ACNM_QUERY_TYPE { get; set; }
        //  查詢選擇 0 or 1.全部, 2.有效, 3.無效
        public int ACNM_QUERY_VALID { get; set; }
        public string ACNM_COMPID { get; set; }
        public string ACNM_KIND { get; set; }
        public string ACNM_CODE { get; set; }
        public string ACNM_BEGIN_ID { get; set; }
        public string ACNM_END_ID { get; set; }
    }

    public class AccAccNameIDs
    {
        public string ACNM_COMPID { get; set; }
        public List<string> ACNM_ID { get; set; }
        public List<String> ACNM_ID1 { get; set; }
        public List<String> ACNM_ID2 { get; set; }
        public List<String> ACNM_ID3 { get; set; }
        public string ACNM_C_NAME { get; set; }
        public string ACNM_J_NAME { get; set; }
        public string ACNM_E_NAME { get; set; }
    }

    public class AccAccNameID
    {
        public string ACNM_COMPID { get; set; }
        public string ACNM_ID1 { get; set; }
        public string ACNM_ID2 { get; set; }
        public string ACNM_ID3 { get; set; }
        public string ACNM_ID { get; set; }
        public string ACNM_C_NAME { get; set; }
        public string ACNM_J_NAME { get; set; }
        public string ACNM_E_NAME { get; set; }
        public string ACNM_PK_FLG { get; set; }
        public string ACNM_AP_FLG { get; set; }
        public string ACNM_FA_FLG { get; set; }
    }

    #endregion

    #region 新增
    // AccMemo
    public class AccAccNameAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccAccName data { get; set; }
    }

    public class AccAccNameQueryByRange
    {
        public BaseRequest baseRequest { get; set; }
        public Pagination pagination { get; set; }
        public AccAccNameRangeIDs data { get; set; }
    }

    public class AccAccNameQuery
    {
        public BaseRequest baseRequest { get; set; }
        public Pagination pagination { get; set; }
        public AccAccNameIDs data { get; set; }
    }

    public class AccAccNameQuery23
    {
        public BaseRequest baseRequest { get; set; }
        public Pagination pagination { get; set; }
        public AccAccNameID data { get; set; }
    }

    public class rs_AccAccNameAdd : rs
    {
        public AccAccNameID data { get; set; }
    }
    #endregion

    #region 刪除
    // AccMemo Delete
    public class AccAccNameDelete
    {
        public BaseRequest baseRequest { get; set; }
        public AccAccNameIDs data { get; set; }

    }
    // Add by Chipen
    public class AccAccNameDelete2
    {
        public BaseRequest baseRequest { get; set; }
        public AccAccNameDelete_item data { get; set; }
    }
    public class AccAccNameDelete_item
    {
        public string ACNM_COMPID { get; set; }
        public List<string> ACNM_ID { get; set; }
    }

    #endregion

    #region 查詢
    public class rs_AccAccNameQuery : rs
    {
        public List<AccAccNameSimple> data { get; set; }
    }

    public class AccAccName_NO {
        public string ACNM_ID1 { get; set; }
        public string ACNM_C_NAME { get; set; }
    }

    public class rs_AccAccNameQuery23 : rs
    {
        public List<AccAccName_NO> data { get; set; }
    }

    #endregion
}