using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAssetParm
    {
        public string ASPA_COMPID { get; set; }
        public string ASPA_ACCD { get; set; }
        public string ASPA_ACCD_T { get; set; }
        public string ASPA_ACCD_D { get; set; }
        public string ASPA_ACCD_P { get; set; }
        public string ASPA_ACCD_L { get; set; }
        public string ASPA_ACCD_EXP1 { get; set; }
        public string ASPA_ACCD_EXP2 { get; set; }
    }
    public class AccAssetParm_item: AccAssetParm
    {
        public string ASPA_A_USER_ID { get; set; }
        public string ASPA_A_USER_NM { get; set; }
        public DateTime? ASPA_A_DATE { get; set; }
        public string ASPA_U_USER_ID { get; set; }
        public string ASPA_U_USER_NM { get; set; }
        public DateTime? ASPA_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string errMsg { get; set; }
        public string State { get; set; }       // AUD
    }

    public class AccAssetParm_qry2:BaseIn
    {
        public AccAssetParm_item data { get; set; }
    }

    public class AccAssetParm_query : AccAssetParm
    {
        public string ACNM_C_NAME { get; set; }
        public string ACNM_C_NAME_T { get; set; }
        public string ACNM_C_NAME_D { get; set; }
        public string ACNM_C_NAME_P { get; set; }
        public string ACNM_C_NAME_EXP1 { get; set; }
        public string ACNM_C_NAME_EXP2 { get; set; }
        public string ACNM_C_NAME_L { get; set; }
    }

    public class rsAccAssetParm_query : rs
    {
        public List<AccAssetParm_query> data { get; set; }
    }


}