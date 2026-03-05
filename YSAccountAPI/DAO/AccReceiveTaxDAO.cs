using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccReceiveTaxDAO : BaseClass
    {
        private const string tableName = "ACC_RECEIVE_TAX";
        private static List<string> PK; 

        public AccReceiveTaxDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccReceiveTax_item data, string employeeNo, string name, CommDAO dao = null)
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
        public bool _AddBatch(AccReceiveTax_item data, CommDAO dao = null)
        {
            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccReceiveTax_item data, CommDAO dao = null)
        {
            return CommDAO._update(comm, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch(AccReceiveTax_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch2(string COMPID, string NO, CommDAO dao)
        {
            string sql = $@"DELETE ACC_RECEIVE_TAX 
where RECT_COMPID='{COMPID}' AND RECT_NO='{NO}'";
            return dao.DB.ExecSQL_T(sql);
        }
        public DataTable _QueryBatch(string RECT_COMPID, string RECT_NO)
        {
            #region check
            if (string.IsNullOrEmpty(RECT_COMPID))
            {
                Log.Info("rsAccReceiveTax_qry: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(RECT_NO))
            {
                Log.Info("rsAccReceiveTax_qry: 無傳票號碼");
                return null;
            }
            #endregion

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND RECT_COMPID='{RECT_COMPID}' ";
            sql += CommDAO.sql_ep(RECT_NO, "RECT_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion

        public rs Add(AccReceiveTax_ins data)
        {
            if (!CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccReceiveTax_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccReceiveTax_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public bool DeleteAll(string RECT_COMPID, string RECT_NO, CommDAO dao = null)
        {
            string sql = $"DELETE {tableName} where RECT_COMPID=@RECT_COMPID AND RECT_NO=@VOMT_NO";
            object[] obj = new object[] { RECT_COMPID, RECT_NO };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rsAccReceiveTax_qry Query(AccReceiveTax_qry data)
        {
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.RECT_COMPID))
            {
                Log.Info("rsAccReceiveM_qry: 無公司代號");
                bOK = false;
            }
            if (string.IsNullOrEmpty(data.data.RECT_NO))
            {
                Log.Info("rsAccReceiveM_qry: 無傳票號碼");
                bOK = false;
            }

            if (!bOK)
                return new rsAccReceiveTax_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };

            DataTable dt = _QueryBatch(data.data.RECT_COMPID, data.data.RECT_NO);
            if (dt != null && dt.Rows.Count != 0)
            {
                return new rsAccReceiveTax_qry()
                {
                    result = new rsItem() { retCode = 0, retMsg = "成功" },
                    data = dt.ToList<AccReceiveTax>()
                };
            }

            return new rsAccReceiveTax_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
        }
    }
}