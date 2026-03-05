using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccCustomGroupMDAO : BaseClass2
    {
        public AccCustomGroupMDAO()
        {
            tableName = "ACC_CUSTOM_GROUP_M";
        }

        public AccCustomGroupMDAO(string tableName) : base(tableName)
        {}

        public rs AUD(AccCustomGroupM_Batch_item data, string EmployeeId, string Name)
        {
            AccCustomGroupDDAO dao = new AccCustomGroupDDAO();

            if (data.state == "A")
            {
                rs rsData = Add(new AccCustomGroupM_item { CUGM_COMPID = data.CUGM_COMPID, CUGM_CUSTID = data.CUGM_CUSTID }, EmployeeId, Name);
                if (rsData.result.retCode == 0)
                {
                    foreach (AccCustomGroupD item in data.item)
                    {
                        item.CUGD_COMPID = data.CUGM_COMPID;
                        item.CUGD_CUSTID = data.CUGM_CUSTID;
                        dao.Add(item);
                    }
                }
            }
            else if (data.state == "U")
            {
                dao.DeleteAll(new AccCustomGroupD { CUGD_COMPID = data.CUGM_COMPID, CUGD_CUSTID = data.CUGM_CUSTID});
                foreach (AccCustomGroupD item in data.item)
                {
                    item.CUGD_COMPID = data.CUGM_COMPID;
                    item.CUGD_CUSTID = data.CUGM_CUSTID;
                    dao.Add(item);
                }
            }
            else if (data.state == "D")
            {
                Delete(new AccCustomGroupM_item { CUGM_COMPID = data.CUGM_COMPID, CUGM_CUSTID = data.CUGM_CUSTID });
                dao.DeleteAll(new AccCustomGroupD { CUGD_COMPID = data.CUGM_COMPID, CUGD_CUSTID = data.CUGM_CUSTID });
            }
            else return CommDAO.getRs1();

            return CommDAO.getRs();
        }

        // 變成多型了. 在呼叫底層 Method
        public rs Add(AccCustomGroupM_item data, string EmployeeId, string Name)
        {
            if (string.IsNullOrEmpty(data.CUGM_COMPID) || string.IsNullOrEmpty(data.CUGM_CUSTID))
            {
                return CommDAO.getRs(1, "參輸入錯誤[CUGM_COMPID or CUGM_CUSTID]");
            }

            data.CUGM_A_DATE = DateTime.Now;
            data.CUGM_A_USER_ID = EmployeeId;
            data.CUGM_A_USER_NM = Name;
            data.CUGM_U_USER_ID = EmployeeId;
            data.CUGM_U_USER_NM = Name;

            return base.Add<AccCustomGroupM_item, rs>(data);
        }

        public rs Update(AccCustomGroupM_item data, string EmployeeId, string Name)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("CUGM_A_DATE");
            outSL.Add("CUGM_A_USER_ID");
            outSL.Add("CUGM_A_USER_NM");

            bool bOK = CommDAO._update(comm, data, tableName, PK, outSL);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccCustomGroupM_item data)
        {
            CommDAO._delete(comm, data, tableName, PK);

            return CommDAO.getRs();
        }

        // 查詢
        public DataTable Query(AccCustomGroupM_qry_item data)
        {
            Dictionary<string, object> P = new Dictionary<string, object>();

            string sql = $@"Select *, TRAN_NAME, TRAN_ABBR  from {tableName} 
LEFT JOIN vw_TRAIN ON CUGM_COMPID=TRAN_COMPID AND CUGM_CUSTID=TRAN_ID
WHERE 1=1";

            sql += BaseClass2.sql_ep(data.CUGM_COMPID, "CUGM_COMPID", ref P);
            sql += BaseClass2.sql_like(data.CUGM_CUSTID, "CUGM_CUSTID", ref P, "%");
            sql += BaseClass2.sql_like(data.TRAN_NAME, "TRAN_NAME", ref P, "%");
            sql += BaseClass2.sql_like(data.TRAN_ABBR, "TRAN_ABBR", ref P, "%");

            return comm.DB.RunSQL(sql, P);
        }

        public rsAccCustomGroupM_qry3 Query3(AccCustomGroupM_qry_item data)
        {
            AccCustomGroupDDAO dao = new AccCustomGroupDDAO("ACC_CUSTOM_GROUP_D");

            List<AccCustomGroupM_qry3_item> main = new List<AccCustomGroupM_qry3_item>();
            DataTable dt = Query(data);

            main = dt.ToList<AccCustomGroupM_qry3_item>();

            foreach (AccCustomGroupM_qry3_item dr in main)
            {
                DataTable body = dao.Query(dr.CUGM_COMPID, dr.CUGM_CUSTID);
                dr.accCustomGroupD = body.ToList<AccCustomGroupD>();
            };

            return new rsAccCustomGroupM_qry3 { result = CommDAO.getRsItem(), data = main};
        }

        // 查詢
        public rsAccCustomGroupM_qry Query2(AccCustomGroupM_qry_item data)
        {
            DataTable dt = Query(data);

            return new rsAccCustomGroupM_qry
            {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccCustomGroupM_qry_item>()
            };
        }
    }
}