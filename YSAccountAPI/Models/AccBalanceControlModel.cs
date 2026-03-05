using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    public class AccBalanceControlModel
    {
    }

    public class AccBalanceControl
    {
        public string BACT_COMPID { get; set; }
        public string BACT_ACNMID { get; set; }
        public string BACT_FLG { get; set; }
        public string BACT_DEPT_FLG { get; set; }
        public string BACT_TRANID_FLG { get; set; }
        public string BACT_INVNO_FLG { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }
    // 新增 and 修改
    public class AccBalanceControl_ins
    {
        public BaseRequest baseRequest { get; set; }
        public AccBalanceControl data { get; set; }
    }
    public class AccBalanceControl_ins2
    {
        public BaseRequest baseRequest { get; set; }
        public List<AccBalanceControl> data { get; set; }
    }
    // 刪除
    public class AccBalanceControl_delItem
    {
        public string BACT_COMPID { get; set; }
        public string BACT_ACNMID { get; set; }
    }
    public class AccBalanceControl_del
    {
        public BaseRequest baseRequest { get; set; }
        public AccBalanceControl_delItem data { get; set; }
        public Pagination pagination { get; set; }
    }
    // 查詢
    public class AccBalanceControl_query:rs
    {
        public List<AccBalanceControl> data { get; set; }
    }
}