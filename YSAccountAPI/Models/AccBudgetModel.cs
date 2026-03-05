using System;
using System.Collections.Generic;

namespace Models
{
    public class AccBudgetModel
    {
    }

    #region 定義
    public class AccBudget
    {
        //  公司代號
        public string BUDG_COMPID { get; set; }
        //  年度
        public int BUDG_YEAR { get; set; }
        //  月份
        public int BUDG_MONTH { get; set; }
        //  部門代號
        public string BUDG_DEPTID { get; set; }
        //  會計科目代號
        public string BUDG_ACNMID { get; set; }
        //  預算金額
        public decimal? BUDG_B_AMT { get; set; }
        //  實際金額
        public decimal? BUDG_R_AMT { get; set; }
        //  分攤金額
        public decimal? BUDG_D_AMT1 { get; set; }
        //  分攤金額1
        public decimal? BUDG_D_AMT2 { get; set; }
        //  登錄者代號
        public string BUDG_A_USER_ID { get; set; }
        //  登錄者姓名
        public string BUDG_A_USER_NM { get; set; }
        //  登錄日期
        public DateTime? BUDG_A_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }

    public class AccBudgetSimple
    {
        //  公司代號
        public string BUDG_COMPID { get; set; }
        //  年度
        public int BUDG_YEAR { get; set; }
        //  月份
        public int BUDG_MONTH { get; set; }
        //  部門代號
        public string BUDG_DEPTID { get; set; }
        //  會計科目代號
        public string BUDG_ACNMID { get; set; }
        //  預算金額
        public decimal BUDG_B_AMT { get; set; }
        //  實際金額
        public decimal BUDG_R_AMT { get; set; }
        //  分攤金額
        public decimal BUDG_D_AMT1 { get; set; }
        //  分攤金額1
        public decimal BUDG_D_AMT2 { get; set; }
    }

    public class AccBudgetIDs
    {
        public string BUDG_COMPID { get; set; }
        //  年度
        public List<int> BUDG_YEAR { get; set; }
        //  月份
        public List<int> BUDG_MONTH { get; set; }
        //  部門代號
        public List<String> BUDG_DEPTID { get; set; }
        //  會計科目代號
        public List<String> BUDG_ACNMID { get; set; }
    }

    public class AccBudgetID
    {
        public string BUDG_COMPID { get; set; }
        public int BUDG_YEAR { get; set; }
        public int BUDG_MONTH { get; set; }
        public string BUDG_DEPTID { get; set; }
        public string BUDG_ACNMID { get; set; }
    }

    #endregion

    #region 新增
    // AccMemo
    public class AccBudgetAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccBudget data { get; set; }
    }
    public class AccBudget_ins
    {
        public BaseRequest baseRequest { get; set; }
        public List<AccBudget> data { get; set; }
    }

    public class AccBudgetQuery
    {
        public BaseRequest baseRequest { get; set; }
        public AccBudgetIDs data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rs_AccBudgetAdd : rs
    {
        public AccBudgetID data { get; set; }
    }
    #endregion

    #region 刪除
    // AccMemo Delete
    public class AccBudgetDelete
    {
        public BaseRequest baseRequest { get; set; }
        public AccBudgetIDs data { get; set; }

    }
    #endregion

    #region 查詢
    public class rs_AccBudgetQuery : rs
    {
        public List<AccBudgetSimple> data { get; set; }
    }
    #endregion

}