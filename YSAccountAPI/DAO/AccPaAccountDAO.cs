using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Helpers;
using System.Data;
namespace DAO
{
    public class AccPaAccountDAO : BaseClass
    {
        private const string tableName = "ACC_PA_ACCOUNT";
        private List<string> PK;

        CommDAO commBatch = new CommDAO();

        public AccPaAccountDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        public bool isExist(AccPaAccount data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }

        public rsAUD addAndUpdate(List<AccPaAccount_item> data, string employeeNo, string name)
        {
            List<rsAUD_item> rs = new List<rsAUD_item>();
            string errMsg = "";

            commBatch.DB.BeginTransaction();
            try
            {
                foreach (AccPaAccount_item item in data)
                {
                    errMsg = "";
                    rsAUD_item aud;

                    if (item.State == "A")
                        Add(item, employeeNo, name, commBatch);
                    else if (item.State == "U")
                        Update(item, employeeNo, name, commBatch);
                    else if (item.State == "D")
                        Delete(item, commBatch);

                    aud = new rsAUD_item { AutoId = item.AutoId, errMsg = errMsg };
                    rs.Add(aud);

                    if (errMsg != "")
                        break;
                }
                if (errMsg == "")
                {
                    commBatch.DB.Commit();
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

            commBatch.DB.Rollback();
            return new rsAUD
            {
                result = CommDAO.getRsItem1(),
                data = rs
            };
        }

        public bool AUD(AccPaAccount_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool Add(AccPaAccount_item data, string employeeNo, string name, CommDAO dao=null)
        {
            // 填入預設資料
            data.PAAC_A_USER_ID = employeeNo;
            data.PAAC_A_USER_NM = name;
            data.PAAC_A_DATE = DateTime.Now;
            data.PAAC_U_USER_ID = employeeNo;
            data.PAAC_U_USER_NM = name;
            data.PAAC_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool Update(AccPaAccount_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.PAAC_U_USER_ID = employeeNo;
            data.PAAC_U_USER_NM = name;
            data.PAAC_U_DATE = DateTime.Now;


            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("PAAC_A_USER_ID");
            outSL.Add("PAAC_A_USER_NM");
            outSL.Add("PAAC_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool Delete(AccPaAccount_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _Query(AccPaAccount_item data)
        {
            if (string.IsNullOrEmpty(data.PAAC_COMPID))
                return null;

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 ";

            sql += CommDAO.sql_ep(data.PAAC_COMPID, "PAAC_COMPID");
            sql += CommDAO.sql_ep(data.PAAC_VENDID, "PAAC_VENDID");
            sql += CommDAO.sql_ep(data.PAAC_INVNO, "PAAC_INVNO");
            #endregion

            return comm.DB.RunSQL(sql);
        }



        public rsAccPaAccount_qry Query(AccPaAccount_qry data, string PAAC_STS = "Y")
        {
            if (string.IsNullOrEmpty(data.data.PAAC_COMPID))
                return new rsAccPaAccount_qry { result = CommDAO.getRsItem1("查無公司代碼") };

            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 ";

            sql += CommDAO.sql_ep(data.data.PAAC_COMPID, "PAAC_COMPID");
            sql += CommDAO.sql_ep(data.data.PAAC_VENDID, "PAAC_VENDID");
            sql += CommDAO.sql_ep(data.data.PAAC_INVNO, "PAAC_INVNO");
            sql += CommDAO.sql_ep(data.data.PAAC_ACCD, "PAAC_ACCD");
            sql += CommDAO.sql_ep_date_between(data.data.PAAC_INV_DATE, data.data.PAAC_INV_DATE_E, "PAAC_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.data.PAAC_DUE_DATE, data.data.PAAC_DUE_DATE_E, "PAAC_DUE_DATE");
            sql += CommDAO.sql_ep(data.data.PAAC_STS, "PAAC_STS");
            sql += CommDAO.sql_like(data.data.PAAC_MEMO, "PAAC_MEMO", "%");
            sql += CommDAO.sql_decimal(data.data.PAAC_NT_BAL, "PAAC_NT_BAL");
            #endregion

            #region 分頁
            sql += " ORDER BY PAAC_COMPID";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccPaAccount_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccPaAccount>(),
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        public rsAccPaAccount_query2_rs Query2(AccPaAccount_query2 data) {

            //data.TRAN_KIND = "2";

            if (data.PAAC_DUE_DATE == null || string.IsNullOrEmpty(data.PAAC_PAY_KIND))
            {
                return new rsAccPaAccount_query2_rs { result = CommDAO.getRsItem1() };
            }

            string sql = $@"SELECT PAAC_VENDID, TRAN_NAME,
 TRAN_BANKID, TRAN_ACNO,
 PAAC_NT_BAL, 
PAAC_CURRID, 
 PAAC_FOR_TOT_AMT, 
 PAAC_FOR_BAL, PAAC_DUE_DATE, PAAC_VENDID, PAAC_INVNO, PAAC_VOUNO
FROM vw_PA_ACCOUNT,vw_TRAIN
WHERE PAAC_DUE_DATE <='{String.Format("{0:yyyy/MM/dd}", data.PAAC_DUE_DATE)}'
AND PAAC_PAY_KIND = @PAAC_PAY_KIND
AND PAAC_STS = @PAAC_STS
AND PAAC_COMPID = TRAN_COMPID ";

            sql += CommDAO.sql_ep(data.TRAN_KIND, "TRAN_KIND");

            sql += $@" AND TRAN_ID = PAAC_VENDID
AND PAAC_CURRID = '{data.PAAC_CURRID}'
AND PAAC_NT_BAL >0
";
            sql += CommDAO.sql_ep(data.PAAC_VENDID, "PAAC_VENDID");

            DataTable dt = comm.DB.RunSQL(sql, new object[]
                { data.PAAC_PAY_KIND, data.PAAC_STS });

            List<AccPaAccount_query2_rs> rs = dt.ToList<AccPaAccount_query2_rs>();

            return new rsAccPaAccount_query2_rs { result = CommDAO.getRsItem(), data = rs };
        }

        public rsAccPaAccount_qry3_rs Query3(AccPaAccount_query2 data)
        {

            if (data.PAAC_DUE_DATE == null || string.IsNullOrEmpty(data.PAAC_PAY_KIND)
                || string.IsNullOrEmpty(data.PAAC_VENDID) || string.IsNullOrEmpty(data.PAAC_STS))
            {
                return new rsAccPaAccount_qry3_rs { result = CommDAO.getRsItem1("參數錯誤") };
            }

            string sql = $@"SELECT PAAC_INV_DATE,PAAC_INVNO, PAAC_NT_BAL, PAAC_FOR_BAL
FROM ACC_PA_ACCOUNT
WHERE PAAC_DUE_DATE <=@PAAC_DUE_DATE
AND PAAC_PAY_KIND =@PAAC_PAY_KIND
AND PAAC_VENDID = @PAAC_VENDID
AND PAAC_NT_BAL > 0
";
            sql += CommDAO.sql_ep(data.PAAC_CURRID, "PAAC_CURRID");
            sql += CommDAO.sql_ep(data.PAAC_STS, "PAAC_STS");

            DataTable dt = comm.DB.RunSQL(sql, new object[]
                { data.PAAC_DUE_DATE, data.PAAC_PAY_KIND, data.PAAC_VENDID });

            List<AccPaAccount_qry3_rs> rs = dt.ToList<AccPaAccount_qry3_rs>();

            return new rsAccPaAccount_qry3_rs { result = CommDAO.getRsItem(), data = rs };
        }

        // ACD120Q_應付帳款資料查詢
        public AccPaAccount_QueryForm_qry Query_ACD120Q(AccPaAccount_QueryForm data)
        {
            DataTable dt = _Query_ACD120Q1(data);

            return new AccPaAccount_QueryForm_qry {
                result = CommDAO.getRsItem(),
                data = dt.ToList<rsAccPaAccount_QueryForm>()
            };
        }

        public DataTable _Query_ACD120Q1(AccPaAccount_QueryForm data)
        {
            switch (data.QueryType)
            {
                case 1: return Query_ACD120Q1(data);
                case 2: return Query_ACD120Q1(data);
                case 3: return Query_ACD120Q1(data);
                case 4: return Query_ACD120Q1(data);
                case 5: return Query_ACD120Q1(data);
                default: return null;
            }
        }

        // 1 未付
        public DataTable Query_ACD120Q1(AccPaAccount_QueryForm data)
        {
            string sql = $@"SELECT 
a.PAAC_INV_DATE as INV_DATE, 
a.PAAC_INVNO as INVNO, 
c.TRAN_NAME, 
a.PAAC_VOUNO as VOUNO,
a.PAAC_DUE_DATE as DUE_DATE, 
a.PAAC_NT_TOT_AMT as NT_TOT_AMT, 
a.PAAC_NT_TOT_AMT-a.PAAC_NT_BAL as NT_AMT, 
a.PAAC_PAY_DATE as ACT_DATE, 
a.PAAC_VENDID as VENDID, 
b.ACNM_C_NAME as C_NAME, 
a.PAAC_A_USER_NM as A_USER_NM,
c.TRAN_BANKID, c.TRAN_ACNO, c.TRAN_ADDRESS
from ACC_PA_ACCOUNT a 
left join ACC_ACCNAME b   on a.PAAC_COMPID=b.ACNM_COMPID and a.PAAC_ACCD = IsNull(b.ACNM_ID1,'')+IsNull(b.ACNM_ID2,'')+IsNull(b.ACNM_ID3,'')
left join vw_TRAIN c  on a.PAAC_COMPID=c.TRAN_COMPID and a.PAAC_VENDID = c.TRAN_ID
where 1 = 1 ";
            sql += " AND a.PAAC_NT_BAL<>0 AND a.PAAC_STS = 'Y' ";

            sql += CommDAO.sql_ep(data.COMPID, "a.PAAC_COMPID");
            sql += CommDAO.sql_ep(data.VENDID, "a.PAAC_VENDID");
            sql += CommDAO.sql_like(data.VENDID_NAME, "c.TRAN_NAME");

            sql += CommDAO.sql_ep_date_between(data.INV_DATE_S, data.INV_DATE_S, "a.PAAC_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE_S, data.PAY_DATE_S, "a.PAAC_PAY_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE_S, data.PRE_DATE_S, "a.PAAC_DUE_DATE");

            sql += CommDAO.sql_ep(data.BANKID, "c.TRAN_BANKID");
            sql += CommDAO.sql_ep(data.INV_NO, "a.PAAC_INVNO");

            return comm.DB.RunSQL(sql);
        }

        // 2 已付
        public DataTable Query_ACD120Q2(AccPaAccount_QueryForm data)
        {
            string sql = $@"SELECT 
f.PAYD_INV_DATE as INV_DATE, 
f.PAYD_INVNO as INVNO, 
c.TRAN_NAME, 
a.PAAC_VOUNO as VOUNO,
a.PAAC_DUE_DATE as DUE_DATE, 
a.PAAC_NT_TOT_AMT as NT_TOT_AMT, 
f.PAYD_NT_AMT as NT_AMT, 
e.PAYM_PAY_DATE as ACT_DATE, 
e.PAYM_VENDID as VENDID, 
b.ACNM_C_NAME as C_NAME, 
e.PAYM_A_USER_NM as A_USER_NM,
c.TRAN_BANKID, c.TRAN_ACNO, c.TRAN_ADDRESS
from ACC_PA_ACCOUNT a 
left join ACC_ACCNAME b on a.PAAC_COMPID=b.ACNM_COMPID and a.PAAC_ACCD = IsNull(b.ACNM_ID1,'')+IsNull(b.ACNM_ID2,'')+IsNull(b.ACNM_ID3,'')
left join vw_TRAIN c    on a.PAAC_COMPID=c.TRAN_COMPID and a.PAAC_VENDID = c.TRAN_ID
left join ACC_PAY_M e   on a.PAAC_COMPID=e.PAYM_COMPID and a.PAAC_VENDID = e.PAYM_VENDID
left join ACC_PAY_D f   on a.PAAC_COMPID=f.PAYD_COMPID and e.PAYM_NO = f.PAYD_NO
where 1 = 1 ";
            sql += " AND e.PAYM_VALID = 'Y' ";

            sql += CommDAO.sql_ep(data.COMPID, "a.PAAC_COMPID");
            sql += CommDAO.sql_ep(data.VENDID, "a.PAAC_VENDID");
            sql += CommDAO.sql_like(data.VENDID_NAME, "c.TRAN_NAME");

            sql += CommDAO.sql_ep_date_between(data.INV_DATE_S, data.INV_DATE_S, "f.PAYD_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE_S, data.PAY_DATE_S, "e.PAYM_PAY_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE_S, data.PRE_DATE_S, "a.PAAC_DUE_DATE");

            sql += CommDAO.sql_ep(data.BANKID, "c.TRAN_BANKID");
            sql += CommDAO.sql_ep(data.INV_NO, "a.PAAC_INVNO");

            return comm.DB.RunSQL(sql);
        }

        // 3 暫不付款
        public DataTable Query_ACD120Q3(AccPaAccount_QueryForm data)
        {
            string sql = $@"SELECT 
a.PAAC_INV_DATE as INV_DATE, 
a.PAAC_INVNO as INVNO, 
c.TRAN_NAME, 
a.PAAC_VOUNO as VOUNO,
a.PAAC_DUE_DATE as DUE_DATE, 
a.PAAC_NT_TOT_AMT as NT_TOT_AMT, 
a.PAAC_NT_TOT_AMT-a.PAAC_NT_BAL as NT_AMT, 
a.PAAC_PAY_DATE as ACT_DATE, 
a.PAAC_VENDID as VENDID, 
b.ACNM_C_NAME as C_NAME, 
a.PAAC_A_USER_NM as A_USER_NM,
c.TRAN_BANKID, c.TRAN_ACNO, c.TRAN_ADDRESS
from ACC_PA_ACCOUNT a 
left join ACC_ACCNAME b on a.PAAC_COMPID=b.ACNM_COMPID and a.PAAC_ACCD = IsNull(b.ACNM_ID1,'')+IsNull(b.ACNM_ID2,'')+IsNull(b.ACNM_ID3,'')
left join vw_TRAIN c    on a.PAAC_COMPID=c.TRAN_COMPID and a.PAAC_VENDID = c.TRAN_ID
where 1 = 1 ";
            sql += " AND a.PAAC_NT_BAL<>0 AND a.PAAC_STS = 'N' ";

            sql += CommDAO.sql_ep(data.COMPID, "a.PAAC_COMPID");
            sql += CommDAO.sql_ep(data.VENDID, "a.PAAC_VENDID");
            sql += CommDAO.sql_like(data.VENDID_NAME, "c.TRAN_NAME");

            sql += CommDAO.sql_ep_date_between(data.INV_DATE_S, data.INV_DATE_S, "a.PAAC_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE_S, data.PAY_DATE_S, "a.PAAC_PAY_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE_S, data.PRE_DATE_S, "a.PAAC_DUE_DATE");

            sql += CommDAO.sql_ep(data.BANKID, "c.TRAN_BANKID");
            sql += CommDAO.sql_ep(data.INV_NO, "a.PAAC_INVNO");

            return comm.DB.RunSQL(sql);
        }

        // 4 全部明細資料
        public DataTable Query_ACD120Q4(AccPaAccount_QueryForm data)
        {
            string sql = $@"SELECT 
a.PAAC_INV_DATE as INV_DATE, 
a.PAAC_INVNO as INVNO, 
c.TRAN_NAME, 
a.PAAC_VOUNO as VOUNO,
a.PAAC_DUE_DATE as DUE_DATE, 
a.PAAC_NT_TOT_AMT as NT_TOT_AMT, 
a.PAAC_NT_TOT_AMT-a.PAAC_NT_BAL as NT_AMT, 
a.PAAC_PAY_DATE as ACT_DATE, 
a.PAAC_VENDID as VENDID, 
b.ACNM_C_NAME as C_NAME, 
a.PAAC_A_USER_NM as A_USER_NM,
c.TRAN_BANKID, c.TRAN_ACNO, c.TRAN_ADDRESS
from ACC_PA_ACCOUNT a 
left join ACC_ACCNAME b on a.PAAC_COMPID=b.ACNM_COMPID and a.PAAC_ACCD = IsNull(b.ACNM_ID1,'')+IsNull(b.ACNM_ID2,'')+IsNull(b.ACNM_ID3,'')
left join vw_TRAIN c    on a.PAAC_COMPID=c.TRAN_COMPID and a.PAAC_VENDID = c.TRAN_ID
where 1 = 1 ";

            sql += CommDAO.sql_ep(data.COMPID, "a.PAAC_COMPID");
            sql += CommDAO.sql_ep(data.VENDID, "a.PAAC_VENDID");
            sql += CommDAO.sql_like(data.VENDID_NAME, "c.TRAN_NAME");

            sql += CommDAO.sql_ep_date_between(data.INV_DATE_S, data.INV_DATE_S, "a.PAAC_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE_S, data.PAY_DATE_S, "a.PAAC_PAY_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE_S, data.PRE_DATE_S, "a.PAAC_DUE_DATE");

            sql += CommDAO.sql_ep(data.BANKID, "c.TRAN_BANKID");
            sql += CommDAO.sql_ep(data.INV_NO, "a.PAAC_INVNO");

            return comm.DB.RunSQL(sql);
        }

        // 5
        public DataTable Query_ACD120Q5(AccPaAccount_QueryForm data)
        {
            string sql = $@"SELECT 
f.PAYD_INV_DATE as INV_DATE, 
f.PAYD_INVNO as INVNO, 
c.TRAN_NAME, 
a.PAAC_VOUNO as VOUNO,
a.PAAC_DUE_DATE as DUE_DATE, 
a.PAAC_NT_TOT_AMT as NT_TOT_AMT, 
f.PAYD_NT_AMT as NT_AMT, 
e.PAYM_PAY_DATE as ACT_DATE, 
e.PAYM_VENDID as VENDID, 
b.ACNM_C_NAME as C_NAME, 
e.PAYM_A_USER_NM as A_USER_NM,
c.TRAN_BANKID, c.TRAN_ACNO, c.TRAN_ADDRESS
from ACC_PA_ACCOUNT a 
left join ACC_ACCNAME b on a.PAAC_COMPID=b.ACNM_COMPID and a.PAAC_ACCD = IsNull(b.ACNM_ID1,'')+IsNull(b.ACNM_ID2,'')+IsNull(b.ACNM_ID3,'')
left join vw_TRAIN c    on a.PAAC_COMPID=c.TRAN_COMPID and a.PAAC_VENDID = c.TRAN_ID
left join ACC_PAY_M e   on a.PAAC_COMPID=e.PAYM_COMPID and a.PAAC_VENDID = e.PAYM_VENDID
left join ACC_PAY_D f   on a.PAAC_COMPID=f.PAYD_COMPID and e.PAYM_NO = f.PAYD_NO
where 1 = 1 ";
            sql += " AND e.PAYM_VALID = 'Y' AND e.PAYM_VOUNO = ''";

            sql += CommDAO.sql_ep(data.COMPID, "a.PAAC_COMPID");
            sql += CommDAO.sql_ep(data.VENDID, "a.PAAC_VENDID");
            sql += CommDAO.sql_like(data.VENDID_NAME, "c.TRAN_NAME");

            sql += CommDAO.sql_ep_date_between(data.INV_DATE_S, data.INV_DATE_S, "f.PAYD_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.PAY_DATE_S, data.PAY_DATE_S, "e.PAYM_PAY_DATE");
            sql += CommDAO.sql_ep_date_between(data.PRE_DATE_S, data.PRE_DATE_S, "a.PAAC_DUE_DATE");

            sql += CommDAO.sql_ep(data.BANKID, "c.TRAN_BANKID");
            sql += CommDAO.sql_ep(data.INV_NO, "a.PAAC_INVNO");

            return comm.DB.RunSQL(sql);
        }

        public DataTable _Query_ACD040M(AccPayM_ACD040M_qry data)
        {
            string sql = $@"SELECT PAAC_INV_DATE,PAAC_INVNO,
PAAC_NT_BAL
FROM ACC_PA_ACCOUNT
WHERE PAAC_DUE_DATE <='{ string.Format("yyyy/MM/dd", data.PAY_DATE) }'
AND PAAC_PAY_KIND ='{ data.PAY_KIND }'
AND PAAC_STS = '{data.STS}'
AND PAAC_CURRID = 'NTD'
AND PAAC_VENDID = '{data.VENDID}'
AND PAAC_NT_BAL > 0
";
            return comm.DB.RunSQL(sql);
        }
        public AccPaAccount_ACD040M_qry_ins Query_ACD040M(AccPayM_ACD040M_qry data)
        {
            DataTable dt = _Query_ACD040M(data);

            return new AccPaAccount_ACD040M_qry_ins {
                result = CommDAO.getRsItem(),
                data = dt.ToList<AccPaAccount_ACD040M_qry>()
            };
        }










    }
}