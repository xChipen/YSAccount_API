using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccInvCounter
    {
        public string INCN_COMPID { get; set; }
        public Int16? INCN_YEAR { get; set; }
        public Int16? INCN_MONTH { get; set; }
        public string INCN_ID { get; set; }
        public string INCN_STNO { get; set; }
        public string INCN_ENNO { get; set; }
        public string INCN_S_UNNO { get; set; }
        public string INCN_FORMAT { get; set; }
    }
    public class AccInvCounter_item: AccInvCounter
    {
        public string INCN_A_USER_ID { get; set; }
        public string INCN_A_USER_NM { get; set; }
        public DateTime? INCN_A_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }
}