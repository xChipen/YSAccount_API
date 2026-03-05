using System;
using System.Collections.Generic;
using Models;
using System.Data;
using Helpers;

namespace DAO
{
    public class AccPrepayAccountM_BatchDAO : BaseClass
    {
        AccPrepayAccountMDAO accPrepayAccountM = new AccPrepayAccountMDAO();
        AccPrepayAccountDDAO accPrepayAccountD = new AccPrepayAccountDDAO();
        AccPrepayAccountShareDAO accPrepayAccountShare = new AccPrepayAccountShareDAO();

        // Master 新增 and 修改 and 刪除
        public bool audM(AccPrepayAccountM_Batch item, string employeeNo, string name, CommDAO dao)
        {
            switch (item.State)
            {
                case "A": return accPrepayAccountM._AddBatch(item, employeeNo, name, dao);
                case "U": return accPrepayAccountM._UpdateBatch(item, employeeNo, name, dao);
                case "D": return accPrepayAccountM._DeleteBatch(item, dao);
                case "": return true;
            }

            return false;
        }
        // Detail 新增 and 修改 and 刪除
        public bool audD(AccPrepayAccountD_item child, CommDAO dao)
        {
            switch (child.State)
            {
                case "A": return accPrepayAccountD._AddBatch(child, dao);
                case "U": return accPrepayAccountD._UpdateBatch(child, dao);
                case "D": return accPrepayAccountD._DeleteBatch(child, dao);
                case "": return true;
            }

            return false;
        }
        // Share 新增 and 修改 and 刪除
        public  bool audShare(AccPrepayAccountShare child, CommDAO dao)
        {
            switch (child.State)
            {
                case "A": return accPrepayAccountShare._AddBatch(child, dao);
                case "U": return accPrepayAccountShare._UpdateBatch(child, dao);
                case "D": return accPrepayAccountShare._DeleteBatch(child, dao);
                case "": return true;
            }

            return false;
        }


