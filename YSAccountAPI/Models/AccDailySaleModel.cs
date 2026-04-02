using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccDailySale
    {
        public string DSAL_COMPID { get; set; }
        public string DSAL_DATE { get; set; }
        public string DSAL_ROUTE { get; set; }
        public string DSAL_DEPTID { get; set; }
        public Int16? DSAL_SEQ { get; set; }
        public string DSAL_ITEMID { get; set; }
        public string DSAL_INVNO { get; set; }
        public string DSAL_CUSTID { get; set; }
        public string DSAL_CREDIT_CARD { get; set; }
        public decimal DSAL_AMT { get; set; }
        public string DSAL_VOUNO { get; set; }
    }
    public class AccDailySale_item: AccDailySale
    {
        public string DSAL_A_USER_ID { get; set; }
        public string DSAL_A_USER_NM { get; set; }
        public DateTime? DSAL_A_DATE { get; set; }
        public string DSAL_U_USER_ID { get; set; }
        public string DSAL_U_USER_NM { get; set; }
        public DateTime? DSAL_AUDATE { get; set; }
    }



    // 查詢
    public class AccDailySale_qry : BaseIn
    {
        public AccDailySale data { get; set; }
    }

    public class rsAccDailySale_qry : rs
    {
        public List<AccDailySale> data { get; set; }
    }

    // 20260402
    public class AccDailySale_qry2 : BaseIn
    {
        public AccDailySale2 data { get; set; }
    }

    public class AccDailySale2 : AccDailySale
    {
        public string Kind { get; set; }
    }

    public class rsAccDailySale2
    {
        public string DSAL_DEPTID { get; set; }
        public string DEPT_NAME { get; set; }
    }

    public class rsAccDailySale_qry2 : rs
    {
        public List<rsAccDailySale2> data { get; set; }
    }


}