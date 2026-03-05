using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;

namespace DAO
{
    public class AccCodeMDAO : BaseClass
    {
        // 資料是否存在
        public DataTable haveAccCodeM(string CODM_ID1, string CODM_ID2)
        {
            string sql = "SELECT * FROM ACC_CODE_M WHERE CODM_ID1 = @CODM_ID1 ";
            if (!string.IsNullOrEmpty(CODM_ID2))
                sql += " and CODM_ID2 = @CODM_ID2 ";

            if (string.IsNullOrEmpty(CODM_ID2))
                return comm.DB.RunSQL(sql, new object[] { CODM_ID1 });
            else
                return comm.DB.RunSQL(sql, new object[] { CODM_ID1, CODM_ID2 });
        }

        // Excel 批次匯入
        public rs importAccCodeM(DataTable data)
        {
            if (data.Rows.Count == 0)
            {
                return CommDAO.getRs(1, "失敗");
            }

            foreach(DataRow dr in data.Rows)
            {
                DataTable dt = haveAccCodeM(dr[0].ToString(), dr[1].ToString());

                #region row = Create AccCodeM_ins
                AccCodeM_ins row = new AccCodeM_ins()
                {
                    baseRequest = new BaseRequest()
                    {
                        employeeNo = "",
                        name = ""
                    },
                    data = new AccCodeM()
                    {
                        CODM_ID1 = dr[0].ToString(),
                        CODM_ID2 = dr[1].ToString(),
                        CODM_NAME1 = dr[2].ToString(),
                        CODM_NAME2 = dr[3].ToString(),
                        CODM_ACCD = dr[4].ToString()
                    }
                };
                #endregion

                if (dt.Rows.Count == 0)
                    _updateAccCodeM(row);
                else
                    _addAccCodeM(row);
            }
            return CommDAO.getRs(0, "成功");
        }

        // 新增
        public rs addAccCodeM(AccCodeM_ins data)
        {
            if (string.IsNullOrEmpty(data.data.CODM_ID1))
                return CommDAO.getRs(1, "未輸入 資料類別代號");

            if (string.IsNullOrEmpty(data.data.CODM_ID2))
                return CommDAO.getRs(1, "未輸入 資料代號");

            DataTable dt = haveAccCodeM(data.data.CODM_ID1, data.data.CODM_ID2);
            if (dt != null && dt.Rows.Count != 0)
                return CommDAO.getRs(1, "資料已經存在");

            bool bOK = _addAccCodeM(data);

            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }
        // 新增 異動資料庫
        public bool _addAccCodeM(AccCodeM_ins data)
        {
            string sql = $@"INSERT INTO ACC_CODE_M(
CODM_ID1,CODM_ID2,CODM_NAME1,CODM_NAME2,CODM_ACCD,CODM_A_USER_ID,CODM_A_USER_NM,CODM_A_DATE,CODM_EXPENSE,CODM_MEMO) VALUES (
@CODM_ID1,@CODM_ID2,@CODM_NAME1,@CODM_NAME2,@CODM_ACCD,@CODM_A_USER_ID,@CODM_A_USER_NM,GetDate(),@CODM_EXPENSE,@CODM_MEMO ) ";

            return comm.DB.ExecSQL(sql, new object[] {
                data.data.CODM_ID1,
                data.data.CODM_ID2,
                data.data.CODM_NAME1,
                data.data.CODM_NAME2,
                data.data.CODM_ACCD,
                data.baseRequest.employeeNo,
                data.baseRequest.name,
                data.data.CODM_EXPENSE,
                data.data.CODM_MEMO
            });
        }

        // 修改
        public rs updateAccCodeM(AccCodeM_ins data)
        {
            if (string.IsNullOrEmpty(data.data.CODM_ID1))
                return CommDAO.getRs(1, "未輸入 資料類別代號");

            if (string.IsNullOrEmpty(data.data.CODM_ID2))
                return CommDAO.getRs(1, "未輸入 資料代號");

            DataTable dt = haveAccCodeM(data.data.CODM_ID1, data.data.CODM_ID2);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, " 資料不存在");

