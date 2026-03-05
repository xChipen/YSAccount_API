using System;
using System.Collections.Generic;
using Models;
using System.Data;
using Helpers;

namespace DAO
{
    public class AccPayM_BatchDAO:BaseClass
    {
        AccPayMDAO accPayM = new AccPayMDAO();
        AccPayDDAO accPayD = new AccPayDDAO();

        // Master 新增 and 修改 and 刪除
        public bool audM(AccPayM_Batch item, string employeeNo, string name, CommDAO dao)
        {
            switch (item.State)
            {
                case "A": return accPayM._AddBatch(item, employeeNo, name, dao);
                case "U": return accPayM._UpdateBatch(item, employeeNo, name, dao);
                case "D": return accPayM._DeleteBatch(item, dao);
                case "": return true;
            }

            return false;
        }
        // Detail 新增 and 修改 and 刪除
        public bool audD(AccPayD child, CommDAO dao)
        {
            switch (child.State)
            {
                case "A": return accPayD._AddBatch(child, dao);
                case "U": return accPayD._UpdateBatch(child, dao);
                case "D": return accPayD._DeleteBatch(child, dao);
                case "": return true;
            }

            return false;
        }

        public rs addUpdate(AccPayM_Batch_ins data)
        {
            string errMsg = "";
            string FK = "";
            bool bSave = true;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccPayM_Batch item in data.data)
                {
                    FK = $"PAYM_COMPID:{item.PAYM_COMPID}-PAYM_NO:{item.PAYM_NO}";
                    if (!string.IsNullOrEmpty(item.PAYM_COMPID) && !string.IsNullOrEmpty(item.PAYM_NO))
                    {
                        #region AccPAYM
                        bSave = audM(item, data.baseRequest.employeeNo, data.baseRequest.name, dao);
                        if (bSave == false)
                        {
                            errMsg = "其他錯誤";
                            break;
                        }
                        #endregion

                        #region AccPAYD
                        foreach (AccPayD child in item.child)
                        {
                            FK = $"PAYD_COMPID:{child.PAYD_COMPID}-PAYD_NO:{child.PAYD_NO}-PAYD_SEQ:{child.PAYD_SEQ}";
                            if (!string.IsNullOrEmpty(child.PAYD_COMPID) && !string.IsNullOrEmpty(child.PAYD_NO))
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
        //public static rs update(AccPayM_Batch_ins data)
        //{
        //    string errMsg = "";
        //    string FK = "";
        //    bool bSave = true;
        //    CommDAO dao = new CommDAO();
        //    dao.DB.BeginTransaction();
        //    try
        //    {
        //        foreach (AccPayM_Batch item in data.data)
        //        {
        //            FK = $"PAYM_COMPID:{item.PAYM_COMPID}-PAYM_NO:{item.PAYM_NO}";
        //            if (!string.IsNullOrEmpty(item.PAYM_COMPID) && !string.IsNullOrEmpty(item.PAYM_NO))
        //            {
        //                bSave = AccPayMDAO.Update(item.PAYM_COMPID, item.PAYM_NO, item.PAYM_APPROVE_FLG, dao);
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
        public rs delete(AccPayM_Batch_ins data)
        {
            bool bSave = true;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccPayM_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.PAYM_COMPID) && !string.IsNullOrEmpty(item.PAYM_NO))
                    {
                        bSave = accPayM._DeleteBatch(item, dao);
                        if (!bSave) break;
                        bSave = accPayD.DeleteAll(item.PAYM_COMPID, item.PAYM_NO);
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
        public rs_AccPayM_Batch queryAccPayM_Batch(AccPayM_Batch_qry data)
        {
            int TotalCount = 0;
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region SQL
            string sql = "SELECT * FROM ACC_PAY_M WHERE 1=1 ";
            if (string.IsNullOrEmpty(data.data.PAYM_COMPID))
            {
                return new rs_AccPayM_Batch()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未傳入公司代碼" }
                };
            }
            else
            {
                sql += $" AND PAYM_COMPID='{data.data.PAYM_COMPID}'";
            }
            if (!string.IsNullOrEmpty(data.data.PAYM_NO))
                sql += $" AND PAYM_NO Like '{data.data.PAYM_NO}%'";

            sql += " ORDER BY PAYM_COMPID, PAYM_NO ";
            #endregion
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccPayM_Batch> rs = new List<AccPayM_Batch>();
            foreach (DataRow dr in dt.Rows)
            {
                #region ACC_PAY_M
                AccPayM_Batch item = new AccPayM_Batch()
                {
                    PAYM_COMPID = dr["PAYM_COMPID"].ToString(),
                    PAYM_NO = dr["PAYM_NO"].ToString(),
                    PAYM_DATE = dr.FieldOrDefault<DateTime?>("PAYM_DATE"),
                    PAYM_VENDID = dr["PAYM_VENDID"].ToString(),
                    PAYM_PAY_KIND = dr["PAYM_PAY_KIND"].ToString(),
                    PAYM_PAY_ACCD = dr["PAYM_PAY_ACCD"].ToString(),
                    PAYM_CURRID = dr["PAYM_CURRID"].ToString(),
                    PAYM_EXRATE = dr.FieldOrDefault<decimal>("PAYM_EXRATE"),
                    PAYM_PAY_NT_AMT = dr.FieldOrDefault<decimal>("PAYM_PAY_NT_AMT"),
                    PAYM_PAY_FOR_AMT = dr.FieldOrDefault<decimal>("PAYM_PAY_FOR_AMT"),
                    PAYM_FEE = dr.FieldOrDefault<decimal>("PAYM_FEE"),
                    PAYM_BANKID = dr["PAYM_BANKID"].ToString(),
                    PAYM_ACNO = dr["PAYM_ACNO"].ToString(),
                    PAYM_CHKNO = dr["PAYM_CHKNO"].ToString(),
                    PAYM_DUE_DATE = dr.FieldOrDefault<DateTime?>("PAYM_DUE_DATE"),
                    PAYM_VALID = dr["PAYM_VALID"].ToString(),
                    PAYM_VOUNO = dr["PAYM_VOUNO"].ToString(),

                    child = new List<AccPayD>()
                };
                #endregion

                #region ACC_VOUMST_D
                DataTable dtCode = accPayD._QueryBatch(new AccPayD() { PAYD_COMPID = dr["PAYM_COMPID"].ToString(), PAYD_NO = dr["PAYM_NO"].ToString() });
                if (dtCode.Rows.Count != 0)
                {
                    foreach (DataRow code in dtCode.Rows)
                    {
                        AccPayD child = new AccPayD()
                        {
                            PAYD_COMPID = code["PAYD_COMPID"].ToString(),
                            PAYD_NO = code["PAYD_NO"].ToString(),
                            PAYD_SEQ = code.FieldOrDefault<Int16>("PAYD_SEQ"),
                            PAYD_ACCD = code["PAYD_ACCD"].ToString(),
                            PAYD_INVNO = code["PAYD_INVNO"].ToString(),
                            PAYD_INV_DATE = code.FieldOrDefault<DateTime?>("PAYD_INV_DATE"),
                            PAYD_CURRID = code["PAYD_CURRID"].ToString(),
                            PAYD_EXRATE = code.FieldOrDefault<decimal>("PAYD_EXRATE"),
                            PAYD_NT_BAL = code.FieldOrDefault<decimal>("PAYD_NT_BAL"),
                            PAYD_FOR_BAL = code.FieldOrDefault<decimal>("PAYD_FOR_BAL"),
                            PAYD_NT_AMT = code.FieldOrDefault<decimal>("PAYD_NT_AMT"),
                            PAYD_FOR_AMT = code.FieldOrDefault<decimal>("PAYD_FOR_AMT")
                        };
                        item.child.Add(child);
                    }
                }
                rs.Add(item);
                #endregion
            }

            return new rs_AccPayM_Batch()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        // 查詢2
        public rs_AccPayM_Batch queryAccPayM_Batch2(AccPayM_Batch_qry2 data)
        {
            int TotalCount = 0;
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region SQL
            string sql = "SELECT * FROM ACC_PAY_M WHERE 1=1 ";
            if (string.IsNullOrEmpty(data.data.PAYM_COMPID))
            {
                return new rs_AccPayM_Batch()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未傳入公司代碼" }
                };
            }
            else
            {
                sql += $" AND PAYM_COMPID='{data.data.PAYM_COMPID}'";
            }
            //
            if (data.data.BDate != null && data.data.EDate != null)
            {
                sql += $@" AND PAYM_DATE >= '{string.Format("{0:yyyy/MM/dd}", data.data.BDate)}' 
AND PAYM_DATE <= '{string.Format("{0:yyyy/MM/dd}", data.data.EDate)}' ";
            }
            //
            if (!string.IsNullOrEmpty(data.data.BNo) && !string.IsNullOrEmpty(data.data.ENo))
            {
                sql += $@" AND PAYM_NO>='{data.data.BNo}' AND PAYM_NO<='{data.data.ENo}'";
            }
            //
            if (!string.IsNullOrEmpty(data.data.UserNo))
                sql += $@" AND PAYM_A_USER_ID='{data.data.UserNo}'";
            //
            //if (!string.IsNullOrEmpty(data.data.VOMM_APPROVE_FLG))
            //{
            //    if (data.data.VOMM_APPROVE_FLG == "Y" || data.data.VOMM_APPROVE_FLG == "N")
            //    {
            //        sql += $@" AND VOMM_APPROVE_FLG='{data.data.VOMM_APPROVE_FLG}'";
            //    }
            //    else
            //    {
            //        return new rs_AccVoumstM_Batch { result = new rsItem { retCode = 1, retMsg = "VOMM_APPROVE_FLG 請填入 Y or N" } };
            //    }
            //}
            //sql += " AND VOMM_VALID ='Y'";
            sql += " ORDER BY PAYM_COMPID, PAYM_NO ";
            #endregion
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccPayM_Batch> rs = new List<AccPayM_Batch>();
            foreach (DataRow dr in dt.Rows)
            {
                #region ACC_PAY_M
                AccPayM_Batch item = new AccPayM_Batch()
                {
                    PAYM_COMPID = dr["PAYM_COMPID"].ToString(),
                    PAYM_NO = dr["PAYM_NO"].ToString(),
                    PAYM_DATE = dr.FieldOrDefault<DateTime>("PAYM_DATE"),
                    PAYM_VENDID = dr["PAYM_VENDID"].ToString(),
                    PAYM_PAY_KIND = dr["PAYM_PAY_KIND"].ToString(),
                    PAYM_PAY_ACCD = dr["PAYM_PAY_ACCD"].ToString(),
                    PAYM_CURRID = dr["PAYM_CURRID"].ToString(),
                    PAYM_EXRATE = dr.FieldOrDefault<decimal>("PAYM_EXRATE"),
                    PAYM_PAY_NT_AMT = dr.FieldOrDefault<decimal>("PAYM_PAY_NT_AMT"),
                    PAYM_PAY_FOR_AMT = dr.FieldOrDefault<decimal>("PAYM_PAY_FOR_AMT"),
                    PAYM_FEE = dr.FieldOrDefault<decimal>("PAYM_FEE"),
                    PAYM_BANKID = dr["PAYM_BANKID"].ToString(),
                    PAYM_ACNO = dr["PAYM_ACNO"].ToString(),
                    PAYM_CHKNO = dr["PAYM_CHKNO"].ToString(),
                    PAYM_DUE_DATE = dr.FieldOrDefault<DateTime>("PAYM_DUE_DATE"),
                    PAYM_VALID = dr["PAYM_VALID"].ToString(),
                    PAYM_VOUNO = dr["PAYM_VOUNO"].ToString(),

                    child = new List<AccPayD>()
                };
                #endregion

                #region ACC_PAY_D
                DataTable dtCode = accPayD._QueryBatch(new AccPayD() { PAYD_COMPID = dr["PAYM_COMPID"].ToString(), PAYD_NO = dr["PAYM_NO"].ToString() });
                if (dtCode.Rows.Count != 0)
                {
                    foreach (DataRow code in dtCode.Rows)
                    {
                        AccPayD child = new AccPayD()
                        {
                            PAYD_COMPID = code["PAYD_COMPID"].ToString(),
                            PAYD_NO = code["PAYD_NO"].ToString(),
                            PAYD_SEQ = code.FieldOrDefault<Int16>("PAYD_SEQ"),
                            PAYD_ACCD = code["PAYD_ACCD"].ToString(),
                            PAYD_INVNO = code["PAYD_INVNO"].ToString(),
                            PAYD_INV_DATE = code.FieldOrDefault<DateTime>("PAYD_INV_DATE"),
                            PAYD_CURRID = code["PAYD_CURRID"].ToString(),
                            PAYD_EXRATE = code.FieldOrDefault<decimal>("PAYD_EXRATE"),
                            PAYD_NT_BAL = code.FieldOrDefault<decimal>("PAYD_NT_BAL"),
                            PAYD_FOR_BAL = code.FieldOrDefault<decimal>("PAYD_FOR_BAL"),
                            PAYD_NT_AMT = code.FieldOrDefault<decimal>("PAYD_NT_AMT"),
                            PAYD_FOR_AMT = code.FieldOrDefault<decimal>("PAYD_FOR_AMT")
                        };
                        item.child.Add(child);
                    }
                }
                #endregion

                rs.Add(item);
            }

            return new rs_AccPayM_Batch()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        public rsAccPayM_qry2_rs Query2(AccPayM_qry2 data)
        {

            if (data.PAYM_DATE == null || string.IsNullOrEmpty(data.PYM_PAY_KIND) || string.IsNullOrEmpty(data.PAYM_VALID))
            {
                return new rsAccPayM_qry2_rs { result = CommDAO.getRsItem1() };
            }

            string sql = $@"SELECT PAYM_VENDID,TRAN_NAME,
PAYM_BANKID,PAYM_ACNO,
PAYM_PAY_NT_AMT
FROM 
ACC_PAY_M,vw_TRAIN
WHERE 
PAYM_COMPID = TRAN_COMPID
AND PAYM_DATE =@PAYM_DATE
AND PYM_PAY_KIND =@PYM_PAY_KIND
AND PAYM_VALID=@PAYM_VALID
";
            sql += CommDAO.sql_ep(data.PAYM_VOUNO, "PAYM_VOUNO");

            DataTable dt = comm.DB.RunSQL(sql, new object[]
                { data.PAYM_DATE , data.PYM_PAY_KIND , data.PAYM_VALID });
            List<AccPayM_qry2_rs> rs = dt.ToList<AccPayM_qry2_rs>();

            return new rsAccPayM_qry2_rs { result = CommDAO.getRsItem(), data = rs };
        }

        public rsAccPayM_qry3_rs Query3(AccPayM_qry3 data)
        {

            if (data.PAYM_DATE == null || string.IsNullOrEmpty(data.PAYM_VENDID) )
            {
                return new rsAccPayM_qry3_rs { result = CommDAO.getRsItem1("參數錯誤") };
            }

            string sql = $@"SELECT PAYD_INV_DATE,PAYD_INVNO,
PAYD_NT_AMT
FROM ACC_PAY_M,ACC_PAY_D
WHERE PAYM_VENDID =@PAYM_VENDID
AND PAYM_DATE =@PAYM_DATE
AND PAYM_COMPID = PAYD_COMPID
AND PAYM_NO = PAYD_NO
";
            DataTable dt = comm.DB.RunSQL(sql, new object[]
                { data.PAYM_VENDID, data.PAYM_DATE });

            List<AccPayM_qry3_rs> rs = dt.ToList<AccPayM_qry3_rs>();

            return new rsAccPayM_qry3_rs { result = CommDAO.getRsItem(), data = rs };
        }
    }
}