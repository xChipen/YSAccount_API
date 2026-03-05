using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccAssetDeprecDAO:BaseClass
    {
        private const string tableName = "ACC_ALLOWANCE_M";
        private List<string> PK;

        public AccAssetDeprecDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccAssetDeprec_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccAssetDeprec_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccAssetDeprec_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.ASDE_A_USER_ID = employeeNo;
            data.ASDE_A_USER_NM = name;
            data.ASDE_A_DATE = DateTime.Now;
            data.ASDE_U_USER_ID = employeeNo;
            data.ASDE_U_USER_NM = name;
            data.ASDE_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccAssetDeprec_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.ASDE_U_USER_ID = employeeNo;
            data.ASDE_U_USER_NM = name;
            data.ASDE_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("ASDE_A_USER_ID");
            outSL.Add("ASDE_A_USER_NM");
            outSL.Add("ASDE_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccAssetDeprec_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccAssetDeprec_item data)
        {
            if (string.IsNullOrEmpty(data.ASDE_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND ASDE_COMPID='{data.ASDE_COMPID}' ";
            sql += CommDAO.sql_ep_int((int)data.ASDE_YEAR, "ASDE_YEAR");
            sql += CommDAO.sql_ep_int((int)data.ASDE_MONTH, "ASDE_MONTH");
            sql += CommDAO.sql_ep(data.ASDE_ASETID, "ASDE_ASETID");

            return comm.DB.RunSQL(sql);
        }
        #endregion
    }
}