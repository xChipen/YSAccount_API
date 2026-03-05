using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccReceiveCheckDAO:BaseClass
    {
        private const string tableName = "ACC_RECEIVE_CHECK";
        private List<string> PK;

        public AccReceiveCheckDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccReceiveCheck_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }

        public bool AUD(AccReceiveCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            switch (data.State)
            {
                case "A": return _AddBatch(data, dao);
                case "U": return _UpdateBatch(data, dao);
                case "D": return _DeleteBatch(data, dao);
                case "": return true;
            }
            return false;
        }

        private bool _AddBatch(AccReceiveCheck_item data, CommDAO dao = null)
        {
            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        private bool _UpdateBatch(AccReceiveCheck_item data, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        private bool _DeleteBatch(AccReceiveCheck_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(string REAK_COMPID, string REAK_NO)
        {
            if (string.IsNullOrEmpty(REAK_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(REAK_NO))
            {
                Log.Info("_QueryBatch: 無收款單號");
                return null;
            }

            string sql = $@"SELECT * from {tableName} WHERE 1=1 
AND REAK_COMPID='{REAK_COMPID}' ";
            sql += CommDAO.sql_ep(REAK_NO, "REAK_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion


        public bool DeleteAll(string REAK_COMPID, string REAK_NO, CommDAO dao)
        {
            string sql = $@"DELETE {tableName} WHERE REAK_COMPID='{REAK_COMPID}' AND REAK_NO='{REAK_NO}'";
            return dao.DB.ExecSQL_T(sql);
        }


    }
}