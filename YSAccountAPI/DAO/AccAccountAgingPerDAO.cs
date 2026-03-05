using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccAccountAgingPerDAO:BaseClass
    {
        private const string tableName = "ACC_ACCOUNT_AGING_PER";
        private List<string> PK;

        public AccAccountAgingPerDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccAccountAgingPer_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        public bool _AddBatch(AccAccountAgingPer_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.APER_A_USER_ID = employeeNo;
            data.APER_A_USER_NM = name;
            data.APER_A_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        public bool _UpdateBatch(AccAccountAgingPer_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("APER_A_USER_ID");
            outSL.Add("APER_A_USER_NM");
            outSL.Add("APER_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        public bool _DeleteBatch(AccAccountAgingPer_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        #endregion


        public bool _AUD(AccAccountAgingPer_item item, string employeeNo, string name, CommDAO dao)
        {
            switch (item.State)
            {
                case "A": return _AddBatch(item, employeeNo, name, dao);
                case "U": return _UpdateBatch(item, employeeNo, name, dao);
                case "D": return _DeleteBatch(item, dao);
            }
            return false;
        }
        public rsAccAccountAgingPer_Batch AUD(List<AccAccountAgingPer_item> data, string employeeNo, string name)
        {
            if (data == null || data.Count == 0)
                return new rsAccAccountAgingPer_Batch { result = CommDAO.getRsItem1() };

            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                foreach (AccAccountAgingPer_item item in data)
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
            catch (Exception ex)
            {
                dao.DB.Rollback();
                return new rsAccAccountAgingPer_Batch
                {
                    result = CommDAO.getRsItem1(ex.Message),
                    data = data
                };
            }

            return new rsAccAccountAgingPer_Batch
            {
                result = CommDAO.getRsItem(),
                data = data
            };
        }

        //public rs Add(AccUndueCheck_ins data)
        //{
        //    // 填入預設資料
        //    data.data.UNDU_A_USER_ID = data.baseRequest.employeeNo;
        //    data.data.UNDU_A_USER_NM = data.baseRequest.name;
        //    data.data.UNDU_A_DATE = DateTime.Now;

        //    if (!isExist(data.data))
        //    {
        //        bool bOK = CommDAO._add(comm, data.data, tableName, PK);
        //        if (bOK)
        //            return CommDAO.getRs();
        //    }
        //    return CommDAO.getRs(1, "資料已經存在");
        //}

        //public rs Update(AccUndueCheck_ins data)
        //{
        //    // 不更新, 排除清單
        //    List<string> outSL = new List<string>();
        //    outSL.Add("UNDU_A_USER_ID");
        //    outSL.Add("UNDU_A_USER_NM");
        //    outSL.Add("UNDU_A_DATE");

        //    if (isExist(data.data))
        //    {
        //        bool bOK = CommDAO._update(comm, data.data, tableName, PK, outSL);
        //        if (bOK)
        //            return CommDAO.getRs();
        //    }
        //    return CommDAO.getRs(1, "資料不存在");
        //}

        //public rs Delete(AccUndueCheck_ins data)
        //{
        //    if (CommDAO.isExist(comm, data.data, tableName, PK))
        //    {
        //        bool bOK = CommDAO._delete(comm, data.data, tableName, PK);
        //        if (bOK)
        //            return CommDAO.getRs();
        //    }
        //    return CommDAO.getRs(1, "資料不存在");
        //}

        //public rs Delete2(AccUndueCheck_item data)
        //{
        //    if (string.IsNullOrEmpty(data.UNDU_COMPID))
        //        return CommDAO.getRs1("無公司代號");
        //    if (data.UNDU_YEAR == null)
        //        return CommDAO.getRs1("無會計年月的年");
        //    if (data.UNDU_MONTH == null)
        //        return CommDAO.getRs1("無會計年月的月");
        //    if (string.IsNullOrEmpty(data.UNDU_ACCD))
        //        return CommDAO.getRs1("無銀行科目代號");

        //    string sql = $"DELETE {tableName} Where UNDU_COMPID='{data.UNDU_COMPID}' ";
        //    sql += CommDAO.sql_ep(data.UNDU_YEAR.ToString(), "UNDU_YEAR");
        //    sql += CommDAO.sql_ep(data.UNDU_MONTH.ToString(), "UNDU_MONTH");
        //    sql += CommDAO.sql_ep(data.UNDU_ACCD.ToString(), "UNDU_ACCD");

        //    bool bOK = comm.DB.ExecSQL(sql);
        //    if (bOK)
        //        return CommDAO.getRs();

        //    return CommDAO.getRs1("刪除失敗");
        //}

        public rsAccAccountAgingPer_qry Query(AccAccountAgingPer_qry data)
        {
            int pageNumber = 0;
            int pageSize = 0;
            int pageNumbers = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);
            
            #region SQL
            string sql = $"SELECT * from {tableName} WHERE 1=1 ";

            sql += CommDAO.sql_ep(data.data.APER_KIND, "APER_KIND");
            sql += CommDAO.sql_ep_int(data.data.APER_DAYS, "APER_DAYS");
            sql += CommDAO.sql_ep_int(data.data.APER_DAYE, "APER_DAYE");
            #endregion

            #region 分頁
            sql += " ORDER BY APER_KIND ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);
            #endregion

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            return new rsAccAccountAgingPer_qry()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = dt.ToList<AccAccountAgingPer>(),
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        public rsACF010M_rs ACF010M_APER_KIND()
        {
            string sql = $@"SELECT APER_KIND FROM ACC_ACCOUNT_AGING_PER
GROUP BY APER_KIND";
            DataTable dt = comm.DB.RunSQL(sql);
            return new rsACF010M_rs {
                result = CommDAO.getRsItem(),
                data = dt.ToList<ACF010M_rs>()
            };
        }

    }
}