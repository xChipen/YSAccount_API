using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccVouCounterDAO:BaseClass
    {
        private const string tableName = "ACC_VOU_COUNTER";
        private List<string> PK;

        public AccVouCounterDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        public string getNO(string VCNT_COMPID, DateTime? VCNT_DATE, out int iNO)
        {
            iNO = 0;
            if (string.IsNullOrEmpty(VCNT_COMPID)) return "";
            if (VCNT_DATE == null) return "";

            DataTable dt = _QueryBatch(new AccVouCounter_item {
                VCNT_COMPID = VCNT_COMPID,
                VCNT_DATE = VCNT_DATE
            });

            string NO = string.Format("{0:yyyyMMdd}", VCNT_DATE);

            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["VCNT_NO"] != null)
                {
                    iNO = (int)dt.Rows[0]["VCNT_NO"] + 1;
                    NO += iNO.ToString("0000");
                }
                else
                {
                    iNO = 1;
                    NO += "0001";
                }
            }
            else
            {
                iNO = 1;
                NO += "0001";
            }

            return NO;
        }

        #region Batch call

        public bool AUD(AccVouCounter_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccVouCounter_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccVouCounter_item data, CommDAO dao = null)
        {
            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccVouCounter_item data, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch(AccVouCounter_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccVouCounter_item data)
        {
            #region check
            if (string.IsNullOrEmpty(data.VCNT_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (data.VCNT_DATE == null)
            {
                Log.Info("_QueryBatch: 無傳票號碼");
                return null;
            }
            #endregion

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND VCNT_COMPID='{data.VCNT_COMPID}' ";
            sql += CommDAO.sql_ep_date(data.VCNT_DATE, "VCNT_DATE");

            return comm.DB.RunSQL(sql);
        }

        #endregion

    }
}