using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPrepayAccountM
    {
        public string PRAM_COMPID { get; set; }
        public string PRAM_NO { get; set; }
        public DateTime? PRAM_DATE { get; set; }
        public string PRAM_D_ACNMID { get; set; }
        public string PRAM_D_DEPTID { get; set; }
        public string PRAM_C_ACNMID { get; set; }
        public string PRAM_C_DEPTID { get; set; }
        public decimal? PRAM_NT_AMT { get; set; }
        public Int16? PRAM_CNT { get; set; }
        public string PRAM_STYM { get; set; }
        public string PRAM_ENYM { get; set; }
        public string PRAM_REMARK { get; set; }
        public string PRAM_RD_ACNMID { get; set; }
        public string PRAM_RD_DEPTID { get; set; }
        public decimal? PRAM_RET_AMT { get; set; }
        public DateTime? PRAM_RET_DATE { get; set; }
        public string PRAM_RET_VOUNO { get; set; }
        public string PRAM_TAXCD { get; set; }
    }
    public class AccPrepayAccountM_item: AccPrepayAccountM
    {
        public string PRAM_A_USER_ID { get; set; }
        public string PRAM_A_USER_NM { get; set; }
        public DateTime? PRAM_A_DATE { get; set; }
        public string PRAM_U_USER_ID { get; set; }
        public string PRAM_U_USER_NM { get; set; }
        public DateTime? PRAM_U_DATE { get; set; } = DateTime.Now;

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    //-------------------------------------------------------------------------

    public class AccPrepayAccountM_ins
    {
        public BaseRequest baseRequest { get; set; }
        public AccPrepayAccountM_item data { get; set; }
    }

    public class AccPrepayAccountM_rs : rs
    {
        public AccPrepayAccountM data { get; set; }
    }

    public class AccPrepayAccountM_qry
    {
        public BaseRequest baseRequest { get; set; }
        public AccPrepayAccountM data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class rsAccPrepayAccountM_qry : rs
    {
        public List<AccPrepayAccountM> data { get; set; }
    }






}