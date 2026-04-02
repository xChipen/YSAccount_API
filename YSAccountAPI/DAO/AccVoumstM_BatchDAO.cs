using System;
using System.Collections.Generic;
using Models;
using System.Data;
using Helpers;
using System.Linq;

namespace DAO
{
    public class AccVoumstM_BatchDAO : BaseClass
    {
        AccVoumstMDAO accVoumstM = new AccVoumstMDAO();
        AccVoumstDDAO accVoumstD = new AccVoumstDDAO();
        AccVoumstTaxDAO accVoumstTax = new AccVoumstTaxDAO();

        AccPrepayAccountMDAO accPrepayAccountM = new AccPrepayAccountMDAO();
        AccPrepayAccountDDAO accPrepayAccountD = new AccPrepayAccountDDAO();
        AccVouCounterDAO accVouCounter = new AccVouCounterDAO();
        AccReAccountDAO accReAccount = new AccReAccountDAO();
        AccPaAccountDAO accPaAccount = new AccPaAccountDAO();
        AccReCheckDAO accReCheck = new AccReCheckDAO();
        AccPaCheckDAO accPaCheck = new AccPaCheckDAO();
        AccOtherAccountDAO accOtherAccount = new AccOtherAccountDAO();

        AccAccNameDAO accName = new AccAccNameDAO();
        AccBalanceControlDAO BalanceControl = new AccBalanceControlDAO();

        public List<AccVouCounter_item> deal_Add_AccVouCounter(string VOMM_COMPID, DateTime? VOMM_DATE, out string NO)
        {
            int iNO;
            NO = accVouCounter.getNO(VOMM_COMPID, VOMM_DATE, out iNO);

            AccVouCounter_item item = new AccVouCounter_item {
                VCNT_COMPID = VOMM_COMPID,
                VCNT_DATE = VOMM_DATE,
                VCNT_NO = iNO,
                State = iNO == 1 ? "A" :"U"
            };

            List<AccVouCounter_item> rs = new List<AccVouCounter_item>();
            rs.Add(item);
            return rs;
        }
        public List<AccVoumstM_item> deal_Add_AccVoumstM(AccVoumstM_item data, string VOMM_NO, string employeeNo, string Name)
        {
            List<AccVoumstM_item> rs = new List<AccVoumstM_item>();
            AccVoumstM_item item = new AccVoumstM_item {
                VOMM_COMPID = data.VOMM_COMPID,
                VOMM_NO = VOMM_NO,
                VOMM_DATE = data.VOMM_DATE,
                VOMM_VALID = "Y",
                VOMM_PRINT_FLG = "N",
                VOMM_VERNO = 1,
                VOMM_GENNO= 0,
                VOMM_APPROVE_FLG= "N",
                VOMM_SOURCE="",
                VOMM_BATCHNO = "", // 文件沒寫
                VOMM_MEMO = data.VOMM_MEMO,
                VOMM_A_USER_ID = employeeNo,
                VOMM_A_USER_NM = Name,
                VOMM_A_DATE = DateTime.Now,
                VOMM_U_USER_ID = employeeNo,
                VOMM_U_USER_NM = Name,
                VOMM_U_DATE = DateTime.Now,
                State = "A"
            };
            rs.Add(item);
            return rs;
        }
        public List<AccVoumstD_item> deal_Add_AccVoumstD(List<AccVoumstD_item> data, string VOMM_COMPID, string VOMM_NO)
        {
            List<AccVoumstD_item> rs = new List<AccVoumstD_item>();

            //int idx = 1;
            foreach (AccVoumstD_item sour in data)
            {
                //string VOMD_INVNO = "";
                //if (string.IsNullOrEmpty(sour.VOMD_INVNO))
                //{
                //    VOMD_INVNO = VOMM_NO + idx.ToString("D4"); //0001...
                //    idx++;
                //}

                AccVoumstD_item item = new AccVoumstD_item
                {
                    VOMD_COMPID = VOMM_COMPID,
                    VOMD_NO = VOMM_NO,
                    VOMD_SEQ = sour.VOMD_SEQ,
                    VOMD_ACCD = sour.VOMD_ACCD,
                    VOMD_D_NT_AMT = sour.VOMD_D_NT_AMT,
                    VOMD_C_NT_AMT = sour.VOMD_C_NT_AMT,
                    VOMD_MEMO = sour.VOMD_MEMO,
                    VOMD_CURR = sour.VOMD_CURR,
                    VOMD_EXRATE = sour.VOMD_EXRATE,
                    VOMD_AMT = sour.VOMD_AMT,
                    VOMD_DEPTID = sour.VOMD_DEPTID,
                    VOMD_TRANID = sour.VOMD_TRANID,
                    VOMD_INVNO = sour.VOMD_INVNO,
                    VOMD_DUEFLG = sour.VOMD_DUEFLG,
                    VOMD_DUEDATE = sour.VOMD_DUEDATE,
                    VOMD_PAY_KIND = sour.VOMD_PAY_KIND,
                    VOMD_DUE_BANK = sour.VOMD_DUE_BANK,
                    VOMD_ACNO = sour.VOMD_ACNO,
                    VOMD_SAV_BANK = sour.VOMD_SAV_BANK,
                    VOMD_CVOUNO = "",
                    VOMD_CSEQ = 0,
                    VOMD_CNT = sour.VOMD_CNT,
                    VOMD_STYM = sour.VOMD_STYM,
                    VOMD_ENYM = sour.VOMD_ENYM,
                    VOMD_D_ACCD = sour.VOMD_D_ACCD,
                    VOMD_D_DEPTID = "",
                    VOMD_D_INVNO = "",
                    State = "A",
                    VOMD_RELATIVE_NO = sour.VOMD_RELATIVE_NO,
                    VOMD_TAXCD = sour.VOMD_TAXCD,
                    VOMD_EXPENSE = sour.VOMD_EXPENSE
                };
                rs.Add(item);
            }
            return rs;
        }

