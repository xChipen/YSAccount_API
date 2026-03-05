using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccCustom
    {
        public string CUST_COMPID { get; set; }
        public string CUST_ID { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_ABBR { get; set; }
    }

    public class AccCustom_ins : BaseIn
    {
        public AccCustom data { get; set; }
    }

    public class AccCustom_del : BaseIn
    {
        public List<AccCustom> data { get; set; }
    }

    public class AccCustom_qry : BaseIn
    {
        public AccCustom data { get; set; }
    }

    public class rs_AccCustom_qry : rs
    {
        public List<AccCustom> data { get; set; }
    }
}