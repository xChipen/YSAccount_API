using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;

namespace DAO
{
    public class AccJouralDDAO : BaseClass
    {
        // Query return
        public static rs_AccJouralDQuery rsQuery(List<AccJouralD> data = null, Pagination pagination= null, int retCode = 1, string retMsg = "失敗")
        {
            return new rs_AccJouralDQuery()
            {
                result = new rsItem() { retCode = retCode, retMsg = retMsg },
                data = data,
                pagination= pagination
            };
        }

        //新增
        public  rs add(AccJouralDAdd model)
        {
            if (string.IsNullOrEmpty(model.data.JOUD_COMPID))
                return CommDAO.getRs(1, "請輸入公司代號");
            if (string.IsNullOrEmpty(model.data.JOUD_CODE))
                return CommDAO.getRs(1, "請輸入常用分錄代號");

            //if (model.data.JOUD_SEQ == null)
            //    return CommDAO.getRs(1, "請輸入明細序號");
  
            DataTable dt = isExist(model.data);

            if (dt != null && dt.Rows.Count != 0)
            {
                return CommDAO.getRs(1, "資料已經存在");
            }

            // 新增自動填入最大號 + 1
            model.data.JOUD_SEQ = maxSEQ(model.data.JOUD_COMPID, model.data.JOUD_CODE);

            bool bOK = insert_data(model.data);
            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }

        public  rs update(AccJouralDAdd model)
        {
            if (string.IsNullOrEmpty(model.data.JOUD_COMPID))
                return CommDAO.getRs(1, "請輸入公司代號");
            if (string.IsNullOrEmpty(model.data.JOUD_CODE))
                return CommDAO.getRs(1, "請輸入常用分錄代號");

            if (model.data.JOUD_SEQ == null)
                return CommDAO.getRs(1, "請輸入明細序號");

            DataTable dt = isExist(model.data);

            if (dt == null || dt.Rows.Count == 0)
            {
                return CommDAO.getRs(1, "資料不存在");
            }

            bool bOK = update_data(model.data);
            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }

        public  rs delete(AccJouralD_del model)
        {
            if (string.IsNullOrEmpty(model.data.JOUD_COMPID))
                return CommDAO.getRs(1, "請輸入公司代號");
            if (string.IsNullOrEmpty(model.data.JOUD_CODE))
                return CommDAO.getRs(1, "請輸入常用分錄代號");

            if (model.data.JOUD_SEQ == null)
                return CommDAO.getRs(1, "請輸入明細序號");

            bool bOK = true;
            foreach (var item in model.data.JOUD_SEQ)
            {
                bOK = delete_data(model.data.JOUD_COMPID, model.data.JOUD_CODE, item);
                if (!bOK) break;
            }
            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }

        // 資料是否存在
        public  DataTable isExist(AccJouralD model)
        {
            string sql = string.Format(@"SELECT * FROM ACC_JOURAL_D 
WHERE 1=1 
AND JOUD_COMPID='{0}'
AND JOUD_CODE='{1}'
AND JOUD_SEQ ={2}"
, model.JOUD_COMPID, model.JOUD_CODE, model.JOUD_SEQ);

            return comm.DB.RunSQL(sql, new object[] { });
        }

        // 最大號 + 1
        public  short maxSEQ(string JOUD_COMPID, string JOUD_CODE)
        {
            string sql = $@"SELECT Max(JOUD_SEQ) as JOUD_SEQ FROM ACC_JOURAL_D 
WHERE 1=1 
AND JOUD_COMPID='{JOUD_COMPID}' AND JOUD_CODE='{JOUD_CODE}'";

            DataTable dt = comm.DB.RunSQL(sql);
            string ss = dt.Rows[0]["JOUD_SEQ"].ToString();
            if (ss =="")
                return 1;
            else
            {
                Int16 rs = Convert.ToInt16(ss);
                rs++;
                return Convert.ToInt16(rs);
            }
        }

        public  bool insert_data(AccJouralD model, CommDAO dao = null)
        {
            string sql = $@"INSERT INTO ACC_JOURAL_D(
JOUD_COMPID, JOUD_CODE, JOUD_SEQ, JOUD_ACCD, JOUD_MEMO
, JOUD_DEPTID, JOUD_TRANID,JOUD_INVNO, JOUD_DAMT, JOUD_CAMT) 
VALUES 
(@JOUD_COMPID, @JOUD_CODE, @JOUD_SEQ, @JOUD_ACCD, @JOUD_MEMO
, @JOUD_DEPTID, @JOUD_TRANID, @JOUD_INVNO, @JOUD_DAMT, @JOUD_CAMT ) ";

