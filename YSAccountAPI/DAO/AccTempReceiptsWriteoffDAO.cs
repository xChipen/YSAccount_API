using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;


namespace DAO
{
    public class AccTempReceiptsWriteoffDAO:BaseClass
    {
        private const string tableName = "ACC_TEMP_RECEIPTS_WRITEOFF";
        private List<string> PK;

        public AccTempReceiptsWriteoffDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccTempReceiptsWriteoff_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }

        public bool AUD(AccTempReceiptsWriteoff_item data, string employeeNo, string name, CommDAO dao = null)
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

        private bool _AddBatch(AccTempReceiptsWriteoff_item data, string employeeNo, string name, CommDAO dao = null)
        {
            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        private bool _UpdateBatch(AccTempReceiptsWriteoff_item data, string employeeNo, string name, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        private bool _DeleteBatch(AccTempReceiptsWriteoff_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(string RTPW_COMPID, string RTPW_NO)
        {
            if (string.IsNullOrEmpty(RTPW_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(RTPW_NO))
            {
                Log.Info("_QueryBatch: 無收款單號");
                return null;
            }

            string sql = $@"SELECT * from {tableName} WHERE 1=1 
AND RTPW_COMPID='{RTPW_COMPID}' ";
            sql += CommDAO.sql_ep(RTPW_NO, "RTPW_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion
        //---------------------------------------------------------------------------

        public bool DeleteAll(string RTPW_COMPID, string RTPW_NO, CommDAO dao)
        {
            string sql = $@"DELETE {tableName} WHERE RTPW_COMPID ='{RTPW_COMPID }' AND RTPW_NO ='{RTPW_NO }'";
            return dao.DB.ExecSQL_T(sql);
        }






    }
}