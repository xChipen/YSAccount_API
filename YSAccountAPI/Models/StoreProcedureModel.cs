using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class StoreProcedureModel
    {
    }

    // 月過帳作業
    public class ACB050B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACB050B data { get; set; }
    }
    public class ACB050B {
        public string COMP_ID { get; set; }
        public string YearS { get; set; }   // YYYY
        public string MonthS { get; set; }  // MM
        public string Date_EN { get; set; } // YYYY/MM/DD
    }

    // 過實帳戶月沖銷作業
    public class ACB060B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACB060B data { get; set; }
    }
    public class ACB060B
    {
        public string COMP_ID { get; set; }
        public string YearS { get; set; }   // YYYY
        public string MonthS { get; set; }  // MM
        public string USER_ID { get; set; } // YYYY/MM/DD
    }

    public class ACB060B_rs:rs {
        public List<ACB060B_rsItem> data { get; set; }
    }
    public class ACB060B_rsItem
    {
        public string ELOG_USERID { get; set; }
        public string ELOG_ACNMID { get; set; }
        public string ELOG_DEPTID { get; set; }
        public string ELOG_TRANID { get; set; }
        public string ELOG_INVNO { get; set; }
        public decimal? ELOG_AMT1 { get; set; }
        public decimal? ELOG_AMT2 { get; set; }
        public string ELOG_MEMO { get; set; }
        public string ELOG_A_DATE { get; set; }
    }

    // 年結作業
    public class ACB070B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACB070B data { get; set; }
    }
    public class ACB070B
    {
        public string COMP_ID { get; set; }
        public string YearS { get; set; }   // YYYY
    }

    // 遞延費用迴轉傳票自動開立
    public class ACB110B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACB110B data { get; set; }
    }
    public class ACB110B {
        public string COMP_ID { get; set; }
        public string VOU_DAT { get; set; }   // YYYY/MM/DD
    }
    public class ACB110B_rs : rs
    {
        public List<string> data { get; set; }
    }

    public class ACB140B_item
    {
        public string VOU_NO { get; set; }
    }
    public class ACB140B_rs : rs
    {
        public ACB140B_item data { get; set; }
    }


    // 評價資料產生處理 120B, 評價傳票自動開立 140B
    public class ACB120B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACB120B data { get; set; }
    }
    public class ACB120B
    {
        public string COMP_ID { get; set; }
        public string YearS { get; set; }   // YYYY/MM/DD
        public string MonthS { get; set; }  // MM
        public string USER_NM { get; set; }
        public string USER_ID { get; set; }
    }

    public class ACG070B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACG070B data { get; set; }
    }
    public class ACG070B
    {
        public string COMP_ID { get; set; }
        public string DUE_DATE { get; set; }   // YYYY/MM/DD
    }

    public class ACD040M2_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACD040M2 data { get; set; }
    }
    public class ACD040M2
    {
        public string COMP_ID { get; set; }
        public string PAY_KIND { get; set; }
        public string DATE { get; set; }
    }

    public class ACD080B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACD080B data { get; set; }
    }
    public class ACD080B
    {
        public string COMP_ID { get; set; }
        public string PAY_DATE { get; set; }
        public string VOU_DATE { get; set; }
        public string PAY_KIND { get; set; }
    }

    public class ACE050B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACE050B data { get; set; }
    }
    public class ACE050B
    {
        public string COMP_ID { get; set; }
        public string DUE_DATE { get; set; }
    }

    public class ACH120B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACH120B data { get; set; }
    }
    public class ACH120B
    {
        public string COMP_ID { get; set; }
        public string YM { get; set; }
        public string UNNO { get; set; } = "";
    }

    public class ACK030B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACK030B data { get; set; }
    }
    public class ACK030B
    {
        public string COMP_ID { get; set; }
        public string VOU_DATE { get; set; }
        public string USER_ID { get; set; }
        public string USER_NM { get; set; }
    }

    public class ACK060B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACK060B data { get; set; }
    }
    public class ACK060B
    {
        public string COMP_ID { get; set; }
        public string PAY_DATE { get; set; }
    }

    // ACB160B2_ins 相同
    public class ACB160B1_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACB160B1 data { get; set; }
    }
    public class ACB160B1
    {
        public string COMP_ID { get; set; }		// 付款日期
        public string SALE_DATE { get; set; }	// 付款日期
//        public string ROUTE { get; set; }
    }

    public class ACD010B2_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACD010B2 data { get; set; }
    }
    public class ACD010B2
    {
        public string COMP_ID { get; set; }		// 付款日期
        public string SALE_DATE { get; set; }	// 付款日期
        public string ROUTE { get; set; }
    }

    public class ACB160B_BATCH_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACB160B_BATCH data { get; set; }
    }
    public class ACB160B_BATCH
    {
        public string COMP_ID { get; set; }		// 付款日期
        public string SALE_DATE { get; set; }	// 付款日期
    }

    public class ACB170B_BATCH_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACB170B data { get; set; }
    }
    public class ACB170B
    {
        public string COMP_ID { get; set; }	
        public string YM { get; set; }
        public string VOU_DATE { get; set; }
    }

    public class ACB010M_REVERSAL_in
    {
        public BaseRequest baseRequest { get; set; }
        public ACB010M_REVERSAL data { get; set; }
    }
    public class ACB010M_REVERSAL
    {
        public string VOMD_COMPID { get; set; }
        public string VOMD_NO { get; set; }
    }

    public class ACD010B1_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACK030B data { get; set; }
    }

    public class ACB180B_in
    {
        public BaseRequest baseRequest { get; set; }
        public ACH120B data { get; set; }
    }

    public class ACD060B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACD060B data { get; set; }
    }
    public class ACD060B
    {
        public string COMP_ID { get; set; }
        public string PAY_DATE { get; set; }    
        public string PAY_KIND { get; set; }
        public string USER_ID { get; set; }
        public string USER_NM { get; set; }
    }

    public class ACD060B_rs : rs
    {
        public List<ACD060B_rs_item> data { get; set; }
    }

    public class ACD060B_rs_item
    {
        public string DATA01 { get; set; }
    }

    public class ACE030B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACE030B data { get; set; }
    }
    public class ACE030B
    {
        public string COMP_ID { get; set; }
        public string PAY_DATE { get; set; }
        public string CHKNO { get; set; }
    }

    public class ACB190B_ins
    {
        public BaseRequest baseRequest { get; set; }
        public ACB190B data { get; set; }
    }
    public class ACB190B
    {
        public string COMP_ID { get; set; }
        public string ISM_NO { get; set; }
    }

    public class rsACB160B1_result:rs
    {
        public List<rsACB160B1> data { get; set; }
    }
    public class rsACB160B1
    {
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
        public string Item5 { get; set; }
        public string Item6 { get; set; }
        public string Item7 { get; set; }
        public string Item8 { get; set; }
        public string Item9 { get; set; }
        public string Item10 { get; set; }
        public string Item11 { get; set; }
    }

    public class rsACB160B2_result : rs
    {
        public List<rsACB160B2> data { get; set; }
    }
    public class rsACB160B2
    {
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
        public string Item5 { get; set; }
        public string Item6 { get; set; }
        public string Item7 { get; set; }
        public string Item8 { get; set; }
        public string Item9 { get; set; }
        public string Item10 { get; set; }
        public string Item11 { get; set; }
        public string Item12 { get; set; }
        public string Item13 { get; set; }
        public string Item14 { get; set; }
        public string Item15 { get; set; }
        public string Item16 { get; set; }
        public string Item17 { get; set; }
        public string Item18 { get; set; }
        public string Item19 { get; set; }
        public string Item20 { get; set; }
        public string Item21 { get; set; }
        public string Item22 { get; set; }
        public string Item23 { get; set; }
        public string Item24 { get; set; }
        public string Item25 { get; set; }
        public string Item26 { get; set; }
        public string Item27 { get; set; }
        public string Item28 { get; set; }
        public string Item29 { get; set; }
        public string Item30 { get; set; }
        public string Item31 { get; set; }
        public string Item32 { get; set; }
        public string Item33 { get; set; }
        public string Item34 { get; set; }
        public string Item35 { get; set; }
        public string Item36 { get; set; }
        public string Item37 { get; set; }
        public string Item38 { get; set; }
        public string Item39 { get; set; }
        public string Item40 { get; set; }
        public string Item41 { get; set; }
        public string Item42 { get; set; }
        public string Item43 { get; set; }
        public string Item44 { get; set; }
        public string Item45 { get; set; }
        public string Item46 { get; set; }
        public string Item47 { get; set; }
        public string Item48 { get; set; }
        public string Item49 { get; set; }
        public string Item50 { get; set; }
        public string Item51 { get; set; }
        public string Item52 { get; set; }
        public string Item53 { get; set; }
        public string Item54 { get; set; }
        public string Item55 { get; set; }
    }

    public class rsACJ040B_result : rs
    {
        public List<rsACJ040B> data { get; set; }
    }
    public class rsACJ040B
    {
        public string Item1 { get; set; }
        public string Item2 { get; set; }
    }

}