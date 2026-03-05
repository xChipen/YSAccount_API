using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccUser
    {
        public string USER_COMPID { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
    }

    public class AccUser_ins : BaseIn
    {
        public AccUser data { get; set; }
    }

    public class AccUser_del : BaseIn
    {
        public List<AccUser> data { get; set; }
    }

    public class AccUser_qry : BaseIn
    {
        public AccUser data { get; set; }
    }

    public class rs_AccUser_qry : rs
    {
        public List<AccUser> data { get; set; }
    }

}