        public bool deal_AccXXXX(bool bAdd, List<AccVoumstD_item> data, 
            string VOMM_COMPID, string VOMM_NO, DateTime? VOMM_DATE, 
            string employeeNo, string Name,
            out List<AccReAccount_item> rsAccReAccount,
            out List<AccPaAccount_item> rsAccPaAccount,
            out List<AccReCheck_item> rsAccReCheck,
            out List<AccPaCheck_item> rsAccPaCheck,
            out List<AccOtherAccount_item> rsAccOtherAccount,
            out List<AccPrepayAccountM_item> rsAccPrepayAccountM,
            out List<AccPrepayAccountD_item> rsAccPrepayAccountD
            )
        {
            int iFlg = 1;
            if (!bAdd) iFlg = -1;  // 反向

            rsAccReAccount = new List<AccReAccount_item>();
            rsAccPaAccount = new List<AccPaAccount_item>();
            rsAccReCheck = new List<AccReCheck_item>();
            rsAccPaCheck = new List<AccPaCheck_item>();
            rsAccOtherAccount = new List<AccOtherAccount_item>();
            rsAccPrepayAccountM = new List<AccPrepayAccountM_item>();
            rsAccPrepayAccountD = new List<AccPrepayAccountD_item>();

            try
            {
                #region AccVoumstD
                foreach (AccVoumstD_item sour in data)
                {
                    DataTable dtAccName = accName._Query(sour.VOMD_ACCD.Substring(0, 4));
                    string ACNM_AR_FLG = dtAccName.Rows[0]["ACNM_AR_FLG"].ToString();
                    string ACNM_AP_FLG = dtAccName.Rows[0]["ACNM_AP_FLG"].ToString();
                    string ACNM_NR_FLG = dtAccName.Rows[0]["ACNM_NR_FLG"].ToString();
                    string ACNM_PK_FLG = dtAccName.Rows[0]["ACNM_PK_FLG"].ToString();
                    string BACT_FLG = "";

                    #region ACNM_AR_FLG => ACC_RE_ACCOUNT
                    if (ACNM_AR_FLG == "Y")
                    {
                        AccReAccount_item item2 = new AccReAccount_item
                        {
                            REAC_COMPID = VOMM_COMPID,
                            REAC_CUSTID = sour.VOMD_TRANID,
                            REAC_INVNO = sour.VOMD_INVNO
                        };

                        DataTable dt = accReAccount._QueryBatch(item2);
                        if (dt.Rows.Count != 0)
                        {
                            #region 更新
                            // 取回舊資料
                            AccReAccount_item item = dt.ToList<AccReAccount_item>()[0];

                            item.REAC_U_USER_ID = employeeNo;
                            item.REAC_U_USER_NM = Name;
                            item.REAC_U_DATE = DateTime.Now;
                            item.State = "U";

                            // 借方金額 > 0 時
                            if (sour.VOMD_D_NT_AMT > 0)
                            {
                                item.REAC_ACCD = sour.VOMD_ACCD; //20240126
                                item.REAC_NT_TOT_AMT = (decimal)dt.Rows[0]["REAC_NT_TOT_AMT"] + sour.VOMD_D_NT_AMT * iFlg;
                                item.REAC_FOR_TOT_AMT = (decimal)dt.Rows[0]["REAC_FOR_TOT_AMT"] + sour.VOMD_AMT * iFlg;
                                item.REAC_NT_BAL = (decimal)dt.Rows[0]["REAC_NT_BAL"] + sour.VOMD_D_NT_AMT * iFlg;
                                item.REAC_FOR_BAL = (decimal)dt.Rows[0]["REAC_FOR_BAL"] + sour.VOMD_AMT * iFlg;
                            }
                            else if (sour.VOMD_C_NT_AMT > 0)
                            {
                                item.REAC_NT_BAL = (decimal)dt.Rows[0]["REAC_NT_BAL"] - sour.VOMD_C_NT_AMT * iFlg;
                                item.REAC_FOR_BAL = (decimal)dt.Rows[0]["REAC_FOR_BAL"] - sour.VOMD_AMT * iFlg;
                            }
                            rsAccReAccount.Add(item);
                            #endregion
                        }
                        else
                        {
                            #region 新增

                            // 新增預設資料
                            AccReAccount_item item = new AccReAccount_item
                            {
                                State = "A",

                                REAC_COMPID = VOMM_COMPID,
                                REAC_CUSTID = sour.VOMD_TRANID,
                                REAC_INVNO = sour.VOMD_INVNO,
                                REAC_INV_DATE = VOMM_DATE,
                                REAC_DUE_DATE = sour.VOMD_DUEDATE,
                                REAC_VOUNO = VOMM_NO,
                                REAC_DEPTID = sour.VOMD_DEPTID,
                                REAC_ACCD = sour.VOMD_D_ACCD,
                                REAC_CURRID = sour.VOMD_CURR,
                                REAC_EXRATE = sour.VOMD_EXRATE,
                                REAC_FOR_TOT_AMT = sour.VOMD_AMT,
                                REAC_FOR_BAL = sour.VOMD_AMT,
                                REAC_MEMO = sour.VOMD_MEMO,

                                REAC_A_USER_ID = employeeNo,
                                REAC_A_USER_NM = Name,
                                REAC_A_DATE = DateTime.Now,
                                REAC_U_USER_ID = employeeNo,
                                REAC_U_USER_NM = Name,
                                REAC_U_DATE = DateTime.Now
                            };

                            if (sour.VOMD_D_NT_AMT > 0) // && sour.VOMD_MEMO != "" && sour.VOMD_MEMO.Substring(0, 2) == "評價")
                            {
                                item.REAC_NT_TOT_AMT = sour.VOMD_D_NT_AMT;   // 借
                                item.REAC_NT_BAL = sour.VOMD_D_NT_AMT;       // 借
                                rsAccReAccount.Add(item);
                            }
                            else if (sour.VOMD_C_NT_AMT > 0 && sour.VOMD_MEMO != "" && (sour.VOMD_MEMO!=""?sour.VOMD_MEMO.Substring(0, 2) == "評價":false))
                            {
                                item.REAC_NT_TOT_AMT = sour.VOMD_C_NT_AMT * -1; // 貸
                                item.REAC_FOR_TOT_AMT = sour.VOMD_AMT * -1;     
                                item.REAC_NT_BAL = sour.VOMD_C_NT_AMT * -1;     // 貸
                                item.REAC_FOR_BAL = sour.VOMD_AMT * -1;         

                                rsAccReAccount.Add(item);
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region ACNM_AP_FLG => ACC_PA_ACCOUNT
                    if (ACNM_AP_FLG == "Y")
                    {
                        AccPaAccount_item item2 = new AccPaAccount_item
                        {
                            PAAC_COMPID = VOMM_COMPID,
                            PAAC_VENDID = sour.VOMD_TRANID,
                            PAAC_INVNO = sour.VOMD_INVNO,
                        };

                        DataTable dt = accPaAccount._Query(item2);
                        if (dt.Rows.Count != 0)
                        {
                            #region 更新
                            // 取回舊資料
                            AccPaAccount_item item = dt.ToList<AccPaAccount_item>()[0];

                            item.PAAC_U_USER_ID = employeeNo;
                            item.PAAC_U_USER_NM = Name;
                            item.PAAC_U_DATE = DateTime.Now;
                            item.State = "U";

                            // 借方金額 > 0 時
                            if (sour.VOMD_D_NT_AMT > 0)
                            {
                                item.PAAC_NT_BAL = (decimal)dt.Rows[0]["PAAC_NT_BAL"] - sour.VOMD_D_NT_AMT * iFlg;
                                item.PAAC_FOR_BAL = (decimal)dt.Rows[0]["PAAC_FOR_BAL"] - sour.VOMD_AMT * iFlg;
                            }
                            else if (sour.VOMD_C_NT_AMT > 0)
                            {
                                item.PAAC_NT_TOT_AMT = (decimal)dt.Rows[0]["PAAC_NT_TOT_AMT"] + sour.VOMD_C_NT_AMT * iFlg;
                                item.PAAC_FOR_TOT_AMT = (decimal)dt.Rows[0]["PAAC_FOR_TOT_AMT"] + sour.VOMD_AMT * iFlg;
                                item.PAAC_NT_BAL = (decimal)dt.Rows[0]["PAAC_NT_BAL"] + sour.VOMD_C_NT_AMT * iFlg;
                                item.PAAC_FOR_BAL = (decimal)dt.Rows[0]["PAAC_FOR_BAL"] + sour.VOMD_AMT * iFlg;
                            }

                            rsAccPaAccount.Add(item);
                            #endregion
                        }
                        else
                        {
                            #region 新增
                            AccPaAccount_item item = new AccPaAccount_item
                            {
                                State = "A",
                                PAAC_COMPID = VOMM_COMPID,
                                PAAC_VENDID = sour.VOMD_TRANID,
                                PAAC_INVNO = sour.VOMD_INVNO,
                                PAAC_INV_DATE = VOMM_DATE,
                                PAAC_DUE_DATE = sour.VOMD_DUEDATE,
                                PAAC_VOUNO = VOMM_NO,
                                PAAC_ACCD = sour.VOMD_D_ACCD,
                                PAAC_CURRID = sour.VOMD_CURR,
                                PAAC_EXRATE = sour.VOMD_EXRATE,
                                PAAC_FOR_TOT_AMT = sour.VOMD_AMT,
                                PAAC_FOR_BAL = sour.VOMD_AMT,
                                PAAC_STS = "Y",
                                PAAC_MEMO = sour.VOMD_MEMO,

                                PAAC_A_USER_ID = employeeNo,
                                PAAC_A_USER_NM = Name,
                                PAAC_A_DATE = DateTime.Now,
                                PAAC_U_USER_ID = employeeNo,
                                PAAC_U_USER_NM = Name,
                                PAAC_U_DATE = DateTime.Now
                            };
                            
                            if (sour.VOMD_C_NT_AMT > 0)
                            {
                                item.PAAC_NT_TOT_AMT = sour.VOMD_C_NT_AMT; // * -1;     // 貸
                                item.PAAC_FOR_TOT_AMT = sour.VOMD_AMT; // * -1;
                                item.PAAC_NT_BAL = sour.VOMD_C_NT_AMT; // * -1;         // 貸
                                item.PAAC_FOR_BAL = sour.VOMD_AMT; // * -1;

                                rsAccPaAccount.Add(item);
                            }
                            else if (sour.VOMD_D_NT_AMT > 0 && sour.VOMD_MEMO != "" && (sour.VOMD_MEMO!=""?sour.VOMD_MEMO.Substring(0, 2) == "評價":false))
                            {
                                item.PAAC_NT_TOT_AMT = sour.VOMD_D_NT_AMT * -1;     // 借
                                item.PAAC_FOR_TOT_AMT = sour.VOMD_AMT * -1;
                                item.PAAC_NT_BAL = sour.VOMD_D_NT_AMT * -1;         // 借
                                item.PAAC_FOR_BAL = sour.VOMD_AMT * -1;

                                rsAccPaAccount.Add(item);
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region ACNM_NR_FLG => ACC_RE_CHECK 
                    if (ACNM_NR_FLG == "Y")
                    {
                        AccReCheck_item item = new AccReCheck_item
                        {
                            RECK_COMPID = VOMM_COMPID,
                            RECK_NO = sour.VOMD_INVNO,

                            RECK_U_USER_ID = employeeNo,
                            RECK_U_USER_NM = Name,
                            RECK_U_DATE = DateTime.Now
                        };

                        DataTable dt = accReCheck._QueryBatch(item);
                        if (dt.Rows.Count != 0)
                        {
                            #region 更新
                            // 取回舊資料
                            item = dt.ToList<AccReCheck_item>()[0];

                            item.RECK_U_USER_ID = employeeNo;
                            item.RECK_U_USER_NM = Name;
                            item.RECK_U_DATE = DateTime.Now;
                            item.State = "U";

                            // 借方金額 > 0 時
                            if (sour.VOMD_D_NT_AMT > 0)
                            {
                                if (iFlg == 1)
                                {
                                    item.RECK_REC_DATE = VOMM_DATE;
                                    item.RECK_DUE_DATE1 = sour.VOMD_DUEDATE;
                                    item.RECK_CUSTID = sour.VOMD_TRANID;
                                    item.RECK_DUE_BANK = sour.VOMD_DUE_BANK;
                                    item.RECK_ACNO = sour.VOMD_ACNO;
                                    item.RECK_AMT = sour.VOMD_D_NT_AMT;
                                    item.RECK_SAV_FLG = "0";
                                    item.RECK_SAV_DATE = null;
                                    item.RECK_SAV_BANK = "";
                                    item.RECK_AREA_FLG = "0";
                                    item.RECK_DUE_DATE2 = null;
                                    item.RECK_DUE_FLG = "0";
                                    item.RECK_DUE_DATE3 = null;
                                    item.RECK_VOU_DATE = null;
                                    item.RECK_VOUNO = "";
                                    item.RECK_C_VOUNO = VOMM_NO;
                                    item.RECK_C_SEQ = sour.VOMD_SEQ;
                                }
                                else
                                {
                                    item.RECK_DUE_FLG = "9";
                                    item.RECK_DUE_DATE2 = null;
                                }

                                rsAccReCheck.Add(item);
                            }
                            else if (sour.VOMD_C_NT_AMT > 0)
                            {
                                if (iFlg == 1)
                                {
                                    item.RECK_DUE_FLG = sour.VOMD_DUEFLG;
                                }
                                else
                                {
                                    item.RECK_DUE_FLG = "0";
                                    item.RECK_DUE_DATE2 = null;
                                }

                                rsAccReCheck.Add(item);
                            }
                            #endregion
                        }
                        else
                        {
                            #region 新增
                            if (sour.VOMD_D_NT_AMT > 0)
                            {
                                item.State = "A";
                                item.RECK_COMPID = VOMM_COMPID;
                                item.RECK_NO = sour.VOMD_INVNO;
                                item.RECK_REC_DATE = VOMM_DATE;
                                item.RECK_DUE_DATE1 = sour.VOMD_DUEDATE;
                                item.RECK_CUSTID = sour.VOMD_TRANID;
                                item.RECK_AMT = sour.VOMD_D_NT_AMT;
                                item.RECK_DUE_BANK = sour.VOMD_DUE_BANK;
                                item.RECK_ACNO = sour.VOMD_ACNO;
                                item.RECK_SAV_FLG = "0";
                                item.RECK_SAV_DATE = null;
                                item.RECK_SAV_BANK = "";
                                item.RECK_AREA_FLG = "0";
                                item.RECK_DUE_DATE2 = null;
                                item.RECK_DUE_FLG = "0";
                                item.RECK_DUE_DATE3 = null;
                                item.RECK_VOU_DATE = null;
                                item.RECK_VOUNO = "";
                                item.RECK_C_VOUNO = VOMM_NO;
                                item.RECK_C_SEQ = sour.VOMD_SEQ;

                                item.RECK_A_USER_ID = employeeNo;
                                item.RECK_A_USER_NM = Name;
                                item.RECK_A_DATE = DateTime.Now;

                                rsAccReCheck.Add(item);
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region ACNM_PK_FLG => ACC_PA_CHECK 
                    if (ACNM_PK_FLG == "Y")
                    {
                        AccPaCheck_item item = new AccPaCheck_item
                        {
                            PACK_COMPID = VOMM_COMPID,
                            PACK_NO = sour.VOMD_INVNO,

                            PACK_U_USER_ID = employeeNo,
                            PACK_U_USER_NM = Name,
                            PACK_U_DATE = DateTime.Now
                        };

                        DataTable dt = accPaCheck._Query(item);
                        if (dt.Rows.Count != 0)
                        {
                            #region 更新
                            // 取回舊資料
                            item = dt.ToList<AccPaCheck_item>()[0];

                            item.PACK_U_USER_ID = employeeNo;
                            item.PACK_U_USER_NM = Name;
                            item.PACK_U_DATE = DateTime.Now;
                            item.State = "U";

                            // 借方金額 > 0 時
                            if (sour.VOMD_D_NT_AMT > 0)
                            {
                                if (iFlg == 1)
                                {
                                    item.PACK_DUE_FLG = sour.VOMD_DUEFLG;
                                }
                                else
                                {
                                    item.PACK_DUE_FLG = sour.VOMD_DUEFLG; // "0";
                                    item.PACK_DUE_DATE2 = null;
                                    item.PACK_VOU_DATE = null;
                                    item.PACK_VOUNO = ""; 
                                }
                                rsAccPaCheck.Add(item);
                            }
                            else if (sour.VOMD_C_NT_AMT > 0)
                            {
                                if (iFlg == 1)
                                {
                                    item.PACK_OPEN_DATE = VOMM_DATE;
                                    item.PACK_DUE_DATE1 = sour.VOMD_DUEDATE;
                                    item.PACK_VENDID = sour.VOMD_TRANID;
                                    item.PACK_DUE_BANK = sour.VOMD_DUE_BANK;
                                    item.PACK_AMT = sour.VOMD_C_NT_AMT;
                                    item.PACK_DUE_FLG = "0";
                                    item.PACK_DUE_DATE2 = null;
                                    item.PACK_VOU_DATE = null;
                                    item.PACK_VOUNO = "";
                                    item.PACK_C_VOUNO = VOMM_NO;
                                    item.PACK_MEMO = sour.VOMD_MEMO;

                                    rsAccPaCheck.Add(item);
                                }
                                else {
                                    item.PACK_DUE_FLG = "9";
                                    item.PACK_DUE_DATE2 = null;

                                    rsAccPaCheck.Add(item);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region 新增
                            if (sour.VOMD_C_NT_AMT > 0)
                            {
                                if (iFlg == 1)
                                {
                                    item.State = "A";
                                    item.PACK_COMPID = VOMM_COMPID;
                                    item.PACK_NO = sour.VOMD_INVNO;
                                    item.PACK_OPEN_DATE = VOMM_DATE;
                                    item.PACK_DUE_DATE1 = sour.VOMD_DUEDATE;
                                    item.PACK_VENDID = sour.VOMD_TRANID;
                                    item.PACK_DUE_BANK = sour.VOMD_DUE_BANK;
                                    item.PACK_AMT = sour.VOMD_C_NT_AMT;
                                    item.PACK_DUE_FLG = "0";
                                    item.PACK_DUE_DATE2 = null;
                                    item.PACK_VOU_DATE = null;
                                    item.PACK_VOUNO = "";
                                    item.PACK_C_VOUNO = VOMM_NO;
                                    item.PACK_MEMO = sour.VOMD_MEMO;

                                    item.PACK_A_USER_ID = employeeNo;
                                    item.PACK_A_USER_NM = Name;
                                    item.PACK_A_DATE = DateTime.Now;

                                    rsAccPaCheck.Add(item);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region ACC_OTHER_ACCOUNT
                    DataTable dtBC = BalanceControl.haveAccBalanceControl(VOMM_COMPID, sour.VOMD_ACCD.Substring(0, 4));
                    if (dtBC.Rows.Count != 0)
                    {
                        BACT_FLG = dtBC.Rows[0]["BACT_FLG"].ToString();

                        AccOtherAccount_item item = new AccOtherAccount_item
                        {
                            OTAC_COMPID = VOMM_COMPID,
                            OTAC_ACCD = sour.VOMD_D_ACCD,
                            OTAC_TRANID = sour.VOMD_TRANID,
                            OTAC_INVNO = sour.VOMD_INVNO,

                            OTAC_U_USER_ID = employeeNo,
                            OTAC_U_USER_NM = Name,
                            OTAC_U_DATE = DateTime.Now
                        };

                        // 20240820
                        DataTable dtOtherAccount = accOtherAccount.getACF030M(new AccOtherAccount_qry_item {
                            OTAC_COMPID = VOMM_COMPID,
                            OTAC_ACCD = sour.VOMD_D_ACCD,
                            OTAC_TRANID = new List<string> { sour.VOMD_TRANID } ,
                            OTAC_INVNO = sour.VOMD_INVNO,
                        }); // old _Query

                        if (dtOtherAccount.Rows.Count != 0)
                        {
                            #region 更新
                            // 取回舊資料
                            item = dtOtherAccount.ToList<AccOtherAccount_item>()[0];

                            item.OTAC_U_USER_ID = employeeNo;
                            item.OTAC_U_USER_NM = Name;
                            item.OTAC_U_DATE = DateTime.Now;
                            item.State = "U";

                            if (sour.VOMD_D_NT_AMT > 0 && sour.VOMD_D_ACCD.StartsWith("1"))
                            {
                                item.OTAC_NT_TOT_AMT = (decimal)dtOtherAccount.Rows[0]["OTAC_NT_TOT_AMT"] + sour.VOMD_D_NT_AMT * iFlg;
                                item.OTAC_NT_BAL = (decimal)dtOtherAccount.Rows[0]["OTAC_NT_BAL"] + sour.VOMD_D_NT_AMT * iFlg;

                                if (iFlg == 1)
                                {
                                    item.OTAC_FOR_TOT_AMT = (decimal)dtOtherAccount.Rows[0]["OTAC_FOR_TOT_AMT"] + sour.VOMD_AMT;
                                    item.OTAC_FOR_BAL = (decimal)dtOtherAccount.Rows[0]["OTAC_FOR_BAL"] + sour.VOMD_AMT;
                                }

                                rsAccOtherAccount.Add(item);
                            }
                            else if (sour.VOMD_C_NT_AMT > 0 && sour.VOMD_D_ACCD.StartsWith("2"))
                            {
                                item.OTAC_NT_TOT_AMT = (decimal)dtOtherAccount.Rows[0]["OTAC_NT_TOT_AMT"] + sour.VOMD_C_NT_AMT * iFlg;
                                item.OTAC_NT_BAL = (decimal)dtOtherAccount.Rows[0]["OTAC_NT_BAL"] + sour.VOMD_C_NT_AMT * iFlg;

                                if (iFlg == 1)
                                {
                                    item.OTAC_FOR_TOT_AMT = (decimal)dtOtherAccount.Rows[0]["OTAC_FOR_TOT_AMT"] + sour.VOMD_AMT;
                                    item.OTAC_FOR_BAL = (decimal)dtOtherAccount.Rows[0]["OTAC_FOR_BAL"] + sour.VOMD_AMT;
                                }
                                rsAccOtherAccount.Add(item);
                            }
                            else if (sour.VOMD_C_NT_AMT > 0 && sour.VOMD_D_ACCD.StartsWith("1"))
                            {
                                item.OTAC_NT_BAL = (decimal)dtOtherAccount.Rows[0]["OTAC_NT_BAL"] - sour.VOMD_C_NT_AMT * iFlg;
                                if (iFlg == 1)
                                {
                                    item.OTAC_FOR_BAL = (decimal)dtOtherAccount.Rows[0]["OTAC_FOR_BAL"] - sour.VOMD_AMT;
                                }
                                rsAccOtherAccount.Add(item);
                            }
                            else if (sour.VOMD_D_NT_AMT > 0 && sour.VOMD_D_ACCD.StartsWith("2"))
                            {
                                item.OTAC_NT_BAL = (decimal)dtOtherAccount.Rows[0]["OTAC_NT_BAL"] - sour.VOMD_D_NT_AMT * iFlg;
                                if (iFlg == 1)
                                {
                                    item.OTAC_FOR_BAL = (decimal)dtOtherAccount.Rows[0]["OTAC_FOR_BAL"] - sour.VOMD_AMT;
                                }
                                rsAccOtherAccount.Add(item);
                            }
                            #endregion
                        }
                        else
                        {
                            #region 新增
                            item.State = "A";
                            if ((sour.VOMD_ACCD.StartsWith("1") && sour.VOMD_D_NT_AMT > 0) ||
                                (sour.VOMD_ACCD.StartsWith("2") && sour.VOMD_C_NT_AMT > 0))
                            {
                                item.OTAC_COMPID = VOMM_COMPID;
                                item.OTAC_ACCD = sour.VOMD_ACCD;
                                item.OTAC_TRANID = sour.VOMD_TRANID;
                                item.OTAC_INVNO = sour.VOMD_INVNO;
                                item.OTAC_INV_DATE = VOMM_DATE;
                                item.OTAC_DEPTID = sour.VOMD_DEPTID;
                                item.OTAC_CURRID = sour.VOMD_CURR;
                                item.OTAC_EXRATE = sour.VOMD_EXRATE;
                                item.OTAC_NT_TOT_AMT = sour.VOMD_D_NT_AMT > 0 ? sour.VOMD_D_NT_AMT : sour.VOMD_C_NT_AMT;
                                item.OTAC_FOR_TOT_AMT = sour.VOMD_AMT;
                                item.OTAC_NT_BAL = sour.VOMD_D_NT_AMT > 0 ? sour.VOMD_D_NT_AMT : sour.VOMD_C_NT_AMT;
                                item.OTAC_FOR_BAL = sour.VOMD_AMT;
                                item.OTAC_MEMO = sour.VOMD_MEMO;

                                item.OTAC_A_USER_ID = employeeNo;
                                item.OTAC_A_USER_NM = Name;
                                item.OTAC_A_DATE = DateTime.Now;

                                rsAccOtherAccount.Add(item);
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region BACT_FLG => ACC_PREPAY_ACCOUNT_M, ACC_PREPAY_ACCOUNT_D
                    if (BACT_FLG == "Y") 
                    {
                        if (iFlg == 1)
                        {
                            string sVOMD_STYM = sour.VOMD_STYM;
                            string sVOMD_Y = sour.VOMD_STYM.Substring(0,4);
                            string sVOMD_M = sour.VOMD_STYM.Substring(5, 2);

                            Int16 VOMD_STYM;
                            Int16 VOMD_ENYM;
                            if (!Int16.TryParse(sVOMD_Y, out VOMD_STYM) ||
                                !Int16.TryParse(sVOMD_M, out VOMD_ENYM))
                            {
                                return false;
                            }

                            #region AccPrepayAccountM
                            AccPrepayAccountM_item item = new AccPrepayAccountM_item
                            {
                                PRAM_COMPID = VOMM_COMPID,
                                PRAM_NO = sour.VOMD_INVNO,
                                PRAM_DATE = VOMM_DATE,
                                PRAM_D_ACNMID = sour.VOMD_ACCD,
                                PRAM_D_DEPTID = sour.VOMD_DEPTID,
                                PRAM_C_ACNMID = sour.VOMD_D_ACCD,
                                PRAM_C_DEPTID = sour.VOMD_DEPTID,
                                PRAM_NT_AMT = sour.VOMD_D_NT_AMT > 0 ? sour.VOMD_D_NT_AMT : sour.VOMD_C_NT_AMT,
                                PRAM_CNT = sour.VOMD_CNT,
                                PRAM_STYM = sour.VOMD_STYM,
                                PRAM_ENYM = sour.VOMD_ENYM,
                                PRAM_REMARK = sour.VOMD_MEMO,
                                PRAM_RD_ACNMID = "",
                                PRAM_RD_DEPTID = "",
                                PRAM_RET_AMT = 0,
                                PRAM_RET_DATE = null,
                                PRAM_RET_VOUNO = "",
                                PRAM_A_USER_ID = employeeNo,
                                PRAM_A_USER_NM = Name,
                                PRAM_A_DATE = DateTime.Now,
                                PRAM_U_USER_ID = employeeNo,
                                PRAM_U_USER_NM = Name,
                                PRAM_U_DATE = DateTime.Now,
                                PRAM_TAXCD = sour.VOMD_TAXCD,
                                State = "A"
                            };
                            rsAccPrepayAccountM.Add(item);
                            #endregion

                            #region AccPrepayAccountD
                            // 來源金額
                            decimal? NT_AMT = 0;
                            NT_AMT = sour.VOMD_D_NT_AMT > 0 ? sour.VOMD_D_NT_AMT : sour.VOMD_C_NT_AMT;
                            // 每期金額
                            int AMT = (int)(NT_AMT / sour.VOMD_CNT);
                            // 差額
                            decimal? AMT2 = 0;
                            if ((AMT * sour.VOMD_CNT) != NT_AMT)
                                AMT2 = NT_AMT - (AMT * sour.VOMD_CNT);

                            for (int ii = 0; ii <= sour.VOMD_CNT - 1; ii++)
                            {
                                if (ii != 0 && VOMD_ENYM > 12)
                                {
                                    VOMD_STYM++;
                                    VOMD_ENYM = 1;
                                }

                                AccPrepayAccountD_item item2 = new AccPrepayAccountD_item
                                {
                                    PRAD_COMPID = VOMM_COMPID,
                                    PRAD_NO = sour.VOMD_INVNO,
                                    PRAD_YEAR = VOMD_STYM,          // 年
                                    PRAD_MONTH = VOMD_ENYM,         // 月
                                    PRAD_NT_AMT = ii == 0 && AMT2 != 0 ? AMT + AMT2 : AMT,   // 每期金額
                                    PRAD_VOUNO = "",
                                    PRAD_TR_FLG = "",
                                    State = "A"
                                };
                                rsAccPrepayAccountD.Add(item2);
                                VOMD_ENYM++;
                            }
                            #endregion
                        }
                        else
                        {// 反向另外處理 填入一筆資料, 提供後續 刪除使用
                            AccPrepayAccountM_item item = new AccPrepayAccountM_item
                            {
                                PRAM_COMPID = VOMM_COMPID,
                                PRAM_NO = sour.VOMD_INVNO,
                            };
                            rsAccPrepayAccountM.Add(item);
                            AccPrepayAccountD_item itemD = new AccPrepayAccountD_item
                            {
                                PRAD_COMPID = VOMM_COMPID,
                                PRAD_NO = sour.VOMD_INVNO,
                            };
                            rsAccPrepayAccountD.Add(itemD);
                        }
                    }
                    #endregion
                }
                #endregion
                return true;
            }
            catch(Exception ex)
            {
                Log.Info(ex.Message);
            }
            return false;
        }

        public List<AccVoumstTax_item> deal_Add_AccVoumstTax(List<AccVoumstTax_item> data, string VOMM_COMPID, string VOMM_NO)
        {
            if (data == null) return data;
            if (data.Count == 0) return data;

            foreach (AccVoumstTax_item sour in data)
            {
                sour.VOMT_COMPID = VOMM_COMPID;
                sour.VOMT_NO = VOMM_NO;
                sour.State = "A";
            }
            return data;
        }

        // 資料整理 : 新增
        public AccVoumstM_Batch deal_add(AccVoumstM_Batch data, string employeeNo, string name, string NO="")
        {
            AccVoumstM_Batch rs = new AccVoumstM_Batch();
            string VOMM_COMPID = data.accVoumstM[0].VOMM_COMPID;
            DateTime? VOMM_DATE = data.accVoumstM[0].VOMM_DATE;
            string VOMM_NO;

            List<AccVouCounter_item> rsAccVouCounter = new List<AccVouCounter_item>();
            // VOMM_NO
            if (NO == "")
                // 全新資料
                rsAccVouCounter = deal_Add_AccVouCounter(VOMM_COMPID, VOMM_DATE, out VOMM_NO);
            else
                VOMM_NO = NO;  // 沿用舊的編號

            List<AccVoumstM_item> rsAccVoumstM = deal_Add_AccVoumstM(data.accVoumstM[0], VOMM_NO, employeeNo, name);
            List<AccVoumstD_item> rsAccVoumstD = deal_Add_AccVoumstD(data.accVoumstD, VOMM_COMPID, VOMM_NO);
            List<AccVoumstTax_item> rsAccVoumstTax = deal_Add_AccVoumstTax(data.accVoumstTax, VOMM_COMPID, VOMM_NO);

            List<AccReAccount_item> rsAccReAccount = new List<AccReAccount_item>();
            List<AccPaAccount_item> rsAccPaAccount = new List<AccPaAccount_item>();
            List<AccReCheck_item> rsAccReCheck = new List<AccReCheck_item>();
            List<AccPaCheck_item> rsAccPaCheck = new List<AccPaCheck_item>();
            List<AccOtherAccount_item> rsAccOtherAccount = new List<AccOtherAccount_item>();
            List<AccPrepayAccountM_item> rsAccPrepayAccountM = new List<AccPrepayAccountM_item>();
            List<AccPrepayAccountD_item> rsAccPrepayAccountD = new List<AccPrepayAccountD_item>();

            //bool bOK = deal_AccXXXX(true, data.accVoumstD, VOMM_COMPID, VOMM_NO, VOMM_DATE,
            //    employeeNo, name,
            //    out rsAccReAccount,
            //    out rsAccPaAccount,
            //    out rsAccReCheck,
            //    out rsAccPaCheck,
            //    out rsAccOtherAccount,
            //    out rsAccPrepayAccountM,
            //    out rsAccPrepayAccountD
            //);
            //if (!bOK) return null;

            //List<AccVoumstTax_item> rsAccPrepayAccountTax = deal_Add_AccVoumstTax(data.accVoumstTax, VOMM_COMPID, VOMM_NO);

            return new AccVoumstM_Batch {
                accVoumstM = rsAccVoumstM,
                accVoumstD = rsAccVoumstD,
                accVoumstTax = rsAccVoumstTax,
                accPrepayAccountM = rsAccPrepayAccountM,
                accPrepayAccountD = rsAccPrepayAccountD,
                accVouCounter = rsAccVouCounter,
                accReAccount = rsAccReAccount,
                accPaAccount = rsAccPaAccount,
                accReCheck = rsAccReCheck,
                accPaCheck = rsAccPaCheck,
                accOtherAccount = rsAccOtherAccount
            };
        }

        // 取舊資料
        public bool getAccVoumst(string VOMM_COMPID, string VOMM_NO, 
            out List<AccVoumstM_item> rsAccVoumstM,
            out List<AccVoumstD_item> rsAccVoumstD,
            out List<AccVoumstTax_item> rsAccVoumstTax
            )
        {
            rsAccVoumstM = new List<AccVoumstM_item>();
            rsAccVoumstD = new List<AccVoumstD_item>();
            rsAccVoumstTax = new List<AccVoumstTax_item>();

            try
            {
                DataTable dtAccVoumstM = accVoumstM._QueryBatch(new AccVoumstM_item
                {
                    VOMM_COMPID = VOMM_COMPID,
                    VOMM_NO = VOMM_NO
                });
                rsAccVoumstM = dtAccVoumstM.ToList<AccVoumstM_item>();

                DataTable dtAccVoumstD = accVoumstD._QueryBatch(new AccVoumstD_item
                {
                    VOMD_COMPID = VOMM_COMPID,
                    VOMD_NO = VOMM_NO
                });
                rsAccVoumstD = dtAccVoumstD.ToList<AccVoumstD_item>();

                DataTable dtAccVoumstTax = accVoumstTax._QueryBatch(new AccVoumstTax_item
                {
                    VOMT_COMPID = VOMM_COMPID,
                    VOMT_NO = VOMM_NO
                });
                rsAccVoumstTax = dtAccVoumstTax.ToList<AccVoumstTax_item>();

                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.Message);
            }
            return false;
        }

        // 資料整理 : 刪除 反向處理
        public AccVoumstM_Batch deal_Modify(AccVoumstM_Batch data, string employeeNo, string name)
        {
            string VOMM_COMPID = data.accVoumstM[0].VOMM_COMPID;
            string VOMM_NO = data.accVoumstM[0].VOMM_NO;
            string STATE = data.accVoumstM[0].State;

            List<AccVoumstM_item> rsAccVoumstM;
            List<AccVoumstD_item> rsAccVoumstD;
            List<AccVoumstTax_item> rsAccVoumstTax;

            // 取舊資料
            bool bOK = getAccVoumst(VOMM_COMPID, VOMM_NO,
                out rsAccVoumstM,
                out rsAccVoumstD,
                out rsAccVoumstTax);
            if (!bOK) return null;

            #region AccVoumstM
            rsAccVoumstM[0].State = "U";

            if (STATE == "U")
            {
                rsAccVoumstM[0].VOMM_VERNO = (short)(rsAccVoumstM[0].VOMM_VERNO + 1);
                rsAccVoumstM[0].VOMM_MEMO = data.accVoumstM[0].VOMM_MEMO;
                rsAccVoumstM[0].VOMM_SOURCE = data.accVoumstM[0].VOMM_SOURCE;
                rsAccVoumstM[0].VOMM_TYPE = data.accVoumstM[0].VOMM_TYPE;
            }
            else if (STATE == "D")
            {
                rsAccVoumstM[0].VOMM_VALID = "N";
            }

            rsAccVoumstM[0].VOMM_U_USER_ID = employeeNo;
            rsAccVoumstM[0].VOMM_U_USER_NM = name;
            rsAccVoumstM[0].VOMM_U_DATE = DateTime.Now;
            #endregion

            List<AccReAccount_item> rsAccReAccount = new List<AccReAccount_item>();
            List<AccPaAccount_item> rsAccPaAccount = new List<AccPaAccount_item>();
            List<AccReCheck_item> rsAccReCheck = new List<AccReCheck_item>();
            List<AccPaCheck_item> rsAccPaCheck = new List<AccPaCheck_item>();
            List<AccOtherAccount_item> rsAccOtherAccount = new List<AccOtherAccount_item>();
            List<AccPrepayAccountM_item> rsAccPrepayAccountM = new List<AccPrepayAccountM_item>();
            List<AccPrepayAccountD_item> rsAccPrepayAccountD = new List<AccPrepayAccountD_item>();

            //                        資料庫中的資料
            //bOK = deal_AccXXXX(false, rsAccVoumstD, VOMM_COMPID, VOMM_NO,
            //                    rsAccVoumstM[0].VOMM_DATE, employeeNo, name,
            //    out rsAccReAccount,
            //    out rsAccPaAccount,
            //    out rsAccReCheck,
            //    out rsAccPaCheck,
            //    out rsAccOtherAccount,
            //    out rsAccPrepayAccountM, // 另外處理, 這裡不使用
            //    out rsAccPrepayAccountD  // 另外處理, 這裡不使用
            //);
            //if (!bOK) return null;

            return new AccVoumstM_Batch
            {
                accVoumstM = rsAccVoumstM,
                accVoumstD = null,
                accVoumstTax = null,
                accPrepayAccountM = rsAccPrepayAccountM,
                accPrepayAccountD = rsAccPrepayAccountD,
                accVouCounter = null,
                accReAccount = rsAccReAccount,
                accPaAccount = rsAccPaAccount,
                accReCheck = rsAccReCheck,
                accPaCheck = rsAccPaCheck,
                accOtherAccount = rsAccOtherAccount
            };
        }

        // 新增刪除修改
        public bool AUD(bool newVoumstM , AccVoumstM_Batch data, CommDAO dao, string employeeNo, string name)
        {
            if (data.accVoumstM == null) return false;

            string VOMM_COMPID = data.accVoumstM[0].VOMM_COMPID;
            string VOMM_NO = data.accVoumstM[0].VOMM_NO;

            bool bAdd = data.accVoumstM[0].State == "A";
            try
            {   // 因為資料異動後又新增的關係, 這時候 accVoumstM 未被刪除, 要用修改的方式
                if (newVoumstM)
                    data.accVoumstM[0].State = "A";
                else
                    data.accVoumstM[0].State = "U";

                bool bOK = accVoumstM.AUD(data.accVoumstM[0], employeeNo, name, dao);

                //if (bAdd)
                //{// 新增
                //    #region accPrepayAccountM
                //    if (bOK && data.accPrepayAccountM != null && data.accPrepayAccountM.Count!=0)
                //    {
                //        foreach (AccPrepayAccountM_item item in data.accPrepayAccountM)
                //        {
                //            bOK = accPrepayAccountM.AUD(item, employeeNo, name, dao);
                //            if (!bOK) break;
                //        }
                //    }
                //    #endregion
                //    #region accPrepayAccountD
                //    if (bOK && data.accPrepayAccountD != null && data.accPrepayAccountD.Count!=0)
                //    {
                //        foreach (AccPrepayAccountD_item item in data.accPrepayAccountD)
                //        {
                //            bOK = accPrepayAccountD.AUD(item, employeeNo, name, dao);
                //            if (!bOK) break;
                //        }
                //    }
                //    #endregion
                //}

                if (bAdd)
                {// 新增
                 //#region accVoumstD
                 //if (bOK && data.accVoumstD!=null && data.accVoumstD.Count!=0)
                 //{
                 //    foreach (AccVoumstD_item item in data.accVoumstD)
                 //    {
                 //        bOK = accVoumstD.AUD(item, employeeNo, name, dao);
                 //        if (!bOK) break;
                 //    }
                 //}
                 //#endregion

                    //20260319 Stop
                    #region accVoumstTax
                    //if (bOK && data.accVoumstTax!=null && data.accVoumstTax.Count!=0)
                    //{
                    //    foreach (AccVoumstTax_item item in data.accVoumstTax)
                    //    {
                    //        bOK = accVoumstTax.AUD(item, employeeNo, name, dao);
                    //        if (!bOK) break;
                    //    }
                    //}
                    #endregion

                    //20260319 Stop
                    #region accVouCounter
                    if (data.accVouCounter != null && data.accVouCounter.Count != 0)
                    {
                        foreach (AccVouCounter_item item in data.accVouCounter)
                        {
                            bOK = accVouCounter.AUD(item, employeeNo, name, dao);
                            if (!bOK) break;
                        }
                    }
                    #endregion
                }

                //#region accReAccount
                //if (bOK && data.accReAccount!= null && data.accReAccount.Count!=0)
                //{
                //    foreach (AccReAccount_item item in data.accReAccount)
                //    {
                //        bOK = accReAccount.AUD(item, employeeNo, name, dao);
                //        if (!bOK) break;
                //    }
                //}
                //#endregion
                //#region accPaAccount
                //if (bOK && data.accPaAccount!=null && data.accPaAccount.Count!=0)
                //{
                //    foreach (AccPaAccount_item item in data.accPaAccount)
                //    {
                //        bOK = accPaAccount.AUD(item, employeeNo, name, dao);
                //        if (!bOK) break;
                //    }
                //}
                //#endregion
                //#region accReCheck
                //if (bOK && data.accReCheck!=null && data.accReCheck.Count!=0)
                //{
                //    foreach (AccReCheck_item item in data.accReCheck)
                //    {
                //        bOK = accReCheck.AUD(item, employeeNo, name, dao);
                //        if (!bOK) break;
                //    }
                //}
                //#endregion
                //#region accPaCheck
                //if (bOK && data.accPaCheck!=null && data.accPaCheck.Count!=0)
                //{
                //    foreach (AccPaCheck_item item in data.accPaCheck)
                //    {
                //        bOK = accPaCheck.AUD(item, employeeNo, name, dao);
                //        if (!bOK) break;
                //    }
                //}
                //#endregion
                //#region accOtherAccount
                //if (bOK && data.accOtherAccount!=null && data.accOtherAccount.Count!=0)
                //{
                //    foreach (AccOtherAccount_item item in data.accOtherAccount)
                //    {
                //        bOK = accOtherAccount.AUD(item, employeeNo, name, dao);
                //        if (!bOK) break;
                //    }
                //}
                //#endregion

                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.Message);
            }
            return false;
        }

        public rs_AccVoumstM_Batch _AUD(AccVoumstM_Batch_ins data)
        {
            CommStoreProcedure sp = new CommStoreProcedure();

            string employeeNo = data.baseRequest.employeeNo;
            string name = data.baseRequest.name;

            if (data.data.accVoumstM[0].State == "A")
            {
                //return AUD_Add(data.data, employeeNo, name);

                List<AccVoumstD_item> accVoumstD;
                rs_AccVoumstM_Batch rs = AUD_Add(data.data, employeeNo, name, out accVoumstD);

                if (rs.result.retCode == 0)
                {
                    foreach (var item in accVoumstD) //data.data.accVoumstD)
                    {
                        sp.runACB010M_ADD(item, employeeNo, name);
                    }
                }

                return rs;
            }
            else if (data.data.accVoumstM[0].State == "U")
            {
                //return AUD_Update(data.data, employeeNo, name);
                List<AccVoumstD_item> accVoumstD;
                rs_AccVoumstM_Batch rs = AUD_Update(data.data, employeeNo, name, out accVoumstD);

                sp.runACB010M_DEL(data.data.accVoumstM[0].VOMM_COMPID, data.data.accVoumstM[0].VOMM_NO, 
                    "U", employeeNo, name);

                foreach (var item in accVoumstD) //data.data.accVoumstD)
                {
                    sp.runACB010M_ADD(item, employeeNo, name);
                }

                return rs;
            }
            else if (data.data.accVoumstM[0].State == "D")
            {
                //return AUD_Delete(data.data, employeeNo, name);

                rs_AccVoumstM_Batch rs = AUD_Delete(data.data, employeeNo, name);

                sp.runACB010M_DEL(data.data.accVoumstM[0].VOMM_COMPID, data.data.accVoumstM[0].VOMM_NO,
                    "D", employeeNo, name);

                return rs;
            }

            return new rs_AccVoumstM_Batch
            {
                result = CommDAO.getRsItem1("State 只允許 A, U")
            };
        }

        public bool AUD_deleteAll(bool bDelete, string COMPID, string NO, CommDAO dao)
        {
            bool bOK = true;

            //bOK = accPrepayAccountM.DeleteAll2(COMPID, NO, dao);
            //if (bOK)
            //    bOK = accPrepayAccountD.DeleteAll2(COMPID, NO, dao);

            //if (!bDelete)
            //{
            //    if (bOK)
            //        bOK = accVoumstD.DeleteAll(COMPID, NO, dao);
            //    if (bOK)

                    bOK = accVoumstTax.DeleteAll(COMPID, NO, dao);
            //}
            return bOK;
        }

        // 資料檢查
        public bool checkAccVoumstD(ref List<AccVoumstD_item> data)
        {
            AccAccNameDAO accName = new AccAccNameDAO();
            AccReAccountDAO accReAccount = new AccReAccountDAO();
            AccPaAccountDAO accPaAccount = new AccPaAccountDAO();
            AccBalanceControlDAO accBalanceControl = new AccBalanceControlDAO();
            AccOtherAccountDAO accOtherAccount = new AccOtherAccountDAO();

            bool bOK = true;
            foreach (AccVoumstD_item item in data)
            {
                string accCode = item.VOMD_ACCD.Substring(0, 4);

                DataTable dt = accName._Query2(accCode);
                if (dt.Rows.Count!=0)
                {
                    #region ACNM_AR_FLG
                    if ((dt.Rows[0]["ACNM_AR_FLG"].ToString() == "Y") && (item.VOMD_C_NT_AMT > 0) 
                        && (item.VOMD_MEMO!="" ? item.VOMD_MEMO.Substring(0,2)!= "評價" : true))
                    {
                        bOK = accReAccount.isExist(new AccReAccount_item
                        {
                            REAC_COMPID = item.VOMD_COMPID,
                            REAC_CUSTID = item.VOMD_TRANID,
                            REAC_INVNO = item.VOMD_INVNO
                        });
                        if (bOK == false)
                        {
                            item.errMsg = "無此立帳紀錄 !!";
                            break;
                        }
                    }
                    #endregion
                    #region ACNM_AP_FLG
                    if (dt.Rows[0]["ACNM_AP_FLG"].ToString() == "Y" && item.VOMD_D_NT_AMT > 0
                        && (item.VOMD_MEMO != "" ? item.VOMD_MEMO.Substring(0, 2) != "評價" : true))
                    {
                        bOK = accPaAccount.isExist(new AccPaAccount_item
                        {
                            PAAC_COMPID = item.VOMD_COMPID,
                            PAAC_VENDID = item.VOMD_TRANID,
                            PAAC_INVNO = item.VOMD_INVNO
                        });
                        if (bOK == false)
                        {
                            item.errMsg = "無此立帳紀錄 !!";
                            break;
                        }
                    }
                    #endregion
                }

                dt = accBalanceControl.haveAccBalanceControl(item.VOMD_COMPID, accCode);
                if (dt.Rows.Count != 0)
                {
                    if ((item.VOMD_C_NT_AMT > 0 && item.VOMD_ACCD.Substring(0, 1) == "1") ||
                        (item.VOMD_D_NT_AMT > 0 && item.VOMD_ACCD.Substring(0, 1) == "2"))
                    {
                        bOK = accOtherAccount.isExist(new AccOtherAccount_item {
                            OTAC_COMPID = item.VOMD_COMPID,
                            OTAC_ACCD = item.VOMD_ACCD,
                            OTAC_TRANID = item.VOMD_TRANID,
                            OTAC_INVNO = item.VOMD_INVNO
                        });
                        if (bOK == false)
                        {
                            item.errMsg = "無此立帳紀錄 !!";
                            break;
                        }
                    }
                }
            }
            return bOK;
        }


        public rs_AccVoumstM_Batch AUD_Add(AccVoumstM_Batch data, string employeeNo, string name, out List<AccVoumstD_item> accVoumstD)
        {
            accVoumstD = null;

            #region 新增時資料檢查
            List<AccVoumstD_item> dd = data.accVoumstD;
            if (checkAccVoumstD(ref dd) == false)
            {
                return new rs_AccVoumstM_Batch
                {
                    result = CommDAO.getRsItem1(),
                    data = new rsAccVoumstM_Batch
                    {
                        accVoumstD = dd,
                    }
                };
            }
            #endregion

            // 資料處理
            AccVoumstM_Batch item = deal_add(data, employeeNo, name);
            accVoumstD = item.accVoumstD; // 20240514

            CommDAO dao = new CommDAO();

            dao.DB.BeginTransaction();
            bool bOK = AUD(true, item, dao, employeeNo, name);
            if (bOK)
            {
                dao.DB.Commit();
                return new rs_AccVoumstM_Batch
                {
                    result = CommDAO.getRsItem(),
                    data = new rsAccVoumstM_Batch
                    {
                        accVoumstM = item.accVoumstM,
                        accVoumstD = item.accVoumstD,
                        accVoumstTax = item.accVoumstTax
                    }
                };
            }
            else dao.DB.Rollback();

            return new rs_AccVoumstM_Batch
            {
                result = CommDAO.getRsItem1()
            };
        }
        public rs_AccVoumstM_Batch AUD_Delete(AccVoumstM_Batch data, string employeeNo, string name)
        {
            CommDAO dao = new CommDAO();

            AccVoumstM_Batch item = deal_Modify(data, employeeNo, name);

            dao.DB.BeginTransaction();
            bool bOK = AUD(false, item, dao, employeeNo, name);
            if (bOK)
            {
                // 刪除資料
                string COMPID = data.accVoumstM[0].VOMM_COMPID;
                string NO = data.accVoumstM[0].VOMM_NO;

                bOK = AUD_deleteAll(true, COMPID, NO, dao);
            }

            if (bOK)
            {
                dao.DB.Commit();

                return new rs_AccVoumstM_Batch
                {
                    result = CommDAO.getRsItem(),
                    data = new rsAccVoumstM_Batch
                    {
                        accVoumstM = item.accVoumstM,
                        accVoumstD = item.accVoumstD,
                        accVoumstTax = item.accVoumstTax
                    }
                };
            }
            else
                dao.DB.Rollback();

            return new rs_AccVoumstM_Batch
            {
                result = CommDAO.getRsItem1()
            };
        }
        public rs_AccVoumstM_Batch AUD_Update(AccVoumstM_Batch data, string employeeNo, string name, out List<AccVoumstD_item> accVoumstD)
        {
            accVoumstD = null;

            CommDAO dao = new CommDAO();

            AccVoumstM_Batch item = deal_Modify(data, employeeNo, name);
            AccVoumstM_Batch item2 = null;

            dao.DB.BeginTransaction();
            // 沖正
            bool bOK = AUD(false, item, dao, employeeNo, name);
            if (bOK)
            {
                // 刪除資料
                string COMPID = data.accVoumstM[0].VOMM_COMPID;
                string NO = data.accVoumstM[0].VOMM_NO;

                bOK = AUD_deleteAll(false, COMPID, NO, dao);
                dao.DB.Commit();

                if (bOK)
                {
                    dao.DB.BeginTransaction();

                    // 加回資料, 需帶回原傳票單號
                    item2 = deal_add(data, employeeNo, name, NO);
                    // 20250703
                    item2.accVoumstM[0].VOMM_SOURCE = item.accVoumstM[0].VOMM_SOURCE;
                    item2.accVoumstM[0].VOMM_TYPE = item.accVoumstM[0].VOMM_TYPE;

                    accVoumstD = item2.accVoumstD;
                    bOK = AUD(false, item2, dao, employeeNo, name);
                    if (bOK)
                    {
                        dao.DB.Commit();

                        return new rs_AccVoumstM_Batch
                        {
                            result = CommDAO.getRsItem(),
                            data = new rsAccVoumstM_Batch
                            {
                                accVoumstM = item2.accVoumstM,
                                accVoumstD = item2.accVoumstD,
                                accVoumstTax = item2.accVoumstTax
                            }
                        };
                    }
                    else
                    {
                        dao.DB.Rollback();
                    }
                }
            }
            else {
                dao.DB.Rollback();
            }

            return new rs_AccVoumstM_Batch
            {
                result = CommDAO.getRsItem1()
            };
        }

        //public rs addUpdate(AccVoumstM_Batch_ins data)
        //{
        //    string errMsg = "";
        //    string FK = "";
        //    bool bSave = true;
        //    CommDAO dao = new CommDAO();
        //    dao.DB.BeginTransaction();
        //    try
        //    {
        //        foreach (AccVoumstM_Batch item in data.data)
        //        {
        //            FK = $"VOMM_COMPID:{item.VOMM_COMPID}-VOMM_NO:{item.VOMM_NO}";
        //            if (!string.IsNullOrEmpty(item.VOMM_COMPID) && !string.IsNullOrEmpty(item.VOMM_NO))
        //            {
        //                #region AccVoumstM
        //                bSave = audM(item, data.baseRequest.employeeNo, data.baseRequest.name, dao);
        //                if (bSave == false)
        //                {
        //                    errMsg = "其他錯誤";
        //                    break;
        //                }
        //                #endregion

        //                #region AccVoumstD
        //                foreach (AccVoumstD child in item.child)
        //                {
        //                    FK = $"VOMD_COMPID:{child.VOMD_COMPID}-VOMD_NO:{child.VOMD_NO}-VOMD_SEQ:{child.VOMD_SEQ}";
        //                    if (!string.IsNullOrEmpty(child.VOMD_COMPID) && !string.IsNullOrEmpty(child.VOMD_NO))
        //                    {
        //                        bSave = audD(child, dao);
        //                        if (bSave == false)
        //                        {
        //                            errMsg = "其他錯誤";
        //                            break;
        //                        }
        //                    }
        //                }
        //                #endregion
        //            }
        //            else
        //            {
        //                errMsg = "PK 資料不完整";
        //                bSave = false;
        //            }
        //            if (bSave == false) break;
        //        }
        //        if (bSave)
        //        {
        //            dao.DB.Commit();
        //            return CommDAO.getRs();
        //        }
        //        dao.DB.Rollback();
        //        return CommDAO.getRs(1, errMsg + " => " + FK);
        //    }
        //    catch (Exception e)
        //    {
        //        dao.DB.Rollback();
        //        return CommDAO.getRs(1, e.Message);
        //    }
        //}

        //// 會計傳票審核登錄 - 更新狀態
        //public static rs update(AccVoumstM_Batch_ins data)
        //{
        //    string errMsg = "";
        //    string FK = "";
        //    bool bSave = true;
        //    CommDAO dao = new CommDAO();
        //    dao.DB.BeginTransaction();
        //    try
        //    {
        //        foreach (AccVoumstM_Batch item in data.data)
        //        {
        //            FK = $"VOMM_COMPID:{item.VOMM_COMPID}-VOMM_NO:{item.VOMM_NO}";
        //            if (!string.IsNullOrEmpty(item.VOMM_COMPID) && !string.IsNullOrEmpty(item.VOMM_NO))
        //            {
        //                bSave = AccVoumstMDAO.Update(item.VOMM_COMPID, item.VOMM_NO, item.VOMM_APPROVE_FLG, dao);
        //                errMsg = "更新失敗";
        //                if (!bSave) break;
        //            }
        //            else
        //            {
        //                errMsg = "PK 資料不完整";
        //                bSave = false;
        //            }
        //            if (bSave == false) break;
        //        }
        //        if (bSave)
        //        {
        //            dao.DB.Commit();
        //            return CommDAO.getRs();
        //        }
        //        dao.DB.Rollback();
        //        return CommDAO.getRs(1, errMsg + " => " + FK);
        //    }
        //    catch (Exception e)
        //    {
        //        dao.DB.Rollback();
        //        return CommDAO.getRs(1, e.Message);
        //    }
        //}

        //// 刪除
        ////public rs delete(AccVoumstM_Batch_ins data)
        ////{
        ////    bool bSave = true;
        ////    CommDAO dao = new CommDAO();
        ////    dao.DB.BeginTransaction();
        ////    try
        ////    {
        ////        foreach (AccVoumstM_Batch item in data.data)
        ////        {
        ////            if (!string.IsNullOrEmpty(item.VOMM_COMPID) && !string.IsNullOrEmpty(item.VOMM_NO))
        ////            {
        ////                bSave = accVoumstM._DeleteBatch(item, dao);
        ////                if (!bSave) break;
        ////                bSave = accVoumstD.DeleteAll(item.VOMM_COMPID, item.VOMM_NO);
        ////                if (!bSave) break;
        ////            }
        ////        }
        ////        if (bSave)
        ////        {
        ////            dao.DB.Commit();
        ////            return CommDAO.getRs();
        ////        }
        ////        dao.DB.Rollback();
        ////        return CommDAO.getRs(1, "錯誤");
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        dao.DB.Rollback();
        ////        return CommDAO.getRs(1, e.Message);
        ////    }
        ////}

        // 查詢
        public rsAccVoumstM_Batch_qry queryAccVoumstM_Batch(AccVoumstM_qry2 data)
        {
            List<rsAccVoumstM_Batch_item> rs = new List<rsAccVoumstM_Batch_item>();

            string sql = $@"SELECT * FROM ACC_VOUMST_M 
LEFT JOIN vw_USER ON VOMM_COMPID=USER_COMPID AND VOMM_A_USER_ID=USERID
WHERE 1=1 ";
            #region 公司代碼
            if (string.IsNullOrEmpty(data.VOMM_COMPID))
            {
                return new rsAccVoumstM_Batch_qry()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未傳入公司代碼" }
                };
            }
            else
            {
                sql += $" AND VOMM_COMPID='{data.VOMM_COMPID}'";
            }
            #endregion

            sql += CommDAO.sql_ep_between(data.BNo, data.ENo, "VOMM_NO");
            sql += CommDAO.sql_ep_date_between(data.BDate, data.EDate, "VOMM_DATE");
            sql += CommDAO.sql_like(data.UserNo, "VOMM_A_USER_ID", "%");
            sql += CommDAO.sql_like(data.UserName, "USERNAME", "%");

            if (!string.IsNullOrEmpty(data.VOMM_APPROVE_FLG))
            {
                if (data.VOMM_APPROVE_FLG == "Y" || data.VOMM_APPROVE_FLG == "N")
                {
                    sql += $@" AND VOMM_APPROVE_FLG='{data.VOMM_APPROVE_FLG}'";
                }
                else
                {
                    return new rsAccVoumstM_Batch_qry { result = new rsItem { retCode = 1, retMsg = "VOMM_APPROVE_FLG 請填入 Y or N" } };
                }
            }
            sql += CommDAO.sql_ep(data.VOMM_VALID, "VOMM_VALID");
            sql += " ORDER BY VOMM_COMPID, VOMM_NO ";

            DataTable dt = comm.DB.RunSQL(sql);

            foreach (DataRow dr in dt.Rows)
            {
                #region ACC_VOUMST_M
                rsAccVoumstM_Batch_item rsItem = new rsAccVoumstM_Batch_item()
                {
                    VOMM_COMPID = dr["VOMM_COMPID"].ToString(),
                    VOMM_NO = dr["VOMM_NO"].ToString(),
                    VOMM_DATE = dr.FieldOrDefault<DateTime?>("VOMM_DATE"),
                    VOMM_VALID = dr["VOMM_VALID"].ToString(),
                    VOMM_PRINT_FLG = dr["VOMM_PRINT_FLG"].ToString(),
                    VOMM_VERNO = dr.FieldOrDefault<Int16>("VOMM_VERNO"),
                    VOMM_GENNO = dr.FieldOrDefault<Int32>("VOMM_GENNO"),
                    VOMM_APPROVE_FLG = dr["VOMM_APPROVE_FLG"].ToString(),
                    VOMM_SOURCE = dr["VOMM_SOURCE"].ToString(),
                    VOMM_BATCHNO = dr["VOMM_BATCHNO"].ToString(),
                    VOMM_MEMO = dr["VOMM_MEMO"].ToString(),
                    accVoumstD = new List<AccVoumstD>(),
                    accVoumstTax = new List<AccVoumstTax>()
                };
                #endregion

                #region ACC_VOUMST_D
                DataTable dtCode = accVoumstD._QueryBatch(new AccVoumstD_item() { VOMD_COMPID = dr["VOMM_COMPID"].ToString(), VOMD_NO = dr["VOMM_NO"].ToString() });
                if (dtCode.Rows.Count != 0)
                {
                    foreach (DataRow code in dtCode.Rows)
                    {
                        AccVoumstD child = new AccVoumstD()
                        {
                            VOMD_COMPID = code["VOMD_COMPID"].ToString(),
                            VOMD_NO = code["VOMD_NO"].ToString(),
                            VOMD_SEQ = code.FieldOrDefault<int>("VOMD_SEQ"),
                            VOMD_ACCD = code["VOMD_ACCD"].ToString(),
                            VOMD_D_NT_AMT = code.FieldOrDefault<decimal>("VOMD_D_NT_AMT"),
                            VOMD_C_NT_AMT = code.FieldOrDefault<decimal>("VOMD_C_NT_AMT"),
                            VOMD_MEMO = code["VOMD_MEMO"].ToString(),
                            VOMD_CURR = code["VOMD_CURR"].ToString(),
                            VOMD_EXRATE = code.FieldOrDefault<decimal>("VOMD_EXRATE"),
                            VOMD_AMT = code.FieldOrDefault<decimal>("VOMD_AMT"),
                            VOMD_DEPTID = code["VOMD_DEPTID"].ToString(),
                            VOMD_TRANID = code["VOMD_TRANID"].ToString(),
                            VOMD_INVNO = code["VOMD_INVNO"].ToString(),
                            VOMD_DUEFLG = code["VOMD_DUEFLG"].ToString(),
                            VOMD_DUEDATE = code.FieldOrDefault<DateTime?>("VOMD_DUEDATE"),
                            VOMD_PAY_KIND = code["VOMD_PAY_KIND"].ToString(),
                            VOMD_DUE_BANK = code["VOMD_DUE_BANK"].ToString(),
                            VOMD_ACNO = code["VOMD_ACNO"].ToString(),
                            VOMD_SAV_BANK = code["VOMD_SAV_BANK"].ToString(),
                            VOMD_CVOUNO = code["VOMD_CVOUNO"].ToString(),
                            VOMD_CSEQ = code.FieldOrDefault<Int16>("VOMD_CSEQ"),
                            VOMD_CNT = code.FieldOrDefault<Int16>("VOMD_CNT"),
                            VOMD_STYM = code["VOMD_STYM"].ToString(),
                            VOMD_ENYM = code["VOMD_ENYM"].ToString(),
                            VOMD_D_ACCD = code["VOMD_D_ACCD"].ToString(),
                            VOMD_D_DEPTID = code["VOMD_D_DEPTID"].ToString(),
                            VOMD_D_INVNO = code["VOMD_D_INVNO"].ToString(),
                            VOMD_EXPENSE = code["VOMD_EXPENSE"].ToString(),
                            VOMD_TAXCD = code["VOMD_TAXCD"].ToString(),
                            VOMD_RELATIVE_NO = code["VOMD_RELATIVE_NO"].ToString(),
                        };
                        rsItem.accVoumstD.Add(child);
                    }
                }
                #endregion

                #region ACC_VOUMST_TAX
                dtCode = accVoumstTax._QueryBatch(new AccVoumstTax_item() { VOMT_COMPID = dr["VOMM_COMPID"].ToString(), VOMT_NO = dr["VOMM_NO"].ToString() });
                if (dtCode.Rows.Count != 0)
                {
                    foreach (DataRow code in dtCode.Rows)
                    {
                        AccVoumstTax child = new AccVoumstTax()
                        {
                            VOMT_COMPID = code["VOMT_COMPID"].ToString(),
                            VOMT_NO = code["VOMT_NO"].ToString(),
                            VOMT_SEQ = code.FieldOrDefault<Int16>("VOMT_SEQ"),
                            VOMT_FORMAT = code["VOMT_FORMAT"].ToString(),
                            VOMT_DATA_YM = code["VOMT_DATA_YM"].ToString(),
                            VOMT_INV_DATE = code.FieldOrDefault<DateTime?>("VOMT_INV_DATE"),
                            VOMT_INVNO = code["VOMT_INVNO"].ToString(),
                            VOMT_S_UNNO = code["VOMT_S_UNNO"].ToString(),
                            VOMT_P_UNNO = code["VOMT_P_UNNO"].ToString(),
                            VOMT_TAXCD = code["VOMT_TAXCD"].ToString(),
                            VOMT_TAXCD1 = code["VOMT_TAXCD1"].ToString(),
                            VOMT_NET_AMT = code.FieldOrDefault<int>("VOMT_NET_AMT"),
                            VOMT_TAX = code.FieldOrDefault<int>("VOMT_TAX"),
                            VOMT_AMT = code.FieldOrDefault<int>("VOMT_AMT"),
                            VOMT_PCODE = code["VOMT_PCODE"].ToString(),
                            VOMT_TOT_FLG = code["VOMT_TOT_FLG"].ToString(),
                            VOMT_QTY = code.FieldOrDefault<Int16>("VOMT_QTY")
                        };
                        rsItem.accVoumstTax.Add(child);
                    }
                }
                #endregion

                rs.Add(rsItem);
            }

            return new rsAccVoumstM_Batch_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs
            };
        }

    }
}