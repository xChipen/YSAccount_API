using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;

namespace DAO
{
    public class AccJouralMDAO : BaseClass
    {
        // 資料是否存在
        public  DataTable haveAccJouralM(string JOUM_COMPID, string JOUM_CODE)
        {
            string sql = "SELECT * FROM ACC_JOURAL_M WHERE JOUM_COMPID = @JOUM_COMPID ";
            if (!string.IsNullOrEmpty(JOUM_CODE))
                sql += " and JOUM_CODE = @JOUM_CODE ";

            if (string.IsNullOrEmpty(JOUM_CODE))
                return comm.DB.RunSQL(sql, new object[] { JOUM_COMPID });
            else
                return comm.DB.RunSQL(sql, new object[] { JOUM_COMPID, JOUM_CODE });
        }

        // 新增
        public  bool _addAccJouralM(AccJouralM data, string employeeNo, string name, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(data.JOUM_COMPID))
            {
                Log.Info("_addAccJouralM: 未輸入 公司代號");
                return false;
            }

            if (string.IsNullOrEmpty(data.JOUM_CODE))
            {
                Log.Info("_addAccJouralM: 未輸入 常用分錄代號");
                return false;
            }

            DataTable dt = haveAccJouralM(data.JOUM_COMPID, data.JOUM_CODE);
            if (dt != null && dt.Rows.Count != 0)
            {
                Log.Info("_addAccJouralM: 資料已經存在");
                return false;
            }

            string sql = $@"INSERT INTO ACC_JOURAL_M(
JOUM_COMPID,JOUM_CODE,JOUM_VALID,JOUM_MEMO,
JOUM_A_USER_ID,JOUM_A_USER_NM,JOUM_A_DATE,JOUM_U_USER_ID,JOUM_U_USER_NM,JOUM_U_DATE
) VALUES (
@JOUM_COMPID,@JOUM_CODE,@JOUM_VALID,@JOUM_MEMO,
@JOUM_A_USER_ID,@JOUM_A_USER_NM,GetDate(),@JOUM_U_USER_ID,@JOUM_U_USER_NM,GetDate() ) ";

            object[] obj = new object[] {
                data.JOUM_COMPID,
                data.JOUM_CODE,
                data.JOUM_VALID,
                data.JOUM_MEMO,
                employeeNo,
                name,
                employeeNo,
                name
            };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }
        public  rs addAccJouralM(AccJouralM_ins data)
        {
            if (string.IsNullOrEmpty(data.data.JOUM_COMPID))
                return CommDAO.getRs(1, "未輸入 公司代號");

            if (string.IsNullOrEmpty(data.data.JOUM_CODE))
                return CommDAO.getRs(1, "未輸入 常用分錄代號");
            
            DataTable dt = haveAccJouralM(data.data.JOUM_COMPID, data.data.JOUM_CODE);
            if (dt != null && dt.Rows.Count != 0)
                return CommDAO.getRs(1, "資料已經存在");

            bool bOK = _addAccJouralM(data.data, data.baseRequest.employeeNo, data.baseRequest.name);

            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }

        // 修改
        public  bool _updateAccJouralM(AccJouralM data, string employeeNo, string name, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(data.JOUM_COMPID))
            {
                Log.Info("_updateAccJouralM: 未輸入 公司代號");
                return false;
                    
            }

            if (string.IsNullOrEmpty(data.JOUM_CODE)) {
                Log.Info("_updateAccJouralM: 未輸入 常用分錄代號");
                return false;
            }

            DataTable dt = haveAccJouralM(data.JOUM_COMPID, data.JOUM_CODE);
            if (dt == null || dt.Rows.Count == 0)
            {
                Log.Info("_updateAccJouralM: 資料不存在");
                return false;
            }

            string sql = $@"UPDATE ACC_JOURAL_M SET
JOUM_VALID=@JOUM_VALID,JOUM_MEMO=@JOUM_MEMO,
JOUM_U_USER_ID=@JOUM_U_USER_ID,JOUM_U_USER_NM=@JOUM_U_USER_NM,JOUM_U_DATE=GetDate() 
WHERE JOUM_COMPID='{data.JOUM_COMPID}' and JOUM_CODE='{data.JOUM_CODE}' ";

            object[] obj = new object[] {
                data.JOUM_VALID,
                data.JOUM_MEMO,
                employeeNo,
                name
            };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }
        public  rs updateAccJouralM(AccJouralM_ins data)
        {
            if (string.IsNullOrEmpty(data.data.JOUM_COMPID))
                return CommDAO.getRs(1, "未輸入 公司代號");

            if (string.IsNullOrEmpty(data.data.JOUM_CODE))
                return CommDAO.getRs(1, "未輸入 常用分錄代號");

            DataTable dt = haveAccJouralM(data.data.JOUM_COMPID, data.data.JOUM_CODE);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, " 資料不存在");

            bool bOK = _updateAccJouralM(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        // 刪除
        public bool _deleteAccJouralM(string JOUM_COMPID, string JOUM_CODE, CommDAO dao = null)
        {
            if (string.IsNullOrEmpty(JOUM_COMPID))
            {
                Log.Info("_deleteAccJouralM: 未輸入 公司代號");
                return false;
            }
                
            string sql = $"DELETE ACC_JOURAL_M WHERE JOUM_COMPID='{JOUM_COMPID}'";

            if (!string.IsNullOrEmpty(JOUM_CODE))
            {
                sql += $" and JOUM_CODE='{JOUM_CODE}'";
            }

            if (dao == null)
                return comm.DB.ExecSQL(sql);
            else
                return dao.DB.ExecSQL_T(sql);
        }
        public rs deleteAccJouralM(AccJouralM_del data)
        {
            if (string.IsNullOrEmpty(data.data.JOUM_COMPID))
                return CommDAO.getRs(1, "未輸入 公司代號");

            bool bOK = _deleteAccJouralM(data.data.JOUM_COMPID, data.data.JOUM_CODE);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }
        // 查詢
        public  AccJouralM_query queryAccJouralM(AccJouralM_del data)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "SELECT * from ACC_JOURAL_M where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.JOUM_COMPID))
                sql += $" and JOUM_COMPID='{data.data.JOUM_COMPID}'";

            if (!string.IsNullOrEmpty(data.data.JOUM_CODE) && !string.IsNullOrEmpty(data.data.JOUM_CODE_E))
            {
                sql += $" and JOUM_CODE>='{data.data.JOUM_CODE}' and JOUM_CODE<='{data.data.JOUM_CODE_E}'";
            }
            else
            {
                if (!string.IsNullOrEmpty(data.data.JOUM_CODE))
                    sql += $" and JOUM_CODE='{data.data.JOUM_CODE}'";
            }

            sql += " ORDER BY JOUM_COMPID,JOUM_CODE ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccJouralM> rs = dt.ToList<AccJouralM>();
            return rsAccJouralM(rs,
                pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null,
                0, "成功");
        }

        // Query return
        public static AccJouralM_query rsAccJouralM(List<AccJouralM> data = null, Pagination pagination=null, int retCode = 1, string retMsg = "失敗")
        {
            return new AccJouralM_query()
            {
                result = new rsItem() { retCode = retCode, retMsg = retMsg },
                data = data,
                pagination= pagination
            };
        }

    }
}