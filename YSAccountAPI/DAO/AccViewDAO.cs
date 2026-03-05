using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Data;
using Helpers;

namespace DAO
{
    public class AccViewDAO: BaseClass
    {
        private string getSQL_ACCD(ACCD_ins data, string tableName= "vw_ACCD")
        {
            string sql = $@"SELECT *, BACT_FLG,BACT_DEPT_FLG,BACT_TRANID_FLG,BACT_INVNO_FLG From {tableName} 
LEFT JOIN ACC_BALANCE_CONTROL ON subString(ACNM_ID,1,4) = BACT_ACNMID

Where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.ACNM_COMPID))
            {
                sql += $" AND ACNM_COMPID='{data.data.ACNM_COMPID}'";
            }

            if (!string.IsNullOrEmpty(data.data.ACNM_ID))
            {
                sql += $" AND ACNM_ID='{data.data.ACNM_ID}'";
            }

            if (!string.IsNullOrEmpty(data.data.ACNM_C_NAME))
            {
                sql += $" AND ACNM_C_NAME LIKE '%{data.data.ACNM_C_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_E_NAME))
            {
                sql += $" AND ACNM_E_NAME LIKE '%{data.data.ACNM_E_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_J_NAME))
            {
                sql += $" AND ACNM_J_NAME LIKE '%{data.data.ACNM_J_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_SAVE_FLG))
                sql += $" AND ACNM_SAVE_FLG='{data.data.ACNM_SAVE_FLG}'";

            sql += " ORDER BY ACNM_ID ";

            return sql;
        }
        private string getSQL_ACCD_like(ACCD_ins data, string tableName = "vw_ACCD")
        {
            //string sql = $"SELECT * From {tableName} Where 1=1 ";

            string sql = $@"SELECT *, BACT_FLG,BACT_DEPT_FLG,BACT_TRANID_FLG,BACT_INVNO_FLG From {tableName} 
LEFT JOIN ACC_BALANCE_CONTROL ON subString(ACNM_ID,1,4) = BACT_ACNMID
Where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.ACNM_COMPID))
            {
                sql += $" AND ACNM_COMPID='{data.data.ACNM_COMPID}'";
            }

            if (!string.IsNullOrEmpty(data.data.ACNM_ID))
            {
                sql += $" AND ACNM_ID LIKE '{data.data.ACNM_ID}%'";
            }

            if (!string.IsNullOrEmpty(data.data.ACNM_C_NAME))
            {
                sql += $" AND ACNM_C_NAME LIKE '%{data.data.ACNM_C_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_E_NAME))
            {
                sql += $" AND ACNM_E_NAME LIKE '%{data.data.ACNM_E_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_J_NAME))
            {
                sql += $" AND ACNM_J_NAME LIKE '%{data.data.ACNM_J_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.ACNM_SAVE_FLG))
                sql += $" AND ACNM_SAVE_FLG='{data.data.ACNM_SAVE_FLG}'";

            sql += " ORDER BY ACNM_ID ";

            return sql;
        }

        // vw_ACCD
        public rs_ACCD queryACCD(ACCD_ins data, int kind=1, string tableName = "vw_ACCD")
        {
            int pageNumber=0;
            int pageSize=0;
            int pageNumbers=0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "";
            if (kind == 1)
                sql = getSQL_ACCD(data, tableName);
            else if (kind == 2)
                sql = getSQL_ACCD_like(data, tableName);

            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<ACCD> rs = dt.ToList<ACCD>();
            return new rs_ACCD()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        // 20260210
        public rs_ACCD queryACCD2(ACCD_ins data, string tableName = "VW_ACCD1")
        {

            string sql = $@"Select * from {tableName} where 1=1 AND ACNM_ID LIKE '6%'";

            if (!string.IsNullOrEmpty(data.data.ACNM_COMPID))
            {
                sql += $" AND ACNM_COMPID='{data.data.ACNM_COMPID}'";
            }

            DataTable dt = comm.DB.RunSQL(sql);

            List<ACCD> rs = dt.ToList<ACCD>();

            return new rs_ACCD()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs
            };
        }


        public rs_ACCD ACCD_ACE010M(ACCD data)
        {
            string sql = $@"SELECT * from vw_ACCD
WHERE 1=1";
            sql += CommDAO.sql_ep(data.ACNM_COMPID, "ACNM_COMPID");
            sql += CommDAO.sql_ep(data.ACNM_ID, "ACNM_ID");
            sql += CommDAO.sql_ep("Y", "ACNM_PK_FLG");
            sql += CommDAO.sql_ep("1", "LEFT(ACNM_ID,1)");

            DataTable dt = comm.DB.RunSQL(sql);

            return new rs_ACCD {
                result = CommDAO.getRsItem(),
                data = dt.ToList<ACCD>()
            };
        }


        // vw_TRAIN
        public string getTRAIN_sql(TRAIN_ins data)
        {
            string sql = "SELECT * From vw_TRAIN Where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.TRAN_COMPID))
            {
                sql += $" AND TRAN_COMPID='{data.data.TRAN_COMPID}'";
            }
            #region SQL Option
            if (!string.IsNullOrEmpty(data.data.TRAN_KIND))
            {
                sql += $" AND TRAN_KIND ='{data.data.TRAN_KIND}'";
            }

            if (data.data.TRAN_ID != null)
            {
                if (data.data.TRAN_ID.Count != 0)
                {
                    string subStr = "";
                    foreach (string item in data.data.TRAN_ID)
                    {
                        subStr += $" TRAN_ID ='{item}' OR ";
                    }
                    subStr = subStr.Substring(0, subStr.Length - 3);
                    sql += $" AND ({subStr})";
                }
            }

            if (!string.IsNullOrEmpty(data.data.TRAN_NAME))
            {
                sql += $" AND TRAN_NAME LIKE '%{data.data.TRAN_NAME}%'";
            }
            if (!string.IsNullOrEmpty(data.data.TRAN_ABBR))
            {
                sql += $" AND TRAN_ABBR LIKE '%{data.data.TRAN_ABBR}%'";
            }
            #endregion

            sql += " ORDER BY TRAN_COMPID ";

            return sql;
        }

        public rs_TRAIN query_TRAIN(TRAIN_ins data, string sql)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<rsTRAIN_item> rs = dt.ToList<rsTRAIN_item>();
            return new rs_TRAIN()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        public rs_TRAIN queryTRAIN(TRAIN_ins data)
        {
            string sql = getTRAIN_sql(data);
            return query_TRAIN(data, sql);
        }

        #region TRAIN ACE040B
        public string getTRAIN_sql_ACE040B(TRAIN_ins data)
        {
            string sql = $@"Select b.* From ACC_PA_CHECK a 
join vw_TRAIN b on a.PACK_VENDID = b.TRAN_ID
and a.PACK_COMPID = b.TRAN_COMPID";

            sql += CommDAO.sql_ep(data.data.TRAN_COMPID, "TRAN_COMPID");
            sql += CommDAO.sql_ep(data.data.TRAN_KIND, "TRAN_KIND");
            sql += CommDAO.sql_array(data.data.TRAN_ID, "TRAN_ID");
            sql += CommDAO.sql_like(data.data.TRAN_NAME, "TRAN_NAME");
            sql += CommDAO.sql_like(data.data.TRAN_ABBR, "TRAN_ABBR");

            sql += " ORDER BY TRAN_COMPID ";

            return sql;
        }
        public rs_TRAIN queryTRAIN_ACE040B(TRAIN_ins data)
        {
            string sql = getTRAIN_sql_ACE040B(data);
            return query_TRAIN(data, sql);
        }
        #endregion

        public rs_TRAIN queryTRAIN_like(TRAIN_ins2 data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "SELECT * From vw_TRAIN Where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.TRAN_COMPID))
            {
                sql += $" AND TRAN_COMPID='{data.data.TRAN_COMPID}'";
            }
            #region SQL Option
            //            if (!string.IsNullOrEmpty(data.data.TRAN_KIND))
            //                sql += $" AND TRAN_KIND LIKE '%{data.data.TRAN_KIND}%'";

            sql += CommDAO.sql_array(data.data.TRAN_KIND, "TRAN_KIND");

            sql += CommDAO.sql_ep(data.data.TRAN_CODE, "TRAN_CODE");

            if (!string.IsNullOrEmpty( data.data.TRAN_ID))
                sql += $" AND TRAN_ID LIKE '{data.data.TRAN_ID}%'";

            if (!string.IsNullOrEmpty(data.data.TRAN_NAME))
                sql += $" AND TRAN_NAME LIKE '%{data.data.TRAN_NAME}%'";

            if (!string.IsNullOrEmpty(data.data.TRAN_ABBR))
                sql += $" AND TRAN_ABBR LIKE '%{data.data.TRAN_ABBR}%'";
            #endregion

            sql += " ORDER BY TRAN_COMPID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<rsTRAIN_item> rs = dt.ToList<rsTRAIN_item>();
            return new rs_TRAIN()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        #region vw_BALANCE
        public rsBALANCE_qry query_BALANCE(BALANCE_qry data, string sql)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount =CommDAO.getTotalCount(dt, pageNumber);

            List<BALANCE> rs = dt.ToList<BALANCE>();
            return new rsBALANCE_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }
        public string getBALANCE_sql(BALANCE data)
        {
            string sql = "SELECT * from vw_BALANCE where 1=1 ";
            sql += CommDAO.sql_ep(data.BALC_COMPID, "BALC_COMPID");
            sql += CommDAO.sql_ep(data.BALC_CUSTID, "BALC_CUSTID");
            sql += CommDAO.sql_ep(data.BALC_INVNO, "BALC_INVNO");
            sql += CommDAO.sql_ep_date(data.BALC_INV_DATE, "BALC_INV_DATE");
            sql += CommDAO.sql_ep_date(data.BALC_DUE_DATE, "BALC_DUE_DATE");
            sql += CommDAO.sql_ep(data.BALC_DEPTID, "BALC_DEPTID");
            sql += CommDAO.sql_ep(data.BALC_ACCD, "BALC_ACCD");
            sql += CommDAO.sql_like(data.BALC_MEMO, "BALC_MEMO");

            sql += " order by BALC_INV_DATE";

            return sql;
        }

        public rsBALANCE_qry vwBALANCE(BALANCE_qry data)
        {
            string sql = getBALANCE_sql(data.data);
            return query_BALANCE(data, sql);
        }
        #endregion

        // vw_DEPT
        public rs_AccDept_qry queryAccDept(AccDept_ins data, string sql = "SELECT * From vw_DEPT Where 1=1 ")
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            //string sql = "SELECT * From vw_DEPT Where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.DEPT_COMPID))
            {
                sql += $" AND DEPT_COMPID='{data.data.DEPT_COMPID}'";
            }
            #region SQL Option
            if (!string.IsNullOrEmpty(data.data.DEPT_ID))
            {
                sql += $" AND DEPT_ID ='{data.data.DEPT_ID}'";
            }
            if (!string.IsNullOrEmpty(data.data.DEPT_NAME))
            {
                sql += $" AND DEPT_NAME LIKE '%{data.data.DEPT_NAME}%'";
            }

            if (!string.IsNullOrEmpty(data.data.DEPT_TYPE))
            {
                sql += $" AND DEPT_TYPE='{data.data.DEPT_TYPE}'";
            }
            if (!string.IsNullOrEmpty(data.data.DEPT_ROUTE))
            {
                sql += $" AND DEPT_ROUTE='{data.data.DEPT_ROUTE}'";
            }
            if (!string.IsNullOrEmpty(data.data.DEPT_BRAND))
            {
                sql += $" AND DEPT_BRAND='{data.data.DEPT_BRAND}'";
            }
            if (!string.IsNullOrEmpty(data.data.DEPT_AREAID))
            {
                sql += $" AND DEPT_AREAID='{data.data.DEPT_AREAID}'";
            }
            #endregion

            sql += " ORDER BY DEPT_COMPID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccDept> rs = dt.ToList<AccDept>();
            return new rs_AccDept_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }


        // vw_USER
        public rs_USER queryAccUser(USER_ins data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "SELECT USER_COMPID, USERID as USER_ID, USERNAME as USER_NAME From vw_USER Where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.USER_COMPID))
            {
                sql += $" AND USER_COMPID='{data.data.USER_COMPID}'";
            }

            if (!string.IsNullOrEmpty(data.data.USER_ID))
            {
                sql += $" AND USERID='{data.data.USER_ID}'";
            }
            #region SQL Option
            if (!string.IsNullOrEmpty(data.data.USER_NAME))
            {
                sql += $" AND USERNAME Like'%{data.data.USER_NAME}%'";
            }
            #endregion

            sql += " ORDER BY USERID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<USER> rs = dt.ToList<USER>();
            return new rs_USER()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }


    }
}