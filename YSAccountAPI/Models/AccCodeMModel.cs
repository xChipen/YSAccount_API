using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    public class AccCodeMModel
    {
    }

    public class AccCodeM
    {
        public string CODM_ID1 { get; set; }
        public string CODM_ID2 { get; set; }
        public string CODM_NAME1 { get; set; }
        public string CODM_NAME2 { get; set; }
        public string CODM_ACCD { get; set; }
        public string CODM_DC { get; set; }
        public string CODM_EXPENSE { get; set; } // 20240920
        public string CODM_MEMO { get; set; }

        public int AutoId { get; set; }
        public string State { get; set; }
        public string errMsg { get; set; }
    }
    // 新增 and 修改
    public class AccCodeM_ins {
        public BaseRequest baseRequest { get; set; }
        public AccCodeM data { get; set; }
    }

    // master-detail
    public class AccCodeM_MD_ins
    {
        public BaseRequest baseRequest { get; set; }
        public List<AccCodeM_MD> data { get; set; }
    }
    public class AccCodeM_Child {
        public string CODM_ID2 { get; set; }
        public string CODM_NAME2 { get; set; }
        public string CODM_ACCD { get; set; }
        public string CODM_DC { get; set; }
        public string CODM_EXPENSE { get; set; }
        public string CODM_MEMO { get; set; }
    }
    public class AccCodeM_MD
    {
        public string CODM_ID1 { get; set; }
        public string CODM_NAME1 { get; set; }
        public List<AccCodeM_Child> Child { get; set; }
    }
    public class AccCodeM_MD_query : rs
    {
        public List<AccCodeM_MD> data { get; set; }
    }


    // 刪除
    public class AccCodeM_delItem
    {
        public string CODM_ID1 { get; set; }
        public string CODM_NAME1 { get; set; }
        public string CODM_ID2 { get; set; }
        public string CODM_EXPENSE { get; set; }
        public string CODM_MEMO { get; set; }
        public int? KIND { get; set; }
    }
    public class AccCodeM_del
    {
        public BaseRequest baseRequest { get; set; }
        public AccCodeM_delItem data { get; set; }
        public Pagination pagination { get; set; }
    }

    // 查詢
    public class AccCodeM_query:rs
    {
        public List<AccCodeM> data { get; set; }
    }






}