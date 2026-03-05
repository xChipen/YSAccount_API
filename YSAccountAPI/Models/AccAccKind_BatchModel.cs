using System.Collections.Generic;

namespace Models
{
    public class AccAccKind_batchModel
    {
    }

    public class AccAccKind_Batch_ins
    {
        public BaseRequest baseRequest { get; set; }
        public List<AccAccKind_Batch> data { get; set; }
    }

    public class AccAccKind_Batch : AccAccKind
    {
        public List<AccAccCode> child { get; set; }
    }

    public class rs_AccAccKind_Batch : rs
    {
        public List<AccAccKind_Batch> data { get; set; }
    }









}