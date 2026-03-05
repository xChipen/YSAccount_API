using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccCustomGroupDDAO:BaseClass2
    {
        public AccCustomGroupDDAO()
        {
            tableName = "ACC_CUSTOM_GROUP_D";
        }
        public AccCustomGroupDDAO(string tableName):base(tableName)
        {}

        // 變成多型了. 在呼叫底層 Method
        public rs Add(AccCustomGroupD data)
        {
            if (string.IsNullOrEmpty(data.CUGD_COMPID) || string.IsNullOrEmpty(data.CUGD_CUSTID) || data.CUGD_SEQ == null)
            {
                return CommDAO.getRs(1, "參輸入錯誤[CUGD_COMPID or CUGD_CUSTID or CUGD_SEQ]");
            }

            //            if (isExist(data) == false)
            return base.Add<AccCustomGroupD, rs>(data);
        }

        public rs Update(AccCustomGroupD data)
        {
            bool? bExist = isExist(data);
            if ( bExist == null ) return CommDAO.getRs(1, "Primary Key 不存在");

            if ( (bool)bExist )
            {
                bool bOK = CommDAO._update(comm, data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccCustomGroupD data)
        {
            CommDAO._delete(comm, data, tableName, PK);

            return CommDAO.getRs();
        }

        public rs DeleteAll(AccCustomGroupD data)
        {
            CommDAO._delete(comm, data, tableName, new List<string> { "CUGD_COMPID", "CUGD_CUSTID" });

            return CommDAO.getRs();
        }

        // 查詢
        public DataTable Query(string COMPID, string CUSTID)
        {
            Dictionary<string, object> P = new Dictionary<string, object>();

            string sql = $@"Select *  from {tableName} WHERE 1=1";

            sql += BaseClass2.sql_ep(COMPID, "CUGD_COMPID", ref P);
            sql += BaseClass2.sql_like(CUSTID, "CUGD_CUSTID", ref P, "%");

            return comm.DB.RunSQL(sql, P);
        }

    }
}