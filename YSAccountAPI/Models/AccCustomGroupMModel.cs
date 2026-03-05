using System;
using System.Collections.Generic;

namespace Models
{
    public class AccCustomGroupM
    {
        public string CUGM_COMPID { get; set; }
        public string CUGM_CUSTID { get; set; }
    }
    public class AccCustomGroupM_item: AccCustomGroupM
    {
        public string CUGM_A_USER_ID { get; set; }
        public string CUGM_A_USER_NM { get; set; }
        public DateTime CUGM_A_DATE { get; set; }
        public string CUGM_U_USER_ID { get; set; }
        public string CUGM_U_USER_NM { get; set; }
        public DateTime CUGM_U_DATE { get; set; } = DateTime.Now;

        public string state { get; set; }   // AUD
    }

    public class AccCustomGroupM_qry_item : AccCustomGroupM
    {
        public string TRAN_NAME { get; set; }
        public string TRAN_ABBR { get; set; }
    }
    public class AccCustomGroupM_qry:BaseIn
    {
        public AccCustomGroupM_qry_item data { get; set; }
    }

    //AUD
    public class AccCustomGroupM_Batch:BaseIn
    {
        public AccCustomGroupM_Batch_item data { get; set; }
    }
    public class AccCustomGroupM_Batch_item: AccCustomGroupM_item
    {
        public List<AccCustomGroupD> item { get; set; }
    }

    public class AccCustomGroupM_ins : BaseIn
    {
        public AccCustomGroupM_qry_item data { get; set; }
    }

    public class rsAccCustomGroupM_qry:rs
    {
        public List<AccCustomGroupM_qry_item> data { get; set; }
    }


    public class rsAccCustomGroupM_qry3:rs
    {
        public List<AccCustomGroupM_qry3_item> data { get; set; }
    }
    public class AccCustomGroupM_qry3_item: AccCustomGroupM
    {
        public List<AccCustomGroupD> accCustomGroupD { get; set; }
    }


}