using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccExrateModel
    {
    }
    #region 欄位定義
    public class AccExrate
    {
        //年
        public int? EXRA_YEAR { get; set; }
        //月
        public int? EXRA_MONTH { get; set; }

        //幣別
        public string EXRA_CURRID { get; set; }
        //上旬匯率
        public decimal? EXRA_RATE1 { get; set; }
        //中旬匯率
        public decimal? EXRA_RATE2 { get; set; }
        //下旬匯率
        public decimal? EXRA_RATE3 { get; set; }
        //月底匯率
        public decimal? EXRA_RATE_E { get; set; }

        //年
        public int? EXRA_YEAR_E { get; set; }
        //月
        public int? EXRA_MONTH_E { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    #endregion

    #region 新增,修改,刪除,查詢
    // AccExrate
    public class AccExrateAdd
    {
        public BaseRequest baseRequest { get; set; }
        public AccExrate data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class AccExrateAdd2
    {
        public BaseRequest baseRequest { get; set; }
        public List<AccExrate> data { get; set; }
        public Pagination pagination { get; set; }
    }

    #endregion

    #region 查詢
    public class rs_AccExrateQuery : rs
    {
        public List<AccExrate> data { get; set; }
    }
    #endregion
}