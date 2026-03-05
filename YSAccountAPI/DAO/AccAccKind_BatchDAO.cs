using System;
using System.Collections.Generic;
using System.Data;
using Models;

namespace DAO
{
    public class AccAccKind_BatchDAO:BaseClass
    {
        AccAccKindDAO accAccKind = new AccAccKindDAO();
        AccAccCodeDAO accAccCode = new AccAccCodeDAO();

        public static rs_AccAccKindAdd rsAccAccKind(int code, string msg, string AccAccKind_id = "")
        {
            return new rs_AccAccKindAdd()
            {
                result = new rsItem() { retCode = code, retMsg = msg },
                data = new AccAccKindID() { ACKD_ID = AccAccKind_id }
            };
        }

        // 新增 + 修改
        public rs addUpdate(AccAccKind_Batch_ins data)
        {
            int ii = 0;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccAccKind_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.ACKD_ID))
                    {
                        DataTable dtM = accAccKind.haveAccAccKind(item.ACKD_ID);
                        if (dtM.Rows.Count==0)
                            ii += accAccKind._addAccAccKind(item, data.baseRequest.employeeNo, data.baseRequest.name, dao) ? 1 : 0;
                        else
                            ii += accAccKind._updateAccAccKind(item, data.baseRequest.employeeNo, data.baseRequest.name, dao) ? 1 : 0;

                        foreach (AccAccCode child in item.child)
                        {
                            if (!string.IsNullOrEmpty(child.ACCD_ID))
                            {
                                DataTable dtD = accAccCode.haveAccAccCode(child);
                                if (dtD.Rows.Count==0)
                                    ii += accAccCode._addAccAccCode(child, dao) ? 1 : 0;
                                else
                                    ii += accAccCode._updateAccAccCode(child, dao) ? 1 : 0;
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
        private rs addAccAccKind_Batch(AccAccKind_Batch_ins data)
        {
            int ii = 0;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccAccKind_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.ACKD_ID))
                    {
                        ii+= accAccKind._addAccAccKind(item, data.baseRequest.employeeNo, data.baseRequest.name, dao) ? 1 : 0;
                        foreach (AccAccCode child in item.child)
                        {
                            if (!string.IsNullOrEmpty(child.ACCD_ID))
                            {
                                ii+= accAccCode._addAccAccCode(child, dao) ? 1 : 0;
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
            catch(Exception e)
            {
                dao.DB.Rollback();
                return CommDAO.getRs(1, e.Message);
            }
        }
        // 停用
        private rs updateAccAccKind_Batch(AccAccKind_Batch_ins data)
        {
            int ii = 0;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccAccKind_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.ACKD_ID))
                    {
                        ii += accAccKind._updateAccAccKind(item, data.baseRequest.employeeNo, data.baseRequest.name, dao) ? 1 : 0;
                        foreach (AccAccCode child in item.child)
                        {
                            if (!string.IsNullOrEmpty(child.ACCD_ID))
                            {
                                ii += accAccCode._updateAccAccCode(child, dao) ? 1 : 0;
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

        public rs deleteAccAccKind_Batch(AccAccKind_Batch_ins data)
        {
            int ii = 0;
            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccAccKind_Batch item in data.data)
                {
                    if (!string.IsNullOrEmpty(item.ACKD_ID))
                    {
                        ii += accAccKind._deleteAccAccKind_ByItem(item.ACKD_ID) ? 1 : 0;
                        foreach (AccAccCode child in item.child)
                        {
                            if (!string.IsNullOrEmpty(child.ACCD_ID))
                            {
                                ii += accAccCode._deleteAccAccCode_ByItem(item.ACKD_ID, child.ACCD_ID) ? 1 : 0;
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

        public rs_AccAccKind_Batch queryAccAccKind_Batch(AccAccKindQuery data)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "SELECT * FROM ACC_ACC_KIND WHERE 1=1 ";

            List<string> slData = new List<string>();
            if (data.data.ACKD_ID.Count != 0)
            {
                string str_id = "";
                for (int ii = 0; ii < data.data.ACKD_ID.Count; ii++)
                {
                    slData.Add(data.data.ACKD_ID[ii]);
                    str_id = str_id + "'" + data.data.ACKD_ID[ii] + "',";
                }
                str_id = str_id.Substring(0, str_id.Length - 1);

                sql += $" AND ACKD_ID IN ({str_id})";
            }

            //C# lambda 

            sql += " ORDER BY ACKD_ID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            DataTable dtCode = accAccCode._queryAccAccCode_ByACCD_KIND(slData);

            List<AccAccKind_Batch> rs = new List<AccAccKind_Batch>();
            foreach (DataRow dr in dt.Rows)
            {
                AccAccKind_Batch item = new AccAccKind_Batch() {
                    ACKD_ID = dr["ACKD_ID"].ToString(),
                    ACKD_C_NAME = dr["ACKD_C_NAME"].ToString(),
                    ACKD_J_NAME = dr["ACKD_J_NAME"].ToString(),
                    ACKD_E_NAME = dr["ACKD_E_NAME"].ToString(),
                    child = new List<AccAccCode>()
                };

                if (dtCode.Rows.Count != 0)
                {
                    DataRow[] codes = dtCode.Select($"ACCD_KIND='{dr["ACKD_ID"].ToString()}'");
                    foreach (DataRow code in codes)
                    {
                        AccAccCode child = new AccAccCode()
                        {
                            ACCD_KIND = code["ACCD_KIND"].ToString(),
                            ACCD_ID = code["ACCD_ID"].ToString(),
                            ACCD_C_NAME = code["ACCD_C_NAME"].ToString(),
                            ACCD_J_NAME = code["ACCD_J_NAME"].ToString(),
                            ACCD_E_NAME = code["ACCD_E_NAME"].ToString()
                        };
                        item.child.Add(child);
                    }
                }
                rs.Add(item); ;
            }

            return new rs_AccAccKind_Batch() {
                result = new rsItem() { retCode=0, retMsg="成功"},
                data = rs,
                pagination = pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null
            };
        }
    }
}