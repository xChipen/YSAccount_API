using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccCheckCounterDAO:BaseClass
    {
        private const string tableName = "ACC_CHECK_COUNTER";
        private List<string> PK; // =  CommDAO.getPK(comm, tableName);

        public AccCheckCounterDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        public bool isExist(AccCheckCounter_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }

        public rs Add(AccCheckCounter_item data, string employeeNo, string name)
        {
            // 填入預設資料
            data.CKCN_A_USER_ID = employeeNo;
            data.CKCN_A_USER_NM = name;
            data.CKCN_A_DATE = DateTime.Now;
            data.CKCN_U_USER_ID = employeeNo;
            data.CKCN_U_USER_NM = name;
            data.CKCN_U_DATE = DateTime.Now;

            data.CKCN_SEQ = getACE010M_CKCN_SEQ(data);

            if (!isExist(data))
            {
                bool bOK = CommDAO._add(comm, data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccCheckCounter_item data, string employeeNo, string name)
        {
            // 填入預設資料
            data.CKCN_U_USER_ID = employeeNo;
            data.CKCN_U_USER_NM = name;
            data.CKCN_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("CKCN_A_USER_ID");
            outSL.Add("CKCN_A_USER_NM");
            outSL.Add("CKCN_A_DATE");

            if (isExist(data))
            {
                bool bOK = CommDAO._update(comm, data, tableName, PK, outSL);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccCheckCounter_item data)
        {
            if (CommDAO.isExist(comm, data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rsAccCheckCounter_qry Query(AccCheckCounter_ins data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region Check Params
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.CKCN_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                bOK = false;
            }
            if (!bOK)
                return new rsAccCheckCounter_qry() { result = CommDAO.getRsItem(1, "失敗") };
            #endregion

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 AND CKCN_COMPID='{data.data.CKCN_COMPID}' ";

            if (!string.IsNullOrEmpty(data.data.CKCN_ACCD))
                sql += $" AND CKCN_ACCD LIKE '{data.data.CKCN_ACCD}%'";
            #endregion

            #region 分頁
            sql += " ORDER BY CKCN_COMPID,CKCN_ACCD ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccCheckCounter_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccCheckCounter>(),
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        public rsAccCheckCounter_qry Query2(string CKCN_COMPID)
        {
            #region Check Params
            if (string.IsNullOrEmpty(CKCN_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                return new rsAccCheckCounter_qry() { result = CommDAO.getRsItem(1, "查無公司代號") };
            }
            #endregion

            #region SQL
            string sql = $@"select TOP 1 *
             from ACC_CHECK_COUNTER
             WHERE CKCN_CUNO <= CKCN_ENNO
             AND CKCN_COMPID = '{CKCN_COMPID}'
             ORDER BY CKCN_A_DATE";
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccCheckCounter_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccCheckCounter>()
            };
        }


        public rsACE010M_qryAll ACE010M_qry(AccCheckCounter data) {
            string sql = $@"SELECT * FROM ACC_CHECK_COUNTER
LEFT JOIN vw_ACCD ON CKCN_COMPID=ACNM_COMPID AND CKCN_ACCD=ACNM_ID
WHERE 1=1";
            sql += CommDAO.sql_ep(data.CKCN_COMPID, "CKCN_COMPID");
            sql += CommDAO.sql_like(data.CKCN_ACCD, "CKCN_ACCD");

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsACE010M_qryAll {
                result = CommDAO.getRsItem(),
                data = dt.ToList<ACE010M_qryAll>()
            };
        }

        public Int16? getACE010M_CKCN_SEQ(AccCheckCounter data)
        {
            string sql = $@"SELECT MAX(CKCN_SEQ) as NO FROM ACC_CHECK_COUNTER
WHERE 1 = 1
";
            sql += CommDAO.sql_ep(data.CKCN_COMPID, "CKCN_COMPID");
            sql += CommDAO.sql_ep(data.CKCN_ACCD, "CKCN_ACCD");

            DataTable dt = comm.DB.RunSQL(sql);
            if (string.IsNullOrEmpty(dt.Rows[0]["NO"].ToString()))
                return 1;
            else
                return (Int16?)((Int16)dt.Rows[0]["NO"] + 1);
        }


    }
}