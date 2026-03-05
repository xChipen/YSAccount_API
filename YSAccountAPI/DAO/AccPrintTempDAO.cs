using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Data;
using Helpers;

namespace DAO
{
    public class AccPrintTempDAO : BaseClass
    {
        private bool DeleteAll(string PRTM_USERID, string PRTM_PROGID) {
            string sql = "DELETE ACC_PRINT_TEMP WHERE PRTM_USERID=@PRTM_USERID AND PRTM_PROGID=@PRTM_PROGID";
            object[] obj = new object[] { PRTM_USERID, PRTM_PROGID };
            return comm.DB.ExecSQL(sql, obj);
        }

        public rs Insert(AccPrintTemp_ins data)
        {
            if (data.data.Count == 0)
                return new rs { result = new rsItem { retCode=1, retMsg="傳入資料錯誤"} };

            DeleteAll(data.data[0].PRTM_USERID, data.data[0].PRTM_PROGID);

            string sql = @"INSERT INTO ACC_PRINT_TEMP
(PRTM_USERID,PRTM_PROGID,PRTM_PARAMETER1,PRTM_PARAMETER2,PRTM_PARAMETER3,PRTM_PARAMETER4,
PRTM_NT_AMT,PRTM_FOR_AMT,PRTM_CNT) VALUES(
@PRTM_USERID,@PRTM_PROGID,@PRTM_PARAMETER1,@PRTM_PARAMETER2,@PRTM_PARAMETER3,@PRTM_PARAMETER4,
@PRTM_NT_AMT,@PRTM_FOR_AMT,@PRTM_CNT)";

            CommDAO dao = new CommDAO();
            foreach (var item in data.data) {
                object[] obj = new object[] {
                    item.PRTM_USERID,
                    item.PRTM_PROGID,
                    item.PRTM_PARAMETER1,
                    item.PRTM_PARAMETER2,
                    item.PRTM_PARAMETER3,
                    item.PRTM_PARAMETER4,
                    item.PRTM_NT_AMT,
                    item.PRTM_FOR_AMT,
                    item.PRTM_CNT
                };

                dao.DB.BeginTransaction();
                try
                {
                    dao.DB.ExecSQL_T(sql, obj);
                    dao.DB.Commit();
                }
                catch
                {
                    dao.DB.Rollback();
                    return new rs { result = new rsItem { retCode = 1, retMsg = "失敗" } };
                }
            }
            return new rs { result = new rsItem { retCode = 0, retMsg = "成功" } };
        }

        public rs_AccPrintTemp query(AccPrintTemp_qry data)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "SELECT * from ACC_PRINT_TEMP where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.PRTM_USERID))
            {
                sql += $" AND PRTM_USERID = '{data.data.PRTM_USERID}' ";
            }
            if (!string.IsNullOrEmpty(data.data.PRTM_PROGID))
            {
                sql += $" AND PRTM_PROGID = '{data.data.PRTM_PROGID}' ";
            }

            sql += " ORDER BY PRTM_USERID, PRTM_PROGID ";

            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccPrintTemp> rs = dt.ToList<AccPrintTemp>();
            return new rs_AccPrintTemp {
                result = new rsItem { retCode=0, retMsg="成功"},
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null,
                data = rs
            };
        }
    }
}