using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class BPM_Query_in: BaseIn
    {
        public BPM_Query_item data { get; set; }
    }
    public class BPM_Query_item : BaseIn
    {
        public string RPVR01 { get; set; }  // 單據編號
        public string RPUSER2 { get; set; } // 申請人
        public string RPGDC6 { get; set; }  // 傳票立帳日期
        public string RPCO { get; set; }    // 公司別

    }

    public class BPM_Query
    {
        public string 資料別 { get; set; }
        public string 單據編號 { get; set; }
        public string 申請日 { get; set; }
        public string 作帳日 { get; set; }
        public string 申請人代碼 { get; set; }
        public string 申請人名稱 { get; set; }
        public string 摘要 { get; set; }
        public double? 請款金額 { get; set; }
    }
    public class rsBPM_Query : rs
    {
        public List<BPM_Query> data { get; set; }
    }
}