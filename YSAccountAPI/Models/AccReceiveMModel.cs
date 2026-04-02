using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Models
{
    public class AccReceiveM
    {
        public string RECM_COMPID { get; set; }
        public string RECM_NO { get; set; }
        public DateTime? RECM_DATE { get; set; }
        public string RECM_DEPTID { get; set; }
        public string RECM_CUSTID { get; set; }
        public decimal? RECM_TOT_AMT { get; set; }
        public decimal? RECM_TEMP_RECEIPTS { get; set; }
        public string RECM_VALID { get; set; }
        public string RECM_VOUNO { get; set; }
        public string TRAN_NAME { get; set; }
        public string RECM_REMARK { get; set; }     //2024/12/20 收款備註
        public string RECM_TR_FLAG { get; set; }
        public string RECM_REC_FLG { get; set; }  // 20260326
    }
    public class AccReceiveM_item: AccReceiveM
    {
        public string RECM_A_USER_ID { get; set; }
        public string RECM_A_USER_NM { get; set; }
        public DateTime? RECM_A_DATE { get; set; }
        public string RECM_U_USER_ID { get; set; }
        public string RECM_U_USER_NM { get; set; }
        public DateTime? RECM_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }   // AUD
        public string errMsg { get; set; }
    }

    public class AccReceiveM_item2 : AccReceiveM_item
    {
        public DateTime? RECM_DATE_B { get; set; }
        public DateTime? RECM_DATE_E { get; set; }
    }

    public class AccReceiveM_qry_ACF050B:BaseIn
    {
        public AccReceiveM_item2 data { get; set; }
    }

    public class rsAccReceiveM_qry_ACF050B_item
    {
        public List<AccReceiveM_item> grid1 { get; set; }
        public List<AccReceiveM_item> grid2 { get; set; }
    }

    public class rsAccReceiveM_qry_ACF050B : rs
    {
        public rsAccReceiveM_qry_ACF050B_item data { get; set; }
    }
    //------------------------------------------------
    // Query 查詢畫面
    public class ACF100Q_qry_ins:BaseIn
    {
        public ACF100Q_qry data { get; set; }
    }
    public class ACF100Q_qry
    {
        // 查詢區分 : 1未收款 2已收款 3全部
        public int KIND { get; set; }           
        // 公司代號
        public string COMPID { get; set; }
        // 客戶代號
        public string CUST_ID { get; set; }    
        // 客戶名稱
        public string CUST_NAME { get; set; }
        // 客戶統編
        public string UNION { get; set; }
        // 發票號碼
        public string INV_NO { get; set; }
        // 發票日期
        public string INV_DATE { get; set; }
        public string INV_DATE_E { get; set; }
        // 收款日期
        public string REC_DATE { get; set; }
        public string REC_DATE_E { get; set; }
    }

    public class rsACF100Q_rs : rs
    {
        public List<ACF100Q_rs> data { get; set; }
    }
    public class ACF100Q_rs
    {
        public DateTime? INV_DATE { get; set; } // 發票日期
        public string INV_NO { get; set; }      // 發票號碼
        public string TRAN_NAME { get; set; }   // 客戶名稱
        public string VOUNO { get; set; }       // 傳票編號
        public decimal? NT_TOT_AMT { get; set; }// 立帳金額
        public decimal? AMT { get; set; }       // 收款金額
        public DateTime REC_DATE { get; set; }  // 收款日期
    }

    public class AccReceiveM_ACF030M_ins : BaseIn
    {
        public AccReceiveM_ACF030M data { get; set; }
    }
    public class AccReceiveM_ACF030M
    {
        public string RECM_COMPID { get; set; }
        public string RECM_NO { get; set; }
        public DateTime? RECM_DATE { get; set; }
        public DateTime? RECM_DATE_E { get; set; }
        public string RECM_CUSTID { get; set; }
        public string RECM_VOUNO { get; set; }
    }

    public class rsAccReceiveM_ACF030M:rs
    {
        public List<AccReceiveM> data { get; set; }
    }

    // 20260316
    public class AccReceiveM_Query3_in : BaseIn
    {
        public AccReceiveM_Query3_item data { get; set; }
    }
    public class AccReceiveM_Query3_item
    {
        public string CUSTID { get; set; }
    }

    public class rsAccReceiveM_Query3 : rs
    {
        public AccReceiveM_Query3 data { get; set; }
    }
    public class AccReceiveM_Query3
    {
        public string DUEBANK { get; set; }
        public string ACNO { get; set; }
    }

    // 20260402
    public class AccReceiveM_Query4_in : BaseIn
    {
        public AccReceiveM_Query4_item data { get; set; }
    }
    public class AccReceiveM_Query4_item
    {
        public string BDate { get; set; }
        public string EDate { get; set; }
    }

    public class rsAccReceiveM_Query4 : rs
    {
        public List<AccReceiveM_Query4> data { get; set; }
    }
    public class AccReceiveM_Query4
    {
        public string RECM_DATEK { get; set; }
        public string RECM_NO { get; set; }
        public string RECM_CUSTID { get; set; }
        public string TRAN_NAME { get; set; }
        public string RECM_A_USER_NM { get; set; }
    }



}