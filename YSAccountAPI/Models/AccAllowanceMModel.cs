using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAllowanceM
    {
        public string ALLM_COMPID { get; set; }
        public string ALLM_NO { get; set; }
        public DateTime? ALLM_DATE { get; set; }
        public string ALLM_PAYCD { get; set; }
        public string ALLM_PAY_KIND { get; set; }
        public string ALLM_DEPTID { get; set; }
        public string ALLM_EMPLID { get; set; }
        public decimal? ALLM_TOTAL_AMT { get; set; }
        public string ALLM_REMARK { get; set; }
        public string ALLM_VALID { get; set; }
        public string ALLM_VOUNO { get; set; }
        public DateTime? ALLM_PAY_DATE { get; set; }
        public decimal? ALLM_PAY_AMT { get; set; }
        public string ALLM_PAY_VOUNO { get; set; }
        public DateTime? ALLM_DUE_DATE { get; set; }
    }
    public class AccAllowanceM_item: AccAllowanceM
    {
        public string ALLM_A_USER_ID { get; set; }
        public string ALLM_A_USER_NM { get; set; }
        public DateTime? ALLM_A_DATE { get; set; }
        public string ALLM_U_USER_ID { get; set; }
        public string ALLM_U_USER_NM { get; set; }
        public DateTime? ALLM_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string errMsg { get; set; }
        public string State { get; set; }
    }

    // Query2
    public class AccAllowanceM_qry2:BaseIn
    {
        public AccAllowanceM_item2 data { get; set; }
    }
    public class AccAllowanceM_item2 : AccAllowanceM
    {
        public DateTime? ALLM_DATE_B { get; set; }  // 起始
        public DateTime? ALLM_DATE_E { get; set; }  // 終止
        public string DEPT_NAME { get; set; }       // 部門名稱
        public string TRAN_NAME { get; set; }       // 請款人名稱
        public DateTime? ALLM_PAY_DATE_B { get; set; }  // 起始
        public DateTime? ALLM_PAY_DATE_E { get; set; }  // 終止
    }
    public class rsAccAllowanceM2
    {
        public string ALLM_COMPID { get; set; }
        public string ALLM_NO { get; set; }
        public DateTime? ALLM_DATE { get; set; }
        public string DEPT_NAME { get; set; }
        public string TRAN_NAME { get; set; }
        public decimal? ALLM_TOTAL_AMT { get; set; }
        public string ALLM_REMARK { get; set; }
        public DateTime? ALLM_PAY_DATE { get; set; }
        public string ALLM_DEPTID { get; set; }
        public string ALLM_EMPLID { get; set; }
        public string ALLM_PAY_KIND { get; set; }
        public string ALLM_VALID { get; set; }
    }
    public class rsAccAllowanceM_qry2 : rs
    {
        public List<rsAccAllowanceM2> data { get; set; }
    }

    // 資料更新
    public class rsAccAllowanceM2_upd:BaseIn
    {
        public List<rsAccAllowanceM2> data { get; set; }
    }



}