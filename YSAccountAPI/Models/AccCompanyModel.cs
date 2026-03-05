using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    public class AccCompanyModel
    {
    }

    public class AccCompany
    {
        public string COMP_ID { get; set; }
        public string COMP_NAME { get; set; }
        public string COMP_ABBR { get; set; }
        public string COMP_RESPON { get; set; }
        public string COMP_UNNO { get; set; }
        public string COMP_TAXNO { get; set; }
        public string COMP_ADDRESS { get; set; }
        public string COMP_TAXNOID { get; set; }
        public string COMP_CONNECT { get; set; }
        public string COMP_TEL { get; set; }
        public string COMP_FAX { get; set; }
        public string COMP_BASE { get; set; }
        public string COMP_MAIL_SERVER { get; set; }
        public string COMP_PORT { get; set; }
        public string COMP_ACCOUNT { get; set; }
        public string COMP_PASSWORD { get; set; }
        public decimal? COMP_TAX_RATE { get; set; }
        public Int16? COMP_LIMIT_MONTHS { get; set; }
        public DateTime? COMP_GL_CLOSE_DATE { get; set; }
        public DateTime? COMP_TAX_CLOSE_DATE { get; set; }
        public DateTime? COMP_COST_CLOSE_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }
    // 新增 and 修改
    public class AccCompany_ins
    {
        public BaseRequest baseRequest { get; set; }
        public AccCompany data { get; set; }
    }
    // 刪除
    public class AccCompany_delItem
    {
        public string COMP_ID { get; set; }
    }
    public class AccCompany_del
    {
        public BaseRequest baseRequest { get; set; }
        public AccCompany_delItem data { get; set; }
    }
    // 查詢
    public class AccCompany_query:rs
    {
        public AccCompany data { get; set; }
    }

    //
    public class AccCompany_CloseDate
    {
        public BaseRequest baseRequest { get; set; }
        public AccCompany_ColseDateItem data { get; set; }
    }

    // 關帳日期設定
    public class AccCompany_ColseDateItem {
        public string COMP_ID { get; set; }
        public DateTime? COMP_GL_CLOSE_DATE { get; set; }
        public DateTime? COMP_TAX_CLOSE_DATE { get; set; }
        public DateTime? COMP_COST_CLOSE_DATE { get; set; }
    }


}