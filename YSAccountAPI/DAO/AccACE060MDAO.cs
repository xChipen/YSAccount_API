using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccACE060MDAO:BaseClass
    {
        AccBankBalanceDAO daoAccBankBalance = new AccBankBalanceDAO();
        AccUndueCheckDAO daoAccUndueCheckDAO = new AccUndueCheckDAO();

        public rs Insert(AccACE060M_item data, string employee, string name)
        {
            CommDAO dao = new CommDAO();
            bool bOK;

            rs rs = daoAccBankBalance.Delete2(new AccBankBalance_item
            {
                ABBL_COMPID = data.ABBL_COMPID,
                ABBL_YEAR = data.ABBL_YEAR,
                ABBL_MONTH = data.ABBL_MONTH,
                ABBL_ACCD = data.ABBL_ACCD,
                ABBL_AMT = data.ABBL_AMT
            });
            bOK = rs.result.retCode == 0;

            if (bOK)
            {
                rs = daoAccUndueCheckDAO.Delete2(new AccUndueCheck_item
                {
                    UNDU_COMPID = data.ABBL_COMPID,
                    UNDU_YEAR = data.ABBL_YEAR,
                    UNDU_MONTH = data.ABBL_MONTH,
                    UNDU_ACCD = data.ABBL_ACCD
                });
                bOK = rs.result.retCode == 0;
            }

            if (bOK)
            {
                dao.DB.BeginTransaction();

                bOK = daoAccBankBalance._AUD(
                    new AccBankBalance_item
                    {
                        ABBL_COMPID = data.ABBL_COMPID,
                        ABBL_YEAR = data.ABBL_YEAR,
                        ABBL_MONTH = data.ABBL_MONTH,
                        ABBL_ACCD = data.ABBL_ACCD,
                        ABBL_AMT = data.ABBL_AMT,
                        State = "A"
                    }, employee, name, dao);

                if (bOK)
                {
                    foreach (UNDU item in data.ACC_UNDUE_CHECK)
                    {
                        bOK = daoAccUndueCheckDAO._AUD(new AccUndueCheck_item
                        {
                            UNDU_COMPID = data.ABBL_COMPID,
                            UNDU_YEAR = data.ABBL_YEAR,
                            UNDU_MONTH = data.ABBL_MONTH,
                            UNDU_ACCD = data.ABBL_ACCD,
                            UNDU_CHKNO = item.UNDU_CHKNO,
                            UNDU_AMT = item.UNDU_AMT,
                            State = "A"
                        }, employee, name, dao);

                        if (bOK == false)
                            break;
                    }
                }

                if (bOK)
                {
                    dao.DB.Commit();
                    return CommDAO.getRs();
                }
                else dao.DB.Rollback();
            }
            return CommDAO.getRs1();
        }
    }
}