using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccPrepayAccountDDAO : BaseClass
    {
        private const string tableName = "ACC_PREPAY_ACCOUNT_D";
        private List<string> PK; 

        public AccPrepayAccountDDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccPrepayAccountD_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccPrepayAccountD_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public  bool _AddBatch(AccPrepayAccountD_item data, CommDAO dao = null)
        {
            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public  bool _UpdateBatch(AccPrepayAccountD_item data, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        public  bool _DeleteBatch(AccPrepayAccountD_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccPrepayAccountD_item data)
        {
            #region check
            if (string.IsNullOrEmpty(data.PRAD_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.PRAD_NO))
            {
                Log.Info("rsAccVoumstM_qry: 無傳票號碼");
                return null;
            }
            #endregion

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND PRAD_COMPID='{data.PRAD_COMPID}' ";
            sql += CommDAO.sql_ep(data.PRAD_NO, "PRAD_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion



        public  rs Add(AccPrepayAccountD_ins data)
        {
            if (!CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public  rs Update(AccPrepayAccountD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  rs Delete(AccPrepayAccountD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public bool DeleteAll(string PRAD_COMPID, string PRAD_NO, CommDAO dao = null)
        {
            string sql = $"DELETE {tableName} where PRAD_COMPID=@PRAD_COMPID AND PRAD_NO=@PRAD_NO";
            object[] obj = new object[] { PRAD_COMPID, PRAD_NO };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public bool DeleteAll2(string PRAD_COMPID, string VOMD_NO, CommDAO dao = null)
        {
            string sql = $@"DELETE {tableName} where PRAD_NO in 
(
select VOMD_INVNO from ACC_VOUMST_D where VOMD_COMPID = '{PRAD_COMPID}' 
and VOMD_NO = '{VOMD_NO}' and VOMD_INVNO <> ''
)";
            if (dao == null)
                return comm.DB.ExecSQL(sql);
            else
                return dao.DB.ExecSQL_T(sql);
        }

        public rsAccPrepayAccountD_qry Query(AccPrepayAccountD_qry data)
        {
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.PRAD_COMPID))
            {
                Log.Info("rsAccPrepayAccountD_qry: 無公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.PRAD_NO))
            {
                Log.Info("rsAccPrepayAccountD_qry: 無傳票號碼");
                bOK = false;
            }

            if (!bOK)
                return new rsAccPrepayAccountD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };

            DataTable dt = _QueryBatch(data.data);
            if (dt != null && dt.Rows.Count != 0)
            {
                return new rsAccPrepayAccountD_qry()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = dt.ToList<AccPrepayAccountD>()
                };
            }

            return new rsAccPrepayAccountD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
        }
    }
}