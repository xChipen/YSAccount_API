using Models;
using System.Collections.Generic;

namespace DAO
{
    public class AccUserDAO : BaseClass
    {
        private const string tableName = "ACC_USER";
        private List<string> PK; // = CommDAO.getPK(tableName);

        public AccUserDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public  bool isExist(AccUser data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccUser data, CommDAO dao = null)
        {
            return CommDAO._add(null, data, tableName, PK, dao);
        }
        public  bool _UpdateBatch(AccUser data, CommDAO dao = null)
        {
            return CommDAO._update(null, data, tableName, PK, null, dao);
        }
        public  bool _DeleteBatch(AccUser data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        #endregion

        public  rs Add(AccUser_ins data)
        {
            if (!isExist(data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public  rs Update(AccUser_ins data)
        {
            if (isExist(data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccUser_del data)
        {
            foreach (AccUser item in data.data)
            {
                CommDAO._delete(comm, item, tableName, PK);
            }
            return CommDAO.getRs();
        }
    }
}