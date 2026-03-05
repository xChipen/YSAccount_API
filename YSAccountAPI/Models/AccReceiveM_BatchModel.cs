using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    // 收款主檔
    public class AccReceiveM_Batch
    {
        // 收款入金主檔
        public AccReceiveM_item accReceiveMs { get; set; }
        // 收款入金明細檔
        public List<AccReceiveD_item> accReceiveDs { get; set; }
        // 收款銷帳明細檔
        public List<AccReceiveWriteoff_item> accReceiveWriteoffs { get; set; }
        // 收款票據明細檔
        public List<AccReceiveCheck_item> accReceiveChecks { get; set; }
        // 暫收款銷帳明細檔
        public List<AccTempReceiptsWriteoff_item> accTempReceiptsWriteoffs { get; set; }

        // 餘額檔
        public List<AccReceiveTax_item> accReceiveTaxs { get; set; }

        // 應收帳款檔
        //public List<AccReAccount_item> accReAccounts { get; set; }
        // 其他科目餘額檔
        //public List<AccOtherAccount_item> accOtherAccounts { get; set; }
        // 應收票據檔
        //public List<AccReCheck_item> accReChecks { get; set; }
    }
    public class AccReceiveM_Batch_ins : BaseIn
    {
        public AccReceiveM_Batch data { get; set; }
    }
    public class rsAccReceiveM_Batch_ins : rs
    {
        public AccReceiveM_Batch data { get; set; }
    }
    //-----------------------------------------------------------------------

    public class AccReceiveM_Batch_qry : BaseIn
    {
        public AccReceiveM_Batch_qry_item data { get; set; }
    }
    public class AccReceiveM_Batch_qry_item
    {
        public string COMPID { get; set; }      //公司代號
        public string NO { get; set; }          //收款單號
        public string CUSTID { get; set; }      //[GRID2].客戶代號
        public string INVNO { get; set; }       //[GRID2].發票號碼
        public string OTAC_INVNO { get; set; }  //[GRID4].對沖憑證編號
        public string RECK_NO { get; set; }     //[GRID3].票據號碼 

        public string ACCD { get; set; }        //2304
    }


    public class rsAccReceiveM_Batch_qry_item
    {
        public List<AccReceiveM> accReceiveMs { get; set; }
        // 收款入金明細檔
        public List<AccReceiveD> accReceiveDs { get; set; }
        // 收款銷帳明細檔
        public List<AccReceiveWriteoff> accReceiveWriteoffs { get; set; }
        // 收款票據明細檔
        public List<AccReceiveCheck> accReceiveChecks { get; set; }
        // 暫收款銷帳明細檔
        public List<AccTempReceiptsWriteoff> accTempReceiptsWriteoffs { get; set; }
        // 餘額檔
        public List<AccReceiveTax> accReceiveTaxs { get; set; }

        // 應收帳款檔
        //        public List<AccReAccount> accReAccounts { get; set; }
        // 其他科目餘額檔
        //        public List<AccOtherAccount> accOtherAccounts { get; set; }
        // 應收票據檔
        //        public List<AccReCheck> accReChecks { get; set; }
    }
    public class rsAccReceiveM_Batch_qry : rs
    {
        public rsAccReceiveM_Batch_qry_item data { get; set; }
    }

    //-----------------------------------------------------------------------







}