            bool bOK = _updateAccCodeM(data);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }
        // 修改 異動資料庫
        public bool _updateAccCodeM(AccCodeM_ins data)
        {
            string sql = $@"UPDATE ACC_CODE_M SET
CODM_NAME1=@CODM_NAME1,CODM_NAME2=@CODM_NAME2,CODM_ACCD=@CODM_ACCD,CODM_EXPENSE=@CODM_EXPENSE,CODM_MEMO=@CODM_MEMO  
WHERE CODM_ID1='{data.data.CODM_ID1}' and CODM_ID2='{data.data.CODM_ID2}' ";

            return comm.DB.ExecSQL(sql, new object[] {
                data.data.CODM_NAME1,
                data.data.CODM_NAME2,
                data.data.CODM_ACCD,
                data.data.CODM_EXPENSE,
                data.data.CODM_MEMO
            });
        }

        // 刪除
        public  rs deleteAccCodeM(AccCodeM_del data)
        {
            if (string.IsNullOrEmpty(data.data.CODM_ID1))
                return CommDAO.getRs(1, "未輸入 資料類別代號");

            string sql = $"DELETE ACC_CODE_M WHERE CODM_ID1='{data.data.CODM_ID1}'";

            if (!string.IsNullOrEmpty(data.data.CODM_ID2))
            {
                sql += $" and CODM_ID2='{data.data.CODM_ID2}'";
            }

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        // 查詢
        public  AccCodeM_query queryAccCodeM(AccCodeM_del data)
        {
            int pageNumber=0;
            int pageSize=0;
            int pageNumbers=0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "SELECT * from ACC_CODE_M where 1=1 ";

            if (data.data.KIND == 2)
            {
                if (!string.IsNullOrEmpty(data.data.CODM_ID1))
                    sql += $" and CODM_ID1 LIKE '{data.data.CODM_ID1}%'";

                if (!string.IsNullOrEmpty(data.data.CODM_ID2))
                    sql += $" and CODM_ID2 LIKE '{data.data.CODM_ID2}%'";
            }
            else
            {
                if (!string.IsNullOrEmpty(data.data.CODM_ID1))
                    sql += $" and CODM_ID1='{data.data.CODM_ID1}'";

                if (!string.IsNullOrEmpty(data.data.CODM_ID2))
                    sql += $" and CODM_ID2='{data.data.CODM_ID2}'";
            }

            if (!string.IsNullOrEmpty(data.data.CODM_NAME1))
                sql += $" and CODM_NAME1 LIKE '%{data.data.CODM_NAME1}%'";

            if (!string.IsNullOrEmpty(data.data.CODM_EXPENSE))
                sql += $" and CODM_EXPENSE LIKE '%{data.data.CODM_EXPENSE}%'";

            if (!string.IsNullOrEmpty(data.data.CODM_MEMO))
                sql += $" and CODM_MEMO LIKE '%{data.data.CODM_MEMO}%'";

            sql += " ORDER BY CODM_ID1,CODM_ID2 ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);

            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccCodeM> rs = dt.ToList<AccCodeM>();
            return rsAccCodeM(rs,
                (pageNumber != 0) ?
                CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers): null,
                0, "成功");
        }

        // Query return
        public static AccCodeM_query rsAccCodeM(List<AccCodeM> data = null, Pagination pagination=null, int retCode =1, string retMsg ="失敗") {
            return new AccCodeM_query()
            {
                result = new rsItem() { retCode = retCode , retMsg = retMsg },
                data = data,
                pagination= pagination
            };
        }

        // MD -------------------------------------------------------------------------------------
        // 新增
        public  rs addAccCodeM_MD(AccCodeM_MD_ins data)
        {
            foreach (AccCodeM_MD item in data.data)
            {
                if (!string.IsNullOrEmpty(item.CODM_ID1)) {
                    foreach (AccCodeM_Child child in item.Child) {
                        if (!string.IsNullOrEmpty(child.CODM_ID2))
                        {
                            DataTable dt = haveAccCodeM(item.CODM_ID1, child.CODM_ID2);
                            if (dt == null || dt.Rows.Count == 0) {
                                _addAccCodeM_MD(new AccCodeM() {
                                    CODM_ID1 = item.CODM_ID1,
                                    CODM_NAME1 = item.CODM_NAME1,
                                    CODM_ID2 = child.CODM_ID2,
                                    CODM_NAME2 = child.CODM_NAME2,
                                    CODM_ACCD = child.CODM_ACCD,
                                    CODM_DC = child.CODM_DC,
                                    CODM_EXPENSE = child.CODM_EXPENSE,
                                    CODM_MEMO = child.CODM_MEMO
                                }, 
                                    data.baseRequest.companyId,
                                    data.baseRequest.name
                                );
                            }
                        }
                    }
                }
            }
            return CommDAO.getRs(0, "成功");
        }
        // 新增 異動資料庫
        public  bool _addAccCodeM_MD(AccCodeM data, string employeeNo, string name)
        {
            string sql = $@"INSERT INTO ACC_CODE_M(
CODM_ID1,CODM_ID2,CODM_NAME1,CODM_NAME2,CODM_ACCD,CODM_A_USER_ID,CODM_A_USER_NM,CODM_A_DATE,
CODM_DC,CODM_EXPENSE,CODM_MEMO) VALUES (
@CODM_ID1,@CODM_ID2,@CODM_NAME1,@CODM_NAME2,@CODM_ACCD,@CODM_A_USER_ID,@CODM_A_USER_NM,GetDate(),
@CODM_DC,@CODM_EXPENSE,@CODM_MEMO) ";

            return comm.DB.ExecSQL(sql, new object[] {
                data.CODM_ID1,
                data.CODM_ID2,
                data.CODM_NAME1,
                data.CODM_NAME2,
                data.CODM_ACCD,
                employeeNo,
                name,
                data.CODM_DC,
                data.CODM_EXPENSE,
                data.CODM_MEMO
            });
        }

        // 修改
        public  rs updateAccCodeM_MD(AccCodeM_MD_ins data)
        {
            foreach (AccCodeM_MD item in data.data)
            {
                if (!string.IsNullOrEmpty(item.CODM_ID1))
                {
                    foreach (AccCodeM_Child child in item.Child)
                    {
                        if (!string.IsNullOrEmpty(child.CODM_ID2))
                        {
                            DataTable dt = haveAccCodeM(item.CODM_ID1, child.CODM_ID2);
                            if (dt != null && dt.Rows.Count != 0)
                            {
                                _updateAccCodeM_MD(new AccCodeM()
                                {
                                    CODM_ID1 = item.CODM_ID1,
                                    CODM_NAME1 = item.CODM_NAME1,
                                    CODM_ID2 = child.CODM_ID2,
                                    CODM_NAME2 = child.CODM_NAME2,
                                    CODM_ACCD = child.CODM_ACCD,
                                    CODM_DC = child.CODM_DC,
                                    CODM_EXPENSE = child.CODM_EXPENSE,
                                    CODM_MEMO = child.CODM_MEMO
                                },
                                    data.baseRequest.companyId,
                                    data.baseRequest.name
                                );
                            }
                        }
                    }
                }
            }
            return CommDAO.getRs();
        }
        // 修改 異動資料庫
        public  bool _updateAccCodeM_MD(AccCodeM data, string employeeNo, string name)
        {
            string sql = $@"UPDATE ACC_CODE_M SET
CODM_NAME1=@CODM_NAME1,CODM_NAME2=@CODM_NAME2,CODM_ACCD=@CODM_ACCD,CODM_DC=@CODM_DC,CODM_EXPENSE=@CODM_EXPENSE,CODM_MEMO=@CODM_MEMO
WHERE CODM_ID1='{data.CODM_ID1}' and CODM_ID2='{data.CODM_ID2}' ";

            return comm.DB.ExecSQL(sql, new object[] {
                data.CODM_NAME1,
                data.CODM_NAME2,
                data.CODM_ACCD,
                data.CODM_DC,
                data.CODM_EXPENSE,
                data.CODM_MEMO
            });
        }

        // 新增修改混用
        public  rs addAndUpdate_MD(AccCodeM_MD_ins data)
        {
            foreach (AccCodeM_MD item in data.data)
            {
                if (!string.IsNullOrEmpty(item.CODM_ID1))
                {
                    foreach (AccCodeM_Child child in item.Child)
                    {
                        if (!string.IsNullOrEmpty(child.CODM_ID2))
                        {
                            DataTable dt = haveAccCodeM(item.CODM_ID1, child.CODM_ID2);
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                _addAccCodeM_MD(new AccCodeM()
                                {
                                    CODM_ID1 = item.CODM_ID1,
                                    CODM_NAME1 = item.CODM_NAME1,
                                    CODM_ID2 = child.CODM_ID2,
                                    CODM_NAME2 = child.CODM_NAME2,
                                    CODM_ACCD= child.CODM_ACCD,
                                    CODM_DC = child.CODM_DC,
                                    CODM_EXPENSE = child.CODM_EXPENSE,
                                    CODM_MEMO = child.CODM_MEMO
                                },
                                    data.baseRequest.companyId,
                                    data.baseRequest.name
                                );
                            }
                            else {
                                _updateAccCodeM_MD(new AccCodeM()
                                    {
                                        CODM_ID1 = item.CODM_ID1,
                                        CODM_NAME1 = item.CODM_NAME1,
                                        CODM_ID2 = child.CODM_ID2,
                                        CODM_NAME2 = child.CODM_NAME2,
                                        CODM_ACCD = child.CODM_ACCD,
                                        CODM_DC = child.CODM_DC,
                                        CODM_EXPENSE = child.CODM_EXPENSE,
                                        CODM_MEMO = child.CODM_MEMO
                                },
                                    data.baseRequest.companyId,
                                    data.baseRequest.name
                                );
                            }
                        }
                    }
                }
            }
            return CommDAO.getRs(0, "成功");
        }

        // 刪除 資料庫
        public  bool _deleteAccCodeM_MD(AccCodeM data)
        {
            string sql = $"DELETE ACC_CODE_M WHERE CODM_ID1='{data.CODM_ID1}' and CODM_ID2='{data.CODM_ID2}'";

            return comm.DB.ExecSQL(sql, new object[] {
                data.CODM_NAME1,
                data.CODM_NAME2
            });
        }
        // 刪除
        public  rs deleteAccCodeM_MD(AccCodeM_MD_ins data)
        {
            foreach (AccCodeM_MD item in data.data)
            {
                if (!string.IsNullOrEmpty(item.CODM_ID1))
                {
                    foreach (AccCodeM_Child child in item.Child)
                    {
                        if (!string.IsNullOrEmpty(child.CODM_ID2))
                        {
                            _deleteAccCodeM_MD(new AccCodeM()
                            {
                                CODM_ID1 = item.CODM_ID1,
                                CODM_NAME1 = item.CODM_NAME1,
                                CODM_ID2 = child.CODM_ID2,
                                CODM_NAME2 = child.CODM_NAME2,
                                CODM_EXPENSE = child.CODM_EXPENSE,
                                CODM_MEMO = child.CODM_MEMO
                            });
                        }
                    }
                }
            }
            return CommDAO.getRs();
        }

        // Query SQL 查詢式
        private static string getSQL_query(AccCodeM_del data)
        {
            string sql = "SELECT CODM_ID1, CODM_NAME1 from ACC_CODE_M where 1=1 ";

            if (data.data.KIND == 2)
            {
                if (!string.IsNullOrEmpty(data.data.CODM_ID1))
                    sql += $" and CODM_ID1 LIKE '{data.data.CODM_ID1}%'";

                if (!string.IsNullOrEmpty(data.data.CODM_ID2))
                    sql += $" and CODM_ID2 LIKE '{data.data.CODM_ID2}%'";
            }
            else
            {
                if (!string.IsNullOrEmpty(data.data.CODM_ID1))
                    sql += $" and CODM_ID1='{data.data.CODM_ID1}'";

                if (!string.IsNullOrEmpty(data.data.CODM_ID2))
                    sql += $" and CODM_ID2='{data.data.CODM_ID2}'";
            }
            if (!string.IsNullOrEmpty(data.data.CODM_NAME1))
                sql += $" and CODM_NAME1='{data.data.CODM_NAME1}'";

            if (!string.IsNullOrEmpty(data.data.CODM_EXPENSE))
                sql += $" and CODM_EXPENSE LIKE '{data.data.CODM_EXPENSE}%'";

            if (!string.IsNullOrEmpty(data.data.CODM_MEMO))
                sql += $" and CODM_MEMO LIKE '{data.data.CODM_MEMO}%'";

            sql += " GROUP BY CODM_ID1, CODM_NAME1 ORDER BY CODM_ID1 ";
            return sql;
        }
        private  DataTable getData_query2(string CODM_ID1)
        {
            string sql = "SELECT CODM_ID2, CODM_NAME2, CODM_ACCD, CODM_DC, CODM_EXPENSE,CODM_MEMO from ACC_CODE_M where CODM_ID1=@CODM_ID1 ";
            return comm.DB.RunSQL(sql, new object[] { CODM_ID1 });
        }

        // 批次查詢
        public  AccCodeM_MD_query queryAccCodeM_MD(AccCodeM_del data)
        {
            List<AccCodeM_MD> rs = new List<AccCodeM_MD>();

            int pageNumber=0;
            int pageSize = 0;
            int pageNumbers = 0;
            int pages = 0;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = getSQL_query(data);
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            pages = CommDAO.getTotalCount(dt, pageNumber);

            foreach (DataRow dr in dt.Rows)
            {
                AccCodeM_MD rsItem = new AccCodeM_MD() {
                    CODM_ID1 = dr["CODM_ID1"].ToString(),
                    CODM_NAME1 = dr["CODM_NAME1"].ToString(),
                    Child = new List<AccCodeM_Child>()
                };

                DataTable dtItem = getData_query2(dr["CODM_ID1"].ToString());
                foreach(DataRow drItem in dtItem.Rows)
                {
                    AccCodeM_Child rsChild = new AccCodeM_Child() {
                        CODM_ID2 = drItem["CODM_ID2"].ToString(),
                        CODM_NAME2 = drItem["CODM_NAME2"].ToString(),
                        CODM_ACCD = drItem["CODM_ACCD"].ToString(),
                        CODM_DC = drItem["CODM_DC"].ToString(),
                        CODM_EXPENSE = drItem["CODM_EXPENSE"].ToString(),
                        CODM_MEMO = drItem["CODM_MEMO"].ToString()
                    };
                    rsItem.Child.Add(rsChild);
                }
                rs.Add(rsItem);
            }

            return new AccCodeM_MD_query() {
                result = new rsItem() { retCode=1, retMsg="成功"},
                pagination = pageNumber != 0 ? CommDAO.getPagination(pages, pageNumber, pageSize, pageNumbers) : null,
                data = rs
            };
        }
    }
}