using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccReceiveM_BatchDAO:BaseClass
    {

        AccReceiveMDAO accReceiveM = new AccReceiveMDAO();
        AccReceiveDDAO accReceiveDs = new AccReceiveDDAO();

        AccReceiveWriteoffDAO accReceiveWriteoffs = new AccReceiveWriteoffDAO();
        AccReceiveCheckDAO accReceiveChecks = new AccReceiveCheckDAO();
        AccTempReceiptsWriteoffDAO accTempReceiptsWriteoffs = new AccTempReceiptsWriteoffDAO();
        AccReAccountDAO accReAccounts = new AccReAccountDAO();
        AccOtherAccountDAO accOtherAccounts = new AccOtherAccountDAO();
        AccReCheckDAO accReChecks = new AccReCheckDAO();

        AccReceiveTaxDAO accReceiveTaxs = new AccReceiveTaxDAO();


        public rsAccReceiveM_Batch_ins rsData(AccReceiveM_Batch data, bool succ= false, string errMsg="")
        {
            return new rsAccReceiveM_Batch_ins
            {
                result = succ? CommDAO.getRsItem() : CommDAO.getRsItem1(errMsg =="" ? "更新失敗":errMsg),
                data = data
            };
        }

        public bool AUD_AccReceiveD(List<AccReceiveD_item> data, string employeeNo, string name, string NO, string COMPID, CommDAO dao)
        {
            bool bOK = false;
            if (data != null && data.Count != 0)
            {
                foreach (AccReceiveD_item item in data)
                {
                    item.State = "A";
                    item.RECD_NO = NO;
                    item.RECD_COMPID = COMPID; 
                    bOK = accReceiveDs.AUD(item, employeeNo, name, dao);
                    if (!bOK)
                    {
                        item.errMsg = "更新失敗";
                        return false;
                    }
                }
            }
            return true;
        }
        
        //20241122
        public bool AUD_AccReceiveTax(List<AccReceiveTax_item> data, string employeeNo, string name, string NO, string COMPID, CommDAO dao)
        {
            bool bOK = false;
            if (data != null && data.Count != 0)
            {
                foreach (AccReceiveTax_item item in data)
                {
                    item.State = "A";
                    item.RECT_NO = NO;
                    item.RECT_COMPID = COMPID;
                    bOK = accReceiveTaxs.AUD(item, employeeNo, name, dao);
                    if (!bOK)
                    {
                        item.errMsg = "更新失敗";
                        return false;
                    }
                }
            }
            return true;
        }

        public bool AUD_AccReceiveWriteoff(List<AccReceiveWriteoff_item> data, string employeeNo, string name, string NO, string COMPID, CommDAO dao)
        {
            bool bOK = false;
            if (data != null && data.Count != 0)
            {
                foreach (AccReceiveWriteoff_item item in data)
                {
                    item.State = "A";
                    item.RECW_NO = NO;
                    item.RECW_COMPID = COMPID;
                    bOK = accReceiveWriteoffs.AUD(item, employeeNo, name, dao);
                    if (!bOK)
                    {
                        item.errMsg = "更新失敗";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool AUD_AccReceiveCheck(List<AccReceiveCheck_item> data, string employeeNo, string name, string NO, string COMPID, CommDAO dao)
        {
            bool bOK = false;
            if (data != null && data.Count != 0)
            {
                foreach (AccReceiveCheck_item item in data)
                {
                    item.State = "A";
                    item.REAK_NO = NO;
                    item.REAK_COMPID = COMPID;
                    bOK = accReceiveChecks.AUD(item, employeeNo, name, dao);
                    if (!bOK)
                    {
                        item.errMsg = "更新失敗";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool AUD_AccTempReceiptsWriteoff(List<AccTempReceiptsWriteoff_item> data, string employeeNo, string name, string NO, string COMPID, CommDAO dao)
        {
            bool bOK = false;
            if (data != null && data.Count != 0)
            {
                foreach (AccTempReceiptsWriteoff_item item in data)
                {
                    item.State = "A";
                    item.RTPW_NO = NO;
                    item.RTPW_COMPID = COMPID;
                    bOK = accTempReceiptsWriteoffs.AUD(item, employeeNo, name, dao);
                    if (!bOK)
                    {
                        item.errMsg = "更新失敗";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool AUD_AccReAccount(List<AccReAccount_item> data, string employeeNo, string name, string COMPID, CommDAO dao)
        {
            bool bOK = false;
            if (data != null && data.Count != 0)
            {
                foreach (AccReAccount_item item in data)
                {
                    item.REAC_COMPID = COMPID;
                    bOK = accReAccounts.AUD(item, employeeNo, name, dao);
                    if (!bOK)
                    {
                        item.errMsg = "更新失敗";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool AUD_AccOtherAccount(List<AccOtherAccount_item> data, string employeeNo, string name, string COMPID, CommDAO dao)
        {
            bool bOK = false;
            if (data != null && data.Count != 0)
            {
                foreach (AccOtherAccount_item item in data)
                {
                    item.OTAC_COMPID = COMPID;
                    bOK = accOtherAccounts.AUD(item, employeeNo, name, dao);
                    if (!bOK)
                    {
                        item.errMsg = "更新失敗";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool AUD_AccReCheck(List<AccReCheck_item> data, string employeeNo, string name, string COMPID, CommDAO dao)
        {
            bool bOK = false;
            if (data != null && data.Count != 0)
            {
                foreach (AccReCheck_item item in data)
                {
                    item.RECK_COMPID = COMPID;
                    bOK = accReChecks.AUD(item, employeeNo, name, dao);
                    if (!bOK)
                    {
                        item.errMsg = "更新失敗";
                        return false;
                    }
                }
            }
            return true;
        }

        public bool checkData(AccReceiveM_Batch data, out string errMsg)
        {
            errMsg = "";
            if (string.IsNullOrEmpty(data.accReceiveMs.RECM_COMPID))
            {
                errMsg = "無 公司代號";
                return false;
            }
            if (data.accReceiveMs.State != "A")
            {
                if (data.accReceiveMs.RECM_NO == "")
                {
                    errMsg = "AccReceiveM 狀態非新增, 無 RECM_NO";
                    return false;
                }
            }
            if (data.accReceiveMs.RECM_DATE == null)
            {
                errMsg = "無 收款日期";
                return false;
            }

            return true;
        }

        public rsAccReceiveM_Batch_ins AUD(AccReceiveM_Batch data, string employeeNo, string name)
        {
            if (data.accReceiveMs.State == "A")
                return _AUD(data, employeeNo, name);

            else if (data.accReceiveMs.State == "U")
                return AUD_Update(data, employeeNo, name);

            else if (data.accReceiveMs.State == "D")
            {
                bool bOK;
                return _AUD_Delete(data, employeeNo, name, out bOK, true);
            }

            return rsData(data, false, "狀態 State 非 AUD");
        }
        // 新增
        public rsAccReceiveM_Batch_ins _AUD(AccReceiveM_Batch data, string employeeNo, string name, bool bReWrite=false)
        {

            string COMPID = data.accReceiveMs.RECM_COMPID;
            DateTime? RECM_DATE = data.accReceiveMs.RECM_DATE;
            string RECM_CUSTID = data.accReceiveMs.RECM_CUSTID;
            string STATE = data.accReceiveMs.State;

            // check
            string errMsg;
            bool bOK = checkData(data, out errMsg);
            if (!bOK)
                return rsData(data, false, errMsg);

            CommDAO dao = new CommDAO();

            string RECM_NO = "";
            if (STATE == "A" && !bReWrite)  // 新增才要取號 [修改也會走一次這個流程]
                // 最大號 +1
                RECM_NO = accReceiveM.getRECM_NO(data.accReceiveMs.RECM_DATE);
            else
            {
                RECM_NO = data.accReceiveMs.RECM_NO;
            }

            dao.DB.BeginTransaction();

            #region accReceiveM
            if (bOK) 
            {

                data.accReceiveMs.RECM_NO = RECM_NO;
                data.accReceiveMs.RECM_VALID = "Y";

                bOK = accReceiveM.AUD(data.accReceiveMs, employeeNo, name, dao);
                if (!bOK)
                    data.accReceiveMs.errMsg = "更新失敗";
            }
            #endregion

            #region GRID1 : accReceiveD
            if (bOK)
                bOK = AUD_AccReceiveD(data.accReceiveDs, employeeNo, name, RECM_NO, COMPID, dao);
            #endregion

            #region GEID2 : accReceiveWriteoffs
            if (bOK)
                bOK = AUD_AccReceiveWriteoff(data.accReceiveWriteoffs, employeeNo, name, RECM_NO, COMPID, dao);
            #endregion

            #region 20241125 餘額表
            if (bOK)
                bOK = AUD_AccReceiveTax(data.accReceiveTaxs, employeeNo, name, RECM_NO, COMPID, dao);
            #endregion

            #region accReAccounts
            if (bOK)
            {
                foreach (AccReceiveWriteoff_item item in data.accReceiveWriteoffs)
                {
                    bOK = accReAccounts.Update_ACF030M(COMPID, item.RECW_CUSTID, item.RECW_INVNO, item.RECW_NT_AMT,
                        RECM_DATE, employeeNo, name, dao);
                    if (!bOK) break;
                }
            }
            #endregion

            #region GRID3 : accReceiveChecks
            if (bOK)
                bOK = AUD_AccReceiveCheck(data.accReceiveChecks, employeeNo, name, RECM_NO, COMPID, dao);
            #endregion

            #region accReChecks
            if (bOK)
            {
                List<AccReCheck_item> AccReChecks = new List<AccReCheck_item>();

                foreach (AccReceiveCheck item in data.accReceiveChecks) {
                    AccReCheck_item obj = new AccReCheck_item {
                        State = "A",

                        RECK_COMPID = COMPID,
                        RECK_NO = item.REAK_CHKNO,  // 票據號碼
                        RECK_REC_DATE = RECM_DATE,
                        RECK_DUE_DATE1 = item.REAK_DUE_DATE,
                        RECK_CUSTID = RECM_CUSTID,
                        RECK_AMT = item.REAK_NT_AMT,
                        RECK_DUE_BANK = item.REAK_BANKID,
                        RECK_ACNO = item.REAK_ACNO,
                        RECK_SAV_FLG = "0",
                        RECK_SAV_DATE = null,
                        RECK_SAV_BANK ="",
                        RECK_AREA_FLG ="0",
                        RECK_DUE_DATE2 = null, 
                        RECK_DUE_FLG ="0",
                        RECK_DUE_DATE3 =null, 
                        RECK_VOU_DATE = null,
                        RECK_VOUNO ="",
                        RECK_C_VOUNO ="",
                        RECK_C_SEQ =0
                    };
                    AccReChecks.Add(obj);
                }
                if (AccReChecks.Count != 0)
                    bOK = AUD_AccReCheck(AccReChecks, employeeNo, name, COMPID, dao);
            }
            #endregion

            #region GRID4 : accTempReceiptsWriteoffs
            if (bOK)
                bOK = AUD_AccTempReceiptsWriteoff(data.accTempReceiptsWriteoffs, employeeNo, name, RECM_NO, COMPID, dao);
            #endregion

            #region accOtherAccounts
            if (bOK)
            {
                //update_ACF030M
                foreach (AccTempReceiptsWriteoff_item item in data.accTempReceiptsWriteoffs)
                {
                    bOK = accOtherAccounts.update_ACF030M(COMPID, RECM_CUSTID, item.RTPW_INVNO, item.RTPW_NT_AMT,
                        employeeNo, name, dao);
                    if (!bOK) break;
                }
            }
            #endregion

            if (bOK)
                dao.DB.Commit();
            else
                dao.DB.Rollback();

            return rsData(data, bOK);
        }
        // 修改
        public rsAccReceiveM_Batch_ins AUD_Update(AccReceiveM_Batch data, string employeeNo, string name)
        {
            bool bOK;

            rsAccReceiveM_Batch_ins item = _AUD_Delete(data, employeeNo, name, out bOK);

            if (!bOK)
                return item;
            else
            {
                data.accReceiveMs.State = "U";
                return _AUD(data, employeeNo, name, true);
            }
        }
        // 刪除
        public rsAccReceiveM_Batch_ins _AUD_Delete(AccReceiveM_Batch data, string employeeNo, string name, out bool bOK, bool bDelete = false)
        {

            string COMPID = data.accReceiveMs.RECM_COMPID;
            string RECM_CUSTID = data.accReceiveMs.RECM_CUSTID;
            string RECM_NO = data.accReceiveMs.RECM_NO;
            DateTime? RECM_DATE = data.accReceiveMs.RECM_DATE;

            // check
            string errMsg;
            bOK = checkData(data, out errMsg);
            if (!bOK)
                return rsData(data, false, errMsg);

            CommDAO dao = new CommDAO();

            dao.DB.BeginTransaction();

            #region accReceiveM
            if (bOK)
            {
                if (bDelete)
                {
                    data.accReceiveMs.State = "U";  // 改成 U 進行異動
                    data.accReceiveMs.RECM_VALID = "N";
                }

                bOK = accReceiveM.AUD(data.accReceiveMs, employeeNo, name, dao);
                if (!bOK)
                    data.accReceiveMs.errMsg = "更新失敗";
            }
            #endregion

            #region accReceiveWriteoffs
            if (bOK)
            {
                DataTable dt = accReceiveWriteoffs._QueryBatch(COMPID, RECM_NO);
                List<AccReceiveWriteoff> items = dt.ToList<AccReceiveWriteoff>();

                foreach (AccReceiveWriteoff item in items)
                {
                    bOK = accReAccounts.Update_ACF030M(COMPID, item.RECW_CUSTID, item.RECW_INVNO, item.RECW_NT_AMT * -1,
                        RECM_DATE, employeeNo, name, dao);
                    if (!bOK) break;
                }
            }
            #endregion

            #region accReceiveChecks
            if (bOK)
            {
                DataTable dt = accReceiveChecks._QueryBatch(COMPID, RECM_NO);
                List<AccReceiveCheck> items = dt.ToList<AccReceiveCheck>();

                foreach (AccReceiveCheck item in items)
                {
                    bOK = accReChecks.DeleteAll(COMPID, item.REAK_CHKNO, dao);
                    if (!bOK) break;
                }
            }
            #endregion

            #region accTempReceiptsWriteoffs
            if (bOK) {
                DataTable dt = accTempReceiptsWriteoffs._QueryBatch(COMPID, RECM_NO);
                List<AccTempReceiptsWriteoff> items = dt.ToList<AccTempReceiptsWriteoff>();
                foreach (AccTempReceiptsWriteoff item in items)
                {
                    bOK = accOtherAccounts.update_ACF030M_2(item.RTPW_COMPID, item.RTPW_CUSTID, item.RTPW_INVNO, 
                        item.RTPW_NT_AMT, employeeNo, name, dao);
                    if (!bOK) break;
                }
            }
            #endregion

            if (bOK && !bDelete)
            {
                #region Delete
                if (bOK)
                    bOK = accReceiveDs.DeleteAll(COMPID, RECM_NO, dao);
                if (bOK)
                    bOK = accReceiveWriteoffs.DeleteAll(COMPID, RECM_NO, dao);
                if (bOK)
                    bOK = accReceiveChecks.DeleteAll(COMPID, RECM_NO, dao);
                if (bOK)
                    bOK = accTempReceiptsWriteoffs.DeleteAll(COMPID, RECM_NO, dao);
                //20241125
                if (bOK)
                    bOK = accReceiveTaxs.DeleteAll(COMPID, RECM_NO, dao);
                #endregion
            }

            if (bOK)
                dao.DB.Commit();
            else
                dao.DB.Rollback();

            return rsData(data, bOK);
        }

        public rsAccReceiveM_Batch_qry Query(AccReceiveM_Batch_qry_item data)
        {

            if (string.IsNullOrEmpty(data.COMPID) || string.IsNullOrEmpty(data.NO))
            {
                return new rsAccReceiveM_Batch_qry
                {
                    result = CommDAO.getRsItem1(),
                };
            }

            string COMPID = data.COMPID;
            string NO = data.NO;

            rsAccReceiveM_Batch_qry_item rs = new rsAccReceiveM_Batch_qry_item();

            rs.accReceiveMs = accReceiveM._QueryBatch(COMPID, NO).ToList<AccReceiveM>(); 
            rs.accReceiveDs = accReceiveDs._QueryBatch(COMPID, NO).ToList<AccReceiveD>(); 
            rs.accReceiveWriteoffs = accReceiveWriteoffs._QueryBatch(COMPID, NO).ToList<AccReceiveWriteoff>(); 
            rs.accReceiveChecks = accReceiveChecks._QueryBatch(COMPID, NO).ToList<AccReceiveCheck>();
            rs.accTempReceiptsWriteoffs = accTempReceiptsWriteoffs._QueryBatch(COMPID, NO).ToList<AccTempReceiptsWriteoff>();
            //20241125
            rs.accReceiveTaxs = accReceiveTaxs._QueryBatch(COMPID, NO).ToList<AccReceiveTax>();

            return new rsAccReceiveM_Batch_qry {
                result = CommDAO.getRsItem(),
                data = rs
            };
        }


    }
}