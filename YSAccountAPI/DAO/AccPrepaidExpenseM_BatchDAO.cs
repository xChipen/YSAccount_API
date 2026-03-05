using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Data;
using Helpers;

namespace DAO
{
    public class AccPrepaidExpenseM_BatchDAO : BaseClass
    {
        AccPrepaidExpenseMDAO accPrepaidExpenseM = new AccPrepaidExpenseMDAO();
        AccPrepaidExpenseDDAO accPrepaidExpenseD = new AccPrepaidExpenseDDAO();


        // Master 新增 and 修改 and 刪除
        public bool audM(AccPrepaidExpenseM_Batch item, string employeeNo, string name, CommDAO dao)
        {
            switch (item.State)
            {
                case "A": return accPrepaidExpenseM._AddBatch(item, employeeNo, name, dao);
                case "U": return accPrepaidExpenseM._UpdateBatch(item, employeeNo, name, dao);
                case "D": return accPrepaidExpenseM._DeleteBatch(item, dao);
                case "": return true;
            }

            return false;
        }
        // Detail 新增 and 修改 and 刪除
        public bool audD(AccPrepaidExpenseD child, CommDAO dao)
        {
            switch (child.State)
            {
                case "A": return accPrepaidExpenseD._AddBatch(child, dao);
                case "U": return accPrepaidExpenseD._UpdateBatch(child, dao);
                case "D": return accPrepaidExpenseD._DeleteBatch(child, dao);
                case "": return true;
            }

            return false;
        }

