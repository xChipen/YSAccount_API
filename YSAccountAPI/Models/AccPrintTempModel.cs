using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccPrintTemp
    {
        public string PRTM_USERID { get; set; }
        public string PRTM_PROGID { get; set; }
        public string PRTM_PARAMETER1 { get; set; }
        public string PRTM_PARAMETER2 { get; set; }
        public string PRTM_PARAMETER3 { get; set; }
        public string PRTM_PARAMETER4 { get; set; }
        public int PRTM_NT_AMT { get; set; }
        public int PRTM_FOR_AMT { get; set; }
        public int PRTM_CNT { get; set; }
    }

    public class AccPrintTemp_ins
    {
        public BaseRequest baseRequest { get; set; }
        public List<AccPrintTemp> data { get; set; }
    }

    public class AccPrintTemp_qry:BaseIn
    {
        public AccPrintTemp data { get; set; }
    }

    public class rs_AccPrintTemp : rs
    {
        public List<AccPrintTemp> data { get; set; }
    }


}