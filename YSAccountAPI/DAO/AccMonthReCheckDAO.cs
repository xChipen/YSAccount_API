using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccMonthReCheckDAO:BaseClass
    {
        private const string tableName = "ACC_MONTH_RE_CHECK";
        private List<string> PK;

        public AccMonthReCheckDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccMonthReCheck_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccMonthReCheck_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccMonthReCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.MREK_A_USER_ID = employeeNo;
            data.MREK_A_USER_NM = name;
            data.MREK_A_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccMonthReCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("MREK_A_USER_ID");
            outSL.Add("MREK_A_USER_NM");
            outSL.Add("MREK_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccMonthReCheck_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccMonthReCheck_item data)
        {
            if (string.IsNullOrEmpty(data.MREK_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND MREK_COMPID='{data.MREK_COMPID}' ";
            sql += CommDAO.sql_ep_int(data.MREK_YEAR, "MREK_YEAR");
            sql += CommDAO.sql_ep_int(data.MREK_MONTH, "MREK_MONTH");
            sql += CommDAO.sql_ep(data.MREK_CUSTID, "MREK_CUSTID");
            sql += CommDAO.sql_ep(data.MREK_INVNO, "MREK_INVNO");

            return comm.DB.RunSQL(sql);
        }
        #endregion

    }
}