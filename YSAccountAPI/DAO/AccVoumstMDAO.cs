using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccVoumstMDAO : BaseClass
    {
        private const string tableName = "ACC_VOUMST_M";
        private List<string> PK; // = CommDAO.getPK(comm, tableName);

        public AccVoumstMDAO(){
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccVoumstM_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccVoumstM_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccVoumstM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.VOMM_A_USER_ID = employeeNo;
            data.VOMM_A_USER_NM = name;
            data.VOMM_A_DATE = DateTime.Now;
            data.VOMM_U_USER_ID = employeeNo;
            data.VOMM_U_USER_NM = name;
            data.VOMM_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public  bool _UpdateBatch(AccVoumstM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.VOMM_U_USER_ID = employeeNo;
            data.VOMM_U_USER_NM = name;
            data.VOMM_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("VOMM_A_USER_ID");
            outSL.Add("VOMM_A_USER_NM");
            outSL.Add("VOMM_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public  bool _DeleteBatch(AccVoumstM_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccVoumstM_item data)
        {
            if (string.IsNullOrEmpty(data.VOMM_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.VOMM_NO))
            {
                Log.Info("_QueryBatch: 無傳票號碼");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND VOMM_COMPID='{data.VOMM_COMPID}' ";
            sql += CommDAO.sql_ep(data.VOMM_NO, "VOMM_NO");

            return comm.DB.RunSQL(sql);
        }

        #endregion

        public  rs Add(AccVoumstM_ins data)
        {
            // 填入預設資料
            data.data.VOMM_A_USER_ID = data.baseRequest.employeeNo;
            data.data.VOMM_A_USER_NM = data.baseRequest.name;
            data.data.VOMM_A_DATE = DateTime.Now;
            data.data.VOMM_U_USER_ID = data.baseRequest.employeeNo;
            data.data.VOMM_U_USER_NM = data.baseRequest.name;
            data.data.VOMM_U_DATE = DateTime.Now;

            if (!isExist(data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }
        public  rs Update(AccVoumstM_ins data)
        {
            // 填入預設資料
            data.data.VOMM_U_USER_ID = data.baseRequest.employeeNo;
            data.data.VOMM_U_USER_NM = data.baseRequest.name;
            data.data.VOMM_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("VOMM_A_USER_ID");
            outSL.Add("VOMM_A_USER_NM");
            outSL.Add("VOMM_A_DATE");

            if (isExist(data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK, outSL);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }
        public  rs Delete(AccVoumstM_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }
        public rsAccVoumstM_qry Query(AccVoumstM_qry data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region Check Params
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.VOMM_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.VOMM_NO))
            {
                Log.Info("rsAccVoumstM_qry: 無傳票號碼");
                bOK = false;
            }
            if (!bOK)
                return new rsAccVoumstM_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
            #endregion

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 AND VOMM_COMPID='{data.data.VOMM_COMPID}' ";

            if (!string.IsNullOrEmpty(data.data.VOMM_NO))
            {
                sql += $" AND VOMM_NO LIKE '{data.data.VOMM_NO}%'";
            }

            if (data.data.VOMM_DATE != null)
            {
                sql += $" AND convert(char(8), VOMM_DATE ,112) = '{string.Format("{0:yyyyMMdd}", data.data.VOMM_DATE)}'";
            }
            #endregion

            #region 分頁
            sql += " ORDER BY VOMM_COMPID,VOMM_NO ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccVoumstM_qry() {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccVoumstM>(),
                pagination= pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        public rs update_ACB030M(List<AccVoumstM_item> data)
        {
            bool bOK = false; 

            foreach (var item in data)
            {
                bOK = Update(item.VOMM_COMPID, item.VOMM_NO, item.VOMM_APPROVE_FLG, comm);
                if (!bOK) break;
            }

            if (bOK)
                return new rs { result = CommDAO.getRsItem() };

            return new rs { result = CommDAO.getRsItem1() };
        }
        public static bool Update(string VOMM_COMPID, string VOMM_NO, string VOMM_APPROVE_FLG, CommDAO dao)
        {
            string sql = $@"UPDATE ACC_VOUMST_M SET VOMM_APPROVE_FLG=@VOMM_APPROVE_FLG
WHERE VOMM_COMPID=@VOMM_COMPID AND VOMM_NO=@VOMM_NO";

            object[] obj = new object[] { VOMM_APPROVE_FLG, VOMM_COMPID, VOMM_NO };

            return dao.DB.ExecSQL(sql, obj);
        }

        public rsAccVoumstM_qry2 Query2_ACB030M(AccVoumstM_qry2 data, bool VOMM_VALID_Y = true)
        {
            string sql = $@"SELECT distinct a.*, a.VOMM_A_USER_ID as USERID, c.USERNAME, d.DEPT_NAME from {tableName} a 
LEFT JOIN ACC_VOUMST_D b ON a.VOMM_COMPID=b.VOMD_COMPID AND a.VOMM_NO=b.VOMD_NO
LEFT JOIN vw_USER c      ON a.VOMM_COMPID=c.USER_COMPID AND a.VOMM_A_USER_ID=c.USERID
LEFT JOIN ACC_DEPT d     ON a.VOMM_COMPID=d.DEPT_COMPID AND  a.VOMM_DEPTID = d.DEPT_ID
WHERE 1=1 ";

            sql += CommDAO.sql_ep(data.VOMM_COMPID, "a.VOMM_COMPID");
            sql += CommDAO.sql_ep_date_between(data.BDate, data.EDate, "a.VOMM_DATE");
            sql += CommDAO.sql_ep_between(data.BNo, data.ENo, "a.VOMM_NO");
            sql += CommDAO.sql_ep(data.UserNo, "a.VOMM_A_USER_ID");
            sql += CommDAO.sql_like(data.UserName, "c.USERNAME");
            sql += CommDAO.sql_ep(data.VOMM_APPROVE_FLG, "a.VOMM_APPROVE_FLG");

            if (data.VOMM_TYPE != null)
            {
                sql += CommDAO.sql_ep(data.VOMM_TYPE.TrimEnd() == "" ? "empty" : data.VOMM_TYPE, "a.VOMM_TYPE");
            }

            sql += CommDAO.sql_ep(data.VOMM_DEPTID, "a.VOMM_DEPTID");
            sql += CommDAO.sql_ep(data.VOMD_TRANID, "b.VOMD_TRANID");


            if (VOMM_VALID_Y)
                sql += CommDAO.sql_ep("Y", "a.VOMM_VALID ");

            //20240115
            sql+= CommDAO.sql_like(data.VOMM_MEMO, "a.VOMM_MEMO", "%");

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccVoumstM_qry2 {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccVoumstM3>()
            };
        }

        #region Query_ACB030M
        public rsAccVoumstM2_qry Query_ACB030M(AccVoumstM data)
        {
            string COMPID = data.VOMM_COMPID;
            string VOMD_NO = data.VOMM_NO;
            //if (string.IsNullOrEmpty(VOMD_NO))
            //{
            //    return new rsAccVoumstM2_qry
            //    {
            //        result = CommDAO.getRsItem1("未輸入 VOMD_NO:憑證資料"),
            //    };
            //}

            DataTable dtM = AccVoumstM2_qry(COMPID, VOMD_NO);
            DataTable dtD = AccVoumstD2_qry(COMPID, VOMD_NO);
            DataTable dtT = AccVoumstTax2_qry(COMPID, VOMD_NO);

            return new rsAccVoumstM2_qry
            {
                result = CommDAO.getRsItem(),
                data = new Models.AccVoumstM2_qry {
                    AccVoumstM = dtM.ToList<AccVoumstM2>(),
                    AccVoumstD = dtD.ToList<AccVoumstD2>(),
                    AccVoumstTax = dtT.ToList<AccVoumstTax2>()
                }
            };
        }
        public DataTable AccVoumstM2_qry(string COMPID, string VOMD_NO)
        {
            string sql = $@"SELECT a.*, b.COMP_NAME from ACC_VOUMST_M a 
LEFT JOIN ACC_COMPANY b ON a.VOMM_COMPID=b.COMP_ID 
WHERE 1 = 1 ";

            sql += CommDAO.sql_ep(COMPID, "a.VOMM_COMPID");
            sql += CommDAO.sql_ep(VOMD_NO, "a.VOMM_NO");

            return comm.DB.RunSQL(sql);
        }
        public DataTable AccVoumstD2_qry(string COMPID, string VOMD_NO)
        {
            string sql = $@"SELECT a.*, b.ACNM_C_NAME, c.DEPT_NAME, d.TRAN_NAME, 
e.ACNM_C_NAME as ACNM_C_NAME2, f.BANK_NAME, g.DEPT_NAME as DEPT_NAME2
FROM ACC_VOUMST_D a 
LEFT JOIN ACC_ACCNAME b ON a.VOMD_COMPID=b.ACNM_COMPID AND a.VOMD_ACCD = IsNull(b.ACNM_ID1,'')+IsNull(b.ACNM_ID2,'')+IsNull(b.ACNM_ID3,'')
LEFT JOIN vw_DEPT c ON a.VOMD_COMPID=c.DEPT_COMPID AND a.VOMD_DEPTID=c.DEPT_ID
LEFT JOIN vw_TRAIN d ON a.VOMD_COMPID=d.TRAN_COMPID AND a.VOMD_TRANID=d.TRAN_ID
LEFT JOIN ACC_ACCNAME e ON a.VOMD_COMPID=e.ACNM_COMPID AND a.VOMD_SAV_BANK = IsNull(e.ACNM_ID1,'')+IsNull(e.ACNM_ID2,'')+IsNull(e.ACNM_ID3,'')
LEFT JOIN ACC_BANK f ON a.VOMD_DUE_BANK=f.BANK_ID
LEFT JOIN vw_DEPT g ON a.VOMD_COMPID=g.DEPT_COMPID AND a.VOMD_D_DEPTID=g.DEPT_ID

WHERE 1=1 ";

            sql += CommDAO.sql_ep(COMPID, "a.VOMD_COMPID");
            sql += CommDAO.sql_ep(VOMD_NO, "a.VOMD_NO");

            return comm.DB.RunSQL(sql);
        }
        public DataTable AccVoumstTax2_qry(string COMPID, string VOMD_NO)
        {
            string sql = $@"SELECT a.*, b.CODM_NAME2
FROM ACC_VOUMST_TAX a 
LEFT JOIN ACC_CODE_M b ON a.VOMT_FORMAT=ISNULL(b.CODM_ID2,'') + ISNULL(b.CODM_NAME2,'')
WHERE b.CODM_ID1='06'
";
            sql += CommDAO.sql_ep(COMPID, "a.VOMT_COMPID");
            sql += CommDAO.sql_ep(VOMD_NO, "a.VOMT_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion

        public rsAccVoumstM_ACB090Q_rs AccVoumstM_ACB090Q(AccVoumstM_ACB090Q data)
        {
            string sql = $@"
SELECT VOMD_ACCD,ACNM_C_NAME,VOMM_DATE,VOMM_NO,VOMD_D_NT_AMT,VOMD_C_NT_AMT,VOMD_MEMO,VOMD_DEPTID,
DEPT_NAME,VOMD_TRANID,TRAN_NAME,VOMD_INVNO, VOMD_CURR, VOMD_AMT, VOMD_DUEDATE
FROM ACC_VOUMST_M  
LEFT JOIN ACC_VOUMST_D  ON VOMM_COMPID=VOMD_COMPID AND VOMM_NO=VOMD_NO
LEFT JOIN vw_DEPT ON VOMD_COMPID=DEPT_COMPID AND VOMD_DEPTID=DEPT_ID
LEFT JOIN vw_TRAIN ON VOMD_COMPID=TRAN_COMPID AND VOMD_TRANID=TRAN_ID
LEFT JOIN ACC_ACCNAME ON VOMM_COMPID=ACNM_COMPID AND VOMD_ACCD=ISNULL(ACNM_ID1,'')+ISNULL(ACNM_ID2,'')+ISNULL(ACNM_ID3,'')
WHERE 1=1 ";

            sql += CommDAO.sql_ep(data.VOMM_COMPID, "VOMM_COMPID");
            sql += CommDAO.sql_ep("Y", "VOMM_VALID");
            sql += CommDAO.sql_ep_date_between(data.VOMM_DATE, data.VOMM_DATE_E, "VOMM_DATE");
            sql += CommDAO.sql_like(data.VOMD_ACCD, "VOMD_ACCD");
            sql += CommDAO.sql_ep(data.VOMD_DEPTID, "VOMD_DEPTID");
            sql += CommDAO.sql_ep(data.VOMD_TRANID, "VOMD_TRANID");
            sql += CommDAO.sql_ep(data.VOMD_INVNO, "VOMD_INVNO");
            sql += CommDAO.sql_decimal(data.VOMD_D_NT_AMT, "VOMD_D_NT_AMT");
            sql += CommDAO.sql_decimal(data.VOMD_C_NT_AMT, "VOMD_C_NT_AMT");
            sql += CommDAO.sql_like(data.VOMD_MEMO, "VOMD_MEMO", "%");

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccVoumstM_ACB090Q_rs {
                result = CommDAO.getRsItem(),
                data = dt.ToList<rsAccVoumstM_ACB090Q>()
            };
        }






    }
}