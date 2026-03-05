using System;
using System.Collections.Generic;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccPosSalesDAO : BaseClass
    {
        private const string tableName = "ACC_POS_SALES";
        private List<string> PK;

        public AccPosSalesDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        public bool isExist(ACC_POS_SALES data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool Add(ACC_POS_SALES data, out string ErrMsg)
        {
            ErrMsg = "";
            if (!isExist(data))
            {
                return CommDAO._add(comm, data, tableName, PK);
            }
            ErrMsg = "資料已經存在";
            return false;
        }
        public bool Update(ACC_POS_SALES data, out string ErrMsg)
        {
            ErrMsg = "";
            if (isExist(data))
            {
                string sql = $@"Update {tableName} SET 
PSAL_CUSTID=@PSAL_CUSTID,
PSAL_NET_AMT=@PSAL_NET_AMT,
PSAL_TAX_AMT=@PSAL_TAX_AMT,
PSAL_U_USER_ID=@PSAL_U_USER_ID,
PSAL_U_USER_NM=@PSAL_U_USER_NM,
PSAL_U_DATE=GETDATE()
WHERE 
PSAL_COMPID=@PSAL_COMPID
AND Convert(char(8), PSAL_DATE,112)=@PSAL_DATE
AND PSAL_DEPTID=@PSAL_DEPTID
AND PSAL_INVNO=@PSAL_INVNO
";
                object[] P = new object[] {
                data.PSAL_CUSTID,
                data.PSAL_NET_AMT,
                data.PSAL_TAX_AMT,
                data.PSAL_U_USER_ID,
                data.PSAL_U_USER_NM,
                data.PSAL_COMPID,
                string.Format("{0:yyyyMMdd}", data.PSAL_DATE),
                data.PSAL_DEPTID,
                data.PSAL_INVNO
            };

                bool bOK = comm.DB.ExecSQL(sql, P);
                if (!bOK)
                    ErrMsg = "更新失敗";

                return bOK;
            }
            ErrMsg = "資料不存在";
            return false;
        }


        #region Batch call

        public bool _AUD(ref List<ACC_POS_SALES> data, string employeeNo, string name)
        {
            bool bOK;
            string ErrMsg = "";

            foreach (ACC_POS_SALES item in data)
            {
                bOK = AUD(item, employeeNo, name, out ErrMsg);
                if (!bOK)
                {
                    item.ErrMsg = ErrMsg;
                    return false;
                }
            }
            return true;
        }

        public bool AUD(ACC_POS_SALES data, string employeeNo, string name, out string ErrMsg)
        {
            ErrMsg = "";

            switch (data.State)
            {
                case "A": return _AddBatch(data, employeeNo, name, out ErrMsg);
                case "U": return _UpdateBatch(data, employeeNo, name, out ErrMsg);
                case "D": return _DeleteBatch(data);
                case "": return true;
            }
            return false;
        }

        public bool _AddBatch(ACC_POS_SALES data, string employeeNo, string name, out string ErrMsg)
        {
            // 填入預設資料
            data.PSAL_A_USER_ID = employeeNo;
            data.PSAL_A_USER_NM = name;
            data.PSAL_A_DATE = DateTime.Now;
            data.PSAL_U_USER_ID = employeeNo;
            data.PSAL_U_USER_NM = name;
            data.PSAL_U_DATE = DateTime.Now;

            return Add(data, out ErrMsg);
        }
        public bool _UpdateBatch(ACC_POS_SALES data, string employeeNo, string name, out string ErrMsg)
        {
            ErrMsg = "";

            data.PSAL_U_USER_ID = employeeNo;
            data.PSAL_U_USER_NM = name;

            return Update(data, out ErrMsg);
        }
        public bool _DeleteBatch(ACC_POS_SALES data)
        {
            return CommDAO._delete(comm, data, tableName, PK);
        }
        public DataTable _QueryBatch(ACC_POS_SALES_item data)
        {
            if (string.IsNullOrEmpty(data.PSAL_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (data.PSAL_DATE == null)
            {
                Log.Info("_QueryBatch: 發票日期");
                return null;
            }

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND PSAL_COMPID='{data.PSAL_COMPID}' ";

            if (data.PSAL_DATE != null)
//                sql += CommDAO.sql_ep(string.Format("{0:yyyyMMdd}", data.PSAL_DATE), "convert(char(8), PSAL_DATE ,112)");
              sql += CommDAO.sql_ep_date( data.PSAL_DATE, "PSAL_DATE");

            if (!string.IsNullOrEmpty(data.PSAL_DEPTID))
                sql += CommDAO.sql_ep(data.PSAL_DEPTID, "PSAL_DEPTID");

            if (!string.IsNullOrEmpty(data.PSAL_CUSTID))
                sql += CommDAO.sql_ep(data.PSAL_CUSTID, "PSAL_CUSTID");

            return comm.DB.RunSQL(sql);
        }
        #endregion
    }
}