using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccPrepaidExpenseMDAO : BaseClass
    {
        private const string tableName = "ACC_PREPAID_EXPENSE_M";
        private static List<string> PK; // = CommDAO.getPK(tableName);

        public AccPrepaidExpenseMDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccPrepaidExpenseM data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccPrepaidExpenseM data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.PEXM_A_USER_ID = employeeNo;
            data.PEXM_A_USER_NM = name;
            data.PEXM_A_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccPrepaidExpenseM data, string employeeNo, string name, CommDAO dao = null)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("PEXM_A_USER_ID");
            outSL.Add("PEXM_A_USER_NM");
            outSL.Add("PEXM_A_DATE");

            return CommDAO._update(null, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccPrepaidExpenseM data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        #endregion

        public  rs Add(AccPrepaidExpenseM_ins data)
        {
            // 填入預設資料
            data.data.PEXM_A_USER_ID = data.baseRequest.employeeNo;
            data.data.PEXM_A_USER_NM = data.baseRequest.name;
            data.data.PEXM_A_DATE = DateTime.Now;

            if (!isExist(data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public  rs Update(AccPrepaidExpenseM_ins data)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("PEXM_A_USER_ID");
            outSL.Add("PEXM_A_USER_NM");
            outSL.Add("PEXM_A_DATE");

            if (isExist(data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK, outSL);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  rs Delete(AccPrepaidExpenseM_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rsAccPrepaidExpenseM_qry Query(AccPrepaidExpenseM_qry data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region Check Params
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.PEXM_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無 公司代號");
                bOK = false;
            }
            //if (string.IsNullOrEmpty(data.data.PEXM_NO))
            //{
            //    Log.Info("rsAccVoumstM_qry: 無 預付申請單號");
            //    bOK = false;
            //}

            if (!bOK)
                return new rsAccPrepaidExpenseM_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
            #endregion

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 AND PEXM_COMPID='{data.data.PEXM_COMPID}' ";

            if (!string.IsNullOrEmpty(data.data.PEXM_NO))
            {
                sql += $" AND PEXM_NO LIKE '{data.data.PEXM_NO}%'";
            }

            if (data.data.PEXM_DATE != null)
            {
                sql += $" AND convert(char(8), PEXM_DATE ,112) = '{string.Format("{0:yyyyMMdd}", data.data.PEXM_DATE)}'";
            }

            sql += CommDAO.sql_ep(data.data.PEXM_VOUNO, "PEXM_VOUNO");
            #endregion

            #region 分頁
            sql += " ORDER BY PEXM_COMPID,PEXM_NO ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccPrepaidExpenseM_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccPrepaidExpenseM>(),
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

//        public static bool Update(string PEXM_COMPID, string PEXM_NO, string VOMM_APPROVE_FLG, CommDAO dao)
//        {
//            string sql = $@"UPDATE ACC_VOUMST_M SET VOMM_APPROVE_FLG=@VOMM_APPROVE_FLG
//WHERE VOMM_COMPID=@VOMM_COMPID AND VOMM_NO=@VOMM_NO";

//            object[] obj = new object[] { VOMM_APPROVE_FLG, VOMM_COMPID, VOMM_NO };
//            return dao.DB.ExecSQL_T(sql, obj);
//        }
    }
}