using System;
using System.Collections.Generic;

namespace Models
{
    public class AccVoumstM_Batch_ins:BaseIn
    {
        public AccVoumstM_Batch data { get; set; }
    }

    public class AccVoumstM_Batch
    {
        // 傳票主檔
        public List<AccVoumstM_item> accVoumstM { get; set; }
        // 傳票明細檔
        public List<AccVoumstD_item> accVoumstD { get; set; }
        // 傳票稅額明細檔
        public List<AccVoumstTax_item> accVoumstTax { get; set; }
        // 遞延費用主檔
        public List<AccPrepayAccountM_item> accPrepayAccountM { get; set; }
        // 遞延費用明細檔
        public List<AccPrepayAccountD_item> accPrepayAccountD { get; set; }
        // 傳票編號檔
        public List<AccVouCounter_item> accVouCounter { get; set; }

        // 應收帳款餘額檔
        public List<AccReAccount_item> accReAccount { get; set; }
        // 應付帳款檔
        public List<AccPaAccount_item> accPaAccount { get; set; }
        // 應收票據檔
        public List<AccReCheck_item> accReCheck { get; set; }
        // 應付票據主檔
        public List<AccPaCheck_item> accPaCheck { get; set; }
        // 其他科目餘額檔
        public List<AccOtherAccount_item> accOtherAccount { get; set; }
    }

    public class rsAccVoumstM_Batch
    {
        // 傳票主檔
        public List<AccVoumstM_item> accVoumstM { get; set; }
        // 傳票明細檔
        public List<AccVoumstD_item> accVoumstD { get; set; }
        // 傳票稅額明細檔
        public List<AccVoumstTax_item> accVoumstTax { get; set; }
    }

    public class AccVoumstM_Batch_qry:BaseIn
    {
        public AccVoumstM_Batch data { get; set; }
    }

    public class rs_AccVoumstM_Batch : rs
    {
        public rsAccVoumstM_Batch data { get; set; }
    }

    public class AccVoumstM_Batch_qry2_item
    {
        public string VOMM_COMPID { get; set; }
        public DateTime? BDate { get; set; }
        public DateTime? EDate { get; set; }
        public string BNo { get; set; }
        public string ENo { get; set; }
        public string UserNo { get; set; }
        public string VOMM_APPROVE_FLG { get; set; }
    }
    public class AccVoumstM_Batch_qry2 : BaseIn
    {
        public AccVoumstM_Batch_qry2_item data { get; set; }
    }

    public class rsAccVoumstM_Batch_qry:rs
    {
        public List<rsAccVoumstM_Batch_item> data { get; set; }
    }
    public class rsAccVoumstM_Batch_item: AccVoumstM
    {
        public List<AccVoumstD> accVoumstD { get; set; }
        public List<AccVoumstTax> accVoumstTax { get; set; }
    }
}