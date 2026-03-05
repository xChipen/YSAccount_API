using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccJouralM_BatchModel
    {
    }

    public class AccJouralM_Batch_ins
    {
        public BaseRequest baseRequest { get; set; }
        public List<AccJouralM_Batch> data { get; set; }
    }

    public class AccJouralM_Batch : AccJouralM
    {
        public List<AccJouralD> child { get; set; }
    }

    public class rs_AccJouralM_Batch : rs
    {
        public List<AccJouralM_Batch> data { get; set; }
    }

}