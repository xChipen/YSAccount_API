using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPrepayAccountM_Batch_ins : BaseIn
    {
        public List<AccPrepayAccountM_Batch> data { get; set; }
    }

    public class AccPrepayAccountM_Batch : AccPrepayAccountM_item
    {
        public List<AccPrepayAccountD_item> child { get; set; }
        public List<AccPrepayAccountShare> share { get; set; }
    }

    public class AccPrepayAccountM_Batch_qry : BaseIn
    {
        public AccPrepayAccountM_Batch data { get; set; }
    }

    public class rs_AccPrepayAccountM_Batch : rs
    {
        public List<AccPrepayAccountM_Batch> data { get; set; }
    }
}