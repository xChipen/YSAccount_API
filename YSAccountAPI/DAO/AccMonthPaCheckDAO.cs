using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccMonthPaCheckDAO:BaseClass
    {
        private const string tableName = "ACC_MONTH_PA_CHECK";
        private List<string> PK;

        public AccMonthPaCheckDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccMonthPaCheck_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccMonthPaCheck_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccMonthPaCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.MPAK_A_USER_ID = employeeNo;
            data.MPAK_A_USER_NM = name;
            data.MPAK_A_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccMonthPaCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("MPAK_A_USER_ID");
            outSL.Add("MPAK_A_USER_NM");
            outSL.Add("MPAK_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccMonthPaCheck_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccMonthPaCheck_item data)
        {
            if (string.IsNullOrEmpty(data.MPAK_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND MPAK_COMPID='{data.MPAK_COMPID}' ";
            sql += CommDAO.sql_ep_int(data.MPAK_YEAR, "MPAK_YEAR");
            sql += CommDAO.sql_ep_int(data.MPAK_MONTH, "MPAK_MONTH");
            sql += CommDAO.sql_ep(data.MPAK_VENDID, "MPAK_VENDID");
            sql += CommDAO.sql_ep(data.MPAK_INVNO, "MPAK_INVNO");

            return comm.DB.RunSQL(sql);
        }
        #endregion
    }
}