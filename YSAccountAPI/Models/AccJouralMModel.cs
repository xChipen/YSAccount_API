using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AccJouralMModel
    {

    }

    public class AccJouralM
    {
        public string JOUM_COMPID { get; set; }
        public string JOUM_CODE { get; set; }
        public string JOUM_VALID { get; set; }
        public string JOUM_MEMO { get; set; }
        public string JOUM_A_USER_ID { get; set; }
        public string JOUM_A_USER_NM { get; set; }
        public DateTime JOUM_A_DATE { get; set; }
        public string JOUM_U_USER_ID { get; set; }
        public string JOUM_U_USER_NM { get; set; }
        public DateTime JOUM_U_DATE { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; } // AUD
        public string errMsg { get; set; }
    }
    // 新增 and 修改
    public class AccJouralM_ins
    {
        public BaseRequest baseRequest { get; set; }
        public AccJouralM data { get; set; }
    }
    // 刪除
    public class AccJouralM_delItem
    {
        public string JOUM_COMPID { get; set; }
        public string JOUM_CODE { get; set; }
        public string JOUM_CODE_E { get; set; }
        public string JOUM_VALID { get; set; }
    }
    public class AccJouralM_del
    {
        public BaseRequest baseRequest { get; set; }
        public AccJouralM_delItem data { get; set; }
        public Pagination pagination { get; set; }
    }
    // 查詢
    public class AccJouralM_query : rs
    {
        public List<AccJouralM> data { get; set; }
    }



}