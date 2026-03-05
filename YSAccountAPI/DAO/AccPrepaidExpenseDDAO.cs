using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccPrepaidExpenseDDAO : BaseClass
    {
        private const string tableName = "ACC_PREPAID_EXPENSE_D";
        private List<string> PK; // = CommDAO.getPK(tableName);

        public AccPrepaidExpenseDDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccPrepaidExpenseD data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccPrepaidExpenseD data, CommDAO dao = null)
        {
            return CommDAO._add(null, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccPrepaidExpenseD data, CommDAO dao = null)
        {
            return CommDAO._update(null, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch(AccPrepaidExpenseD data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccPrepaidExpenseD data)
        {
            if (string.IsNullOrEmpty(data.PEXD_COMPID))
            {
                Log.Info("_QueryBatch: 無 公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.PEXD_NO))
            {
                Log.Info("_QueryBatch: 無 預付申請單號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND PEXD_COMPID='{data.PEXD_COMPID}' ";

            if (!string.IsNullOrEmpty(data.PEXD_NO))
            {
                sql += $" AND PEXD_NO='{data.PEXD_NO}'";
            }

            return comm.DB.RunSQL(sql);
        }

        #endregion



        public rs Add(AccPrepaidExpenseD_ins data)
        {
            if (!CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccPrepaidExpenseD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccPrepaidExpenseD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public bool DeleteAll(string PEXD_COMPID, string PEXD_NO, CommDAO dao = null)
        {
            string sql = $"DELETE {tableName} where PEXD_COMPID=@PEXD_COMPID AND PEXD_NO=@PEXD_NO";
            object[] obj = new object[] { PEXD_COMPID, PEXD_NO };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rsAccPrepaidExpenseD_qry Query(AccPrepaidExpenseD_ins data)
        {
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.PEXD_COMPID))
            {
                Log.Info("rsAccPrepaidExpenseD_qry: 無公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.PEXD_NO))
            {
                Log.Info("rsAccPrepaidExpenseD_qry: 無 預付申請單號");
                bOK = false;
            }

            if (!bOK)
                return new rsAccPrepaidExpenseD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };

            DataTable dt = _QueryBatch(data.data);
            if (dt != null && dt.Rows.Count != 0)
            {
                return new rsAccPrepaidExpenseD_qry()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = dt.ToList<AccPrepaidExpenseD>()
                };
            }

            return new rsAccPrepaidExpenseD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
        }
    }
}