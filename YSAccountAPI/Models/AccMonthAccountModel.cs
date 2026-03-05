using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{

    public class AccMonthAccount_query : BaseIn
    {
        public AccMonthAccount_query_item data { get; set; }
    }

    public class AccMonthAccount_query_item
    {
        public string MOAC_COMPID { get; set; }
        public int? MOAC_YEAR { get; set; }
        public int? MOAC_MONTH { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }

    public class rs_AccMonthAccount_query:rs
    {
        public List<rs_AccMonthAccount_query_item> data { get; set; }
    }

    public class rs_AccMonthAccount_query_item
    {
        public string MOAC_CURRID { get; set; }
        public decimal? EXRA_RATE_E { get; set; }
    }
}