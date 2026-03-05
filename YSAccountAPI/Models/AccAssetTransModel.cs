using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAssetTrans
    {
        public string ASTR_COMPID { get; set; }
        public string ASTR_ASETID { get; set; }
        public Int16? ASTR_SEQ { get; set; }
        public string ASTR_TYPE { get; set; }
        public string ASTR_DATE { get; set; }
        public string ASTR_NAME { get; set; }
        public string ASTR_SPEC { get; set; }
        public decimal? ASTR_AMT { get; set; }
        public string ASTR_DEPTID_S { get; set; }
        public string ASTR_DEPTID_E { get; set; }
        public string ASTR_EMPLID_S { get; set; }
        public string ASTR_EMPLID_E { get; set; }
        public string ASTR_ACCD_S { get; set; }
        public string ASTR_ACCD_E { get; set; }
        public string ASTR_M_ACCD_S { get; set; }
        public string ASTR_M_ACCD_E { get; set; }
        public string ASTR_GET_DATE { get; set; }
        public decimal? ASTR_GET_AMT { get; set; }
        public DateTime ASTR_EVA_DATE { get; set; }
        public decimal? ASTR_EVA_AMT { get; set; }
        public string ASTR_VENDID { get; set; }
        public DateTime ASTR_INV_DATE { get; set; }
        public string ASTR_INV_NO { get; set; }
        public string ASTR_FORMAT { get; set; }
        public decimal? ASTR_TAX { get; set; }
        public decimal? ASTR_MDPR { get; set; }
        public decimal? ASTR_REMVAL { get; set; }
        public string ASTR_STAYM { get; set; }
        public string ASTR_ENDYM { get; set; }
        public Int16? ASTR_YEARS1 { get; set; }
        public Int16? ASTR_YEARS2 { get; set; }
        public Int16? ASTR_YEARS3 { get; set; }
        public string ASTR_MEMO { get; set; }
        public string ASTR_PLACE { get; set; }
        public decimal? ASTR_T_DPR { get; set; }
        public decimal? ASTR_T_LYDPR { get; set; }
        public decimal? ASTR_T_CYDPR { get; set; }
        public string ASTR_ASETID_B { get; set; }
        public DateTime ASTR_END_DATE { get; set; }
        public string ASTR_VOUNO { get; set; }
    }
    public class AccAssetTrans_item: AccAssetTrans
    {
        public string ASTR_A_USER_ID { get; set; }
        public string ASTR_A_USER_NM { get; set; }
        public DateTime? ASTR_A_DATE { get; set; }
        public string ASTR_U_USER_ID { get; set; }
        public string ASTR_U_USER_NM { get; set; }
        public DateTime? ASTR_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string errMsg { get; set; }
        public string State { get; set; }       // AUD
    }
}