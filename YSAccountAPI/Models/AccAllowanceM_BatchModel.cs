using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAllowanceM_Batch_ins:BaseIn
    {
        public AccAllowanceM_Batch data { get; set; }
    }
    public class AccAllowanceM_Batch
    {
        public List<AccAllowanceM_item> accAllowanceM { get; set; }
        public List<AccAllowanceD_item> accAllowanceD { get; set; }
    }

    public class rsAccAllowanceM_Batch:rs
    {
        public AccAllowanceM_Batch data { get; set; }
    }


    public class AccAllowanceM_Batch_qry : BaseIn
    {
        public AccAllowanceM data { get; set; }
    }
    public class AccAllowanceM_Batch2
    {
        public AccAllowanceM accAllowanceM { get; set; }
        public List<AccAllowanceD> accAllowanceD { get; set; }
    }
    public class rsAccAllowanceM_Batch_qry : rs
    {
        public AccAllowanceM_Batch2 data { get; set; }
    }



}