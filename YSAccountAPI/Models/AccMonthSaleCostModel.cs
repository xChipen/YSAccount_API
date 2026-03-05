using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccMonthSaleCost
    {
        public string MSCO_COMPID { get; set; }
        public string MSCO_YEAR { get; set; }
        public string MSCO_MONTH { get; set; }
        public string MSCO_DEPTID { get; set; }
        public string MSCO_SEQ { get; set; }
        public string MSCO_ITEMID { get; set; }
        public string MSCO_AMT { get; set; }
        public string MSCO_VOUNO { get; set; }
    }
    public class AccMonthSaleCost_item: AccMonthSaleCost
    {
        public string MSCO_A_USER_ID { get; set; }
        public string MSCO_A_USER_NM { get; set; }
        public string MSCO_A_DATE { get; set; }
        public string MSCO_U_USER_ID { get; set; }
        public string MSCO_U_USER_NM { get; set; }
        public string MSCO_U_DATE { get; set; }
    }


    // 查詢
    public class AccMonthSaleCost_qry : BaseIn
    {
        public AccMonthSaleCost data { get; set; }
    }

    public class rsAccMonthSaleCost_qry : rs
    {
        public List<AccMonthSaleCost> data { get; set; }
    }


}