using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAO;
using Models;

namespace Models
{
    public class AccDept
    {
        public string DEPT_COMPID { get; set; }
        public string DEPT_ID { get; set; }
        public string DEPT_NAME { get; set; }
        public string DEPT_CODE { get; set; }
        public string DEPT_TYPE { get; set; }
        public string DEPT_ROUTE { get; set; }
        public string DEPT_BRAND { get; set; }
        public string DEPT_AREAID { get; set; }
        public int? DEPT_SORT_SEQ { get; set; }
    }

    public class AccDept_ins : BaseIn
    {
        public AccDept data { get; set; }
    }

    public class AccDept_del : BaseIn
    {
        public List<AccDept> data { get; set; }
    }

    public class AccDept_qry:BaseIn
    {
        public AccDept data { get; set; }
    }

    public class rs_AccDept_qry : rs
    {
        public List<AccDept> data { get; set; }
    }

}