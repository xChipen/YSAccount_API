using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{

    public class ACD030B_Query_in
    {
        public BaseRequest baseRequest { get; set; }
        public ACD030B_Query data { get; set; }
    }

    public class ACD030B_Query
    {
        public int KIND { get; set; }
        public string COMPID { get; set; }
        public DateTime BDATE { get; set; }
        public DateTime EDATE { get; set; }
        public string VENDID { get; set; }
    }

    public class rsACD030B_Query
    {
        public string ABBR { get; set; }
        public string ABBR2 { get; set; }
        public string NO { get; set; }
        public DateTime DATE { get; set; }
        public string CURRID { get; set; }
        public decimal? NT_AMT { get; set; }
        public decimal? FOR_AMT { get; set; }
        public decimal? AMT { get; set; }
    }

    public class rsACD030B_Query_result:rs
    {
        public List<rsACD030B_Query> data { get; set; }
    }

    #region ACC_PURCH_VOUNO
    public class ACC_PURCH_VOUNO_Query_in
    {
        public BaseRequest baseRequest { get; set; }
        public ACC_PURCH_VOUNO data { get; set; }
    }

    public class ACC_PURCH_VOUNO
    {
        public string PUVO_USER_ID { get; set; }
        public string PUVO_PROGID { get; set; }
        public string PUVO_NO { get; set; }
        public string PUVO_VOUNO { get; set; }
    }

    public class rsACC_PURCH_VOUNO_result : rs
    {
        public List<ACC_PURCH_VOUNO> data { get; set; }
    }
    #endregion

}