using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccAssetParmDAO:BaseClass
    {
        private const string tableName = "ACC_ASSET_PARM";
        private List<string> PK;

        public AccAssetParmDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public rs AddAndUpdate(AccAssetParm_item data, string employeeNo, string name)
        {
            if (AUD(data, employeeNo, name))
                return CommDAO.getRs();

            return CommDAO.getRs1();
        }

        public bool AUD(AccAssetParm_item data, string employeeNo, string name, CommDAO dao = null)
        {
            switch (data.State)
            {
                case "A": return _AddBatch(data, employeeNo, name, dao);
                case "U": return _UpdateBatch(data, employeeNo, name, dao);
                case "D": return _DeleteBatch(data, dao);
                case "": return true;
            }
            return false;
        }

        public bool isExist(AccAssetParm_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccAssetParm_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.ASPA_A_USER_ID = employeeNo;
            data.ASPA_A_USER_NM = name;
            data.ASPA_A_DATE = DateTime.Now;
            data.ASPA_U_USER_ID = employeeNo;
            data.ASPA_U_USER_NM = name;
            data.ASPA_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccAssetParm_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.ASPA_U_USER_ID = employeeNo;
            data.ASPA_U_USER_NM = name;
            data.ASPA_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("ASPA_A_USER_ID");
            outSL.Add("ASPA_A_USER_NM");
            outSL.Add("ASPA_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccAssetParm_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccAssetParm_item data)
        {
            if (string.IsNullOrEmpty(data.ASPA_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND ASPA_COMPID='{data.ASPA_COMPID}' ";
            sql += CommDAO.sql_ep(data.ASPA_ACCD, "ASPA_ACCD");

            return comm.DB.RunSQL(sql);
        }
        #endregion

        public rs_AccAccNameQuery Query2(AccAssetParm data)
        {
            string sql = $@"SELECT b.* from ACC_ASSET_PARM a 
join ACC_ACCNAME b on a.ASPA_COMPID = b.ACNM_COMPID AND a.ASPA_ACCD = ISNULL(b.ACNM_ID1,'')+ISNULL(b.ACNM_ID2,'')+ISNULL(b.ACNM_ID3,'')
";
            sql += CommDAO.sql_ep(data.ASPA_COMPID, "a.ASPA_COMPID");
            sql += CommDAO.sql_ep(data.ASPA_ACCD, "a.ASPA_ACCD");

            DataTable dt = comm.DB.RunSQL(sql);

            return new rs_AccAccNameQuery {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccAccNameSimple>()
            };
        }

        public rsAccAssetParm_query Query3(AccAssetParm data)
        {
            string sql = $@"
Select b.ACNM_C_NAME, 
c.ACNM_C_NAME as ACNM_C_NAME_T, 
d.ACNM_C_NAME as ACNM_C_NAME_D, 
e.ACNM_C_NAME as ACNM_C_NAME_P,
f.ACNM_C_NAME as ACNM_C_NAME_EXP1, 
g.ACNM_C_NAME as ACNM_C_NAME_EXP2, 
h.ACNM_C_NAME as ACNM_C_NAME_L,
* From ACC_ASSET_PARM a
left join ACC_ACCNAME b on a.ASPA_ACCD     = ISNULL(b.ACNM_ID1,'')+ISNULL(b.ACNM_ID2,'')+ISNULL(b.ACNM_ID3,'')
left join ACC_ACCNAME c on a.ASPA_ACCD_T   = ISNULL(c.ACNM_ID1,'')+ISNULL(c.ACNM_ID2,'')+ISNULL(c.ACNM_ID3,'')
left join ACC_ACCNAME d on a.ASPA_ACCD_D   = ISNULL(bd.ACNM_ID1,'')+ISNULL(d.ACNM_ID2,'')+ISNULL(d.ACNM_ID3,'')
left join ACC_ACCNAME e on a.ASPA_ACCD_P   = ISNULL(e.ACNM_ID1,'')+ISNULL(e.ACNM_ID2,'')+ISNULL(e.ACNM_ID3,'')
left join ACC_ACCNAME f on a.ASPA_ACCD_EXP1= ISNULL(f.ACNM_ID1,'')+ISNULL(f.ACNM_ID2,'')+ISNULL(f.ACNM_ID3,'')
left join ACC_ACCNAME g on a.ASPA_ACCD_EXP2= ISNULL(g.ACNM_ID1,'')+ISNULL(g.ACNM_ID2,'')+ISNULL(g.ACNM_ID3,'')
left join ACC_ACCNAME h on a.ASPA_ACCD_L   = ISNULL(h.ACNM_ID1,'')+ISNULL(h.ACNM_ID2,'')+ISNULL(h.ACNM_ID3,'')
";
            sql += CommDAO.sql_ep(data.ASPA_COMPID, "a.ASPA_COMPID");
            sql += CommDAO.sql_ep(data.ASPA_ACCD, "a.ASPA_ACCD");

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccAssetParm_query
            {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccAssetParm_query>()
            };
        }


    }
}