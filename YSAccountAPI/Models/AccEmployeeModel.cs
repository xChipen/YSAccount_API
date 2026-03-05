using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccEmployee
    {
        public string EMPL_COMPID { get; set; }
        public string EMPL_ID { get; set; }
        public string EMPL_NAME { get; set; }
        public string EMPL_BANKID { get; set; }
        public string EMPL_ACNO { get; set; }
    }
    public class AccEmployee_ins : BaseIn
    {
        public AccEmployee data { get; set; }
    }

    public class AccEmployee_del : BaseIn
    {
        public List<AccEmployee> data { get; set; }
    }

    public class AccEmployee_qry : BaseIn
    {
        public AccEmployee data { get; set; }
    }

    public class rs_AccEmployee_qry : rs
    {
        public List<AccEmployee> data { get; set; }
    }


}