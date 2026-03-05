using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccVoumstM
    {
        public string VOMM_COMPID { get; set; }
        public string VOMM_NO { get; set; }
        public DateTime? VOMM_DATE { get; set; }
        public string VOMM_VALID { get; set; }
        public string VOMM_PRINT_FLG { get; set; }
        public Int16? VOMM_VERNO { get; set; }
        public int? VOMM_GENNO { get; set; }
        public string VOMM_APPROVE_FLG { get; set; }
        public string VOMM_SOURCE { get; set; }
        public string VOMM_BATCHNO { get; set; }
        public string VOMM_MEMO { get; set; }
        public string VOMM_TYPE { get; set; } = "";
        public string VOMM_DEPTID { get; set; } = "";
    }
    public class AccVoumstM_item: AccVoumstM
    {
        public string VOMM_A_USER_ID { get; set; }
        public string VOMM_A_USER_NM { get; set; }
        public DateTime? VOMM_A_DATE { get; set; }
        public string VOMM_U_USER_ID { get; set; }
        public string VOMM_U_USER_NM { get; set; }
        public DateTime? VOMM_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    //---------------------------------------------------

    public class AccVoumstM_update : BaseIn
    {
        public List<AccVoumstM_item> data { get; set; }
    }

    public class AccVoumstM_ins:BaseIn
    {
        public AccVoumstM_item data { get; set; }
    }
    public class AccVoumstM_qry
    {
        public BaseRequest baseRequest { get; set; }
        public AccVoumstM data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rsAccVoumstM_qry:rs
    {
        public List<AccVoumstM> data { get; set; }
    }

    // ACB030M Query
    public class AccVoumstM2_qry_ins:BaseIn
    {
        public AccVoumstM data { get; set; }
    }
    public class AccVoumstM2_qry
    {
        public List<AccVoumstM2> AccVoumstM { get; set; }
        public List<AccVoumstD2> AccVoumstD { get; set; }
        public List<AccVoumstTax2> AccVoumstTax { get; set; }
    }

    public class AccVoumstM2 : AccVoumstM
    {
        public string COMP_NAME { get; set; }       // 公司名稱
    }
    public class AccVoumstD2 : AccVoumstD
    {
        public string ACNM_C_NAME { get; set; }     // 科目名稱
        public string DEPT_NAME { get; set; }       // 部門名稱
        public string TRAN_NAME { get; set; }       // 對象名稱
        public string ACNM_C_NAME2 { get; set; }    // 託收行名稱
        public string BANK_NAME { get; set; }       // 付款行名稱
        public string DEPT_NAME2 { get; set; }      // 遞延迴轉部門名稱
    }
    public class AccVoumstTax2 : AccVoumstTax
    {
        public string CODM_NAME2 { get; set; }      // 格式名稱
    }

    public class rsAccVoumstM2_qry : rs
    {
        public AccVoumstM2_qry data { get; set; }
    }
    //-------------------------------------------------------------------------

    // ACB030M Query2
    public class AccVoumstM_qry2
    {
        public string VOMM_COMPID { get; set; }
        public DateTime? BDate { get; set; }
        public DateTime? EDate { get; set; }
        public string BNo { get; set; }
        public string ENo { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string VOMM_APPROVE_FLG { get; set; }
        public string VOMM_MEMO { get; set; }       // 傳票說明
        public string VOMM_VALID { get; set; }
        public string VOMM_TYPE { get; set; }      // 空白查空白, null 查全部
        public string VOMM_DEPTID { get; set; }
        public string VOMD_TRANID { get; set; }
    }
    public class AccVoumstM_qry2_ins : BaseIn
    {
        public AccVoumstM_qry2 data { get; set; }
    }

    public class AccVoumstM3 : AccVoumstM
    {
        public string USERID { get; set; }
        public string USERNAME { get; set; }
        public string DEPT_NAME { get; set; }
    }
    public class rsAccVoumstM_qry2 : rs
    {
        public List<AccVoumstM3> data { get; set; }
    }

    public class AccVoumstM_ACB090Q_qry : BaseIn
    {
        public AccVoumstM_ACB090Q data { get; set; }
    }
    public class AccVoumstM_ACB090Q
    {
        public string VOMM_COMPID { get; set; }     // 公司
        public DateTime? VOMM_DATE { get; set; }    // 傳票日期
        public DateTime? VOMM_DATE_E { get; set; }
        public string VOMD_ACCD { get; set; }       
        public string VOMD_DEPTID { get; set; }
        public string VOMD_TRANID { get; set; }
        public string VOMD_INVNO { get; set; }
        public decimal? VOMD_D_NT_AMT { get; set; }
        public decimal? VOMD_C_NT_AMT { get; set; }
        public string VOMD_MEMO { get; set; }
    }

    public class rsAccVoumstM_ACB090Q_rs : rs
    {
        public List<rsAccVoumstM_ACB090Q> data { get; set; }
    }
    public class rsAccVoumstM_ACB090Q
    {
        public string VOMD_ACCD { get; set; }
        public string ACNM_C_NAME { get; set; }
        public DateTime? VOMM_DATE { get; set; }
        public string VOMM_NO { get; set; }
        public decimal? VOMD_D_NT_AMT { get; set; }
        public decimal? VOMD_C_NT_AMT { get; set; }
        public string VOMD_MEMO { get; set; }
        public string VOMD_DEPTID { get; set; }
        public string DEPT_NAME { get; set; }
        public string VOMD_TRANID { get; set; }
        public string TRAN_NAME { get; set; }
        public string VOMD_INVNO { get; set; }
        public string VOMD_CURR { get; set; }
        public int VOMD_AMT { get; set; }
        public DateTime? VOMD_DUEDATE { get; set; }
    }




}