        public rs addUpdate(AccPrepayAccountM_Batch_ins data)
        {
            string errMsg = "";
            string FK = "";
            bool bSave = true;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccPrepayAccountM_Batch item in data.data)
                {
                    FK = $"PRAM_COMPID:{item.PRAM_COMPID}-PRAM_NO:{item.PRAM_NO}";
                    if (!string.IsNullOrEmpty(item.PRAM_COMPID) && !string.IsNullOrEmpty(item.PRAM_NO))
                    {
                        #region AccPrepayAccountD
                        bSave = audM(item, data.baseRequest.employeeNo, data.baseRequest.name, dao);
                        if (bSave == false) {
                            errMsg = "其他錯誤";
                            break;
                        }
                        #endregion

                        #region AccPrepayAccountD
                        foreach (AccPrepayAccountD_item child in item.child)
                        {
                            FK = $"PRAD_COMPID:{child.PRAD_COMPID}-PRAD_NO:{child.PRAD_NO}-PRAD_YEAR:{child.PRAD_YEAR}-PRAD_MONTH:{child.PRAD_MONTH}";
                            if (!string.IsNullOrEmpty(child.PRAD_COMPID) && !string.IsNullOrEmpty(child.PRAD_NO))
                            {
                                bSave = audD(child, dao);
                                if (bSave == false)
                                {
                                    errMsg = "其他錯誤";
                                    break;
                                }
                            }
                            else
                            {
                                errMsg = "PK 資料不完整";
                                bSave = false;
                            }
                        }
                        #endregion

                        #region AccPrepayAccountShare
                        foreach (AccPrepayAccountShare child in item.share)
                        {
                            FK = $"PRAS_COMPID:{child.PRAS_COMPID}-PRAS_NO:{child.PRAS_NO}-PRAS_SEQ:{child.PRAS_SEQ}";
                            if (!string.IsNullOrEmpty(child.PRAS_COMPID) && !string.IsNullOrEmpty(child.PRAS_NO))
                            {
                                bSave = audShare(child, dao);
                                if (bSave == false)
                                {
                                    errMsg = "其他錯誤";
                                    break;
                                }
                            }
                            else
                            {
                                errMsg = "PK 資料不完整";
                                bSave = false;
                            }
                        }
                        #endregion
                    }
                    else {
                        errMsg = "PK 資料不完整";
                        bSave = false;
                    }
                    if (bSave == false) break;
                }
                if (bSave)
                {
                    dao.DB.Commit();
                    return CommDAO.getRs();
                }
                dao.DB.Rollback();
                return CommDAO.getRs(1, errMsg + " => " + FK);
            }
            catch (Exception e)
            {
                dao.DB.Rollback();
                return CommDAO.getRs(1, e.Message);
            }
        }

        // 刪除
        public rs delete(AccPrepayAccountM_Batch_ins data)
        {
            bool bSave = true;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccPrepayAccountM_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.PRAM_COMPID) && !string.IsNullOrEmpty(item.PRAM_NO))
                    {
                        bSave = accPrepayAccountM._DeleteBatch(item, dao);
                        if (!bSave) break;
                        bSave = accPrepayAccountD.DeleteAll(item.PRAM_COMPID, item.PRAM_NO, dao);
                        if (!bSave) break;
                        bSave = accPrepayAccountShare.DeleteAll(item.PRAM_COMPID, item.PRAM_NO, dao);
                        if (!bSave) break;
                    }
                }
                if (bSave)
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

        // 查詢
        public rs_AccPrepayAccountM_Batch queryAccPrepayAccountM_Batch(AccPrepayAccountM_Batch_qry data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region SQL
            string sql = "SELECT * FROM ACC_PREPAY_ACCOUNT_M WHERE 1=1 ";
            if (string.IsNullOrEmpty(data.data.PRAM_COMPID))
            {
                return new rs_AccPrepayAccountM_Batch()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未傳入公司代碼" }
                };
            }
            else
            {
                sql += $" AND PRAM_COMPID='{data.data.PRAM_COMPID}'";
            }
            if (!string.IsNullOrEmpty(data.data.PRAM_NO))
                sql += $" AND PRAM_NO='{data.data.PRAM_NO}'";

            if (!string.IsNullOrEmpty(data.data.PRAM_D_ACNMID))
                sql += $" AND PRAM_D_ACNMID Like '{data.data.PRAM_D_ACNMID}%'";
            if (!string.IsNullOrEmpty(data.data.PRAM_REMARK))
                sql += $" AND PRAM_REMARK Like '%{data.data.PRAM_REMARK }%'";
            if (!string.IsNullOrEmpty(data.data.PRAM_C_ACNMID))
                sql += $" AND PRAM_C_ACNMID Like '{data.data.PRAM_C_ACNMID }%'";


            sql += " ORDER BY PRAM_COMPID, PRAM_NO ";
            #endregion

            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            string COMPID = "";
            string NO = "";
            List<AccPrepayAccountM_Batch> rs = new List<AccPrepayAccountM_Batch>();
            foreach (DataRow dr in dt.Rows)
            {
                COMPID = dr["PRAM_COMPID"].ToString();
                NO = dr["PRAM_NO"].ToString();

                #region AccPrepayAccountM
                AccPrepayAccountM_Batch item = new AccPrepayAccountM_Batch()
                {
                    PRAM_COMPID = dr["PRAM_COMPID"].ToString(),
                    PRAM_NO = dr["PRAM_NO"].ToString(),
                    PRAM_DATE = dr.FieldOrDefault<DateTime?>("PRAM_DATE"),
                    PRAM_D_ACNMID = dr["PRAM_D_ACNMID"].ToString(),
                    PRAM_D_DEPTID = dr["PRAM_D_DEPTID"].ToString(),
                    PRAM_C_ACNMID = dr["PRAM_C_ACNMID"].ToString(),
                    PRAM_C_DEPTID = dr["PRAM_C_DEPTID"].ToString(),
                    PRAM_NT_AMT = dr.FieldOrDefault<decimal>("PRAM_NT_AMT"),
                    PRAM_CNT = dr.FieldOrDefault<Int16>("PRAM_CNT"),
                    PRAM_STYM = dr["PRAM_STYM"].ToString(),
                    PRAM_ENYM = dr["PRAM_ENYM"].ToString(),
                    PRAM_REMARK = dr["PRAM_REMARK"].ToString(),
                    PRAM_RD_ACNMID = dr["PRAM_RD_ACNMID"].ToString(),
                    PRAM_RD_DEPTID = dr["PRAM_RD_DEPTID"].ToString(),
                    PRAM_RET_AMT = dr.FieldOrDefault<decimal>("PRAM_RET_AMT"),
                    PRAM_RET_DATE = dr.FieldOrDefault<DateTime?>("PRAM_RET_DATE"),
                    PRAM_RET_VOUNO = dr["PRAM_RET_VOUNO"].ToString(),
                    PRAM_A_USER_ID = dr["PRAM_A_USER_ID"].ToString(),
                    PRAM_A_USER_NM = dr["PRAM_A_USER_NM"].ToString(),
                    PRAM_A_DATE = dr.FieldOrDefault<DateTime?>("PRAM_A_DATE"),
                    PRAM_U_USER_ID = dr["PRAM_U_USER_ID"].ToString(),
                    PRAM_U_USER_NM = dr["PRAM_U_USER_NM"].ToString(),
                    PRAM_U_DATE = dr.FieldOrDefault<DateTime?>("PRAM_U_DATE"),

                    child = new List<AccPrepayAccountD_item>(),
                    share = new List<AccPrepayAccountShare>()
                };
                #endregion

                #region AccPrepayAccountD
                DataTable dtCode = accPrepayAccountD._QueryBatch(new AccPrepayAccountD_item() { PRAD_COMPID = COMPID, PRAD_NO = NO });
                if (dtCode.Rows.Count != 0)
                {
                    foreach (DataRow code in dtCode.Rows)
                    {
                        AccPrepayAccountD_item child = new AccPrepayAccountD_item()
                        {
                            PRAD_COMPID = code["PRAD_COMPID"].ToString(),
                            PRAD_NO = code["PRAD_NO"].ToString(),
                            PRAD_YEAR = code.FieldOrDefault<Int16>("PRAD_YEAR"),
                            PRAD_MONTH = code.FieldOrDefault<Int16>("PRAD_MONTH"),
                            PRAD_NT_AMT = code.FieldOrDefault<decimal>("PRAD_NT_AMT"),
                            PRAD_VOUNO = code["PRAD_VOUNO"].ToString(),
                            PRAD_TR_FLG = code["PRAD_TR_FLG"].ToString(),
                        };
                        item.child.Add(child);
                    }
                }
                #endregion

                #region AccPrepayAccountShare
                DataTable dtShare = accPrepayAccountShare._QueryBatch(new AccPrepayAccountShare() { PRAS_COMPID = COMPID, PRAS_NO = NO });
                if (dtCode.Rows.Count != 0)
                {
                    foreach (DataRow code in dtShare.Rows)
                    {
                        AccPrepayAccountShare share = new AccPrepayAccountShare()
                        {
                            PRAS_COMPID = code["PRAS_COMPID"].ToString(),
                            PRAS_NO = code["PRAS_NO"].ToString(),
                            PRAS_SEQ = code.FieldOrDefault<Int16>("PRAS_SEQ"),
                            PRAS_DEPTID = code["PRAS_DEPTID"].ToString(),
                            PRAS_NT_AMT = code.FieldOrDefault<decimal>("PRAS_NT_AMT"),
                        };
                        item.share.Add(share);
                    }
                }
                #endregion
                rs.Add(item);
            }

            return new rs_AccPrepayAccountM_Batch()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }
    }
}