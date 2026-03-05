using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccAllowanceMDAO:BaseClass
    {
        private const string tableName = "ACC_ALLOWANCE_M";
        private List<string> PK; 

        public AccAllowanceMDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public string getNO_Max(string ALLM_COMPID, string ALLM_DATE)
        {
            string sql = $@"SELECT MAX(ALLM_NO) as NO from {tableName} where 1=1";
            sql += CommDAO.sql_ep(ALLM_COMPID, "ALLM_COMPID");
            sql += CommDAO.sql_like(ALLM_DATE, "ALLM_NO");

            DataTable dt = comm.DB.RunSQL(sql);

            return dt.Rows[0]["NO"].ToString();
        }

        public string getNO(string ALLM_COMPID, DateTime? ALLM_DATE)
        {
            if (string.IsNullOrEmpty(ALLM_COMPID)) return "";
            if (ALLM_DATE == null) return "";

            string HEAD = string.Format("{0:yyyyMM}", ALLM_DATE);
            string NO = getNO_Max(ALLM_COMPID, HEAD);

            if (NO != "")
            {
                NO = NO.Substring(6);
                int iNO;
                if (Int32.TryParse(NO, out iNO))
                {
                    return HEAD + (iNO + 1).ToString("0000");
                }
            }

            return HEAD + "0001";
        }

        public bool AUD(AccAllowanceM_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccAllowanceM_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccAllowanceM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            data.ALLM_VALID = "Y";
            data.ALLM_VOUNO = "";
            data.ALLM_PAY_DATE = null;
            data.ALLM_PAY_AMT = 0;
            data.ALLM_PAY_VOUNO = "";

            // 填入預設資料
            data.ALLM_A_USER_ID = employeeNo;
            data.ALLM_A_USER_NM = name;
            data.ALLM_A_DATE = DateTime.Now;
            data.ALLM_U_USER_ID = employeeNo;
            data.ALLM_U_USER_NM = name;
            data.ALLM_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccAllowanceM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.ALLM_U_USER_ID = employeeNo;
            data.ALLM_U_USER_NM = name;
            data.ALLM_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("ALLM_A_USER_ID");
            outSL.Add("ALLM_A_USER_NM");
            outSL.Add("ALLM_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccAllowanceM_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccAllowanceM_item data)
        {
            if (string.IsNullOrEmpty(data.ALLM_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.ALLM_NO))
            {
                Log.Info("_QueryBatch: 無請款單號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND ALLM_COMPID='{data.ALLM_COMPID}' ";
            sql += CommDAO.sql_ep(data.ALLM_NO, "ALLM_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion

        public DataTable Query2(AccAllowanceM_item2 data)
        {
            string sql = $@"SELECT a.*, b.DEPT_NAME, c.TRAN_NAME from {tableName} a
LEFT JOIN vw_DEPT b on a.ALLM_COMPID = b.DEPT_COMPID AND a.ALLM_DEPTID=b.DEPT_ID
LEFT JOIN vw_TRAIN c on a.ALLM_COMPID = c.TRAN_COMPID AND a.ALLM_EMPLID=c.TRAN_ID
WHERE 1=1 ";

            sql += CommDAO.sql_ep(data.ALLM_COMPID, "a.ALLM_COMPID");
            sql += CommDAO.sql_ep_date_between(data.ALLM_DATE_B, data.ALLM_DATE_E, "a.ALLM_DATE");
            sql += CommDAO.sql_ep(data.ALLM_VALID, "a.ALLM_VALID");
            sql += CommDAO.sql_ep(data.ALLM_VOUNO, "a.ALLM_VOUNO");
            sql += CommDAO.sql_ep(data.ALLM_DEPTID, "a.ALLM_DEPTID");
            sql += CommDAO.sql_like(data.DEPT_NAME, "b.DEPT_NAME");
            sql += CommDAO.sql_ep(data.ALLM_EMPLID, "a.ALLM_EMPLID");
            sql += CommDAO.sql_like(data.TRAN_NAME, "c.TRAN_NAME");
            sql += CommDAO.sql_ep_date(data.ALLM_PAY_DATE, "a.ALLM_PAY_DATE");
            sql += CommDAO.sql_ep(data.ALLM_PAY_KIND, "a.ALLM_PAY_KIND");
            sql += CommDAO.sql_ep_date_between(data.ALLM_PAY_DATE_B, data.ALLM_PAY_DATE_E, "a.ALLM_PAY_DATE");
            if (data.ALLM_PAY_AMT != null)
                sql += CommDAO.sql_ep_int((int)data.ALLM_PAY_AMT, "a.ALLM_PAY_AMT");

            return comm.DB.RunSQL(sql);
        }

        public rsAccAllowanceM_qry2 rsQuery2(AccAllowanceM_item2 data)
        {
            DataTable dt = Query2(data);
            return new rsAccAllowanceM_qry2 {
                result = CommDAO.getRsItem(),
                data = dt.ToList<rsAccAllowanceM2>()
            };
        }

        // 付款
        private bool _update1(rsAccAllowanceM2 data, string employeeNo, string name)
        {

            string sql = $@"UPDATE {tableName} SET 
convert(char(8), ALLM_PAY_DATE ,112) = '{string.Format("{0:yyyyMMdd}", data.ALLM_PAY_DATE)}',
ALLM_PAY_AMT = {data.ALLM_TOTAL_AMT},
ALLM_U_USER_ID = '{employeeNo}'
ALLM_U_USER_NM = '{name}'
ALLM_U_DATE = GETDATE()
WHERE ALLM_COMPID ='{data.ALLM_COMPID}' AND ALLM_NO ='{data.ALLM_NO}'
";
            return comm.DB.ExecSQL(sql);
        }
        // 取消付款
        private bool _update2(rsAccAllowanceM2 data, string employeeNo, string name)
        {

            string sql = $@"UPDATE {tableName} SET 
ALLM_PAY_DATE = null,
ALLM_PAY_AMT = 0,
ALLM_U_USER_ID = '{employeeNo}'
ALLM_U_USER_NM = '{name}'
ALLM_U_DATE = GETDATE()
WHERE ALLM_COMPID ='{data.ALLM_COMPID}' AND ALLM_NO ='{data.ALLM_NO}'
";
            return comm.DB.ExecSQL(sql);
        }

        public rs update1(List<rsAccAllowanceM2> data, string employeeNo, string name)
        {
            foreach (rsAccAllowanceM2 item in data)
            {
                _update1(item, employeeNo, name);
            }
            return new rs {
                result = CommDAO.getRsItem()
            };
        }
        public rs update2(List<rsAccAllowanceM2> data, string employeeNo, string name)
        {
            foreach (rsAccAllowanceM2 item in data)
            {
                _update2(item, employeeNo, name);
            }
            return new rs
            {
                result = CommDAO.getRsItem()
            };
        }

    }
}