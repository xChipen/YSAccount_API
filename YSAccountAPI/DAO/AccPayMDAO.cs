using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccPayMDAO:BaseClass
    {
        private const string tableName = "ACC_PAY_M";
        private List<string> PK;

        public AccPayMDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccPayM_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccPayM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.PAYM_A_USER_ID = employeeNo;
            data.PAYM_A_USER_NM = name;
            data.PAYM_A_DATE = DateTime.Now;
            data.PAYM_U_USER_ID = employeeNo;
            data.PAYM_U_USER_NM = name;
            data.PAYM_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccPayM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.PAYM_U_USER_ID = employeeNo;
            data.PAYM_U_USER_NM = name;
            data.PAYM_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("PAYM_A_USER_ID");
            outSL.Add("PAYM_A_USER_NM");
            outSL.Add("PAYM_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccPayM_item data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        #endregion

        public rs Add(AccPayM_ins data)
        {
            // 填入預設資料
            data.data.PAYM_A_USER_ID = data.baseRequest.employeeNo;
            data.data.PAYM_A_USER_NM = data.baseRequest.name;
            data.data.PAYM_A_DATE = DateTime.Now;
            data.data.PAYM_U_USER_ID = data.baseRequest.employeeNo;
            data.data.PAYM_U_USER_NM = data.baseRequest.name;
            data.data.PAYM_U_DATE = DateTime.Now;

            if (!isExist(data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccPayM_ins data)
        {
            // 填入預設資料
            data.data.PAYM_U_USER_ID = data.baseRequest.employeeNo;
            data.data.PAYM_U_USER_NM = data.baseRequest.name;
            data.data.PAYM_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("PAYM_A_USER_ID");
            outSL.Add("PAYM_A_USER_NM");
            outSL.Add("PAYM_A_DATE");

            if (isExist(data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK, outSL);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccPayM_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rsAccPayM_qry Query(AccPayM_qry data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region Check Params
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.PAYM_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.PAYM_NO))
            {
                Log.Info("rsAccVoumstM_qry: 無傳票號碼");
                bOK = false;
            }
            if (!bOK)
                return new rsAccPayM_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
            #endregion

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 AND PAYM_COMPID='{data.data.PAYM_COMPID}' ";

            if (!string.IsNullOrEmpty(data.data.PAYM_NO))
            {
                sql += $" AND PAYM_NO LIKE '{data.data.PAYM_NO}%'";
            }

            if (data.data.PAYM_DATE != null)
            {
                sql += $" AND convert(char(8), PAYM_DATE ,112) = '{string.Format("{0:yyyyMMdd}", data.data.PAYM_DATE)}'";
            }

            // PAY_DATE 起, 迄
            //sql += CommDAO.sql_ep_date_between(data.data.PAYM_DATE, "PAYM_DATE");


            sql += CommDAO.sql_ep(data.data.PAYM_VALID, "PAYM_VALID");
            #endregion

            #region 分頁
            sql += " ORDER BY PAYM_COMPID,PAYM_NO ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccPayM_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccPayM>(),
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }


        public rsAccPayM_ACD040M_1_qry queryACD040M_Query1(AccPayM_ACD040M_qry data) {

            string sql = $@"SELECT PAAC_VENDID as VENDID,MAX(TRAN_NAME) as NAME,
MAX(TRAN_BANKID) as BANKID,MAX(TRAN_ACNO) as ACNO,
SUM(PAAC_NT_BAL) as NT_BAL
FROM ACC_PA_ACCOUNT,vw_TRAIN
WHERE PAAC_DUE_DATE <= '{ string.Format("{0:yyyy/MM/dd}", data.PAY_DATE) }'
AND PAAC_PAY_KIND = '{ data.PAY_KIND }'
AND PAAC_STS = '{data.STS}'
AND PAAC_COMPID = TRAN_COMPID
AND TRAN_KIND = '2'
AND TRAN_ID = PAAC_VENDID
AND PAAC_CURRID = 'NTD'
AND PAAC_NT_BAL >0
AND PAAC_COMPID='{data.COMPID}'
GROUP BY PAAC_VENDID 
";
            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccPayM_ACD040M_1_qry
            {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccPayM_ACD040M_1>()
            };
        }

        public rsAccPayM_ACD040M_1_qry queryACD040M_Query2(AccPayM_ACD040M_qry data)
        {

            string sql = $@"SELECT PAYM_VENDID as VENDID,TRAN_NAME as NAME,
PAYM_BANKID as BANKID,PAYM_ACNO as ACNO,
PAYM_PAY_NT_AMT as NT_BAL, PAYM_PAY_FOR_AMT as NT_BAL2, PAYM_CURRID
FROM ACC_PAY_M,vw_TRAIN
WHERE PAYM_DATE ='{ string.Format("{0:yyyy/MM/dd}", data.PAY_DATE) }'
AND PYM_PAY_KIND = '{ data.PAY_KIND }'
AND PAYM_VALID='Y'
AND PAYM_VOUNO =''
AND PAYM_COMPID='{data.COMPID}'
";
            sql+= CommDAO.sql_ep(data.CURRID, "PAYM_CURRID");

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccPayM_ACD040M_1_qry
            {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccPayM_ACD040M_1>()
            };
        }

        public rsAccPayM_ACD040M_qry queryACD040M_Query3(AccPayM_ACD040M_qry data)
        {

            string sql = $@"SELECT PAAC_INV_DATE as INV_DATE,PAAC_INVNO as INVNO,
PAAC_NT_BAL as NT_BAL
FROM ACC_PA_ACCOUNT
WHERE PAAC_DUE_DATE <= '{ string.Format("{0:yyyy/MM/dd}", data.PAY_DATE) }'
AND PAAC_PAY_KIND = '{ string.Format("{0:yyyy/MM/dd}", data.PAY_DATE) }'
AND PAAC_STS = 'Y'
AND PAAC_CURRID = 'NTD'
AND PAAC_VENDID = '{data.VENDID}'
AND PAAC_NT_BAL > 0
AND PAAC_COMPID='{data.COMPID}'
";
            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccPayM_ACD040M_qry
            {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccPayM_ACD040M_qry2>()
            };
        }

        public rsAccPayM_ACD040M_qry queryACD040M_Query4(AccPayM_ACD040M_qry data)
        {
            string sql = $@"SELECT PAAC_INV_DATE as INV_DATE,PAAC_INVNO as INVNO,
PAAC_NT_BAL as NT_BAL
FROM ACC_PA_ACCOUNT
WHERE PAAC_DUE_DATE <='{ string.Format("{0:yyyy/MM/dd}", data.PAY_DATE) }'
AND PAAC_PAY_KIND = '{data.PAY_KIND}'
AND PAAC_VENDID = '{data.VENDID}'
AND PAAC_STS = 'N'
AND PAAC_CURRID = 'NTD'
AND PAAC_NT_BAL > 0
AND PAAC_COMPID='{data.COMPID}'
";
            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccPayM_ACD040M_qry {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccPayM_ACD040M_qry2>()
            };
        }

        public rsAccPayM_ACD040M_qry queryACD040M_Query5(AccPayM_ACD040M_qry data)
        {
            string sql = $@"
SELECT PAYD_INV_DATE as INV_DATE,PAYD_INVNO as INVNO,
PAYD_NT_AMT as NT_ANT
FROM ACC_PAY_M,ACC_PAY_D
WHERE PAYM_VENDID ='{data.VENDID}'
AND PAYM_DATE = '{ string.Format("{0:yyyy/MM/dd}", data.PAY_DATE) }'
AND PAYM_COMPID = PAYD_COMPID
AND PAYM_NO = PAYD_NO
AND PAYM_COMPID='{data.COMPID}'
";
            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccPayM_ACD040M_qry
            {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccPayM_ACD040M_qry2>()
            };
        }

        //------

        // 未付款
        public DataTable ACD120Q_1(ACD120Q_qry data)
        {
            string sql = $@"SELECT PAAC_INV_DATE as INV_DATE, PAAC_INVNO as INVNO, TRAN_NAME,
PAAC_VOUNO as VOUNO, PAAC_DUE_DATE as DUE_DATE, PAAC_NT_TOT_AMT as NT_TOT_AMT,
PAAC_NT_TOT_AMT-PAAC_NT_BAL as AMT, PAAC_PAY_DATE as PAY_DATE, PAAC_VENDID as VENDID,
ACNM_C_NAME as C_NAME, PAAC_A_USER_NM as A_USER_NM
FROM ACC_PA_ACCOUNT  
LEFT JOIN vw_ACCD ON PAAC_COMPID = ACNM_COMPID AND PAAC_ACCD = ACNM_ID
LEFT JOIN vw_TRAIN ON PAAC_COMPID = TRAN_COMPID AND PAAC_VENDID = TRAN_ID
LEFT JOIN ACC_COMPANY ON PAAC_COMPID = COMP_ID
WHERE 1 = 1
AND PAAC_NT_BAL <> 0
AND PAAC_STS = 'Y'
";
            sql += CommDAO.sql_ep(data.COMPID, "PAAC_COMPID");
            sql += CommDAO.sql_like(data.COMPID_NAME, "COMP_NAME");
            sql += CommDAO.sql_ep(data.VENDID, "PAAC_VENDID");
            sql += CommDAO.sql_like(data.VEND_NAME, "TRAN_NAME");
            sql += CommDAO.sql_ep_date_between(data.INV_DATE, data.INV_DATE_E, "PAAC_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE, data.PAY_DATE_E, "PAAC_PAY_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE, data.PRE_DATE_E, "PAAC_DUE_DATE");
            sql += CommDAO.sql_ep(data.BANKID, "TRAN_BANKID");
            sql += CommDAO.sql_ep(data.TRAN_ACNO, "TRAN_ACNO");
            sql += CommDAO.sql_ep(data.ADDRESS, "TRAN_ADDRESS");
            sql += CommDAO.sql_ep(data.INV_NO, "PAAC_INVNO");

            return comm.DB.RunSQL(sql);
        }
        // 已付款
        public DataTable ACD120Q_2(ACD120Q_qry data)
        {
            string sql = $@"SELECT PAYD_INV_DATE as INV_DATE, PAYD_INVNO as INVNO, TRAN_NAME,
PAAC_VOUNO as VOUNO, PAAC_DUE_DATE as DUE_DATE, PAAC_NT_TOT_AMT as NT_TOT_AMT,
PAYD_NT_AMT as AMT, PAYM_DATE as PAY_DATE, PAYM_VENDID as VENDID,
ACNM_C_NAME as C_NAME, PAYM_A_USER_NM as A_USER_NM
FROM ACC_PAY_M 
LEFT JOIN ACC_PAY_D ON PAYM_COMPID=PAYD_COMPID AND PAYM_NO=PAYD_NO
LEFT JOIN ACC_PA_ACCOUNT ON PAYM_COMPID=PAAC_COMPID AND PAYM_VENDID=PAAC_VENDID AND PAYD_INVNO=PAAC_INVNO
LEFT JOIN vw_ACCD ON PAAC_COMPID=ACNM_COMPID AND PAAC_ACCD = ACNM_ID
LEFT JOIN vw_TRAIN ON PAAC_COMPID = TRAN_COMPID AND PAAC_VENDID = TRAN_ID
LEFT JOIN ACC_COMPANY ON PAAC_COMPID = COMP_ID
WHERE PAYM_VALID  = 'Y'
";
            sql += CommDAO.sql_ep(data.COMPID, "PAYM_COMPID");
            sql += CommDAO.sql_like(data.COMPID_NAME, "COMP_NAME");
            sql += CommDAO.sql_ep(data.VENDID, "PAYM_VENDID");
            sql += CommDAO.sql_like(data.VEND_NAME, "TRAN_NAME");
            sql += CommDAO.sql_ep_date_between(data.INV_DATE, data.INV_DATE_E, "PAYD_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE, data.PAY_DATE_E, "PAYM_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE, data.PRE_DATE_E, "PAAC_DUE_DATE");
            sql += CommDAO.sql_ep(data.BANKID, "TRAN_BANKID");
            sql += CommDAO.sql_ep(data.TRAN_ACNO, "TRAN_ACNO");
            sql += CommDAO.sql_ep(data.ADDRESS, "TRAN_ADDRESS");
            sql += CommDAO.sql_ep(data.INV_NO, "PAYD_INVNO");

            return comm.DB.RunSQL(sql);
        }
        // 暫不付款
        public DataTable ACD120Q_3(ACD120Q_qry data)
        {
            string sql = $@"SELECT PAAC_INV_DATE as INV_DATE, PAAC_INVNO as INVNO, TRAN_NAME,
PAAC_VOUNO as VOUNO, PAAC_DUE_DATE as DUE_DATE, PAAC_NT_TOT_AMT as NT_TOT_AMT,
PAAC_NT_TOT_AMT-PAAC_NT_BAL as AMT, PAAC_PAY_DATE as PAY_DATE, PAAC_VENDID as VENDID,
ACNM_C_NAME as C_NAME, PAAC_A_USER_NM as A_USER_NM
FROM ACC_PA_ACCOUNT  
LEFT JOIN vw_ACCD ON PAAC_COMPID = ACNM_COMPID AND PAAC_ACCD = ACNM_ID
LEFT JOIN vw_TRAIN ON PAAC_COMPID = TRAN_COMPID AND PAAC_VENDID = TRAN_ID
LEFT JOIN ACC_COMPANY ON PAAC_COMPID = COMP_ID
WHERE 1 = 1
AND PAAC_NT_BAL <> 0
AND PAAC_STS ='N'
";
            sql += CommDAO.sql_ep(data.COMPID, "PAAC_COMPID");
            sql += CommDAO.sql_like(data.COMPID_NAME, "COMP_NAME");
            sql += CommDAO.sql_ep(data.VENDID, "PAAC_VENDID");
            sql += CommDAO.sql_like(data.VEND_NAME, "TRAN_NAME");
            sql += CommDAO.sql_ep_date_between(data.INV_DATE, data.INV_DATE_E, "PAAC_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE, data.PAY_DATE_E, "PAAC_PAY_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE, data.PRE_DATE_E, "PAAC_DUE_DATE");
            sql += CommDAO.sql_ep(data.BANKID, "TRAN_BANKID");
            sql += CommDAO.sql_ep(data.TRAN_ACNO, "TRAN_ACNO");
            sql += CommDAO.sql_ep(data.ADDRESS, "TRAN_ADDRESS");
            sql += CommDAO.sql_ep(data.INV_NO, "PAAC_INVNO");

            return comm.DB.RunSQL(sql);
        }
        // 全部 
        public DataTable ACD120Q_4(ACD120Q_qry data)
        {
            string sql = $@"SELECT PAAC_INV_DATE as INV_DATE, PAAC_INVNO as INVNO, TRAN_NAME,
PAAC_VOUNO as VOUNO, PAAC_DUE_DATE as DUE_DATE, PAAC_NT_TOT_AMT as NT_TOT_AMT,
PAAC_NT_TOT_AMT-PAAC_NT_BAL as AMT, PAAC_PAY_DATE as PAY_DATE, PAAC_VENDID as VENDID,
ACNM_C_NAME as C_NAME, PAAC_A_USER_NM as A_USER_NM
FROM ACC_PA_ACCOUNT  
LEFT JOIN vw_ACCD ON PAAC_COMPID = ACNM_COMPID AND PAAC_ACCD = ACNM_ID
LEFT JOIN vw_TRAIN ON PAAC_COMPID = TRAN_COMPID AND PAAC_VENDID = TRAN_ID
LEFT JOIN ACC_COMPANY ON PAAC_COMPID = COMP_ID
WHERE 1 = 1
";
            sql += CommDAO.sql_ep(data.COMPID, "PAAC_COMPID");
            sql += CommDAO.sql_like(data.COMPID_NAME, "COMP_NAME");
            sql += CommDAO.sql_ep(data.VENDID, "PAAC_VENDID");
            sql += CommDAO.sql_like(data.VEND_NAME, "TRAN_NAME");
            sql += CommDAO.sql_ep_date_between(data.INV_DATE, data.INV_DATE_E, "PAAC_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE, data.PAY_DATE_E, "PAAC_PAY_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE, data.PRE_DATE_E, "PAAC_DUE_DATE");
            sql += CommDAO.sql_ep(data.BANKID, "TRAN_BANKID");
            sql += CommDAO.sql_ep(data.TRAN_ACNO, "TRAN_ACNO");
            sql += CommDAO.sql_ep(data.ADDRESS, "TRAN_ADDRESS");
            sql += CommDAO.sql_ep(data.INV_NO, "PAAC_INVNO");

            return comm.DB.RunSQL(sql);
        }
        // 未轉傳票
        public DataTable ACD120Q_5(ACD120Q_qry data)
        {
            string sql = $@"SELECT PAYD_INV_DATE as INV_DATE, PAYD_INVNO as INVNO, TRAN_NAME,
PAAC_VOUNO as VOUNO, PAAC_DUE_DATE as DUE_DATE, PAAC_NT_TOT_AMT as NT_TOT_AMT,
PAYD_NT_AMT as AMT, PAYM_DATE as PAY_DATE, PAYM_VENDID as VENDID,
ACNM_C_NAME as C_NAME, PAYM_A_USER_NM as A_USER_NM
FROM ACC_PAY_M 
LEFT JOIN ACC_PAY_D ON PAYM_COMPID=PAYD_COMPID AND PAYM_NO=PAYD_NO
LEFT JOIN ACC_PA_ACCOUNT ON PAYM_COMPID=PAAC_COMPID AND PAYM_VENDID=PAAC_VENDID AND PAYD_INVNO=PAAC_INVNO
LEFT JOIN vw_ACCD ON PAAC_COMPID=ACNM_COMPID AND PAAC_ACCD = ACNM_ID
LEFT JOIN vw_TRAIN ON PAAC_COMPID = TRAN_COMPID AND PAAC_VENDID = TRAN_ID
LEFT JOIN ACC_COMPANY ON PAAC_COMPID = COMP_ID
WHERE 1=1
AND PAYM_VALID  = 'Y' AND PAYM_VOUNO = ''
";
            sql += CommDAO.sql_ep(data.COMPID, "PAYM_COMPID");
            sql += CommDAO.sql_like(data.COMPID_NAME, "COMP_NAME");
            sql += CommDAO.sql_ep(data.VENDID, "PAYM_VENDID");
            sql += CommDAO.sql_like(data.VEND_NAME, "TRAN_NAME");
            sql += CommDAO.sql_ep_date_between(data.INV_DATE, data.INV_DATE_E, "PAYD_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE, data.PAY_DATE_E, "PAYM_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE, data.PRE_DATE_E, "PAAC_DUE_DATE");
            sql += CommDAO.sql_ep(data.BANKID, "TRAN_BANKID");
            sql += CommDAO.sql_ep(data.TRAN_ACNO, "TRAN_ACNO");
            sql += CommDAO.sql_ep(data.ADDRESS, "TRAN_ADDRESS");
            sql += CommDAO.sql_ep(data.INV_NO, "PAYD_INVNO");

            return comm.DB.RunSQL(sql);
        }

        public rsACD120Q_rs ACD120Q_Query(ACD120Q_qry data)
        {
            DataTable dt;
            if (data.KIND == 1)
                dt = ACD120Q_1(data);

            else if (data.KIND == 2)
                dt = ACD120Q_2(data);

            else if (data.KIND == 3)
                dt = ACD120Q_3(data);

            else if (data.KIND == 4)
                dt = ACD120Q_4(data);

            else if (data.KIND == 5)
                dt = ACD120Q_5(data);

            else
                return new rsACD120Q_rs
                {
                    result = CommDAO.getRsItem1("無效狀態:" + data.KIND)
                };

            return new rsACD120Q_rs {
                result = CommDAO.getRsItem(),
                data = dt.ToList<ACD120Q_rs>()
            };
        }

    }
}