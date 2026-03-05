using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccVoumstDDAO : BaseClass
    {
        private const string tableName = "ACC_VOUMST_D";
        private List<string> PK;

        public AccVoumstDDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccVoumstD_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccVoumstD_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public  bool _AddBatch(AccVoumstD_item data, CommDAO dao = null)
        {
            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public  bool _UpdateBatch(AccVoumstD_item data, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        public  bool _DeleteBatch(AccVoumstD_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch2(string COMPID, string NO, CommDAO dao)
        {
            string sql = $@"DELETE ACC_VOUMST_D 
where VOMD_COMPID='{COMPID}' AND VOMD_NO='{NO}'";
            return dao.DB.ExecSQL_T(sql);
        }
        public  DataTable _QueryBatch(AccVoumstD_item data)
        {
            #region check
            if (string.IsNullOrEmpty(data.VOMD_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.VOMD_NO))
            {
                Log.Info("rsAccVoumstM_qry: 無傳票號碼");
                return null;
            }
            #endregion

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND VOMD_COMPID='{data.VOMD_COMPID}' ";
            sql += CommDAO.sql_ep(data.VOMD_NO, "VOMD_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion



        public  rs Add(AccVoumstD_ins data)
        {
            if (!CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public  rs Update(AccVoumstD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  rs Delete(AccVoumstD_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  bool DeleteAll(string VOMD_COMPID, string VOMD_NO, CommDAO dao = null)
        {
            string sql = $"DELETE {tableName} where VOMD_COMPID=@VOMD_COMPID AND VOMD_NO=@VOMD_NO";
            object[] obj = new object[] { VOMD_COMPID, VOMD_NO };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rsAccVoumstD_qry Query(AccVoumstD_qry data)
        {
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.VOMD_COMPID))
            {
                Log.Info("rsAccVoumstD_qry: 無公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.VOMD_NO))
            {
                Log.Info("rsAccVoumstD_qry: 無傳票號碼");
                bOK = false;
            }

            if (!bOK)
                return new rsAccVoumstD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };

            DataTable dt = _QueryBatch(data.data);
            if (dt!= null && dt.Rows.Count != 0)
            {
                return new rsAccVoumstD_qry()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = dt.ToList<AccVoumstD>()
                };
            }

            return new rsAccVoumstD_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
        }
    }
}