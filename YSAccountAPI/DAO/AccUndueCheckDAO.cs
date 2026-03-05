using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccUndueCheckDAO: BaseClass
    {

        private const string tableName = "ACC_UNDUE_CHECK";
        private List<string> PK;

        public AccUndueCheckDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccUndueCheck_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccUndueCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.UNDU_A_USER_ID = employeeNo;
            data.UNDU_A_USER_NM = name;
            data.UNDU_A_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccUndueCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("UNDU_A_USER_ID");
            outSL.Add("UNDU_A_USER_NM");
            outSL.Add("UNDU_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccUndueCheck_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        #endregion


        public bool _AUD(AccUndueCheck_item item, string employeeNo, string name, CommDAO dao)
        {
            switch (item.State)
            {
                case "A": return _AddBatch(item, employeeNo, name, dao);
                case "U": return _UpdateBatch(item, employeeNo, name, dao);
                case "D": return _DeleteBatch(item, dao);
            }
            return false;
        }
        public rsAccUndueCheck_Batch AUD(List<AccUndueCheck_item> data, string employeeNo, string name)
        {
            if (data == null || data.Count == 0)
                return new rsAccUndueCheck_Batch { result = CommDAO.getRsItem1() };

            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccUndueCheck_item item in data)
                {
                    bool bOK = _AUD(item, employeeNo, name, dao);
                    if (!bOK)
                    {
                        item.errMsg = "更新失敗";
                        throw new Exception(item.errMsg);
                    }
                }
                dao.DB.Commit();
            }
            catch(Exception ex)
            {
                dao.DB.Rollback();
                return new rsAccUndueCheck_Batch
                {
                    result = CommDAO.getRsItem1(ex.Message),
                    data = data
                };
            }

            return new rsAccUndueCheck_Batch {
                result = CommDAO.getRsItem(),
                data = data
            };
        }

        public rs Add(AccUndueCheck_ins data)
        {
            // 填入預設資料
            data.data.UNDU_A_USER_ID = data.baseRequest.employeeNo;
            data.data.UNDU_A_USER_NM = data.baseRequest.name;
            data.data.UNDU_A_DATE = DateTime.Now;

            if (!isExist(data.data))
            {
                bool bOK = CommDAO._add(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料已經存在");
        }

        public rs Update(AccUndueCheck_ins data)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("UNDU_A_USER_ID");
            outSL.Add("UNDU_A_USER_NM");
            outSL.Add("UNDU_A_DATE");

            if (isExist(data.data))
            {
                bool bOK = CommDAO._update(comm, data.data, tableName, PK, outSL);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete(AccUndueCheck_ins data)
        {
            if (CommDAO.isExist(comm, data.data, tableName, PK))
            {
                bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
                if (bOK)
                    return CommDAO.getRs();
            }
            return CommDAO.getRs(1, "資料不存在");
        }

        public rs Delete2(AccUndueCheck_item data, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(data.UNDU_COMPID))
                return CommDAO.getRs1("無公司代號");
            if (data.UNDU_YEAR == null)
                return CommDAO.getRs1("無會計年月的年");
            if (data.UNDU_MONTH == null)
                return CommDAO.getRs1("無會計年月的月");
            if (string.IsNullOrEmpty(data.UNDU_ACCD))
                return CommDAO.getRs1("無銀行科目代號");

            string sql = $"DELETE {tableName} Where UNDU_COMPID='{data.UNDU_COMPID}' ";
            sql += CommDAO.sql_ep(data.UNDU_YEAR.ToString(), "UNDU_YEAR");
            sql += CommDAO.sql_ep(data.UNDU_MONTH.ToString(), "UNDU_MONTH");
            sql += CommDAO.sql_ep(data.UNDU_ACCD.ToString(), "UNDU_ACCD");

            bool bOK;
            if (dao == null)
                bOK = comm.DB.ExecSQL(sql);
            else
                bOK = dao.DB.ExecSQL(sql);

            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs1("刪除失敗");
        }

        public rsAccUndueCheck_qry Query(AccUndueCheck_ins data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            if (string.IsNullOrEmpty(data.data.UNDU_COMPID))
            {
                return new rsAccUndueCheck_qry() { result = new rsItem() { retCode = 1, retMsg = "無輸入 公司代號" } };
            }

            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 AND UNDU_COMPID='{data.data.UNDU_COMPID}' ";

            sql += CommDAO.sql_ep(data.data.UNDU_YEAR.ToString(), "UNDU_YEAR");
            sql += CommDAO.sql_ep(data.data.UNDU_MONTH.ToString(), "UNDU_MONTH");
            sql += CommDAO.sql_ep(data.data.UNDU_ACCD, "UNDU_ACCD");
            sql += CommDAO.sql_ep(data.data.UNDU_CHKNO, "UNDU_CHKNO");
            #endregion

            #region 分頁
            sql += " ORDER BY UNDU_COMPID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccUndueCheck_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccUndueCheck>(),
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }



    }
}