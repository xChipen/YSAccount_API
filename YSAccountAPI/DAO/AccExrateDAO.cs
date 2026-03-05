
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Helpers;

namespace DAO
{
    public class AccExrateDAO : BaseClass
    {
        // Query return
        public static rs_AccExrateQuery rsQuery(List<AccExrate> data = null, Pagination pagination = null, int retCode = 1, string retMsg = "失敗")
        {
            return new rs_AccExrateQuery()
            {
                result = new rsItem() { retCode = retCode, retMsg = retMsg },
                data = data,
                pagination = pagination
            };
        }

        //新增
        public rs add_Batch(AccExrateAdd2 model)
        {
            if (model.data.Count == 0)
            {
                return CommDAO.getRs(1, "無資料");
            }

            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                int ii = 0;
                foreach (AccExrate item in model.data)
                {
                    ii += add2(item, dao) ? 1 : 0;
                }

                if (ii != 0) {
                    dao.DB.Commit();
                    return CommDAO.getRs(0, "成功");
                }
            }
            catch (Exception e)
            {
                Log.Info(e.Message);
            }

            dao.DB.Rollback();
            return CommDAO.getRs(1, "失敗");
        }

        public  bool add2(AccExrate model, CommDAO dao = null)
        {
            if (model.EXRA_YEAR == null)
            {
                Log.Info("AccExrateDAO.Add2: 請輸入年");
                return false;
            }
            if (model.EXRA_MONTH == null)
            {
                Log.Info("AccExrateDAO.Add2: 請輸入月");
                return false;
            }
            if (string.IsNullOrEmpty(model.EXRA_CURRID))
            {
                Log.Info("AccExrateDAO.Add2: 請輸入幣別");
                return false;
            }

            DataTable dt = isExist(model);

            if (dt != null && dt.Rows.Count != 0)
            {
                Log.Info("AccExrateDAO.Add2: 資料已經存在");
                return false;
            }

            return insert_data(model, dao);
        }

        public  rs add(AccExrateAdd model, CommDAO dao = null)
        {
            if (model.data.EXRA_YEAR==null)
                return CommDAO.getRs(1, "請輸入年");
            if (model.data.EXRA_MONTH == null)
                return CommDAO.getRs(1, "請輸入月");
            if (string.IsNullOrEmpty(model.data.EXRA_CURRID))
                return CommDAO.getRs(1, "請輸入幣別");

            DataTable dt = isExist(model.data);

            if (dt != null && dt.Rows.Count != 0)
            {
                return CommDAO.getRs(1, "資料已經存在");
            }

            bool bOK = insert_data(model.data, dao);
            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }

        public  rs update(AccExrateAdd model)
        {
            if (model.data.EXRA_YEAR == null)
                return CommDAO.getRs(1, "請輸入年");
            if (model.data.EXRA_MONTH == null)
                return CommDAO.getRs(1, "請輸入月");
            if (string.IsNullOrEmpty(model.data.EXRA_CURRID))
                return CommDAO.getRs(1, "請輸入幣別");

            DataTable dt = isExist(model.data);

            if (dt == null || dt.Rows.Count == 0)
            {
                return CommDAO.getRs(1, "資料不存在");
            }

            bool bOK = update_data(model);
            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }

        public  rs delete(AccExrateAdd model)
        {
            if (model.data.EXRA_YEAR == null)
                return CommDAO.getRs(1, "請輸入年");
            if (model.data.EXRA_MONTH == null)
                return CommDAO.getRs(1, "請輸入月");
            if (string.IsNullOrEmpty(model.data.EXRA_CURRID))
                return CommDAO.getRs(1, "請輸入幣別");

            DataTable dt = isExist(model.data);

            //if (dt != null && dt.Rows.Count != 0)
            //{
            //    return CommDAO.getRs(1, "資料不存在");
            //}

            bool bOK = delete_data(model);
            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }


