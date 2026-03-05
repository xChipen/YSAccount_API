using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAsset
    {
        public string ASET_COMPID { get; set; }
        public string ASET_ID { get; set; }
        public string ASET_NAME { get; set; }
        public string ASET_SPEC { get; set; }
        public string ASET_TYPE { get; set; }
        public string ASET_DEPTID { get; set; }
        public string ASET_EMPLID { get; set; }
        public string ASET_ACCD { get; set; }
        public decimal? ASET_QTY { get; set; }
        public string ASET_UNIT { get; set; }
        public DateTime ASET_PRO_DATE { get; set; }
        public DateTime ASET_GET_DATE { get; set; }
        public decimal? ASET_GET_AMT { get; set; }
        public DateTime ASET_EVA_DATE { get; set; }
        public decimal? ASET_EVA_AMT { get; set; }
        public decimal? ASET_MDPR { get; set; }
        public decimal? ASET_REMVAL { get; set; }
        public string ASET_STAYM { get; set; }
        public string ASET_ENDYM { get; set; }
        public Int16? ASET_YEARS1 { get; set; }
        public Int16? ASET_YEARS2 { get; set; }
        public Int16? ASET_YEARS3 { get; set; }
        public string ASET_MEMO { get; set; }
        public string ASET_M_ACCD { get; set; }
        public string ASET_PLACE { get; set; }
        public string ASET_OWNER_FLG { get; set; }
        public string ASET_YEARS_FLG { get; set; }
    }

    public class AccAsset_item: AccAsset
    {
        public string ASET_A_USER_ID { get; set; }
        public string ASET_A_USER_NM { get; set; }
        public DateTime? ASET_A_DATE { get; set; }
        public string ASET_U_USER_ID { get; set; }
        public string ASET_U_USER_NM { get; set; }
        public DateTime? ASET_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string errMsg { get; set; }
        public string State { get; set; }       // AUD
    }


}