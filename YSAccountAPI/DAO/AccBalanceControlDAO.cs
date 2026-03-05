using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;
using System;

namespace DAO
{
    public class AccBalanceControlDAO : BaseClass
    {
        // 資料是否存在
        public DataTable haveAccBalanceControl(string BACT_COMPID, string BACT_ACNMID)
        {
            string sql = "SELECT * FROM ACC_BALANCE_CONTROL WHERE BACT_COMPID = @BACT_COMPID ";
            if (!string.IsNullOrEmpty(BACT_ACNMID))
                sql += " and BACT_ACNMID = @BACT_ACNMID ";

            if (string.IsNullOrEmpty(BACT_COMPID))
                return comm.DB.RunSQL(sql, new object[] { BACT_COMPID });
            else
                return comm.DB.RunSQL(sql, new object[] { BACT_COMPID, BACT_ACNMID });
        }
        // 新增
        public bool addAccBalanceControl2(AccBalanceControl data, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(data.BACT_COMPID))
            {
                Log.Info("addAccBalanceControl2: 未輸入 公司代號");
                return false;
            }

            if (string.IsNullOrEmpty(data.BACT_ACNMID))
            {
                Log.Info("addAccBalanceControl2: 未輸入 科目代號");
                return false;
            }

            DataTable dt = haveAccBalanceControl(data.BACT_COMPID, data.BACT_ACNMID);
            if (dt != null && dt.Rows.Count != 0)
            {
                Log.Info("addAccBalanceControl2: 資料已經存在");
                return false;
            }

            string sql = $@"INSERT INTO ACC_BALANCE_CONTROL(
BACT_COMPID,BACT_ACNMID,BACT_FLG,BACT_DEPT_FLG,BACT_TRANID_FLG,BACT_INVNO_FLG
) VALUES (
@BACT_COMPID,@BACT_ACNMID,@BACT_FLG,@BACT_DEPT_FLG,@BACT_TRANID_FLG,@BACT_INVNO_FLG ) ";

            object[] obj = new object[] {
                data.BACT_COMPID,
                data.BACT_ACNMID,
                data.BACT_FLG,
                data.BACT_DEPT_FLG,
                data.BACT_TRANID_FLG,
                data.BACT_INVNO_FLG
            };
            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public rs addAccBalanceControl_Batch(AccBalanceControl_ins2 data)
        {
            if (data.data.Count == 0)
            {
                return CommDAO.getRs(1, "無資料");
            }

            CommDAO dao = new CommDAO();
            dao.DB.BeginTransaction();
            try
            {
                int ii = 0;
                foreach (AccBalanceControl item in data.data)
                {
                    ii += addAccBalanceControl2(item, dao) ? 1 : 0;
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
        public rs addAccBalanceControl(AccBalanceControl_ins data)
        {
            if (string.IsNullOrEmpty(data.data.BACT_COMPID))
                return CommDAO.getRs(1, "未輸入 公司代號");

            if (string.IsNullOrEmpty(data.data.BACT_ACNMID))
                return CommDAO.getRs(1, "未輸入 科目代號");

            DataTable dt = haveAccBalanceControl(data.data.BACT_COMPID, data.data.BACT_ACNMID);
            if (dt != null && dt.Rows.Count != 0)
                return CommDAO.getRs(1, "資料已經存在");

            string sql = $@"INSERT INTO ACC_BALANCE_CONTROL(
BACT_COMPID,BACT_ACNMID,BACT_FLG,BACT_DEPT_FLG,BACT_TRANID_FLG,BACT_INVNO_FLG
) VALUES (
@BACT_COMPID,@BACT_ACNMID,@BACT_FLG,@BACT_DEPT_FLG,@BACT_TRANID_FLG,@BACT_INVNO_FLG ) ";
            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.BACT_COMPID,
                data.data.BACT_ACNMID,
                data.data.BACT_FLG,
                data.data.BACT_DEPT_FLG,
                data.data.BACT_TRANID_FLG,
                data.data.BACT_INVNO_FLG
            });
            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }
        // 修改
        public rs updateAccBalanceControl(AccBalanceControl_ins data)
        {
            if (string.IsNullOrEmpty(data.data.BACT_COMPID))
                return CommDAO.getRs(1, "未輸入 公司代號");

            if (string.IsNullOrEmpty(data.data.BACT_ACNMID))
                return CommDAO.getRs(1, "未輸入 科目代號");

            DataTable dt = haveAccBalanceControl(data.data.BACT_COMPID, data.data.BACT_ACNMID);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, " 資料不存在");

            string sql = $@"UPDATE ACC_BALANCE_CONTROL SET
BACT_FLG=@BACT_FLG,BACT_DEPT_FLG=@BACT_DEPT_FLG,BACT_TRANID_FLG=@BACT_TRANID_FLG,BACT_INVNO_FLG=@BACT_INVNO_FLG  
WHERE BACT_COMPID='{data.data.BACT_COMPID}' and BACT_ACNMID='{data.data.BACT_ACNMID}' ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.BACT_FLG,
                data.data.BACT_DEPT_FLG,
                data.data.BACT_TRANID_FLG,
                data.data.BACT_INVNO_FLG
            });
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }
        // 刪除
        public rs deleteAccBalanceControl(AccBalanceControl_del data)
        {
            if (string.IsNullOrEmpty(data.data.BACT_COMPID))
                return CommDAO.getRs(1, "未輸入 公司代號");

            string sql = $"DELETE ACC_BALANCE_CONTROL WHERE BACT_COMPID='{data.data.BACT_COMPID}'";

            if (!string.IsNullOrEmpty(data.data.BACT_ACNMID))
            {
                sql += $" and BACT_ACNMID='{data.data.BACT_ACNMID}'";
            }

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }
        // 查詢
        public AccBalanceControl_query queryAccBalanceControl(AccBalanceControl_del data)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "SELECT * from ACC_BALANCE_CONTROL where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.BACT_COMPID))
                sql += $" and BACT_COMPID='{data.data.BACT_COMPID}'";

            if (!string.IsNullOrEmpty(data.data.BACT_ACNMID))
                sql += $" and BACT_ACNMID='{data.data.BACT_ACNMID}'";

            sql += " ORDER BY BACT_COMPID,BACT_ACNMID ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccBalanceControl> rs = dt.ToList<AccBalanceControl>();
            return rsAccBalanceControl(rs,
                pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null,
                0, "成功");
        }

        // Query return
        public static AccBalanceControl_query rsAccBalanceControl(List<AccBalanceControl> data = null, Pagination pagination = null, int retCode = 1, string retMsg = "失敗")
        {
            return new AccBalanceControl_query()
            {
                result = new rsItem() { retCode = retCode, retMsg = retMsg },
                data = data,
                pagination = pagination
            };
        }
    }
}