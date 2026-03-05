using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccRemitControl
    {
        public string RECT_COMPID { get; set; }
        public string RECT_ACNMID { get; set; }
        public string RECT_BANKID { get; set; }
        public string RECT_ACNO { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }
}