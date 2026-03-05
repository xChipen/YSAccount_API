using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ACC_POS_SALES_item
    {
        public string PSAL_COMPID { get; set; }
        public DateTime? PSAL_DATE { get; set; }
        public string PSAL_DEPTID { get; set; }
        public string PSAL_INVNO { get; set; }
        public string PSAL_CUSTID { get; set; }
        public decimal PSAL_NET_AMT { get; set; }
        public decimal PSAL_TAX_AMT { get; set; }
    }

    public class ACC_POS_SALES: ACC_POS_SALES_item
    {
        public string PSAL_A_USER_ID { get; set; }
        public string PSAL_A_USER_NM { get; set; }
        public DateTime PSAL_A_DATE { get; set; }
        public string PSAL_U_USER_ID { get; set; }
        public string PSAL_U_USER_NM { get; set; }
        public DateTime PSAL_U_DATE { get; set; }
        public string State { get; set; }           // AUD
        public string ErrMsg { get; set; }
    }

    public class ACC_POS_SALES_in
    {
        public BaseRequest baseRequest { get; set; }
        public List<ACC_POS_SALES> data { get; set; }
    }
    public class ACC_POS_SALES_in_rs:rs
    {
        public List<ACC_POS_SALES> data { get; set; }
    }



    public class ACC_POS_SALES_qry
    {
        public BaseRequest baseRequest { get; set; }
        public ACC_POS_SALES_item data { get; set; }
    }

    public class ACC_POS_SALES_qry_out:rs
    {
        public List<ACC_POS_SALES_item> data { get; set; }
    }



}