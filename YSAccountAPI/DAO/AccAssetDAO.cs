using System;
using System.Collections.Generic;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccAssetDAO:BaseClass
    {
        private const string tableName = "ACC_ASSET";
        private List<string> PK;

        public AccAssetDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccAsset_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccAsset_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccAsset_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.ASET_A_USER_ID = employeeNo;
            data.ASET_A_USER_NM = name;
            data.ASET_A_DATE = DateTime.Now;
            data.ASET_U_USER_ID = employeeNo;
            data.ASET_U_USER_NM = name;
            data.ASET_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccAsset_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.ASET_U_USER_ID = employeeNo;
            data.ASET_U_USER_NM = name;
            data.ASET_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("ASET_A_USER_ID");
            outSL.Add("ASET_A_USER_NM");
            outSL.Add("ASET_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccAsset_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccAsset_item data)
        {
            if (string.IsNullOrEmpty(data.ASET_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.ASET_ID))
            {
                Log.Info("_QueryBatch: 資產代號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND ASET_COMPID='{data.ASET_COMPID}' ";
            sql += CommDAO.sql_ep(data.ASET_ID, "ASET_ID");

            return comm.DB.RunSQL(sql);
        }
        #endregion


    }
}