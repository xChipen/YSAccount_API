using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccCheckCounter
    {
        public string CKCN_COMPID { get; set; }
        public string CKCN_ACCD { get; set; }
        public Int16? CKCN_SEQ { get; set; }
        public string CKCN_NOID { get; set; }
        public string CKCN_STNO { get; set; }
        public string CKCN_ENNO { get; set; }
        public string CKCN_CUNO { get; set; }
        public string CKCN_A_USER_ID { get; set; }
        public string CKCN_A_USER_NM { get; set; }
        public DateTime? CKCN_A_DATE { get; set; }
        public string CKCN_U_USER_ID { get; set; }
        public string CKCN_U_USER_NM { get; set; }
        public DateTime? CKCN_U_DATE { get; set; }


    }
    public class AccCheckCounter_item : AccCheckCounter
    {
        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    public class AccCheckCounter_ins : BaseIn
    {
        public AccCheckCounter_item data { get; set; }
    }


    public class rsAccCheckCounter_qry : rs
    {
        public List<AccCheckCounter> data { get; set; }
    }

    // ACE010M SELECT ACC_CHECK_COUNTER, vw_ACCD 所有資料
    public class ACE010M_qryAll : AccCheckCounter
    {
        public string ACNM_COMPID { get; set; }
        public string ACNM_ID { get; set; }
        public string ACNM_C_NAME { get; set; }
        public string ACNM_J_NAME { get; set; }
        public string ACNM_E_NAME { get; set; }
        public string ACNM_SAVE_FLG { get; set; }
        public string ACNM_AP_FLG { get; set; }
    }
    public class rsACE010M_qryAll : rs {
        public List<ACE010M_qryAll> data { get; set; }
    }



}