using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccTempReceiptsWriteoff
    {
        public string RTPW_COMPID { get; set; }
        public string RTPW_NO { get; set; }
        public Int16? RTPW_SEQ { get; set; }
        public string RTPW_CUSTID { get; set; }
        public string RTPW_INVNO { get; set; }
        public DateTime? RTPW_INV_DATE { get; set; }
        public string RTPW_MEMO { get; set; }
        public decimal? RTPW_NT_AMT { get; set; }
    }

    public class AccTempReceiptsWriteoff_item : AccTempReceiptsWriteoff
    {
        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }
}