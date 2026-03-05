using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccAssetDeprec
    {
        public string ASDE_COMPID { get; set; }
        public Int16? ASDE_YEAR { get; set; }
        public Int16? ASDE_MONTH { get; set; }
        public string ASDE_ASETID { get; set; }
        public string ASDE_TYPE { get; set; }
        public decimal? ASDE_QTY { get; set; }
        public string ASDE_UNIT { get; set; }
        public string ASDE_DEPTID { get; set; }
        public string ASDE_ACCD { get; set; }
        public string ASDE_M_ACCD { get; set; }
        public string ASDE_GET_DATE { get; set; }
        public decimal? ASDE_GET_AMT { get; set; }
        public DateTime ASDE_EVA_DATE { get; set; }
        public decimal? ASDE_EVA_AMT { get; set; }
        public decimal? ASDE_REMVAL { get; set; }
        public decimal? ASDE_LY_AMT { get; set; }
        public decimal? ASDE_CY_AMT { get; set; }
        public decimal? ASDE_M_AMT { get; set; }
        public string ASDE_STAYM { get; set; }
        public string ASDE_ENDYM { get; set; }
        public Int16? ASDE_YEARS1 { get; set; }
        public Int16? ASDE_YEARS2 { get; set; }
        public Int16? ASDE_YEARS3 { get; set; }
        public string ASDE_VOUNO { get; set; }
    }
    public class AccAssetDeprec_item: AccAssetDeprec
    {
        public string ASDE_A_USER_ID { get; set; }
        public string ASDE_A_USER_NM { get; set; }
        public DateTime? ASDE_A_DATE { get; set; }
        public string ASDE_U_USER_ID { get; set; }
        public string ASDE_U_USER_NM { get; set; }
        public DateTime? ASDE_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string errMsg { get; set; }
        public string State { get; set; }       // AUD
    }

}