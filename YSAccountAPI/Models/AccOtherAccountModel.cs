using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccOtherAccount_qry_item
    {
        public string OTAC_COMPID { get; set; }
        public string OTAC_ACCD { get; set; }
        public List<string> OTAC_TRANID { get; set; }
        public string OTAC_INVNO { get; set; }
        public DateTime? OTAC_INV_DATE { get; set; }
        public string OTAC_DEPTID { get; set; }
        public string OTAC_CURRID { get; set; }
        public decimal? OTAC_EXRATE { get; set; }
        public decimal? OTAC_NT_TOT_AMT { get; set; }
        public decimal? OTAC_FOR_TOT_AMT { get; set; }
        public decimal? OTAC_NT_BAL { get; set; }
        public decimal? OTAC_FOR_BAL { get; set; }
        public string OTAC_MEMO { get; set; }

        public DateTime? OTAC_INV_DATE_E { get; set; }
    }
    public class AccOtherAccount
    {
        public string OTAC_COMPID { get; set; }
        public string OTAC_ACCD { get; set; }
        public string OTAC_TRANID { get; set; }
        public string OTAC_INVNO { get; set; }
        public DateTime? OTAC_INV_DATE { get; set; }
        public string OTAC_DEPTID { get; set; }
        public string OTAC_CURRID { get; set; }
        public decimal? OTAC_EXRATE { get; set; }
        public decimal? OTAC_NT_TOT_AMT { get; set; }
        public decimal? OTAC_FOR_TOT_AMT { get; set; }
        public decimal? OTAC_NT_BAL { get; set; }
        public decimal? OTAC_FOR_BAL { get; set; }
        public string OTAC_MEMO { get; set; }
    }
    public class AccOtherAccount_item: AccOtherAccount
    {
        public string OTAC_A_USER_ID { get; set; }
        public string OTAC_A_USER_NM { get; set; }
        public DateTime? OTAC_A_DATE { get; set; }
        public string OTAC_U_USER_ID { get; set; }
        public string OTAC_U_USER_NM { get; set; }
        public DateTime? OTAC_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string  errMsg { get; set; }
    }
    //-------------------------------------------------------------------------

    public class AccOtherAccount_qry : BaseIn
    {
        public AccOtherAccount_qry_item data { get; set; }
    }

    public class rsAccOtherAccount_qry:rs
    {
        public List<AccOtherAccount> data { get; set; }
    }

}