using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccBankBalance
    {
        public string ABBL_COMPID { get; set; }
        public Int16? ABBL_YEAR { get; set; }
        public Int16? ABBL_MONTH { get; set; }
        public string ABBL_ACCD { get; set; }
        public decimal? ABBL_AMT { get; set; }
    }
    public class AccBankBalance_item: AccBankBalance
    {
        public int AutoId { get; set; }
        public string  State { get; set; }
        public string errMsg { get; set; } = "";
        public string ABBL_A_USER_ID { get; set; }
        public string ABBL_A_USER_NM { get; set; }
        public DateTime? ABBL_A_DATE { get; set; }
    }

    public class AccBankBalance_ins:BaseIn
    {
        public AccBankBalance_item data { get; set; }
    }

    public class rsAccBankBalance_qry : rs
    {
        public List<AccBankBalance> data { get; set; }
    }

    // Batch
    public class AccBankBalance_Batch_ins : BaseIn
    {
        public List<AccBankBalance_item> data { get; set; }
    }

    public class rsAccBankBalance_Batch : rs
    {
        public List<AccBankBalance_item> data { get; set; }
    }


}