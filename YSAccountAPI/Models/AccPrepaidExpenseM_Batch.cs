using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPrepaidExpenseM_Batch_ins : BaseIn
    {
        public List<AccPrepaidExpenseM_Batch> data { get; set; }
    }

    public class AccPrepaidExpenseM_Batch : AccPrepaidExpenseM
    {
        public List<AccPrepaidExpenseD> child { get; set; }
    }

    public class AccPrepaidExpenseM_Batch_qry : BaseIn
    {
        public AccPrepaidExpenseM_Batch data { get; set; }
    }

    public class rs_AccPrepaidExpenseM_Batch : rs
    {
        public List<AccPrepaidExpenseM_Batch> data { get; set; }
    }

    //public class AccPrepaidExpenseM_Batch_qry2_item
    //{
    //    public string VOMM_COMPID { get; set; }
    //    public DateTime? BDate { get; set; }
    //    public DateTime? EDate { get; set; }
    //    public string BNo { get; set; }
    //    public string ENo { get; set; }
    //    public string UserNo { get; set; }
    //    public string VOMM_APPROVE_FLG { get; set; }
    //}
    //public class AccPrepaidExpenseM_Batch_qry2 : BaseIn
    //{
    //    public AccVoumstM_Batch_qry2_item data { get; set; }
    //}
}