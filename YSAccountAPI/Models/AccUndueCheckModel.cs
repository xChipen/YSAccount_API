using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAO;

namespace Models
{
    public class AccUndueCheck
    {
        public string UNDU_COMPID { get; set; }
        public Int16? UNDU_YEAR { get; set; }
        public Int16? UNDU_MONTH { get; set; }
        public string UNDU_ACCD { get; set; }
        public string UNDU_CHKNO { get; set; }
        public decimal? UNDU_AMT { get; set; }
    }
    public class AccUndueCheck_item: AccUndueCheck
    {
        public int AutoId { get; set; }
        public string State { get; set; }           // AUD
        public string errMsg { get; set; } = "";
        public string UNDU_A_USER_ID { get; set; }
        public string UNDU_A_USER_NM { get; set; }
        public DateTime? UNDU_A_DATE { get; set; }
    }

    public class AccUndueCheck_ins : BaseIn
    {
        public AccUndueCheck_item data { get; set; }
    }

    public class rsAccUndueCheck_qry:rs
    {
        public List<AccUndueCheck> data { get; set; }
    }

    // Batch
    public class AccUndueCheck_Batch_ins : BaseIn
    {
        public List<AccUndueCheck_item> data { get; set; }
    }

    public class rsAccUndueCheck_Batch : rs
    {
        public List<AccUndueCheck_item> data { get; set; }
    }





}