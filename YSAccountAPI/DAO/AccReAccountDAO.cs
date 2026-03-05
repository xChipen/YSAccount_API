using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccReAccountDAO:BaseClass
    {
        private const string tableName = "ACC_RE_ACCOUNT";
        private List<string> PK;

        public AccReAccountDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccReAccount_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }

        public bool AUD(AccReAccount_item data, string employeeNo, string name, CommDAO dao = null)
        {
            switch (data.State)
            {
                case "A": return _AddBatch(data, employeeNo, name, dao);
                case "U": return _UpdateBatch(data, employeeNo, name, dao);
                case "D": return _DeleteBatch(data, dao);
                case "": return true;
            }
            return false;
        }

        private bool _AddBatch(AccReAccount_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.REAC_A_USER_ID = employeeNo;
            data.REAC_A_USER_NM = name;
            data.REAC_A_DATE = DateTime.Now;
            data.REAC_U_USER_ID = employeeNo;
            data.REAC_U_USER_NM = name;
            data.REAC_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        private bool _UpdateBatch(AccReAccount_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.REAC_U_USER_ID = employeeNo;
            data.REAC_U_USER_NM = name;
            data.REAC_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("REAC_A_USER_ID");
            outSL.Add("REAC_A_USER_NM");
            outSL.Add("REAC_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        private bool _DeleteBatch(AccReAccount_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccReAccount_item data)
        {
            if (string.IsNullOrEmpty(data.REAC_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.REAC_CUSTID))
            {
                Log.Info("_QueryBatch: 無客戶代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.REAC_INVNO))
            {
                Log.Info("_QueryBatch: 無發票號碼");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND REAC_COMPID='{data.REAC_COMPID}' ";
            sql += CommDAO.sql_ep(data.REAC_CUSTID, "REAC_CUSTID");
            sql += CommDAO.sql_ep(data.REAC_INVNO, "REAC_INVNO");
            sql += CommDAO.sql_ep_date(data.REAC_INV_DATE, "REAC_INV_DATE");
            sql += CommDAO.sql_ep_date(data.REAC_DUE_DATE, "REAC_DUE_DATE");
            sql += CommDAO.sql_ep(data.REAC_VOUNO, "REAC_VOUNO");
            sql += CommDAO.sql_ep(data.REAC_DEPTID, "REAC_DEPTID");
            sql += CommDAO.sql_like(data.REAC_MEMO, "REAC_MEMO");


            return comm.DB.RunSQL(sql);
        }
        #endregion


        public rsAccReAccount_qry ACF030M(AccReAccount_item2 data)
        {
            //AccCustomGroupMDAO dao = new AccCustomGroupMDAO("ACC_CUSTOM_GROUP_M");
            //DataTable dt = dao.Query(new AccCustomGroupM_qry_item { CUGM_COMPID = data.COMPID, CUGM_CUSTID = data.CUSTID});
            //if (dt.Rows.Count == 0)
                return Query_ACF030M(data);
            //else
            //    return Query_ACF030M_ByGroup(data);
        }

        public rsAccReAccount_qry Query_ACF030M(AccReAccount_item2 data)
        {
            if (string.IsNullOrEmpty(data.COMPID))
                return new rsAccReAccount_qry { result = CommDAO.getRsItem1("無公司代碼")};

            if (data.CUSTID.Count == 0)
                return new rsAccReAccount_qry { result = CommDAO.getRsItem1("無客戶代號") };

            string sql = $@"SELECT ACC_RE_ACCOUNT.*, TRAN_NAME 
from ACC_RE_ACCOUNT 
left join vw_TRAIN on REAC_CUSTID=TRAN_ID
WHERE 1=1
AND REAC_COMPID='{data.COMPID}' ";

            string CUSTID = "";
            foreach (string item in data.CUSTID)
                CUSTID += $"REAC_CUSTID = '{item}' OR ";

            CUSTID = CUSTID.Substring(0, CUSTID.Length - 3);

            sql += " AND (" + CUSTID + ") ";

            sql += CommDAO.sql_ep(data.REAC_CURRID, "REAC_CURRID");

            sql += CommDAO.sql_ep(data.REAC_REC_KIND, "REAC_REC_KIND");

            sql += CommDAO.sql_ep_date_between(data.REAC_INV_DATE_B, data.REAC_INV_DATE_E, "REAC_INV_DATE");

            sql += "AND (REAC_NT_BAL<>0 OR REAC_FOR_BAL<>0)";

            sql += " ORDER BY REAC_INV_DATE ";

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccReAccount_qry {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccReAccount>()
            };
        }
//        public rsAccReAccount_qry Query_ACF030M_ByGroup(AccReAccount_item2 data)
//        {
//            if (string.IsNullOrEmpty(data.COMPID))
//                return new rsAccReAccount_qry { result = CommDAO.getRsItem1("無公司代碼") };

//            if (string.IsNullOrEmpty(data.CUSTID) && string.IsNullOrEmpty(data.CUSTID1))
//                return new rsAccReAccount_qry { result = CommDAO.getRsItem1("無客戶代號") };

//            Dictionary<string, object> P = new Dictionary<string, object>();

//            string sql = $@"Select ACC_RE_ACCOUNT.* From ACC_CUSTOM_GROUP_D
//JOIN ACC_RE_ACCOUNT ON
//CUGD_COMPID = REAC_COMPID AND (REAC_CUSTID = CUGD_CUSTID OR REAC_CUSTID = CUGD_CUSTID_D)";         

//            sql += "WHERE (REAC_NT_BAL<>0 OR REAC_FOR_BAL<>0)";

//            sql += BaseClass2.sql_ep(data.CUSTID, "CUGD_CUSTID", ref P);

//            sql += " ORDER BY REAC_INV_DATE ";

//            DataTable dt = comm.DB.RunSQL(sql, P);

//            return new rsAccReAccount_qry
//            {
//                result = CommDAO.getRsItem(),
//                data = dt.ToList<AccReAccount>()
//            };
//        }

        public bool Update_ACF030M(string REAC_COMPID, string REAC_CUSTID, string REAC_INVNO, 
            decimal? AMT, DateTime? REAC_REC_DATE, string employeeNo, string name, CommDAO dao)
        {
            string sREAC_REC_DATE = "";
            if (AMT >= 0)
            {
                sREAC_REC_DATE = string.Format("{0:yyyy/MM/dd}", REAC_REC_DATE);
                sREAC_REC_DATE = $@",REAC_REC_DATE = '{sREAC_REC_DATE}' ";
            }

            string sql = $@"UPDATE {tableName} SET 
 REAC_NT_BAL = REAC_NT_BAL - {AMT}
,REAC_FOR_BAL = REAC_FOR_BAL - {AMT}
{sREAC_REC_DATE}
,REAC_U_USER_ID = '{employeeNo}'
,REAC_U_USER_NM = '{name}'
,REAC_U_DATE = GETDATE() 
WHERE REAC_COMPID='{REAC_COMPID}' AND REAC_CUSTID='{REAC_CUSTID}' AND REAC_INVNO='{REAC_INVNO}'
";
            return dao.DB.ExecSQL_T(sql);
        }

        // 進銷存呼叫
        public rs update(List<AccReAccount_update> data, string employeeNo, string name)
        {
            bool bOK = true;
            if (data != null && data.Count != 0)
            {
                CommDAO batch = new CommDAO();
                batch.DB.BeginTransaction();

                foreach (AccReAccount_update item in data)
                {
                    bOK = Update_ACF030M2(item.REAC_COMPID, item.REAC_CUSTID, item.REAC_INVNO, item.AMT,
                        employeeNo, name, comm);
                    if (!bOK) break;
                }

                if (bOK)
                {
                    batch.DB.Commit();

                    return new rsAccReAccount_qry
                    {
                        result = CommDAO.getRsItem()
                    };
                }
                else
                    batch.DB.Rollback();
            }

            return new rsAccReAccount_qry
            {
                result = CommDAO.getRsItem1()
            };
        }

        // 進銷存呼叫
        public bool Update_ACF030M2(string REAC_COMPID, string REAC_CUSTID, string REAC_INVNO,
            decimal? AMT, string employeeNo, string name, CommDAO dao)
        {

            string sql = $@"UPDATE {tableName} SET 
 REAC_NT_BAL = REAC_NT_BAL - {AMT}
,REAC_FOR_BAL = REAC_FOR_BAL - {AMT}
,REAC_U_USER_ID = '{employeeNo}'
,REAC_U_USER_NM = '{name}'
,REAC_U_DATE = GETDATE() 
WHERE REAC_COMPID='{REAC_COMPID}' AND REAC_CUSTID='{REAC_CUSTID}' AND REAC_INVNO='{REAC_INVNO}'
";
            return dao.DB.ExecSQL(sql);
        }



    }
   
}