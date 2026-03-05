
// 暫不使用, 直接取用 ERP 資料

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccDeptDAO : BaseClass
    {
        private const string tableName = "ACC_DEPT";
        private List<string> PK;

        public AccDeptDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccDept data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccDept data, CommDAO dao = null)
        {
            return CommDAO._add(null, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccDept data, CommDAO dao = null)
        {
            return CommDAO._update(null, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch(AccDept data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        #endregion

        public rs Add(AccDept_ins data)
        {
            if (!isExist(data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccDept_ins data)
        {
            if (isExist(data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccDept_del data)
        {
            foreach (AccDept item in data.data)
            {
                CommDAO._delete(comm, item, tableName, PK);
            }
            return CommDAO.getRs();
        }

    }
}