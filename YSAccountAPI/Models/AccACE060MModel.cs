using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccACE060M
    {
        public string ABBL_COMPID { get; set; }
        public Int16? ABBL_YEAR { get; set; }
        public Int16? ABBL_MONTH { get; set; }
        public string ABBL_ACCD { get; set; }
        public decimal? ABBL_AMT { get; set; }
        public List<UNDU> ACC_UNDUE_CHECK { get; set; }
    }
    public class UNDU
    {
        public string UNDU_CHKNO { get; set; }
        public decimal? UNDU_AMT { get; set; }
    }
    public class AccACE060M_item: AccACE060M
    {
        public string ABBL_A_USER_ID { get; set; }
        public string ABBL_A_USER_NM { get; set; }
        public DateTime? ABBL_A_DATE { get; set; }
    }
    // 
    public class AccACE060M_ins:BaseIn
    {
        public AccACE060M_item data { get; set; }
    }



}