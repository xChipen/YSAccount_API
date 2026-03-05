using Models;
using System.Collections.Generic;
using System.Data;
using Helpers;
using System;

namespace DAO
{
    public class AccVendorDAO : BaseClass2
    {
        public AccVendorDAO(string tableName):base(tableName)
        {
        }

        //public int getSEQ()
        //{
        //    string sql = "Select Max(SEQ) as SEQ from ACC_VENDOR";
        //    DataTable dt = comm.DB.RunSQL(sql);
        //    if (dt.Rows.Count != 0)
        //    {
        //        int seq = (int)dt.Rows[0]["SEQ"] + 1;
        //        return seq;
        //    }
        //    return 0;
        //}
        //public int? getSEQ_qry(string VEND_COMPID, string VEND_ID)
        //{
        //    string sql = $"Select SEQ from ACC_VENDOR where VEND_COMPID=@VEND_COMPID and VEND_ID=@VEND_ID";
        //    DataTable dt = comm.DB.RunSQL(sql, new object[] { VEND_COMPID , VEND_ID });
        //    if (dt.Rows.Count != 0)
        //    {
        //        int seq = (int)dt.Rows[0]["SEQ"];
        //        return seq;
        //    }
        //    return null;
        //}



        //private const string tableName = "ACC_VENDOR";
        //private List<string> PK; // = CommDAO.getPK(tableName);

        //public AccVendorDAO() {
        //    PK = CommDAO.getPK(comm, tableName);
        //}

        //#region Batch call
        //public  bool isExist(AccVendor data)
        //{
        //    return CommDAO.isExist(comm, data, tableName, PK);
        //}
        //public  bool _AddBatch(AccVendor data, CommDAO dao = null)
        //{
        //    return CommDAO._add(null, data, tableName, PK, dao);
        //}
        //public  bool _UpdateBatch(AccVendor data, CommDAO dao = null)
        //{
        //    return CommDAO._update(null, data, tableName, PK, null, dao);
        //}
        //public  bool _DeleteBatch(AccVendor data, CommDAO dao = null)
        //{
        //    return CommDAO._delete(null, data, tableName, PK, null, dao);
        //}
        //#endregion

        // 變成多型了. 在呼叫底層 Method
        public rs Add(AccVendor data)
        {
            if (string.IsNullOrEmpty(data.VEND_ABBR))
                data.VEND_ABBR = "ABBR";

            return base.Add<AccVendor, rs>(data);
        }

        //public rs Update(AccVendor_ins data)
        //{
        //    if (isExist(data.data))
        //    {

        //        // 不更新, 排除清單
        //        List<string> outSL = new List<string>();
        //        outSL.Add("SEQ");

        //        bool bOK = CommDAO._update(comm, data.data, tableName, PK, outSL);
        //        if (bOK)
        //            return CommDAO.getRs();
        //    }
        //    return CommDAO.getRs(1, "資料不存在");
        //}

        //public rs Delete(AccVendor_del data)
        //{
        //    foreach (AccVendor item in data.data)
        //    {
        //        CommDAO._delete(comm, item, tableName, PK);
        //    }
        //    return CommDAO.getRs();
        //}

        public rs_AccVendor_qry Query(AccVendor data)
        {
            string sql = $@"SELECT * from {base.tableName} 
WHERE 1=1 ";
            sql += CommDAO.sql_ep(data.VEND_COMPID, "VEND_COMPID");
            sql += CommDAO.sql_ep(data.VEND_ID, "VEND_ID");
            sql += CommDAO.sql_like(data.VEND_NAME, "VEND_NAME", "%");
            sql += CommDAO.sql_like(data.VEND_ABBR, "VEND_ABBR", "%");
            sql += CommDAO.sql_like(data.VEND_BANKID, "VEND_BANKID", "%");
            sql += CommDAO.sql_like(data.VEND_ACNO, "VEND_ACNO", "%");
            sql += CommDAO.sql_like(data.VEND_ADDRESS, "VEND_ADDRESS", "%");

            DataTable dt = comm.DB.RunSQL(sql);

            return new rs_AccVendor_qry {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccVendor>()
            };
        }





    }
}