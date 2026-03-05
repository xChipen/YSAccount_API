using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccAssetTransDAO:BaseClass
    {
        private const string tableName = "ACC_ASSET_TRANS";
        private List<string> PK;

        public AccAssetTransDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccAssetTrans_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccAssetTrans_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccAssetTrans_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.ASTR_A_USER_ID = employeeNo;
            data.ASTR_A_USER_NM = name;
            data.ASTR_A_DATE = DateTime.Now;
            data.ASTR_U_USER_ID = employeeNo;
            data.ASTR_U_USER_NM = name;
            data.ASTR_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccAssetTrans_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.ASTR_U_USER_ID = employeeNo;
            data.ASTR_U_USER_NM = name;
            data.ASTR_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("ASTR_A_USER_ID");
            outSL.Add("ASTR_A_USER_NM");
            outSL.Add("ASTR_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccAssetTrans_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccAssetTrans_item data)
        {
            if (string.IsNullOrEmpty(data.ASTR_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.ASTR_ASETID))
            {
                Log.Info("_QueryBatch: 無請款單號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND ASTR_COMPID='{data.ASTR_COMPID}' ";
            sql += CommDAO.sql_ep(data.ASTR_ASETID, "ASTR_ASETID");

            return comm.DB.RunSQL(sql);
        }
        #endregion
    }
}