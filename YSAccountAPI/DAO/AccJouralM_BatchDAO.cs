using System;
using System.Collections.Generic;
using System.Data;
using Models;

namespace DAO
{
    public class AccJouralM_BatchDAO : BaseClass
    {
        AccJouralMDAO accJouralM = new AccJouralMDAO();
        AccJouralDDAO accJouralD = new AccJouralDDAO();

        private Int16 getJOUD_SEQ(string JOUD_COMPID, string JOUD_CODE)
        {
            string sql = $@"SELECT MAX(JOUD_SEQ) as NO from ACC_JOURAL_D where 
JOUD_COMPID= @JOUD_COMPID AND JOUD_CODE=@JOUD_CODE";
            object[] obj = new object[] { JOUD_COMPID , JOUD_CODE };
            DataTable dt = comm.DB.RunSQL(sql, obj);
            if (dt.Rows[0]["NO"].ToString() == "")
                return 0;
            else
                return (Int16)dt.Rows[0]["NO"];
        }
        // 新增 and 修改
        public rs addUpdate(AccJouralM_Batch_ins data)
        {
            bool bOK = true;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccJouralM_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.JOUM_COMPID) && !string.IsNullOrEmpty(item.JOUM_CODE))
                    {
                        DataTable dtM = accJouralM.haveAccJouralM(item.JOUM_COMPID, item.JOUM_CODE);
                        if (dtM.Rows.Count == 0)
                        {
                            bOK = accJouralM._addAccJouralM(item, data.baseRequest.employeeNo, data.baseRequest.name, dao);
                            if (!bOK) break;
                        }
                        else
                        {
                            bOK = accJouralM._updateAccJouralM(item, data.baseRequest.employeeNo, data.baseRequest.name, dao);
                            if (!bOK) break;
                        }
                        // get max JOUD_SEQ
                        Int16 JOUD_SEQ = getJOUD_SEQ(item.JOUM_COMPID, item.JOUM_CODE);

                        foreach (AccJouralD child in item.child)
                        {
                            if (!string.IsNullOrEmpty(child.JOUD_COMPID) && !string.IsNullOrEmpty(child.JOUD_CODE))
                            {
                                DataTable dtD = accJouralD.isExist(child);
                                if (dtD.Rows.Count == 0)
                                {
                                    JOUD_SEQ++;
                                    child.JOUD_SEQ = JOUD_SEQ;
                                    bOK= accJouralD.insert_data(child, dao);
                                    if (!bOK) break;
                                }
                                else
                                {
                                    bOK = accJouralD.update_data(child, dao);
                                    if (!bOK) break;
                                }
                            }
                            if (!bOK) break;
                        }
                    }
                }
                if (bOK)
                {
                    dao.DB.Commit();
                    return CommDAO.getRs();
                }
                dao.DB.Rollback();
                return CommDAO.getRs(1, "錯誤");
            }
            catch (Exception e)
            {
                dao.DB.Rollback();
                return CommDAO.getRs(1, e.Message);
            }
        }

        // 停用
        private rs add(AccJouralM_Batch_ins data)
        {
            int ii = 0;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccJouralM_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.JOUM_COMPID) && !string.IsNullOrEmpty(item.JOUM_CODE))
                    {
                        ii += accJouralM._addAccJouralM(item, data.baseRequest.employeeNo, data.baseRequest.name, dao) ? 1 : 0;
                        foreach (AccJouralD child in item.child)
                        {
                            if (!string.IsNullOrEmpty(child.JOUD_COMPID) && !string.IsNullOrEmpty(child.JOUD_CODE))
                            {
                                ii += accJouralD.insert_data(child, dao) ? 1 : 0;
                            }
                        }
                    }
                }
                if (ii != 0)
                {
                    dao.DB.Commit();
                    return CommDAO.getRs();
                }
                dao.DB.Rollback();
                return CommDAO.getRs(1, "錯誤");
            }
            catch (Exception e)
            {
                dao.DB.Rollback();
                return CommDAO.getRs(1, e.Message);
            }
        }

        // 停用
        private  rs update(AccJouralM_Batch_ins data)
        {
            int ii = 0;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccJouralM_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.JOUM_COMPID) && !string.IsNullOrEmpty(item.JOUM_CODE))
                    {
                        ii += accJouralM._updateAccJouralM(item, data.baseRequest.employeeNo, data.baseRequest.name, dao) ? 1 : 0;
                        foreach (AccJouralD child in item.child)
                        {
                            if (!string.IsNullOrEmpty(child.JOUD_COMPID) && !string.IsNullOrEmpty(child.JOUD_CODE))
                            {
                                ii += accJouralD.update_data(child, dao) ? 1 : 0;
                            }
                        }
                    }
                }
                if (ii != 0)
                {
                    dao.DB.Commit();
                    return CommDAO.getRs();
                }
                return CommDAO.getRs(1, "錯誤");
            }
            catch (Exception e)
            {
                dao.DB.Rollback();
                return CommDAO.getRs(1, e.Message);
            }
        }

        // 刪除
        public rs delete(AccJouralM_Batch_ins data)
        {
            int ii = 0;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccJouralM_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.JOUM_COMPID) && !string.IsNullOrEmpty(item.JOUM_CODE))
                    {
                        ii += accJouralM._deleteAccJouralM(item.JOUM_COMPID, item.JOUM_CODE, dao) ? 1 : 0;
                        foreach (AccJouralD child in item.child)
                        {
                            if (!string.IsNullOrEmpty(child.JOUD_COMPID) && !string.IsNullOrEmpty(child.JOUD_CODE) && child.JOUD_SEQ != null)
                            {
                                ii += accJouralD.delete_data(child.JOUD_COMPID, child.JOUD_CODE, child.JOUD_SEQ, dao) ? 1 : 0;
                            }
                        }
                    }
                }
                if (ii != 0)
                {
                    dao.DB.Commit();
                    return CommDAO.getRs();
                }
                return CommDAO.getRs(1, "錯誤");
            }
            catch (Exception e)
            {
                dao.DB.Rollback();
                return CommDAO.getRs(1, e.Message);
            }
        }

        // 查詢
        public rs_AccJouralM_Batch queryAccJouralM_Batch(AccJouralM_del data)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region SQL
            string sql = "SELECT * FROM ACC_JOURAL_M WHERE 1=1 ";
            if (string.IsNullOrEmpty(data.data.JOUM_COMPID))
            {
                return new rs_AccJouralM_Batch()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未傳入公司代碼" }
                };
            }
            else
            {
                sql += $" AND JOUM_COMPID='{data.data.JOUM_COMPID}'";
            }
            if (!string.IsNullOrEmpty(data.data.JOUM_CODE))
                sql += $" AND JOUM_CODE='{data.data.JOUM_CODE}'";

            sql += CommDAO.sql_ep(data.data.JOUM_VALID, "JOUM_VALID");

            sql += " ORDER BY JOUM_COMPID, JOUM_CODE ";
            #endregion
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccJouralM_Batch> rs = new List<AccJouralM_Batch>();
            foreach (DataRow dr in dt.Rows)
            {
                AccJouralM_Batch item = new AccJouralM_Batch()
                {
                    JOUM_COMPID = dr["JOUM_COMPID"].ToString(),
                    JOUM_CODE = dr["JOUM_CODE"].ToString(),
                    JOUM_VALID = dr["JOUM_VALID"].ToString(),
                    JOUM_MEMO = dr["JOUM_MEMO"].ToString(),
                    JOUM_A_USER_ID = dr["JOUM_A_USER_ID"].ToString(),
                    JOUM_A_USER_NM = dr["JOUM_A_USER_NM"].ToString(),

                    child = new List<AccJouralD>()
                };

                DataTable dtCode = accJouralD._select_data_batch(dr["JOUM_COMPID"].ToString(), dr["JOUM_CODE"].ToString());
                if (dtCode.Rows.Count != 0)
                {
                    foreach (DataRow code in dtCode.Rows)
                    {
                        AccJouralD child = new AccJouralD()
                        {
                            JOUD_COMPID = code["JOUD_COMPID"].ToString(),
                            JOUD_CODE = code["JOUD_CODE"].ToString(),
                            JOUD_SEQ = (int)code["JOUD_SEQ"],
                            JOUD_ACCD = code["JOUD_ACCD"].ToString(),
                            JOUD_MEMO = code["JOUD_MEMO"].ToString(),
                            JOUD_DEPTID = code["JOUD_DEPTID"].ToString(),
                            JOUD_TRANID = code["JOUD_TRANID"].ToString(),
                            JOUD_INVNO = code["JOUD_INVNO"].ToString(),
                            JOUD_DAMT = (decimal)code["JOUD_DAMT"],
                            JOUD_CAMT = (decimal)code["JOUD_CAMT"]
                        };
                        item.child.Add(child);
                    }
                }
                rs.Add(item); ;
            }

            return new rs_AccJouralM_Batch()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber!=0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }
    }
}