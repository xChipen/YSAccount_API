using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccExpenseReceiptMDAO : BaseClass
    {
        private const string tableName = "ACC_EXPENSE_RECEIPT_M";
        private List<string> PK; // = CommDAO.getPK(tableName);

        public AccExpenseReceiptMDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccExpenseReceiptM data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public  bool _AddBatch(AccExpenseReceiptM data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.EXRM_A_USER_ID = employeeNo;
            data.EXRM_A_USER_NM = name;
            data.EXRM_A_DATE = DateTime.Now;

            return CommDAO._add(null, data, tableName, PK, dao);
        }
        public  bool _UpdateBatch(AccExpenseReceiptM data, string employeeNo, string name, CommDAO dao = null)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("EXRM_A_USER_ID");
            outSL.Add("EXRM_A_USER_NM");
            outSL.Add("EXRM_A_DATE");

            return CommDAO._update(null, data, tableName, PK, outSL, dao);
        }
        public  bool _DeleteBatch(AccExpenseReceiptM data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        #endregion

        public rs Add(AccExpenseReceiptM_ins data)
        {
            // 填入預設資料
            data.data.EXRM_A_USER_ID = data.baseRequest.employeeNo;
            data.data.EXRM_A_USER_NM = data.baseRequest.name;
            data.data.EXRM_A_DATE = DateTime.Now;

            if (!isExist(data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccExpenseReceiptM_ins data)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("EXRM_A_USER_ID");
            outSL.Add("EXRM_A_USER_NM");
            outSL.Add("EXRM_A_DATE");

            if (isExist(data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK, outSL);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccExpenseReceiptM_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  rsAccExpenseReceiptM_qry Query(AccExpenseReceiptM_qry data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region Check Params
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.EXRM_COMPID))
            {
                Log.Info("rsAccExpenseReceiptM_qry: 無 公司代號");
                bOK = false;
            }
            //if (string.IsNullOrEmpty(data.data.EXRM_NO))
            //{
            //    Log.Info("rsAccExpenseReceiptM_qry: 無 費用申請單號");
            //    bOK = false;
            //}
            if (!bOK)
                return new rsAccExpenseReceiptM_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
            #endregion

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 AND EXRM_COMPID='{data.data.EXRM_COMPID}' ";

            if (!string.IsNullOrEmpty(data.data.EXRM_NO))
            {
                sql += $" AND EXRM_NO LIKE '{data.data.EXRM_NO}%'";
            }

            if (data.data.EXRM_DATE != null)
            {
                sql += $" AND convert(char(8), EXRM_DATE ,112) = '{string.Format("{0:yyyyMMdd}", data.data.EXRM_DATE)}'";
            }

            sql += CommDAO.sql_ep(data.data.EXRM_VOUNO, "EXRM_VOUNO");
            #endregion

            #region 分頁
            sql += " ORDER BY EXRM_COMPID,EXRM_NO ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccExpenseReceiptM_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccExpenseReceiptM>(),
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

//        public static bool Update(string VOMM_COMPID, string VOMM_NO, string VOMM_APPROVE_FLG, CommDAO dao)
//        {
//            string sql = $@"UPDATE ACC_VOUMST_M SET VOMM_APPROVE_FLG=@VOMM_APPROVE_FLG
//WHERE VOMM_COMPID=@VOMM_COMPID AND VOMM_NO=@VOMM_NO";

//            object[] obj = new object[] { VOMM_APPROVE_FLG, VOMM_COMPID, VOMM_NO };
//            return dao.DB.ExecSQL_T(sql, obj);
//        }
    }
}