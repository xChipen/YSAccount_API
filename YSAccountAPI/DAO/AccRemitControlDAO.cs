using DAO;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Helpers;


namespace DAO
{
    public class AccRemitDAO : BaseClass
    {
        // Query return
        public static rs_AccRemitQuery rsQuery(List<AccRemit> data = null, Pagination pagination = null, int retCode = 1, string retMsg = "失敗")
        {
            return new rs_AccRemitQuery()
            {
                result = new rsItem() { retCode = retCode, retMsg = retMsg },
                data = data,
                pagination = pagination
            };
        }

        //新增
        public  bool add2(AccRemit model, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(model.RECT_COMPID))
            {
                Log.Info("AccRemitDAO.add_Batch: 請輸入公司代號");
                return false;
            }
            if (string.IsNullOrEmpty(model.RECT_ACNMID))
            {
                Log.Info("AccRemitDAO.add_Batch: 請輸入匯款銀行存款科目代號");
                return false;
            }
            if (string.IsNullOrEmpty(model.RECT_BANKID))
            {
                Log.Info("AccRemitDAO.add_Batch: 請輸入匯款銀行代號");
                return false;
            }
            if (string.IsNullOrEmpty(model.RECT_ACNO))
            {
                Log.Info("AccRemitDAO.add_Batch: 請輸入匯款帳號");
                return false;
            }

            DataTable dt = isExist(model);

            if (dt != null && dt.Rows.Count != 0)
            {
                Log.Info("AccRemitDAO.add_Batch: 資料已經存在");
                return false;
            }

            return insert_data(model, dao);
        }

        public  rs add_Batch(AccRemitAdd2 model)
        {
            if (model.data.Count == 0) {
                return CommDAO.getRs(1, "無資料");
            }

            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                int ii = 0;
                foreach (AccRemit item in model.data)
                {
                    ii += add2(item, dao) ? 1 : 0;
                }

                if (ii != 0)
                {
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


        public  rs add(AccRemitAdd model)
        {
            if (string.IsNullOrEmpty(model.data.RECT_COMPID))
                return CommDAO.getRs(1, "請輸入公司代號");
            if (string.IsNullOrEmpty(model.data.RECT_ACNMID))
                return CommDAO.getRs(1, "請輸入匯款銀行存款科目代號");
            if (string.IsNullOrEmpty(model.data.RECT_BANKID))
                return CommDAO.getRs(1, "請輸入匯款銀行代號");
            if (string.IsNullOrEmpty(model.data.RECT_ACNO))
                return CommDAO.getRs(1, "請輸入匯款帳號");
           
            DataTable dt = isExist(model.data);
            
            if (dt != null && dt.Rows.Count != 0)
            {                
                return CommDAO.getRs(1,"資料已經存在");
            }

            bool bOK = insert_data(model.data);
            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }

        public  rs update(AccRemitAdd model)
        {
            if (string.IsNullOrEmpty(model.data.RECT_COMPID))
                return CommDAO.getRs(1, "請輸入公司代號");
            if (string.IsNullOrEmpty(model.data.RECT_ACNMID))
                return CommDAO.getRs(1, "請輸入匯款銀行存款科目代號");
            
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

        public  rs delete(AccRemitAdd model)
        {
            if (string.IsNullOrEmpty(model.data.RECT_COMPID))
                return CommDAO.getRs(1, "請輸入公司代號");
            if (string.IsNullOrEmpty(model.data.RECT_ACNMID))
                return CommDAO.getRs(1, "請輸入匯款銀行存款科目代號");

            DataTable dt = isExist(model.data);

            bool bOK = delete_data(model);
            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }


        public  DataTable isExist(AccRemit model)
        {
            string sql = string.Format(@"SELECT * FROM ACC_REMIT_CONTROL 
WHERE 1=1 AND RECT_COMPID='{0}' AND RECT_ACNMID='{1}'"
,model.RECT_COMPID,model.RECT_ACNMID);

            return comm.DB.RunSQL(sql);
        }

        public  bool insert_data(AccRemit model, CommDAO dao = null)
        {
            string sql = $@"INSERT INTO ACC_REMIT_CONTROL(
RECT_COMPID, RECT_ACNMID, RECT_BANKID, RECT_ACNO) 
VALUES 
(@RECT_COMPID, @RECT_ACNMID, @RECT_BANKID, @RECT_ACNO ) ";

            object[] obj = new object[] {
                model.RECT_COMPID,
                model.RECT_ACNMID,
                model.RECT_BANKID,
                model.RECT_ACNO };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
            
        }

        public  bool update_data(AccRemitAdd model)
        {
            string sql = $@"UPDATE ACC_REMIT_CONTROL SET RECT_BANKID=@RECT_BANKID, RECT_ACNO=@RECT_ACNO 
WHERE RECT_COMPID=@RECT_COMPID AND RECT_ACNMID=@RECT_ACNMID ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                model.data.RECT_BANKID,
                model.data.RECT_ACNO,
                model.data.RECT_COMPID,
                model.data.RECT_ACNMID});
            return bOK;
        }
        public  bool delete_data(AccRemitAdd model)
        {
            string sql = $@"DELETE ACC_REMIT_CONTROL
WHERE RECT_COMPID=@RECT_COMPID AND RECT_ACNMID=@RECT_ACNMID ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                model.data.RECT_COMPID,
                model.data.RECT_ACNMID });
            return bOK;
        }

        public  rs_AccRemitQuery select_data(AccRemitAdd model)
        {
            int pageNumber=0;
            int pageSize=0;
            int pageNumbers=0;
            CommDAO.initPagination(model.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = $@"SELECT * FROM ACC_REMIT_CONTROL
WHERE 1=1 ";

            if (model.data.RECT_COMPID != null)
                sql += $" AND RECT_COMPID='{model.data.RECT_COMPID}' ";

            if (! string.IsNullOrEmpty( model.data.RECT_ACNMID) )
                sql += $" AND RECT_ACNMID='{model.data.RECT_ACNMID}' ";

            sql += " ORDER BY RECT_COMPID,RECT_ACNMID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = new DataTable();
            dt = comm.DB.RunSQL(sql, new object[] { });

            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccRemit> rs = dt.ToList<AccRemit>();
            return rsQuery(rs,
                (pageNumber != 0) ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null,
                0, "成功");
        }
    }
}