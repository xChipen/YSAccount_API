using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccAllowanceM_BatchDAO
    {
        AccAllowanceMDAO accAllowanceM = new AccAllowanceMDAO();
        AccAllowanceDDAO accAllowanceD = new AccAllowanceDDAO();

        public rsAccAllowanceM_Batch AUD(AccAllowanceM_Batch data, string employeeNo, string name)
        {
            CommDAO dao = new CommDAO();
            string COMPID = data.accAllowanceM[0].ALLM_COMPID;
            DateTime? ALLM_DATE = data.accAllowanceM[0].ALLM_DATE;
            string State = data.accAllowanceM[0].State;

            string ALLM_NO = data.accAllowanceM[0].ALLM_NO;

            if (data.accAllowanceM[0].State == "A")         // 新增
            {
                ALLM_NO = accAllowanceM.getNO(COMPID, ALLM_DATE);
                data.accAllowanceM[0].ALLM_NO = ALLM_NO;
            }
            else if (data.accAllowanceM[0].State == "U")    // 異動 : 先刪除 AccAllowanceD 的資料, 然後全部 insert
            {
                accAllowanceD._DeleteAll(COMPID, ALLM_NO);
            }
            else if (data.accAllowanceM[0].State == "D")    // 作廢
            {
                data.accAllowanceM[0].ALLM_VALID = "N";
                data.accAllowanceM[0].State = "U";          // 將狀態 D => U, 異動狀態
            }

            dao.DB.BeginTransaction();
            try
            {
                bool bOK = accAllowanceM.AUD(data.accAllowanceM[0], employeeNo, name, dao);
                if (bOK)
                {
                    if (!(State == "D")) // State : A, U
                    {
                        foreach (AccAllowanceD_item item in data.accAllowanceD)
                        {
                            item.State = "A";
                            item.ALLD_COMPID = COMPID;
                            item.ALLD_NO = ALLM_NO;

                            bOK = accAllowanceD.AUD(item, employeeNo, name, dao);
                            if (!bOK) break;
                        }
                    }
                }
                if (bOK)
                {
                    dao.DB.Commit();
                    return new rsAccAllowanceM_Batch()
                    {
                        result = CommDAO.getRsItem(),
                        data = data
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Info(ex.Message);
            }

            dao.DB.Rollback();

            return new rsAccAllowanceM_Batch() {
                result = CommDAO.getRsItem1()
            };
        }

        public rsAccAllowanceM_Batch_qry Query(AccAllowanceM_Batch_qry data)
        {
            DataTable dtAccAllowanceM = accAllowanceM._QueryBatch(new AccAllowanceM_item
            {
                ALLM_COMPID = data.data.ALLM_COMPID,
                ALLM_NO = data.data.ALLM_NO
            });
            DataTable dtAccAllowanceD = accAllowanceD._QueryBatch(new AccAllowanceD_item
            {
                ALLD_COMPID = data.data.ALLM_COMPID,
                ALLD_NO = data.data.ALLM_NO
            });

            List<AccAllowanceM> M = dtAccAllowanceM.ToList<AccAllowanceM>();

            return new rsAccAllowanceM_Batch_qry {
                result = CommDAO.getRsItem(),
                data = new AccAllowanceM_Batch2
                {
                    accAllowanceM = M.First(),
                    accAllowanceD = dtAccAllowanceD.ToList<AccAllowanceD>()
                }
            };
        }
    }
}