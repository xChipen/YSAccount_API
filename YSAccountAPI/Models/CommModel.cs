using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class comm_in
    {
        public int _auto_id { get; set; }
        public string _State { get; set; }
    }

    public class rsAUD : rs
    {
        public List<rsAUD_item> data { get; set; }
    }
    public class rsAUD_item
    {
        public int AutoId { get; set; }
        //public string Id { get; set; }
        public string errMsg { get; set; }
    }

    public class BaseRequest {
        public string id { get; set; }
        public string employeeNo { get; set; }
        public string name { get; set; }
        public string companyId { get; set; }
        public string departmentId { get; set; }
        public string email { get; set; }
        public List<DepartmentId> departmentIds { get; set; }
        public List<Name> roleNames { get; set; }
    }
    public class DepartmentId
    {
        public string departmentId { get; set; }
    }
    public class Name
    {
        public string name { get; set; }
    }
    
    // result
    public class rsItem
    {
        public int retCode { get; set; }
        public string retMsg { get; set; }
    }

    public interface Irs
    {
        rsItem result { get; set; }
        Pagination pagination { get; set; }
    }

    public class rs:Irs
    {
        public rsItem result { get; set; }
        public Pagination pagination { get; set; }
    }
    public class Pagination
    {
        public int pageNumber { get; set; } // 分頁
        public int pageSize { get; set; }   // 每頁筆數      
        public int pageNumbers { get; set; }// 總頁數
        public int pages { get; set; }      // 總筆數
    }

    public class rsList
    {
        public rsItem result { get; set; }
        public List<comm_in> data { get; set; }
    }

    public class Id {
        public string id { get; set; }
    }

    public class rsComm
    {
        public rsItem result { get; set; }
        public Id data { get; set; }
    }

    public class BaseIn {
        public BaseRequest baseRequest { get; set; }
        public Pagination pagination { get; set; }
    }






}