using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccReceiveMDAO : BaseClass
    {
        private const string tableName = "ACC_RECEIVE_M";
        private List<string> PK;

        public AccReceiveMDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccReceiveM_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }

        public bool AUD(AccReceiveM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            switch (data.State)
            {
                case "A":
                    {
                        data.RECM_VALID = "Y";
                        return _AddBatch(data, employeeNo, name, dao);
                    }
                case "U": return _UpdateBatch(data, employeeNo, name, dao);
                case "D": return _DeleteBatch(data, dao);
                case "": return true;
            }
            return false;
        }

        private bool _AddBatch(AccReceiveM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.RECM_A_USER_ID = employeeNo;
            data.RECM_A_USER_NM = name;
            data.RECM_A_DATE = DateTime.Now;
            data.RECM_U_USER_ID = employeeNo;
            data.RECM_U_USER_NM = name;
            data.RECM_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        private bool _UpdateBatch(AccReceiveM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.RECM_U_USER_ID = employeeNo;
            data.RECM_U_USER_NM = name;
            data.RECM_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("RECM_A_USER_ID");
            outSL.Add("RECM_A_USER_NM");
            outSL.Add("RECM_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        private bool _DeleteBatch(AccReceiveM_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        #endregion

        public string getRECM_NO(DateTime? RECM_DATE)
        {
            if (RECM_DATE == null) return "";

            string RECM_NO = string.Format("{0:yyyyMM}", RECM_DATE);

            string sql = "SELECT Max(RECM_NO) as RECM_NO from ACC_RECEIVE_M where 1=1";
            sql += CommDAO.sql_like(RECM_NO, "RECM_NO");

            DataTable dt = comm.DB.RunSQL(sql);
            if (dt.Rows.Count != 0)
            {
                string NO = dt.Rows[0]["RECM_NO"].ToString();
                if (NO != "")
                {
                    int iRECM_NO = Convert.ToInt32(NO) + 1;
                    return iRECM_NO.ToString();
                }
            }
            return RECM_NO + "0001";
        }

        public DataTable _QueryBatch(string RECM_COMPID, string RECM_NO)
        {
            if (string.IsNullOrEmpty(RECM_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(RECM_NO))
            {
                Log.Info("_QueryBatch: 無收款單號");
                return null;
            }

            string sql = $@"SELECT * from {tableName} WHERE 1=1 
AND RECM_COMPID='{RECM_COMPID}' ";
            sql += CommDAO.sql_ep(RECM_NO, "RECM_NO");

            return comm.DB.RunSQL(sql);
        }

        public DataTable _Query(AccReceiveM_item2 data)
        {

            string sql = $@"
SELECT CONVERT(VARCHAR(10),RECM_DATE,111) as RECM_DATE, RECM_NO, RECM_CUSTID, ISNULL(TRAN_NAME,''), RECM_A_USER_NM
FROM ACC_RECEIVE_M WITH (NOLOCK)
LEFT JOIN VW_TRAIN WITH (NOLOCK) ON RECM_COMPID = TRAN_COMPID AND RECM_CUSTID = TRAN_ID
WHERE RECM_VALID='Y'
AND RECM_COMPID='{data.RECM_COMPID}' ";

            sql += CommDAO.sql_ep(data.RECM_NO, "RECM_NO");
            sql += CommDAO.sql_ep(data.RECM_VOUNO, "RECM_VOUNO ");

            sql += CommDAO.sql_ep_date_between2(data.RECM_DATE_B, data.RECM_DATE_E, "RECM_DATE ");

            if (data.RECM_A_USER_ID.StartsWith("<>"))
            {
                string ss = data.RECM_A_USER_ID.Substring(2);
                sql += CommDAO.sql_ep(ss, "RECM_A_USER_ID", "<>");
            }
            else
            {
                sql += CommDAO.sql_ep(data.RECM_A_USER_ID, "RECM_A_USER_ID");
            }

            return comm.DB.RunSQL(sql);
        }

        public rsAccReceiveM_qry_ACF050B Query_ACF050B(AccReceiveM_item2 data)
        {
            DataTable dt = _Query(new AccReceiveM_item2 {
                RECM_COMPID = data.RECM_COMPID,
                RECM_VOUNO = "empty",
                RECM_A_USER_ID = data.RECM_A_USER_ID,
                RECM_DATE_B = data.RECM_DATE_B,
                RECM_DATE_E = data.RECM_DATE_E
            });
            DataTable dt2 = _Query(new AccReceiveM_item2
            {
                RECM_COMPID = data.RECM_COMPID,
                RECM_VOUNO = "empty",
                RECM_A_USER_ID = "<>" + data.RECM_A_USER_ID,
                RECM_DATE_B = data.RECM_DATE_B,
                RECM_DATE_E = data.RECM_DATE_E
            });

            return new rsAccReceiveM_qry_ACF050B {
                result = CommDAO.getRsItem(),
                data = new rsAccReceiveM_qry_ACF050B_item {
                    grid1 = dt.ToList<AccReceiveM_item>(),
                    grid2 = dt2.ToList<AccReceiveM_item>()
                }
            };
        }

        // ACF100Q_應收帳款明細查詢
        // 未收款
        public DataTable ACF100Q_Query_1(ACF100Q_qry data)
        {
            string sql = $@"SELECT REAC_INV_DATE as INV_DATE, REAC_INVNO as INV_NO,TRAN_NAME,
REAC_VOUNO as VOUNO, REAC_NT_TOT_AMT as NT_TOT_AMT,
REAC_NT_TOT_AMT-REAC_NT_BAL as AMT, REAC_REC_DATE as REC_DATE
FROM ACC_RE_ACCOUNT
LEFT JOIN vw_TRAIN ON REAC_COMPID=TRAN_COMPID AND REAC_CUSTID=TRAN_ID 
WHERE REAC_NT_BAL <> 0
";
            sql += CommDAO.sql_ep(data.COMPID, "REAC_COMPID");
            sql += CommDAO.sql_ep(data.CUST_ID, "REAC_CUSTID");
            sql += CommDAO.sql_like(data.CUST_NAME, "TRAN_NAME");
            sql += CommDAO.sql_ep(data.UNION, "REAC_VOUNO");
            sql += CommDAO.sql_ep(data.INV_NO, "REAC_INVNO");
            sql += CommDAO.sql_ep_between(data.INV_DATE, data.INV_DATE_E, "REAC_INV_DATE");
            sql += CommDAO.sql_ep_between(data.REC_DATE, data.REC_DATE_E, "REAC_REC_DATE");

            return comm.DB.RunSQL(sql);
        }
        // 已收款
        public DataTable ACF100Q_Query_2(ACF100Q_qry data)
        {
            string sql = $@"SELECT RECW_INV_DATE as INV_DATE, RECW_INVNO as INV_NO, TRAN_NAME,
RECM_VOUNO as VOUNO, REAC_NT_TOT_AMT as NT_TOT_AMT,
RECW_NT_AMT as AMT, RECM_DATE as REC_DATE
FROM ACC_RECEIVE_M
LEFT JOIN ACC_RECEIVE_WRITEOFF ON RECM_COMPID=RECW_COMPID AND RECM_NO=RECW_NO
LEFT JOIN ACC_RE_ACCOUNT ON RECW_COMPID=REAC_COMPID AND RECW_CUSTID=REAC_CUSTID AND RECW_INVNO=REAC_INVNO 
LEFT JOIN vw_TRAIN ON REAC_CUSTID=TRAN_ID 
WHERE RECM_VALID = 'Y'
";
            sql += CommDAO.sql_ep(data.COMPID, "RECM_COMPID");
            sql += CommDAO.sql_ep(data.CUST_ID, "REAC_CUSTID");
            sql += CommDAO.sql_like(data.CUST_NAME, "TRAN_NAME");
            sql += CommDAO.sql_ep(data.UNION, "RECM_VOUNO");
            sql += CommDAO.sql_ep(data.INV_NO, "RECW_INVNO");
            sql += CommDAO.sql_ep_between(data.INV_DATE, data.INV_DATE_E, "RECW_INV_DATE");
            sql += CommDAO.sql_ep_between(data.REC_DATE, data.REC_DATE_E, "RECM_DATE");

            return comm.DB.RunSQL(sql);
        }
        // 全部
        public DataTable ACF100Q_Query_3(ACF100Q_qry data)
        {
            string sql = $@"SELECT REAC_INV_DATE as INV_DATE, REAC_INVNO as INV_NO,TRAN_NAME,
REAC_VOUNO as VOUNO, REAC_NT_TOT_AMT as NT_TOT_AMT,
REAC_NT_TOT_AMT-REAC_NT_BAL as AMT, REAC_REC_DATE as REC_DATE
FROM ACC_RE_ACCOUNT
LEFT JOIN vw_TRAIN ON REAC_CUSTID=TRAN_ID 
";

            sql += CommDAO.sql_ep(data.COMPID, "REAC_COMPID");
            sql += CommDAO.sql_ep(data.CUST_ID, "REAC_CUSTID");
            sql += CommDAO.sql_like(data.CUST_NAME, "TRAN_NAME");
            sql += CommDAO.sql_ep(data.UNION, "REAC_VOUNO");
            sql += CommDAO.sql_ep(data.INV_NO, "REAC_INVNO");
            sql += CommDAO.sql_ep_between(data.INV_DATE, data.INV_DATE_E, "REAC_INV_DATE");
            sql += CommDAO.sql_ep_between(data.REC_DATE, data.REC_DATE_E, "REAC_REC_DATE");

            return comm.DB.RunSQL(sql);
        }

        public rsACF100Q_rs ACF100Q_Query(ACF100Q_qry data)
        {
            DataTable dt;
            if (data.KIND == 1)
                dt = ACF100Q_Query_1(data);
            else if (data.KIND == 2)
                dt = ACF100Q_Query_2(data);
            else if (data.KIND == 3)
                dt = ACF100Q_Query_3(data);
            else
                return new rsACF100Q_rs {
                    result = CommDAO.getRsItem()
                };

            return new rsACF100Q_rs
            {
                result = CommDAO.getRsItem1(),
                data = dt.ToList<ACF100Q_rs>()
            };
        }

        public rsAccReceiveM_ACF030M ACF030M(AccReceiveM_ACF030M data)
        {
            string sql = $@"SELECT ACC_RECEIVE_M.*, TRAN_NAME from ACC_RECEIVE_M
LEFT JOIN vw_TRAIN ON RECM_COMPID=TRAN_COMPID AND RECM_CUSTID=TRAN_ID
WHERE 1=1 ";
            sql += CommDAO.sql_ep(data.RECM_COMPID, "RECM_COMPID");
            sql += CommDAO.sql_ep(data.RECM_NO, "RECM_NO");
            sql += CommDAO.sql_ep_date_between(data.RECM_DATE, data.RECM_DATE_E, "RECM_DATE");
            sql += CommDAO.sql_ep(data.RECM_CUSTID, "RECM_CUSTID");

            sql += CommDAO.sql_ep(data.RECM_VOUNO, "RECM_VOUNO");

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccReceiveM_ACF030M {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccReceiveM>()
            };
        }

        public rsAccReceiveM_Query3 AccReceiveM_Query3(string CUSTID)
        {
            string sql = $@"SELECT TOP 1  
REAK_BANKID, 
REAK_ACNO
FROM ACC_RECEIVE_M,ACC_RECEIVE_CHECK
WHERE RECM_CUSTID = '{CUSTID}'
AND   RECM_NO = REAK_NO
AND   RECM_VALID = 'Y'
ORDER BY RECM_DATE DESC";

            DataTable dt = comm.DB.RunSQL(sql);

            string DUEBANK = "";
            string ACNO = "";
            if (dt.Rows.Count != 0)
            {
                DUEBANK = dt.Rows[0]["REAK_BANKID"].ToString();
                ACNO = dt.Rows[0]["REAK_ACNO"].ToString();
            }

            return new rsAccReceiveM_Query3
            {
                result = CommDAO.getRsItem(),
                data = new Models.AccReceiveM_Query3
                {
                    DUEBANK = DUEBANK,
                    ACNO = ACNO
                }
            };
        }

        public rsAccReceiveM_Query4 AccReceiveM_Query4(string BDate, string EDate)
        {
            string sql = $@"SELECT CONVERT(VARCHAR(10),RECM_DATE,111) as RECM_DATE
,RECM_NO, RECM_CUSTID, ISNULL(TRAN_NAME,'') as TRAN_NAME, RECM_A_USER_NM

FROM ACC_RECEIVE_M WITH (NOLOCK)
LEFT JOIN VW_TRAIN  WITH (NOLOCK) ON RECM_COMPID = TRAN_COMPID AND RECM_CUSTID = TRAN_ID
WHERE RECM_VALID='Y'
AND RECM_DATE  < '{EDate}'
AND RECM_DATE  > '{BDate}'
";

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccReceiveM_Query4
            {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccReceiveM_Query4>()
            };
        }


    }
}