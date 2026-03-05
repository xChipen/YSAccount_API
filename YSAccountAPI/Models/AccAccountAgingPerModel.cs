using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAccountAgingPer
    {
        public string APER_KIND { get; set; }
        public int? APER_DAYS { get; set; }
        public int? APER_DAYE { get; set; }
        public decimal? APER_PER { get; set; }
    }
    public class AccAccountAgingPer_item: AccAccountAgingPer
    {
        public string APER_A_USER_ID { get; set; }
        public string APER_A_USER_NM { get; set; }
        public DateTime? APER_A_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }   // AUD
        public string errMsg { get; set; }
    }

    public class AccAccountAgingPer_qry : BaseIn
    {
        public AccAccountAgingPer_item data { get; set; }
    }

    public class rsAccAccountAgingPer_qry : rs
    {
        public List<AccAccountAgingPer> data { get; set; }
    }

    // Batch
    public class AccAccountAgingPer_Batch_ins : BaseIn
    {
        public List<AccAccountAgingPer_item> data { get; set; }
    }

    public class rsAccAccountAgingPer_Batch : rs
    {
        public List<AccAccountAgingPer_item> data { get; set; }
    }

    // ACF010M 查詢
    public class rsACF010M_rs : rs
    {
        public List<ACF010M_rs> data { get; set; }
    }
    public class ACF010M_rs
    {
        public string APER_KIND { get; set; }
    }



}