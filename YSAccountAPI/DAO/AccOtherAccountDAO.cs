using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccOtherAccountDAO:BaseClass
    {
        private const string tableName = "ACC_OTHER_ACCOUNT";
        private List<string> PK;

        public AccOtherAccountDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccOtherAccount_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }

        public bool AUD(AccOtherAccount_item data, string employeeNo, string name, CommDAO dao = null)
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

        private bool _AddBatch(AccOtherAccount_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.OTAC_A_USER_ID = employeeNo;
            data.OTAC_A_USER_NM = name;
            data.OTAC_A_DATE = DateTime.Now;
            data.OTAC_U_USER_ID = employeeNo;
            data.OTAC_U_USER_NM = name;
            data.OTAC_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        private bool _UpdateBatch(AccOtherAccount_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.OTAC_U_USER_ID = employeeNo;
            data.OTAC_U_USER_NM = name;
            data.OTAC_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("RECK_A_USER_ID");
            outSL.Add("RECK_A_USER_NM");
            outSL.Add("RECK_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        private bool _DeleteBatch(AccOtherAccount_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccOtherAccount_item data)
        {
            if (string.IsNullOrEmpty(data.OTAC_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.OTAC_ACCD))
            {
                Log.Info("_QueryBatch: 無科目代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.OTAC_TRANID))
            {
                Log.Info("_QueryBatch: 無對象代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.OTAC_INVNO))
            {
                Log.Info("_QueryBatch: 憑證編號");
                return null;
            }

            string sql = $@"SELECT * from {tableName} WHERE 1=1 
AND OTAC_COMPID='{data.OTAC_COMPID}' ";
            sql += CommDAO.sql_ep(data.OTAC_ACCD, "OTAC_ACCD");
            sql += CommDAO.sql_ep(data.OTAC_TRANID, "OTAC_TRANID");
            sql += CommDAO.sql_ep(data.OTAC_INVNO, "OTAC_INVNO");

            return comm.DB.RunSQL(sql);
        }
        #endregion

        //---------------------------------------------------------------------------
        #region 查詢 ACF030M

        private DataTable _Query(AccOtherAccount_qry_item data)
        {
            string sql = $"SELECT * from {tableName} WHERE 1=1";

            // 20240820 改成 List<string>
            if (data.OTAC_TRANID != null && data.OTAC_TRANID.Count != 0)
            {
                string CUSTID = "";
                foreach (string item in data.OTAC_TRANID)
                    CUSTID += $"OTAC_TRANID = '{item}' OR ";

                CUSTID = CUSTID.Substring(0, CUSTID.Length - 3);

                sql += " AND (" + CUSTID + ") ";
            }

            sql += CommDAO.sql_ep(data.OTAC_COMPID, "OTAC_COMPID");
            sql += CommDAO.sql_ep(data.OTAC_ACCD, "OTAC_ACCD");
            //sql += CommDAO.sql_ep(data.OTAC_TRANID, "OTAC_TRANID");
            sql += CommDAO.sql_ep(data.OTAC_INVNO, "OTAC_INVNO");
            sql += CommDAO.sql_ep_date(data.OTAC_INV_DATE, "OTAC_INV_DATE");
            sql += CommDAO.sql_ep(data.OTAC_DEPTID, "OTAC_DEPTID");
            sql += CommDAO.sql_like(data.OTAC_MEMO, "OTAC_MEMO");

            sql += CommDAO.sql_ep_date_between(data.OTAC_INV_DATE, data.OTAC_INV_DATE_E, "OTAC_INV_DATE");

            // 餘額 <> 0
            if (data.OTAC_NT_TOT_AMT.ToString() == "0.00" || data.OTAC_FOR_TOT_AMT.ToString() == "0.00")
                sql += $" AND (OTAC_NT_TOT_AMT<>0 OR OTAC_FOR_TOT_AMT<>0)";

            if (data.OTAC_NT_BAL.ToString() == "0.00" || data.OTAC_FOR_BAL.ToString() == "0.00")
                sql += $" AND (OTAC_NT_BAL<>0 OR OTAC_FOR_BAL<>0)";

            return comm.DB.RunSQL(sql);
        }
        private DataTable _Query2(AccOtherAccount data)
        {
            Dictionary<string, object> P = new Dictionary<string, object>();

            string sql = $@"Select {tableName}.* From ACC_CUSTOM_GROUP_D
JOIN {tableName} ON
CUGD_COMPID = OTAC_COMPID AND (OTAC_TRANID = CUGD_CUSTID OR OTAC_TRANID = CUGD_CUSTID_D)
WHERE 1=1 ";

            sql += BaseClass2.sql_ep(data.OTAC_COMPID, "OTAC_COMPID", ref P);
            sql += BaseClass2.sql_ep(data.OTAC_ACCD, "OTAC_ACCD", ref P);
            sql += BaseClass2.sql_ep(data.OTAC_INVNO, "OTAC_INVNO", ref P);
            sql += BaseClass2.sql_ep_date(data.OTAC_INV_DATE, "OTAC_INV_DATE", ref P);
            sql += BaseClass2.sql_ep(data.OTAC_DEPTID, "OTAC_DEPTID", ref P);
            sql += BaseClass2.sql_like(data.OTAC_MEMO, "OTAC_MEMO", ref P);

            // 餘額 <> 0
            if (data.OTAC_NT_TOT_AMT.ToString() == "0.00" || data.OTAC_FOR_TOT_AMT.ToString() == "0.00")
                sql += $" AND (OTAC_NT_TOT_AMT<>0 OR OTAC_FOR_TOT_AMT<>0)";

            if (data.OTAC_NT_BAL.ToString() == "0.00" || data.OTAC_FOR_BAL.ToString() == "0.00")
                sql += $" AND (OTAC_NT_BAL<>0 OR OTAC_FOR_BAL<>0)";

            return comm.DB.RunSQL(sql, P);
        }

        // use in AccVoumstM_Batch
        public DataTable getACF030M(AccOtherAccount_qry_item data)
        {
//            AccCustomGroupMDAO dao = new AccCustomGroupMDAO("ACC_CUSTOM_GROUP_M");
//            DataTable dt = dao.Query(new AccCustomGroupM_qry_item { CUGM_COMPID = data.OTAC_COMPID, CUGM_CUSTID = data.OTAC_TRANID });
//            if (dt.Rows.Count == 0)
                return _Query(data);
//            else
//                return _Query2(data);
        }

        public rsAccOtherAccount_qry Query(AccOtherAccount_qry_item data)
        {
            DataTable dt = getACF030M(data);

            return new rsAccOtherAccount_qry {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccOtherAccount>()
            };
        }
        #endregion

        // 新增
        public bool update_ACF030M(string OTAC_COMPID, string OTAC_TRANID, string OTAC_INVNO, decimal? AMT,
            string employeeNo, string name, CommDAO dao)
        {
            string sql = $@"UPDATE {tableName}
SET OTAC_NT_BAL = OTAC_NT_BAL - {AMT}
,OTAC_ACCD = '2304'
,OTAC_FOR_BAL = OTAC_FOR_BAL - {AMT}
,OTAC_U_USER_ID = '{employeeNo}'
,OTAC_U_USER_NM = '{name}'
,OTAC_U_DATE = GETDATE()
WHERE OTAC_COMPID = '{OTAC_COMPID}' AND OTAC_TRANID='{OTAC_TRANID}' AND OTAC_INVNO='{OTAC_INVNO}'
";
            return dao.DB.ExecSQL_T(sql);
        }
        // 修改
        public bool update_ACF030M_2(string OTAC_COMPID, string OTAC_TRANID, string OTAC_INVNO, decimal? AMT,
            string employeeNo, string name, CommDAO dao)
        {
            string sql = $@"UPDATE {tableName}
SET OTAC_NT_BAL = OTAC_NT_BAL + {AMT}
,OTAC_U_USER_ID = '{employeeNo}'
,OTAC_U_USER_NM = '{name}'
,OTAC_U_DATE = GETDATE()
WHERE OTAC_COMPID = '{OTAC_COMPID}' AND OTAC_ACCD = '2304' AND OTAC_TRANID='{OTAC_TRANID}' AND OTAC_INVNO='{OTAC_INVNO}'
";
            return dao.DB.ExecSQL_T(sql);
        }


    }
}