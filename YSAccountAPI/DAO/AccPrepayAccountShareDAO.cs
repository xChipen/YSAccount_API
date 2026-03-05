using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccPrepayAccountShareDAO : BaseClass
    {
        private const string tableName = "ACC_PREPAY_ACCOUNT_SHARE";
        private List<string> PK; // = CommDAO.getPK(tableName);

        public AccPrepayAccountShareDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public  bool isExist(AccPrepayAccountShare data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccPrepayAccountShare data, CommDAO dao = null)
        {
            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccPrepayAccountShare data, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch(AccPrepayAccountShare data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccPrepayAccountShare data)
        {
            if (string.IsNullOrEmpty(data.PRAS_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.PRAS_NO))
            {
                Log.Info("rsAccVoumstM_qry: 無傳票號碼");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND PRAS_COMPID='{data.PRAS_COMPID}' ";

            if (!string.IsNullOrEmpty(data.PRAS_NO))
            {
                sql += $" AND PRAS_NO='{data.PRAS_NO}'";
            }

            return comm.DB.RunSQL(sql);
        }

        #endregion



        public  rs Add(AccPrepayAccountShare_ins data)
        {
            if (!CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public  rs Update(AccPrepayAccountShare_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  rs Delete(AccPrepayAccountShare_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public bool DeleteAll(string PRAS_COMPID, string PRAS_NO, CommDAO dao = null)
        {
            string sql = $"DELETE {tableName} where PRAS_COMPID=@PRAS_COMPID AND PRAS_NO=@PRAS_NO";
            object[] obj = new object[] { PRAS_COMPID, PRAS_NO };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rsAccPrepayAccountShare_qry Query(AccPrepayAccountShare_qry data)
        {
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.PRAS_COMPID))
            {
                Log.Info("rsAccPrepayAccountShare_qry: 無公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.PRAS_NO))
            {
                Log.Info("rsAccPrepayAccountShare_qry: 無傳票號碼");
                bOK = false;
            }

            if (!bOK)
                return new rsAccPrepayAccountShare_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };

            DataTable dt = _QueryBatch(data.data);
            if (dt != null && dt.Rows.Count != 0)
            {
                return new rsAccPrepayAccountShare_qry()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = dt.ToList<AccPrepayAccountShare>()
                };
            }

            return new rsAccPrepayAccountShare_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
        }
    }
}