        public rs addUpdate(AccPrepaidExpenseM_Batch_ins data)
        {
            string errMsg = "";
            string FK = "";
            bool bSave = true;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccPrepaidExpenseM_Batch item in data.data)
                {
                    FK = $"PEXM_COMPID:{item.PEXM_COMPID}-PEXM_NO:{item.PEXM_NO}";
                    if (!string.IsNullOrEmpty(item.PEXM_COMPID) && !string.IsNullOrEmpty(item.PEXM_NO))
                    {
                        #region AccPrepaidExpenseM
                        bSave = audM(item, data.baseRequest.employeeNo, data.baseRequest.name, dao);
                        if (bSave == false)
                        {
                            errMsg = "其他錯誤";
                            break;
                        }
                        #endregion

                        #region AccPrepaidExpenseD
                        foreach (AccPrepaidExpenseD child in item.child)
                        {
                            FK = $"PEXD_COMPID:{child.PEXD_COMPID}-PEXD_NO:{child.PEXD_NO}-PEXD_PAY_EMPLID:{child.PEXD_PAY_EMPLID}";
                            if (!string.IsNullOrEmpty(child.PEXD_COMPID) && !string.IsNullOrEmpty(child.PEXD_NO))
                            {
                                bSave = audD(child, dao);
                                if (bSave == false)
                                {
                                    errMsg = "其他錯誤";
                                    break;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
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

        // 會計傳票審核登錄 - 更新狀態
        //public static rs update(AccPrepaidExpenseM_Batch_ins data)
        //{
        //    string errMsg = "";
        //    string FK = "";
        //    bool bSave = true;
        //    CommDAO dao = new CommDAO();
        //    dao.DB.BeginTransaction();
        //    try
        //    {
        //        foreach (AccPrepaidExpenseM_Batch item in data.data)
        //        {
        //            FK = $"PEXM_COMPID:{item.PEXM_COMPID}-PEXM_NO:{item.PEXM_NO}";
        //            if (!string.IsNullOrEmpty(item.PEXM_COMPID) && !string.IsNullOrEmpty(item.PEXM_NO))
        //            {
        //                bSave = AccPrepaidExpenseMDAO.Update(item.PEXM_COMPID, item.PEXM_NO, item.VOMM_APPROVE_FLG, dao);
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

        // 刪除
        public rs delete(AccPrepaidExpenseM_Batch_ins data)
        {
            bool bSave = true;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccPrepaidExpenseM_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.PEXM_COMPID) && !string.IsNullOrEmpty(item.PEXM_NO))
                    {
                        bSave = accPrepaidExpenseM._DeleteBatch(item, dao);
                        if (!bSave) break;
                        bSave = accPrepaidExpenseD.DeleteAll(item.PEXM_COMPID, item.PEXM_NO);
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
        public rs_AccPrepaidExpenseM_Batch queryAccPrepaidExpenseM_Batch(AccPrepaidExpenseM_Batch_qry data)
        {
            int TotalCount = 0;
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region SQL
            string sql = "SELECT * FROM ACC_PREPAID_EXPENSE_M WHERE 1=1 ";
            if (string.IsNullOrEmpty(data.data.PEXM_COMPID))
            {
                return new rs_AccPrepaidExpenseM_Batch()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未傳入公司代碼" }
                };
            }
            else
            {
                sql += $" AND PEXM_COMPID='{data.data.PEXM_COMPID}'";
            }
            if (!string.IsNullOrEmpty(data.data.PEXM_NO))
                sql += $" AND PEXM_NO Like '{data.data.PEXM_NO}%'";

            sql += " ORDER BY PEXM_COMPID, PEXM_NO ";
            #endregion
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccPrepaidExpenseM_Batch> rs = new List<AccPrepaidExpenseM_Batch>();
            foreach (DataRow dr in dt.Rows)
            {
                #region ACC_PREPAID_EXPENSE_M
                AccPrepaidExpenseM_Batch item = new AccPrepaidExpenseM_Batch()
                {

                    PEXM_COMPID = dr["PEXM_COMPID"].ToString(),
                    PEXM_NO = dr["PEXM_NO"].ToString(),
                    PEXM_TYPE = dr["PEXM_TYPE"].ToString(),
                    PEXM_DATE = dr.FieldOrDefault<DateTime?>("PEXM_DATE"),
                    PEXM_APPLY_EMPLID = dr["PEXM_APPLY_EMPLID"].ToString(),
                    PEXM_APPLY_NAME = dr["PEXM_APPLY_NAME"].ToString(),
                    PEXM_DEPTID = dr["PEXM_DEPTID"].ToString(),
                    PEXM_CURRID = dr["PEXM_CURRID"].ToString(),
                    PEXM_NT_AMT = dr.FieldOrDefault<decimal>("PEXM_NT_AMT"),
                    PEXM_MEMO = dr["PEXM_MEMO"].ToString(),
                    PEXM_PAY_DATE = dr.FieldOrDefault<DateTime?>("PEXM_PAY_DATE"),
                    PEXM_PAY_TYPE = dr["PEXM_PAY_TYPE"].ToString(),
                    PEXM_VOUNO = dr["PEXM_VOUNO"].ToString(),
                    PEXM_A_USER_ID = dr["PEXM_A_USER_ID"].ToString(),
                    PEXM_A_USER_NM = dr["PEXM_A_USER_NM"].ToString(),
                    PEXM_A_DATE = dr.FieldOrDefault<DateTime?>("PEXM_A_DATE"),

                    child = new List<AccPrepaidExpenseD>()
                };
                #endregion

                #region ACC_VOUMST_D
                DataTable dtCode = accPrepaidExpenseD._QueryBatch(new AccPrepaidExpenseD() { PEXD_COMPID = dr["PEXM_COMPID"].ToString(), PEXD_NO = dr["PEXM_NO"].ToString() });
                if (dtCode.Rows.Count != 0)
                {
                    foreach (DataRow code in dtCode.Rows)
                    {
                        AccPrepaidExpenseD child = new AccPrepaidExpenseD()
                        {

                            PEXD_COMPID = code["PEXD_COMPID"].ToString(),
                            PEXD_NO = code["PEXD_NO"].ToString(),
                            PEXD_PAY_EMPLID = code["PEXD_PAY_EMPLID"].ToString(),
                            PEXD_CURRID = code["PEXD_CURRID"].ToString(),
                            PEXD_AMT = code.FieldOrDefault<decimal>("PEXD_AMT"),
                            PEXD_EXRATE = code.FieldOrDefault<decimal>("PEXD_EXRATE"),
                            PEXD_NT_AMT = code.FieldOrDefault<decimal>("PEXD_NT_AMT")
                        };
                        item.child.Add(child);
                    }
                }
                rs.Add(item);
                #endregion
            }

            return new rs_AccPrepaidExpenseM_Batch()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        // 查詢2
//        public static rs_AccPrepaidExpenseM_Batch queryAccVoumstM_Batch2(AccPrepaidExpenseM_Batch_qry2 data)
//        {
//            int TotalCount = 0;
//            int pageNumber = 0;
//            int pageSize = 0;
//            int pageNumbers = 0;
//            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

//            #region SQL
//            string sql = "SELECT * FROM ACC_VOUMST_M WHERE 1=1 ";
//            if (string.IsNullOrEmpty(data.data.VOMM_COMPID))
//            {
//                return new rs_AccVoumstM_Batch()
//                {
//                    result = new rsItem() { retCode = 1, retMsg = "未傳入公司代碼" }
//                };
//            }
//            else
//            {
//                sql += $" AND VOMM_COMPID='{data.data.VOMM_COMPID}'";
//            }
//            //
//            if (data.data.BDate != null && data.data.EDate != null)
//            {
//                sql += $@" AND VOMM_DATE >= '{string.Format("{0:yyyy/MM/dd}", data.data.BDate)}' 
//AND VOMM_DATE <= '{string.Format("{0:yyyy/MM/dd}", data.data.EDate)}' ";
//            }
//            //
//            if (!string.IsNullOrEmpty(data.data.BNo) && !string.IsNullOrEmpty(data.data.ENo))
//            {
//                sql += $@" AND VOMM_NO>='{data.data.BNo}' AND VOMM_NO<='{data.data.ENo}'";
//            }
//            //
//            if (!string.IsNullOrEmpty(data.data.UserNo))
//                sql += $@" AND VOMM_A_USER_ID='{data.data.UserNo}'";
//            //
//            if (!string.IsNullOrEmpty(data.data.VOMM_APPROVE_FLG))
//            {
//                if (data.data.VOMM_APPROVE_FLG == "Y" || data.data.VOMM_APPROVE_FLG == "N")
//                {
//                    sql += $@" AND VOMM_APPROVE_FLG='{data.data.VOMM_APPROVE_FLG}'";
//                }
//                else
//                {
//                    return new rs_AccVoumstM_Batch { result = new rsItem { retCode = 1, retMsg = "VOMM_APPROVE_FLG 請填入 Y or N" } };
//                }
//            }
//            sql += " AND VOMM_VALID ='Y'";
//            sql += " ORDER BY VOMM_COMPID, VOMM_NO ";
//            #endregion
//            if (pageNumber != 0)
//                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

//            DataTable dt = CommDAO.RunSQL(sql);
//            TotalCount = dt.Rows.Count != 0 ? (int)dt.Rows[0]["TotalCount"] : 0;

//            List<AccVoumstM_Batch> rs = new List<AccVoumstM_Batch>();
//            foreach (DataRow dr in dt.Rows)
//            {
//                #region ACC_VOUMST_M
//                AccVoumstM_Batch item = new AccVoumstM_Batch()
//                {
//                    VOMM_COMPID = dr["VOMM_COMPID"].ToString(),
//                    VOMM_NO = dr["VOMM_NO"].ToString(),
//                    VOMM_DATE = dr.FieldOrDefault<DateTime>("VOMM_DATE"),
//                    VOMM_VALID = dr["VOMM_VALID"].ToString(),
//                    VOMM_PRINT_FLG = dr["VOMM_PRINT_FLG"].ToString(),
//                    VOMM_VERNO = dr.FieldOrDefault<Int16>("VOMM_VERNO"),
//                    VOMM_GENNO = dr.FieldOrDefault<Int32>("VOMM_GENNO"),
//                    VOMM_APPROVE_FLG = dr["VOMM_APPROVE_FLG"].ToString(),
//                    VOMM_SOURCE = dr["VOMM_SOURCE"].ToString(),
//                    VOMM_BATCHNO = dr["VOMM_BATCHNO"].ToString(),
//                    VOMM_MEMO = dr["VOMM_MEMO"].ToString(),

//                    child = new List<AccVoumstD>(),
//                    tax = new List<AccVoumstTax>()
//                };
//                #endregion

//                #region ACC_VOUMST_D
//                DataTable dtCode = AccVoumstDDAO._QueryBatch(new AccVoumstD() { VOMD_COMPID = dr["VOMM_COMPID"].ToString(), VOMD_NO = dr["VOMM_NO"].ToString() });
//                if (dtCode.Rows.Count != 0)
//                {
//                    foreach (DataRow code in dtCode.Rows)
//                    {
//                        AccVoumstD child = new AccVoumstD()
//                        {
//                            VOMD_COMPID = code["VOMD_COMPID"].ToString(),
//                            VOMD_NO = code["VOMD_NO"].ToString(),
//                            VOMD_SEQ = code.FieldOrDefault<Int16>("VOMD_SEQ"),
//                            VOMD_ACCD = code["VOMD_ACCD"].ToString(),
//                            VOMD_D_NT_AMT = code.FieldOrDefault<decimal>("VOMD_D_NT_AMT"),
//                            VOMD_C_NT_AMT = code.FieldOrDefault<decimal>("VOMD_C_NT_AMT"),
//                            VOMD_MEMO = code["VOMD_MEMO"].ToString(),
//                            VOMD_CURR = code["VOMD_CURR"].ToString(),
//                            VOMD_AMT = code.FieldOrDefault<decimal>("VOMD_AMT"),
//                            VOMD_DEPTID = code["VOMD_DEPTID"].ToString(),
//                            VOMD_TRANID = code["VOMD_TRANID"].ToString(),
//                            VOMD_INVNO = code["VOMD_INVNO"].ToString(),
//                            VOMD_DUEFLG = code["VOMD_DUEFLG"].ToString(),
//                            VOMD_DUEDATE = code.FieldOrDefault<DateTime>("VOMD_DUEDATE"),
//                            VOMD_DUE_BANK = code["VOMD_DUE_BANK"].ToString(),
//                            VOMD_ACNO = code["VOMD_ACNO"].ToString(),
//                            VOMD_SAV_BANK = code["VOMD_SAV_BANK"].ToString(),
//                            VOMD_CVOUNO = code["VOMD_CVOUNO"].ToString(),
//                            VOMD_CSEQ = code.FieldOrDefault<Int16>("VOMD_CSEQ"),
//                            VOMD_CNT = code.FieldOrDefault<Int16>("VOMD_CNT"),
//                            VOMD_STYM = code["VOMD_STYM"].ToString(),
//                            VOMD_ENYM = code["VOMD_ENYM"].ToString(),
//                            VOMD_D_ACCD = code["VOMD_D_ACCD"].ToString(),
//                            VOMD_D_DEPTID = code["VOMD_D_DEPTID"].ToString(),
//                            VOMD_D_INVNO = code["VOMD_D_INVNO"].ToString()
//                        };
//                        item.child.Add(child);
//                    }
//                }
//                #endregion

//                #region ACC_VOUMST_TAX
//                DataTable dtTax = AccVoumstTaxDAO._QueryBatch(new AccVoumstTax() { VOMT_COMPID = dr["VOMM_COMPID"].ToString(), VOMT_NO = dr["VOMM_NO"].ToString() });
//                if (dtTax.Rows.Count != 0)
//                {
//                    foreach (DataRow code in dtTax.Rows)
//                    {
//                        AccVoumstTax child = new AccVoumstTax()
//                        {

//                            VOMT_COMPID = code["VOMT_COMPID"].ToString(),
//                            VOMT_NO = code["VOMT_NO"].ToString(),
//                            VOMT_SEQ = code.FieldOrDefault<Int16>("VOMT_SEQ"),
//                            VOMT_FORMAT = code["VOMT_FORMAT"].ToString(),
//                            VOMT_DATA_YM = code["VOMT_DATA_YM"].ToString(),
//                            VOMT_INV_DATE = code.FieldOrDefault<DateTime>("VOMT_INV_DATE"),
//                            VOMT_INVNO = code["VOMT_INVNO"].ToString(),
//                            VOMT_S_UNNO = code["VOMT_S_UNNO"].ToString(),
//                            VOMT_P_UNNO = code["VOMT_P_UNNO"].ToString(),
//                            VOMT_TAXCD = code["VOMT_TAXCD"].ToString(),
//                            VOMT_AMT = code.FieldOrDefault<int>("VOMT_AMT"),
//                            VOMT_TAX = code.FieldOrDefault<int>("VOMT_TAX"),
//                            VOMT_PCODE = code["VOMT_PCODE"].ToString()
//                        };
//                        item.tax.Add(child);
//                    }
//                }
//                #endregion

//                rs.Add(item);
//            }

//            return new rs_AccVoumstM_Batch()
//            {
//                result = new rsItem() { retCode = 0, retMsg = "成功" },
//                data = rs,
//                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
//            };
//        }


    }
}