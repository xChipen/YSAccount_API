using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccVendor:comm_in
    {
        public string VEND_COMPID { get; set; }
        public string VEND_ID { get; set; }
        public string VEND_NAME { get; set; }
        public string VEND_ABBR { get; set; }
        public string VEND_BANKID { get; set; }
        public string VEND_ACNO { get; set; }
        public string VEND_ADDRESS { get; set; }
    }
    public class AccVendor_ins : BaseIn
    {
        public AccVendor data { get; set; }
    }

    public class AccVendor_item: AccVendor
    {
        public int auto_id { get; set; }
        public string State { get; set; }
    }
    public class AccVendor_aud : BaseIn
    {
        public List<AccVendor_item> data { get; set; }
    }

    public class AccVendor_del : BaseIn
    {
        public List<AccVendor> data { get; set; }
    }

    public class AccVendor_qry : BaseIn
    {
        public AccVendor data { get; set; }
    }

    public class rs_AccVendor_qry : rs
    {
        public List<AccVendor> data { get; set; }
    }
}