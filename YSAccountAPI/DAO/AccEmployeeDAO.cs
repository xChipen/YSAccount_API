using Models;
using System.Collections.Generic;

namespace DAO
{
    public class AccEmployeeDAO : BaseClass
    {
        private const string tableName = "ACC_EMPLOYEE";
        private List<string> PK; // = CommDAO.getPK(tableName);

        public AccEmployeeDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccEmployee data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public  bool _AddBatch(AccEmployee data, CommDAO dao = null)
        {
            return CommDAO._add(null, data, tableName, PK, dao);
        }
        public  bool _UpdateBatch(AccEmployee data, CommDAO dao = null)
        {
            return CommDAO._update(null, data, tableName, PK, null, dao);
        }
        public  bool _DeleteBatch(AccEmployee data, CommDAO dao = null)
        {
            return CommDAO._delete(null, data, tableName, PK, null, dao);
        }
        #endregion

        public rs Add(AccEmployee_ins data)
        {
            if (!isExist(data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccEmployee_ins data)
        {
            if (isExist(data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  rs Delete(AccEmployee_del data)
        {
            foreach (AccEmployee item in data.data)
            {
                CommDAO._delete(comm, item, tableName, PK);
            }
            return CommDAO.getRs();
        }
    }
}