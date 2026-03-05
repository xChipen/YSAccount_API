using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccVoumstD
    {
        public string VOMD_COMPID { get; set; }
        public string VOMD_NO { get; set; }
        public int VOMD_SEQ { get; set; }
        public string VOMD_ACCD { get; set; }
        public decimal? VOMD_D_NT_AMT { get; set; }
        public decimal? VOMD_C_NT_AMT { get; set; }
        public string VOMD_EXPENSE { get; set; } = ""; //20240923
        public string VOMD_MEMO { get; set; }
        public string VOMD_CURR { get; set; }
        public decimal? VOMD_EXRATE { get; set; }
        public decimal? VOMD_AMT { get; set; }
        public string VOMD_DEPTID { get; set; }
        public string VOMD_TRANID { get; set; }
        public string VOMD_INVNO { get; set; }
        public string VOMD_DUEFLG { get; set; }
        public DateTime? VOMD_DUEDATE { get; set; }
        public string VOMD_PAY_KIND { get; set; }
        public string VOMD_DUE_BANK { get; set; }
        public string VOMD_ACNO { get; set; }
        public string VOMD_SAV_BANK { get; set; }
        public string VOMD_CVOUNO { get; set; }
        public Int16? VOMD_CSEQ { get; set; }
        public Int16? VOMD_CNT { get; set; }
        public string VOMD_STYM { get; set; }
        public string VOMD_ENYM { get; set; }
        public string VOMD_D_ACCD { get; set; }
        public string VOMD_D_DEPTID { get; set; }
        public string VOMD_D_INVNO { get; set; }
        public string VOMD_TAXCD { get; set; }
        public string VOMD_RELATIVE_NO { get; set; } = "";    //20250730
    }
    public class AccVoumstD_item: AccVoumstD
    {
        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    //---------------------------------------------------




    public class AccVoumstD_ins:BaseIn
    {
        public AccVoumstD data { get; set; }
    }
    public class AccVoumstD_qry:BaseIn
    {
        public AccVoumstD_item data { get; set; }
    }

    public class rsAccVoumstD_qry : rs
    {
        public List<AccVoumstD> data { get; set; }
    }



}