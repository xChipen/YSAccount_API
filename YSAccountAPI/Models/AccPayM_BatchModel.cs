using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPayM_Batch_ins : BaseIn
    {
        public List<AccPayM_Batch> data { get; set; }
    }

    public class AccPayM_Batch : AccPayM_item
    {
        public List<AccPayD> child { get; set; }
    }

    public class AccPayM_Batch_qry : BaseIn
    {
        public AccPayM data { get; set; }
    }

    public class rs_AccPayM_Batch : rs
    {
        public List<AccPayM_Batch> data { get; set; }
    }

    public class AccPayM_Batch_qry2_item
    {
        public string PAYM_COMPID { get; set; }
        public DateTime? BDate { get; set; }
        public DateTime? EDate { get; set; }
        public string BNo { get; set; }
        public string ENo { get; set; }
        public string UserNo { get; set; }
        //public string VOMM_APPROVE_FLG { get; set; }
    }
    public class AccPayM_Batch_qry2 : BaseIn
    {
        public AccPayM_Batch_qry2_item data { get; set; }
    }
}