        public  DataTable isExist(AccExrate model)
        {
            string sql = string.Format(@"SELECT * FROM ACC_EXRATE 
WHERE 1=1 
AND EXRA_YEAR={0}
AND EXRA_MONTH={1}
AND EXRA_CURRID ='{2}'"
, model.EXRA_YEAR, model.EXRA_MONTH, model.EXRA_CURRID);

            return comm.DB.RunSQL(sql, new object[] { });
        }

        public  bool insert_data(AccExrate model, CommDAO dao = null)
        {
            string sql = $@"INSERT INTO ACC_EXRATE(
EXRA_YEAR, EXRA_MONTH, EXRA_CURRID, EXRA_RATE1,EXRA_RATE2,EXRA_RATE3,EXRA_RATE_E) 
VALUES 
(@EXRA_YEAR, @EXRA_MONTH, @EXRA_CURRID, @EXRA_RATE1,@EXRA_RATE2,@EXRA_RATE3,@EXRA_RATE_E ) ";

            object[] obj = new object[] {
                model.EXRA_YEAR,
                model.EXRA_MONTH,
                model.EXRA_CURRID,
                model.EXRA_RATE1,
                model.EXRA_RATE2,
                model.EXRA_RATE3,
                model.EXRA_RATE_E};

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public  bool update_data(AccExrateAdd model)
        {
            string sql = $@"UPDATE ACC_EXRATE SET EXRA_RATE1=@EXRA_RATE1, EXRA_RATE2=@EXRA_RATE2 
, EXRA_RATE3=@EXRA_RATE3 , EXRA_RATE_E=@EXRA_RATE_E 
WHERE EXRA_YEAR=@EXRA_YEAR AND EXRA_MONTH=@EXRA_MONTH AND EXRA_CURRID=@EXRA_CURRID ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                model.data.EXRA_RATE1,
                model.data.EXRA_RATE2,
                model.data.EXRA_RATE3,
                model.data.EXRA_RATE_E,
                model.data.EXRA_YEAR,
                model.data.EXRA_MONTH,
                model.data.EXRA_CURRID});
            return bOK;
        }
        public  bool delete_data(AccExrateAdd model)
        {
            string sql = $@"DELETE ACC_EXRATE
WHERE EXRA_YEAR=@EXRA_YEAR AND EXRA_MONTH=@EXRA_MONTH AND EXRA_CURRID=@EXRA_CURRID ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                model.data.EXRA_YEAR,
                model.data.EXRA_MONTH,
                model.data.EXRA_CURRID });
            return bOK;
        }

        public  rs_AccExrateQuery select_data(AccExrateAdd model)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(model.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = $@"SELECT * FROM ACC_EXRATE
WHERE 1=1 ";

            if (model.data.EXRA_YEAR != null && model.data.EXRA_YEAR_E != null)
            {
                sql += $" AND EXRA_YEAR>={model.data.EXRA_YEAR} and EXRA_YEAR<={model.data.EXRA_YEAR_E}";
            }
            else
            {
                if (model.data.EXRA_YEAR != null)
                    sql += $" AND EXRA_YEAR={model.data.EXRA_YEAR}";
            }

            if (model.data.EXRA_MONTH != null && model.data.EXRA_MONTH_E != null)
            {
                sql += $" AND EXRA_MONTH>={model.data.EXRA_MONTH} and EXRA_MONTH<={model.data.EXRA_MONTH_E}";
            }
            else
            {
                if (model.data.EXRA_MONTH != null)
                    sql += $" AND EXRA_MONTH={model.data.EXRA_MONTH}";
            }

            if (!string.IsNullOrEmpty(model.data.EXRA_CURRID))
                sql += $" AND EXRA_CURRID='{model.data.EXRA_CURRID}'";

            sql += " ORDER BY EXRA_YEAR,EXRA_MONTH,EXRA_CURRID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql, new object[] { });

            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccExrate> rs = dt.ToList<AccExrate>();
            return rsQuery(rs,
                pageNumber!=0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null,
                0, "成功");
        }
    }
}