            object[] obj = new object[] {
                model.JOUD_COMPID,
                model.JOUD_CODE,
                model.JOUD_SEQ,
                model.JOUD_ACCD,
                model.JOUD_MEMO,
                model.JOUD_DEPTID,
                model.JOUD_TRANID,
                model.JOUD_INVNO,
                model.JOUD_DAMT,
                model.JOUD_CAMT };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }

        public  bool update_data(AccJouralD model, CommDAO dao = null)
        {
            string sql = $@"UPDATE ACC_JOURAL_D SET 
JOUD_ACCD=@JOUD_ACCD, JOUD_MEMO=@JOUD_MEMO, JOUD_DEPTID=@JOUD_DEPTID, JOUD_TRANID=@JOUD_TRANID
, JOUD_INVNO=@JOUD_INVNO, JOUD_DAMT=@JOUD_DAMT, JOUD_CAMT=@JOUD_CAMT 

WHERE JOUD_COMPID=@JOUD_COMPID AND JOUD_CODE=@JOUD_CODE AND JOUD_SEQ=@JOUD_SEQ ";
            object[] obj = new object[] {
                model.JOUD_ACCD,
                model.JOUD_MEMO,
                model.JOUD_DEPTID,
                model.JOUD_TRANID,

                model.JOUD_INVNO,
                model.JOUD_DAMT,
                model.JOUD_CAMT,

                model.JOUD_COMPID,
                model.JOUD_CODE,
                model.JOUD_SEQ
            };

            if (dao != null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }
        public  bool delete_data(string JOUD_COMPID, string JOUD_CODE, int? JOUD_SEQ, CommDAO dao = null)
        {
            string sql = $@"DELETE ACC_JOURAL_D
WHERE JOUD_COMPID=@JOUD_COMPID AND JOUD_CODE=@JOUD_CODE AND JOUD_SEQ=@JOUD_SEQ ";

            object[] obj = new object[] {
                JOUD_COMPID,
                JOUD_CODE,
                JOUD_SEQ };

            if (dao == null)
                return comm.DB.ExecSQL(sql, obj);
            else
                return dao.DB.ExecSQL_T(sql, obj);
        }


        public  DataTable _select_data_batch(string JOUD_COMPID, string JOUD_CODE = null, int? JOUD_SEQ = null)
        {
            string sql = $@"SELECT * FROM ACC_JOURAL_D
WHERE 1=1 ";
            if (!string.IsNullOrEmpty(JOUD_COMPID))
                sql += $" AND JOUD_COMPID='{JOUD_COMPID}' ";

            if (!string.IsNullOrEmpty(JOUD_CODE))
                sql += $" AND JOUD_CODE='{JOUD_CODE}' ";

            if (JOUD_SEQ != null)
                sql += $" AND JOUD_SEQ={JOUD_SEQ} ";

            sql += " ORDER BY JOUD_COMPID,JOUD_CODE,JOUD_SEQ ";

            DataTable dt = new DataTable();

            return comm.DB.RunSQL(sql);
        }
        public  rs_AccJouralDQuery select_data(AccJouralD_ins model)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(model.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = $@"SELECT * FROM ACC_JOURAL_D
WHERE 1=1 ";
            if (!string.IsNullOrEmpty(model.data.JOUD_COMPID))
                sql += $" AND JOUD_COMPID='{model.data.JOUD_COMPID}' ";

            if (!string.IsNullOrEmpty(model.data.JOUD_CODE))
                sql += $" AND JOUD_CODE='{model.data.JOUD_CODE}' ";

            if (model.data.JOUD_SEQ != null)
                sql += $" AND JOUD_SEQ={model.data.JOUD_SEQ} ";

            sql += " ORDER BY JOUD_COMPID,JOUD_CODE,JOUD_SEQ ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = new DataTable();
            dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccJouralD> rs = dt.ToList<AccJouralD>();
            return rsQuery(rs,
                pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null,
                0, "成功");

        }
    }
}