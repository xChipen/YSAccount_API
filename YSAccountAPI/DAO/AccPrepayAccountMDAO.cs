using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccPrepayAccountMDAO : BaseClass
    {
        private const string tableName = "ACC_PREPAY_ACCOUNT_M";
        private List<string> PK; 

        public AccPrepayAccountMDAO() {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call

        public bool AUD(AccPrepayAccountM_item data, string employeeNo, string name, CommDAO dao = null)
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

        public bool isExist(AccPrepayAccountM_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccPrepayAccountM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.PRAM_A_USER_ID = employeeNo;
            data.PRAM_A_USER_NM = name;
            data.PRAM_A_DATE = DateTime.Now;
            data.PRAM_U_USER_ID = employeeNo;
            data.PRAM_U_USER_NM = name;
            data.PRAM_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccPrepayAccountM_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.PRAM_U_USER_ID = employeeNo;
            data.PRAM_U_USER_NM = name;
            data.PRAM_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("PRAM_A_USER_ID");
            outSL.Add("PRAM_A_USER_NM");
            outSL.Add("PRAM_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccPrepayAccountM_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccPrepayAccountM_item data)
        {
            #region check
            if (string.IsNullOrEmpty(data.PRAM_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.PRAM_NO))
            {
                Log.Info("_QueryBatch: 無傳票號碼");
                return null;
            }
            #endregion

            string sql = $"SELECT * from {tableName} WHERE 1=1 AND PRAM_COMPID='{data.PRAM_COMPID}' ";
            sql += CommDAO.sql_ep(data.PRAM_NO, "PRAM_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion

        public rs Add(AccPrepayAccountM_ins data)
        {
            // 填入預設資料
            data.data.PRAM_A_USER_ID = data.baseRequest.employeeNo;
            data.data.PRAM_A_USER_NM = data.baseRequest.name;
            data.data.PRAM_A_DATE = DateTime.Now;
            data.data.PRAM_U_USER_ID = data.baseRequest.employeeNo;
            data.data.PRAM_U_USER_NM = data.baseRequest.name;
            data.data.PRAM_U_DATE = DateTime.Now;

            if (!isExist(data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public  rs Update(AccPrepayAccountM_ins data)
        {
            // 填入預設資料
            data.data.PRAM_U_USER_ID = data.baseRequest.employeeNo;
            data.data.PRAM_U_USER_NM = data.baseRequest.name;
            data.data.PRAM_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("PRAM_A_USER_ID");
            outSL.Add("PRAM_A_USER_NM");
            outSL.Add("PRAM_A_DATE");

            if (isExist(data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK, outSL);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public  rs Delete(AccPrepayAccountM_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rsAccPrepayAccountM_qry Query(AccPrepayAccountM_qry data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            #region Check Params
            bool bOK = true;
            if (string.IsNullOrEmpty(data.data.PRAM_COMPID))
            {
                Log.Info("rsAccVoumstM_qry: 無公司代號");
                bOK = false;
            }
            if (!bOK)
                return new rsAccPrepayAccountM_qry() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
            #endregion

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 AND PRAM_COMPID='{data.data.PRAM_COMPID}' ";

            if (string.IsNullOrEmpty(data.data.PRAM_NO))
            {
                sql += $" AND PRAM_NO LIKE '%{data.data.PRAM_NO}%'";
            }

            if (data.data.PRAM_DATE != null)
            {
                sql += $" AND convert(char(8), PRAM_DATE ,112) = '{string.Format("{0:yyyyMMdd}", data.data.PRAM_DATE)}'";
            }
            #endregion

            #region 分頁
            sql += " ORDER BY PRAM_COMPID,PRAM_NO ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccPrepayAccountM_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccPrepayAccountM>(),
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        public bool DeleteAll2(string PRAD_COMPID, string VOMD_NO, CommDAO dao = null)
        {
            string sql = $@"DELETE {tableName} where PRAM_NO in 
(
select VOMD_INVNO from ACC_VOUMST_D where VOMD_COMPID = '{PRAD_COMPID}' 
and VOMD_NO = '{VOMD_NO}' and VOMD_INVNO <> ''
)";
            if (dao == null)
                return comm.DB.ExecSQL(sql);
            else
                return dao.DB.ExecSQL_T(sql);
        }

    }
}