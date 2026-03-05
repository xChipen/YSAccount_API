using System;
using System.Collections.Generic;

namespace Models
{
    public class ACC_INVNO_ID
    {
        public int? INID_YEAR { get; set; }
        public int? INID_MONTH { get; set; }
        public string INID_FORMAT { get; set; }
        public string INID_ID { get; set; }
    }

    public class ACC_INVNO_ID_qry : BaseIn
    {
        public ACC_INVNO_ID data { get; set; }
    }

    public class rsACC_INVNO_ID:rs
    {
        public List<ACC_INVNO_ID> data { get; set; }
    }

}