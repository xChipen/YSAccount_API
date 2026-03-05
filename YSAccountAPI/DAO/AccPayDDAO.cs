using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccPayDDAO:BaseClass
    {
        private const string tableName = "ACC_PAY_D";
        private List<string> PK; 

        public AccPayDDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccPayD data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccPayD data, CommDAO dao = null)
        {
            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccPayD data, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch(AccPayD data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccPayD data)
        {
            if (string.IsNullOrEmpty(data.PAYD_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.PAYD_NO))
            {
                Log.Info("rsAccVoumstM_qry: 無傳票號碼");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND PAYD_COMPID='{data.PAYD_COMPID}' ";

            if (!string.IsNullOrEmpty(data.PAYD_NO))
            {
                sql += $" AND PAYD_NO='{data.PAYD_NO}'";
            }

            return comm.DB.RunSQL(sql);
        }
        #endregion

        public rs Add(AccPayD_ins data)
        {
            if (!CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccPayD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccPayD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public bool DeleteAll(string PAYD_COMPID, string PAYD_NO, CommDAO dao = null)
        {
            string sql = $"DELETE {tableName} where PAYD_COMPID=@VOMD_COMPID AND PAYD_NO=@VOMD_NO";
            object[] obj = new object[] { PAYD_COMPID, PAYD_NO };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rsAccPayD_qry Query(AccPayD_qry data)
        {
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.PAYD_COMPID))
            {
                Log.Info("rsAccPayD_qry: 無公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.PAYD_NO))
            {
                Log.Info("rsAccPayD_qry: 無傳票號碼");
                bOK = false;
            }

            if (!bOK)
                return new rsAccPayD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };

            DataTable dt = _QueryBatch(data.data);
            if (dt != null && dt.Rows.Count != 0)
            {
                return new rsAccPayD_qry()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = dt.ToList<AccPayD>()
                };
            }

            return new rsAccPayD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
        }
    }
}