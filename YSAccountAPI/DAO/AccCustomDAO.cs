using Models;
using System.Collections.Generic;

namespace DAO
{
    public class AccCustomDAO : BaseClass
    {
        private const string tableName = "ACC_CUSTOM";
        private List<string> PK;

        public AccCustomDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(CommDAO dao, AccCustom data)
        {
            return CommDAO.isExist(dao, data, tableName, PK);
        }
        public bool _AddBatch(AccCustom data, CommDAO dao = null)
        {
            return CommDAO._add(null, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccCustom data, CommDAO dao = null)
        {
            return CommDAO._update(null, data, tableName, PK, null, dao);
        }
        public bool _DeleteBatch(AccCustom data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        #endregion

        public rs Add(AccCustom_ins data)
        {
            if (!isExist(comm, data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccCustom_ins data)
        {
            if (isExist(comm, data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccCustom_del data)
        {
            foreach (AccCustom item in data.data)
            {
                CommDAO._delete(comm, item, tableName, PK);
            }
            return CommDAO.getRs();
        }


    }
}