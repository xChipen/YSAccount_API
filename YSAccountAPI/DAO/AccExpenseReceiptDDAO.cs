using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccExpenseReceiptDDAO : BaseClass
    {
        private const string tableName = "ACC_EXPENSE_RECEIPT_D";
        private List<string> PK; // = CommDAO.getPK(tableName);

        public AccExpenseReceiptDDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccExpenseReceiptD data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public  bool _AddBatch(AccExpenseReceiptD data, CommDAO dao = null)
        {
            return CommDAO._add(null, data, tableName, PK, dao);
        }
        public  bool _UpdateBatch(AccExpenseReceiptD data, CommDAO dao = null)
        {
            return CommDAO._update(null, data, tableName, PK, null, dao);
        }
        public  bool _DeleteBatch(AccExpenseReceiptD data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        public  DataTable _QueryBatch(AccExpenseReceiptD data)
        {
            if (string.IsNullOrEmpty(data.EXRD_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無 公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.EXRD_NO))
            {
                Log.Info("rsAccVoumstM_qry: 無 費用申請單號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND EXRD_COMPID='{data.EXRD_COMPID}' ";

            if (!string.IsNullOrEmpty(data.EXRD_NO))
            {
                sql += $" AND EXRD_NO='{data.EXRD_NO}'";
            }

            return comm.DB.RunSQL(sql);
        }

        #endregion



        public rs Add(AccExpenseReceiptD_ins data)
        {
            if (!CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccExpenseReceiptD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccExpenseReceiptD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  bool DeleteAll(string EXRD_COMPID, string EXRD_NO, CommDAO dao = null)
        {
            string sql = $"DELETE {tableName} where EXRD_COMPID=@EXRD_COMPID AND EXRD_NO=@EXRD";
            object[] obj = new object[] { EXRD_COMPID, EXRD_NO };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rsAccExpenseReceiptD_qry Query(AccExpenseReceiptD_ins data)
        {
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.EXRD_COMPID))
            {
                Log.Info("rsAccExpenseReceiptD_qry: 無 公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.EXRD_NO))
            {
                Log.Info("rsAccExpenseReceiptD_qry: 無 費用申請單號");
                bOK = false;
            }

            if (!bOK)
                return new rsAccExpenseReceiptD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };

            DataTable dt = _QueryBatch(data.data);
            if (dt != null && dt.Rows.Count != 0)
            {
                return new rsAccExpenseReceiptD_qry()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = dt.ToList<AccExpenseReceiptD>()
                };
            }

            return new rsAccExpenseReceiptD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
        }

    }
}