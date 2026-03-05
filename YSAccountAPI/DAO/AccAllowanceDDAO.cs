using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccAllowanceDDAO:BaseClass
    {
        private const string tableName = "ACC_ALLOWANCE_D";
        private List<string> PK;

        public AccAllowanceDDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccAllowanceD_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccAllowanceD_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccAllowanceD_item data, string employeeNo, string name, CommDAO dao = null)
        {
            data.ALLD_INV_AMT = data.ALLD_NET_AMT + data.ALLD_TAX_AMT;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccAllowanceD_item data, string employeeNo, string name, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch(AccAllowanceD_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccAllowanceD_item data)
        {
            if (string.IsNullOrEmpty(data.ALLD_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.ALLD_NO))
            {
                Log.Info("_QueryBatch: 無請款單號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND ALLD_COMPID='{data.ALLD_COMPID}' ";
            sql += CommDAO.sql_ep(data.ALLD_NO, "ALLD_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion

        public bool _DeleteAll(string ALLD_COMPID, string ALLD_NO)
        {
            string sql = $@"DELETE {tableName} WHERE ALLD_COMPID='{ALLD_COMPID}' AND ALLD_NO='{ALLD_NO}'";
            return comm.DB.ExecSQL(sql);
        }
    }
}