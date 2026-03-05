using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccPaCheckDAO:BaseClass
    {
        private const string tableName = "ACC_PA_CHECK";
        private List<string> PK;

        //CommDAO commBatch = new CommDAO();

        public AccPaCheckDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        public bool isExist(AccPaCheck_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }

        private void updateDate(AccPaCheck_item data)
        {
            Dictionary<string, object> P = new Dictionary<string, object>();

            string sql = $@"update ACC_PA_CHECK set PACK_OPEN_DATE=@PACK_OPEN_DATE, 
PACK_DUE_DATE1=@PACK_DUE_DATE1,
PACK_DUE_DATE2=@PACK_DUE_DATE2,
PACK_VOU_DATE=@PACK_VOU_DATE
where 1=1";
            BaseClass2.addKeyValue(ref P, "PACK_OPEN_DATE", string.Format("{0:yyyy-MM-dd}", data.PACK_OPEN_DATE));
            BaseClass2.addKeyValue(ref P, "PACK_DUE_DATE1", string.Format("{0:yyyy-MM-dd}", data.PACK_DUE_DATE1));
            BaseClass2.addKeyValue(ref P, "PACK_DUE_DATE2", string.Format("{0:yyyy-MM-dd}", data.PACK_DUE_DATE2));
            BaseClass2.addKeyValue(ref P, "PACK_VOU_DATE" , string.Format("{0:yyyy-MM-dd}", data.PACK_VOU_DATE));

            sql += BaseClass2.sql_ep(data.PACK_COMPID, "PACK_COMPID", ref P);
            sql += BaseClass2.sql_ep(data.PACK_NO, "PACK_NO", ref P);

            comm.DB.ExecSQL(sql);
        }
        public rsAUD addAndUpdate(List<AccPaCheck_item> data, string employeeNo, string name)
        {
            List<rsAUD_item> rs = new List<rsAUD_item>();
            string errMsg = "";

            //commBatch.DB.BeginTransaction();
            try
            {
                foreach (AccPaCheck_item item in data)
                {
                    errMsg = "";
                    rsAUD_item aud;

                    if (item.State == "A")
                    {
                        Add(item, employeeNo, name); //, commBatch);
                        //updateDate(item);
                    }
                    else if (item.State == "U")
                    {
                        Update(item, employeeNo, name); //, commBatch);
                        //updateDate(item);
                    }
                    else if (item.State == "D")
                        Delete(item); //, commBatch);

                    aud = new rsAUD_item { AutoId = item.AutoId, errMsg = errMsg };
                    rs.Add(aud);

                    if (errMsg != "")
                        break;
                }
                if (errMsg == "")
                {
                    //commBatch.DB.Commit();
                    return new rsAUD
                    {
                        result = CommDAO.getRsItem(),
                        data = rs
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Info(ex.Message);
            }

            //commBatch.DB.Rollback();
            return new rsAUD
            {
                result = CommDAO.getRsItem1(),
                data = rs
            };
        }

        public bool AUD(AccPaCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            switch (data.State)
            {
                case "A": return Add(data, employeeNo, name, dao);
                case "U": return Update(data, employeeNo, name, dao);
                case "D": return Delete(data, dao);
                case "": return true;
            }
            return false;
        }
        public bool Add(AccPaCheck_item data, string employeeNo, string name, CommDAO dao=null)
        {
            // 填入預設資料
            data.PACK_A_USER_ID = employeeNo;
            data.PACK_A_USER_NM = name;
            data.PACK_A_DATE = DateTime.Now;
            data.PACK_U_USER_ID = employeeNo;
            data.PACK_U_USER_NM = name;
            data.PACK_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool Update(AccPaCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.PACK_U_USER_ID = employeeNo;
            data.PACK_U_USER_NM = name;
            data.PACK_U_DATE = DateTime.Now;


            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("PACK_A_USER_ID");
            outSL.Add("PACK_A_USER_NM");
            outSL.Add("PACK_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool Delete(AccPaCheck_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }

        public DataTable _Query(AccPaCheck_item data)
        {
            if (string.IsNullOrEmpty(data.PACK_COMPID))
                return null;

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 ";

            sql += CommDAO.sql_ep(data.PACK_COMPID, "PACK_COMPID");
            sql += CommDAO.sql_ep(data.PACK_NO, "PACK_NO");
            #endregion

            return comm.DB.RunSQL(sql);
        }

        public rsAccPaCheck_qry Query(AccPaCheck_qry data)
        {
            if (string.IsNullOrEmpty(data.data.PACK_COMPID))
                return new rsAccPaCheck_qry { result = CommDAO.getRsItem1("查無公司代碼") };

            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 ";

            sql += CommDAO.sql_ep(data.data.PACK_COMPID, "PACK_COMPID");
            sql += CommDAO.sql_ep(data.data.PACK_NO, "PACK_NO");
            sql += CommDAO.sql_ep_date_between(data.data.PACK_OPEN_DATE, data.data.PACK_OPEN_DATE_E, "PACK_OPEN_DATE");
            sql += CommDAO.sql_ep_date_between(data.data.PACK_DUE_DATE1, data.data.PACK_DUE_DATE1_E, "PACK_DUE_DATE1");

            sql += CommDAO.sql_ep_date(data.data.PACK_DUE_DATE2, "PACK_DUE_DATE2");

            sql += CommDAO.sql_ep(data.data.PACK_VENDID, "PACK_VENDID");
            sql += CommDAO.sql_ep(data.data.PACK_DUE_FLG, "PACK_DUE_FLG");
            sql += CommDAO.sql_ep(data.data.PACK_VOUNO, "PACK_VOUNO");
            sql += CommDAO.sql_ep(data.data.PACK_C_VOUNO, "PACK_C_VOUNO");
            sql += CommDAO.sql_like(data.data.PACK_MEMO, "PACK_MEMO", "%");
            sql += CommDAO.sql_ep(data.data.PACK_DUE_BANK, "PACK_DUE_BANK ");

            #endregion

            #region 分頁
            sql += " ORDER BY PACK_COMPID, PACK_NO ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccPaCheck_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccPaCheck>(),
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        private string GetNO(string CompanyID)
        {
            string sql = $@"select TOP 1 CKCN_NOID+CKCN_CUNO as NO
             from ACC_CHECK_COUNTER
             WHERE CKCN_CUNO <= CKCN_ENNO
             AND CKCN_COMPID = '{CompanyID}'
             ORDER BY CKCN_A_DATE";

            DataTable dt = comm.DB.RunSQL(sql);

            if (dt.Rows.Count != 0)
                return dt.Rows[0]["NO"].ToString();

            return "";
        }
        private bool UpdateNO(string CompanyID, string NO)
        {
                string sql = $@"UPDATE  ACC_CHECK_COUNTER 
   SET  CKCN_CUNO = 
RIGHT('0000000'+CONVERT(VARCHAR(10),CONVERT(INT, CKCN_CUNO)+1),
   LEN(CKCN_CUNO) )
   WHERE CKCN_NOID+CKCN_CUNO = '{NO}' and CKCN_COMPID='{CompanyID}'
";
                return comm.DB.ExecSQL(sql);
        }
        private bool QueryData(string CompanyID, string PACK_NO)
        {
            string sql = $@"Select 
PACK_COMPID,PACK_NO,PACK_OPEN_DATE,PACK_DUE_DATE1,PACK_VENDID,PACK_AMT,PACK_DUE_BANK,PACK_DUE_FLG,PACK_DUE_DATE2,
PACK_VOU_DATE,PACK_VOUNO,PACK_C_VOUNO,PACK_MEMO,PACK_A_USER_ID,PACK_A_USER_NM,PACK_A_DATE,PACK_U_USER_ID,PACK_U_USER_NM,PACK_U_DATE
From ACC_PA_CHECK where PACK_NO='{PACK_NO}' and PACK_COMPID='{CompanyID}'
";
            DataTable dt = comm.DB.RunSQL(sql);
            return dt.Rows.Count != 0;
        }
        private bool CreateNewData(string CompanyID, string PACK_NO, string New_PACK_NO)
        {
            string sql = $@"
Insert into ACC_PA_CHECK(PACK_COMPID,PACK_NO,PACK_OPEN_DATE,PACK_DUE_DATE1,PACK_VENDID,PACK_AMT,PACK_DUE_BANK,PACK_DUE_FLG,PACK_DUE_DATE2,
PACK_VOU_DATE,PACK_VOUNO,PACK_C_VOUNO,PACK_MEMO,PACK_A_USER_ID,PACK_A_USER_NM,PACK_A_DATE,PACK_U_USER_ID,PACK_U_USER_NM,PACK_U_DATE
)
Select 
PACK_COMPID,'{New_PACK_NO}',PACK_OPEN_DATE,PACK_DUE_DATE1,PACK_VENDID,PACK_AMT,PACK_DUE_BANK,PACK_DUE_FLG,PACK_DUE_DATE2,
PACK_VOU_DATE,PACK_VOUNO,PACK_C_VOUNO,PACK_MEMO,PACK_A_USER_ID,PACK_A_USER_NM,PACK_A_DATE,PACK_U_USER_ID,PACK_U_USER_NM,PACK_U_DATE
From ACC_PA_CHECK where PACK_NO='{PACK_NO}' and PACK_COMPID='{CompanyID}'
";
            return comm.DB.ExecSQL(sql);
        }
        private bool UpdateData(string CompanyID, string Old_PACK_NO)
        {
            string sql = $@"
Update ACC_PA_CHECK set PACK_DUE_FLG='9' where PACK_NO='{Old_PACK_NO}' and PACK_COMPID='{CompanyID}'";

            return comm.DB.ExecSQL(sql);
        }

        // 作廢重新產生
        public rs ReCreateData(string CompanyID, string PACK_NO)
        {
            if (string.IsNullOrEmpty(PACK_NO))
                return CommDAO.getRs1("票據號碼未輸入");

            string NO = GetNO(CompanyID);
            if (string.IsNullOrEmpty(NO))
                return CommDAO.getRs1("查無票據資料");

            try
            {
                if (!QueryData(CompanyID, PACK_NO))
                    return CommDAO.getRs1("查無舊資料資料 : " + PACK_NO);

                if (!CreateNewData(CompanyID, PACK_NO, NO))
                    return CommDAO.getRs1("產生新資料失敗 : " + NO);

                UpdateData(CompanyID, PACK_NO);

                UpdateNO(CompanyID, NO);

                return CommDAO.getRs();
            }
            catch(Exception ex)
            {
                Log.Info(ex.Message);
                return CommDAO.getRs1(ex.Message);
            }
        }


    }
}