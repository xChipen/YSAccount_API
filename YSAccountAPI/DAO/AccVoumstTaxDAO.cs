using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccVoumstTaxDAO : BaseClass
    {
        private const string tableName = "ACC_VOUMST_TAX";
        private static List<string> PK; // = CommDAO.getPK(tableName);

        public AccVoumstTaxDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccVoumstTax_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccVoumstTax_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public  bool _AddBatch(AccVoumstTax_item data, CommDAO dao = null)
        {
            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public  bool _UpdateBatch(AccVoumstTax_item data, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch(AccVoumstTax_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch2(string COMPID, string NO, CommDAO dao)
        {
            string sql = $@"DELETE ACC_VOUMST_TAX 
where VOMT_COMPID='{COMPID}' AND VOMT_NO='{NO}'";
            return dao.DB.ExecSQL_T(sql);
        }
        public DataTable _QueryBatch(AccVoumstTax_item data)
        {
            #region check
            if (string.IsNullOrEmpty(data.VOMT_COMPID))
            {
                Log.Info("rsAccVoumstTax_qry: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.VOMT_NO))
            {
                Log.Info("rsAccVoumstTax_qry: 無傳票號碼");
                return null;
            }
            #endregion

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND VOMT_COMPID='{data.VOMT_COMPID}' ";
            sql += CommDAO.sql_ep(data.VOMT_NO, "VOMT_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion



        public  rs Add(AccVoumstTax_ins data)
        {
            if (!CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public  rs Update(AccVoumstTax_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  rs Delete(AccVoumstTax_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public bool DeleteAll(string VOMT_COMPID, string VOMT_NO, CommDAO dao = null)
        {
            string sql = $"DELETE {tableName} where VOMT_COMPID=@VOMT_COMPID AND VOMT_NO=@VOMT_NO";
            object[] obj = new object[] { VOMT_COMPID, VOMT_NO };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rsAccVoumstTax_qry Query(AccVoumstTax_qry data)
        {
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.VOMT_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.VOMT_NO))
            {
                Log.Info("rsAccVoumstM_qry: 無傳票號碼");
                bOK = false;
            }

            if (!bOK)
                return new rsAccVoumstTax_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };

            DataTable dt = _QueryBatch(data.data);
            if (dt != null && dt.Rows.Count != 0)
            {
                return new rsAccVoumstTax_qry()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = dt.ToList<AccVoumstTax>()
                };
            }

            return new rsAccVoumstTax_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
        }
    }
}