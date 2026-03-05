using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    // IN
    public class AccACF100Q_qry:BaseIn
    {
        public AccACF100Q_item_qry data { get; set; }
    }
    public class AccACF100Q_item_qry
    {
        public int QueryType { get; set; }          // 查詢區分 1: 未收款 2:已收款 3: 全部
        public string COMPID { get; set; }          // 公司代號
        public string CUSTID { get; set; }          // 客戶代號
        public string CUSTID_NAME { get; set; }     // 客戶名稱
        public string INV_NO { get; set; }          // 發票號碼
        public DateTime? INV_DATE { get; set; }     // 發票日期-起
        public DateTime? INV_DATE_E { get; set; }   // 發票日期-迄
        public DateTime? REC_DATE { get; set; }     // 收款日期
        public DateTime? REC_DATE_E { get; set; }   // 收款日期-迄
        public string RTPW_NO { get; set; }         // 收款單號
    }

    // OUT
    public class rsAccACF100Q : rs
    {
        public List<rsAccACF100Q_qry> data { get; set; }
    }
    public class rsAccACF100Q_qry
    {
        public DateTime? INV_DATE { get; set; } // 發票日期
        public string INV_NO { get; set; }      // 發票號碼
        public string TRAN_NAME { get; set; }   // 客戶名稱
        public string VOUNO { get; set; }       // 傳票編號
        public decimal? REAC_NT_TOT_AMT { get; set; }   // 立帳金額
        public decimal? REC_AMT { get; set; }   // 收款金額
        public DateTime? REC_DATE { get; set; } // 收款日期
        public string RECW_NO { get; set; }
        public string REAC_CUSTID { get; set; }

    }









}