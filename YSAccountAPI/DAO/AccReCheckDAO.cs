using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Helpers;

namespace DAO
{
    public class AccReCheckDAO : BaseClass
    {
        private const string tableName = "ACC_RE_CHECK";
        private List<string> PK;

        public AccReCheckDAO()
        {
            PK = CommDAO.getPK(comm, tableName);
        }

        #region Batch call
        public bool isExist(AccReCheck_item data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }

        public bool AUD(AccReCheck_item data, string employeeNo, string name, CommDAO dao = null)
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

        private bool _AddBatch(AccReCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.RECK_A_USER_ID = employeeNo;
            data.RECK_A_USER_NM = name;
            data.RECK_A_DATE = DateTime.Now;
            data.RECK_U_USER_ID = employeeNo;
            data.RECK_U_USER_NM = name;
            data.RECK_U_DATE = DateTime.Now;

            return CommDAO._add(comm, data, tableName, PK, dao);
        }
        private bool _UpdateBatch(AccReCheck_item data, string employeeNo, string name, CommDAO dao = null)
        {
            // 填入預設資料
            data.RECK_U_USER_ID = employeeNo;
            data.RECK_U_USER_NM = name;
            data.RECK_U_DATE = DateTime.Now;

            // 不更新, 排除清單
            List<string> outSL = new List<string>();
            outSL.Add("RECK_A_USER_ID");
            outSL.Add("RECK_A_USER_NM");
            outSL.Add("RECK_A_DATE");

            return CommDAO._update(comm, data, tableName, PK, outSL, dao);
        }
        private bool _DeleteBatch(AccReCheck_item data, CommDAO dao = null)
        {
            return CommDAO._delete(comm, data, tableName, PK, null, dao);
        }
        public DataTable _QueryBatch(AccReCheck_item data)
        {
            #region check
            if (string.IsNullOrEmpty(data.RECK_COMPID))
            {
                Log.Info("_QueryBatch: 無公司代號");
                return null;
            }
            if (string.IsNullOrEmpty(data.RECK_NO))
            {
                Log.Info("_QueryBatch: 無收款單號");
                return null;
            }
            #endregion
            string sql = $@"SELECT * from {tableName} WHERE 1=1 
AND RECK_COMPID='{data.RECK_COMPID}' ";
            sql += CommDAO.sql_ep(data.RECK_NO, "RECK_NO");

            return comm.DB.RunSQL(sql);
        }
        #endregion

        //---------------------------------------------------------------------------

        public static rs_AccReCheckAdd rsAccReCheck(int code, string msg, AccReCheckID AccReCheck_id = null)
        {
            if (AccReCheck_id != null)
                return new rs_AccReCheckAdd()
                {
                    result = new rsItem() { retCode = code, retMsg = msg },
                    data = new AccReCheckID()
                    {
                        RECK_COMPID = AccReCheck_id.RECK_COMPID,
                        RECK_NO = AccReCheck_id.RECK_NO
                    }
                };
            else
                return new rs_AccReCheckAdd()
                {
                    result = new rsItem() { retCode = code, retMsg = msg },
                    data = null
                };
        }

        public  DataTable haveAccReCheck(AccReCheckID AccReCheck_id)
        {
            string sql = "SELECT * FROM ACC_RE_CHECK WHERE RECK_COMPID = '" + AccReCheck_id.RECK_COMPID +
                "' AND RECK_NO = '" + AccReCheck_id.RECK_NO + "' ";

            return comm.DB.RunSQL(sql, new object[] { AccReCheck_id });
        }

        public  rs_AccReCheckAdd addAccReCheck(AccReCheckAdd data)
        {
            if (string.IsNullOrEmpty(data.data.RECK_COMPID))
                return rsAccReCheck(1, "未輸入公司代號");
            if (string.IsNullOrEmpty(data.data.RECK_NO))
                return rsAccReCheck(1, "未輸入票據號碼");
            AccReCheckID id = new AccReCheckID()
            {
                RECK_COMPID = data.data.RECK_COMPID,
                RECK_NO = data.data.RECK_NO
            };

            DataTable dt = haveAccReCheck(id);
            if (dt != null && dt.Rows.Count != 0)
                return rsAccReCheck(1, "票據號碼, 已經存在");

            string sql = $@"INSERT INTO ACC_RE_CHECK( 
RECK_COMPID, RECK_NO, RECK_REC_DATE, RECK_DUE_DATE1, RECK_CUSTID, RECK_AMT, RECK_DUE_BANK, RECK_ACNO, RECK_SAV_FLG, 
RECK_SAV_DATE, RECK_SAV_BANK, RECK_AREA_FLG, RECK_DUE_DATE2, RECK_DUE_FLG,RECK_DUE_DATE3, RECK_VOU_DATE, RECK_VOUNO, RECK_C_VOUNO, RECK_C_SEQ, 
RECK_A_USER_ID, RECK_A_USER_NM, RECK_A_DATE) VALUES ( 
@RECK_COMPID, @RECK_NO, @RECK_REC_DATE, @RECK_DUE_DATE1, @RECK_CUSTID, @RECK_AMT, @RECK_DUE_BANK, @RECK_ACNO, @RECK_SAV_FLG, 
@RECK_SAV_DATE, @RECK_SAV_BANK, @RECK_AREA_FLG, @RECK_DUE_DATE2, @RECK_DUE_FLG, @RECK_DUE_DATE3, @RECK_VOU_DATE, @RECK_VOUNO, @RECK_C_VOUNO, @RECK_C_SEQ, 
@RECK_A_USER_ID, @RECK_A_USER_NM, GetDate() ) ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.RECK_COMPID,
                data.data.RECK_NO,
                data.data.RECK_REC_DATE,
                data.data.RECK_DUE_DATE1,
                data.data.RECK_CUSTID,
                data.data.RECK_AMT,
                data.data.RECK_DUE_BANK,
                data.data.RECK_ACNO,
                data.data.RECK_SAV_FLG,
                data.data.RECK_SAV_DATE,
                data.data.RECK_SAV_BANK,
                data.data.RECK_AREA_FLG,
                data.data.RECK_DUE_DATE2,
                data.data.RECK_DUE_FLG,
                data.data.RECK_DUE_DATE3,
                data.data.RECK_VOU_DATE,
                data.data.RECK_VOUNO,
                data.data.RECK_C_VOUNO,
                data.data.RECK_C_SEQ,
                data.baseRequest.employeeNo,
                data.baseRequest.name
            });
            if (bOK)
                return rsAccReCheck(0, "成功");

            return rsAccReCheck(1, "失敗");
        }

        public rs updateAccReCheck(AccReCheck_ins data)
        {
            bool bOK = true;
            foreach (var item in data.data)
            {
                bOK = update(item, data.baseRequest.employeeNo, data.baseRequest.name);
                if (!bOK) break;
            }
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        private  bool update(AccReCheck data, string employeeNo, string name)
        {
            if (string.IsNullOrEmpty(data.RECK_COMPID))
                return false;
            if (string.IsNullOrEmpty(data.RECK_NO))
                return false;

            AccReCheckID id = new AccReCheckID()
            {
                RECK_COMPID = data.RECK_COMPID,
                RECK_NO = data.RECK_NO
            };

            DataTable dt = haveAccReCheck(id);
            if (dt == null || dt.Rows.Count == 0)
                return false;

            string sql = $@"UPDATE ACC_RE_CHECK SET RECK_REC_DATE=@RECK_REC_DATE, RECK_DUE_DATE1=@RECK_DUE_DATE1, RECK_CUSTID=@RECK_CUSTID, 
RECK_AMT=@RECK_AMT, RECK_DUE_BANK=@RECK_DUE_BANK, RECK_ACNO=@RECK_ACNO, RECK_SAV_FLG=@RECK_SAV_FLG, RECK_SAV_DATE=@RECK_SAV_DATE, RECK_SAV_BANK=@RECK_SAV_BANK, 
RECK_AREA_FLG=@RECK_AREA_FLG, RECK_DUE_DATE2=@RECK_DUE_DATE2, RECK_DUE_FLG=@RECK_DUE_FLG, RECK_DUE_DATE3=@RECK_DUE_DATE3, RECK_VOU_DATE=@RECK_VOU_DATE, 
RECK_VOUNO=@RECK_VOUNO, RECK_C_VOUNO=@RECK_C_VOUNO, RECK_C_SEQ=@RECK_C_SEQ, 
RECK_U_USER_ID=@RECK_U_USER_ID, RECK_U_USER_NM=@RECK_U_USER_NM, RECK_U_DATE = GetDate() " +
                $@"WHERE RECK_COMPID='{data.RECK_COMPID}' AND RECK_NO='{data.RECK_NO}' ";

            return comm.DB.ExecSQL(sql, new object[] {
               data.RECK_REC_DATE,
                data.RECK_DUE_DATE1,
                data.RECK_CUSTID,
                data.RECK_AMT,
                data.RECK_DUE_BANK,
                data.RECK_ACNO,
                data.RECK_SAV_FLG,
                data.RECK_SAV_DATE,
                data.RECK_SAV_BANK,
                data.RECK_AREA_FLG,
                data.RECK_DUE_DATE2,
                data.RECK_DUE_FLG,
                data.RECK_DUE_DATE3,
                data.RECK_VOU_DATE,
                data.RECK_VOUNO,
                data.RECK_C_VOUNO,
                data.RECK_C_SEQ,
                employeeNo,
                name
            });
        }

        public rs deleteAccReCheck(AccReCheckDelete data)
        {
            if (data.data == null)
                return CommDAO.getRs(1, "未輸入公司代號");
            if (data.data.RECK_COMPID == null)
                return CommDAO.getRs(1, "未輸入公司代號");
            if (data.data.RECK_NO == null)
                return CommDAO.getRs(1, "未輸入票據號碼");
            if (data.data.RECK_NO.Count == 0)
                return CommDAO.getRs(1, "未輸入票據號碼");

            string AccReCheck_id = "";
            foreach (string id in data.data.RECK_NO)
            {
                AccReCheck_id += "'" + id + "',";
            }
            AccReCheck_id = AccReCheck_id.Substring(0, AccReCheck_id.Length - 1);

            string sql = $"DELETE ACC_RE_CHECK WHERE RECK_COMPID='{data.data.RECK_COMPID}' AND RECK_NO IN (" + AccReCheck_id + ")";

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }

        public  rs_AccReCheckQuery queryAccReCheck(AccReCheckQuery dataIn)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(dataIn.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = "";
            AccReCheckIDs ida = null;
            Log.Info("sql : " + sql);
            if (dataIn.data != null)
            {
                ida = dataIn.data;
            }
            if (ida == null)
            {
                //  改成沒有找到 MEMO_ID 就不給查詢了。
                return new rs_AccReCheckQuery()
                {
                    result = new rsItem() { retCode = 1, retMsg = "未輸入公司代號" },
                };
            }
            else
            {
                if (ida.RECK_NO.Count == 0)
                    sql = "SELECT * FROM ACC_RE_CHECK WHERE RECK_COMPID = '" + ida.RECK_COMPID + "' ";
                else
                {
                    sql = "SELECT * FROM ACC_RE_CHECK WHERE RECK_COMPID = '" + ida.RECK_COMPID + "' AND ";
                    String str_id = "";
                    List<String> ids = ida.RECK_NO;

                    for (int i = 0; i < ids.Count; i++)
                    {
                        String id = ids[i];
                        str_id = str_id + "'" + id + "',";
                    }
                    str_id = str_id.Substring(0, str_id.Length - 1);
                    sql += $" RECK_NO IN ({str_id}) ";
                }

                if (dataIn.data.RECK_REC_DATE != null)
                    sql += $" AND  convert(char(8), RECK_REC_DATE ,112)>='{string.Format("{0:yyyyMMdd}", dataIn.data.RECK_REC_DATE)}'";
                if (dataIn.data.RECK_DUE_DATE1 != null)
                    sql += $" AND  convert(char(8), RECK_DUE_DATE1 ,112)<='{string.Format("{0:yyyyMMdd}", dataIn.data.RECK_DUE_DATE1)}'";

                if (!string.IsNullOrEmpty(dataIn.data.RECK_CUSTID))
                    sql += $" AND RECK_CUSTID Like '{dataIn.data.RECK_CUSTID}%'";

                if (!string.IsNullOrEmpty(dataIn.data.RECK_DUE_BANK))
                    sql += $" AND RECK_DUE_BANK Like '%{dataIn.data.RECK_DUE_BANK}%'";

                if (dataIn.data.RECK_SAV_DATE != null)
                    sql += $" AND  convert(char(8), RECK_SAV_DATE ,112)='{string.Format("{0:yyyyMMdd}", dataIn.data.RECK_SAV_DATE)}'";

                if (!string.IsNullOrEmpty( dataIn.data.RECK_SAV_BANK))
                    sql += $" AND  RECK_SAV_BANK Like '%{dataIn.data.RECK_SAV_BANK}%'";

                if (dataIn.data.RECK_DUE_DATE3 != null)
                    sql += $" AND  convert(char(8), RECK_DUE_DATE3 ,112)='{string.Format("{0:yyyyMMdd}", dataIn.data.RECK_DUE_DATE3)}'";

                if (!string.IsNullOrEmpty(dataIn.data.RECK_DUE_FLG))
                    sql += $" AND  RECK_DUE_FLG Like '%{ dataIn.data.RECK_DUE_FLG }%'";

                if (!string.IsNullOrEmpty(dataIn.data.RECK_SAV_FLG))
                    sql += $" AND  RECK_SAV_FLG ='{ dataIn.data.RECK_SAV_FLG }'";

                sql += $" AND  RECK_VOUNO ='{ dataIn.data.RECK_VOUNO }'";
                
            }
            sql += " ORDER BY RECK_COMPID,RECK_NO ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccReCheckSimple> rs = dt.ToList<AccReCheckSimple>();
            return new rs_AccReCheckQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber!=0? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers):null
            };
        }

        public  rs_AccReCheckQuery queryAccReCheck2(AccReCheckQuery2 data)
        {
            int pageNumber;
            int pageSize;
            int pageNumbers;
            CommDAO.initPagination(data.pagination, out pageNumber, out pageSize, out pageNumbers);

            string sql = $@"SELECT *, ACNM_C_NAME FROM ACC_RE_CHECK 
LEFT JOIN ACC_ACCNAME ON RECK_SAV_BANK = ISNULL(ACNM_ID1,'')+ISNULL(ACNM_ID2,'')+ISNULL(ACNM_ID3,'')
WHERE RECK_COMPID='{data.data.RECK_COMPID}' ";

            if (!string.IsNullOrEmpty(data.data.RECK_NO))
                sql += $" AND RECK_NO LIKE '{data.data.RECK_NO}%'";

            if (data.data.RECK_REC_DATE_B != null && data.data.RECK_REC_DATE_E != null)
                sql += $" AND convert(char(8), RECK_REC_DATE,112) >='{string.Format("{0:yyyyMMdd}", data.data.RECK_REC_DATE_B)}' AND convert(char(8), RECK_REC_DATE,112) <='{string.Format("{0:yyyyMMdd}", data.data.RECK_REC_DATE_E)}'";

            if (data.data.RECK_DUE_DATE1_B != null && data.data.RECK_DUE_DATE1_E != null)
                sql += $" AND convert(char(8), RECK_DUE_DATE1,112) >='{string.Format("{0:yyyyMMdd}", data.data.RECK_DUE_DATE1_B)}' AND convert(char(8), RECK_DUE_DATE1,112) <='{string.Format("{0:yyyyMMdd}", data.data.RECK_DUE_DATE1_E)}'";

            if (!string.IsNullOrEmpty(data.data.RECK_CUSTID))
                sql += $" AND RECK_CUSTID LIKE '{data.data.RECK_CUSTID}%'";

            if (!string.IsNullOrEmpty(data.data.RECK_DUE_BANK))
                sql += $" AND RECK_DUE_BANK LIKE '%{data.data.RECK_DUE_BANK}%'";

            if (data.data.RECK_SAV_DATE_B != null && data.data.RECK_SAV_DATE_E != null)
                sql += $" AND convert(char(8), RECK_SAV_DATE,112) >='{string.Format("{0:yyyyMMdd}", data.data.RECK_SAV_DATE_B)}' AND convert(char(8), RECK_SAV_DATE,112) <='{string.Format("{0:yyyyMMdd}", data.data.RECK_SAV_DATE_E)}'";

            if (data.data.RECK_SAV_BANK_B != null && data.data.RECK_SAV_BANK_E != null)
                sql += $" AND convert(char(8), RECK_SAV_BANK,112) >='{string.Format("{0:yyyyMMdd}", data.data.RECK_SAV_BANK_B)}' AND convert(char(8), RECK_SAV_BANK,112) <='{string.Format("{0:yyyyMMdd}", data.data.RECK_SAV_BANK_E)}'";

            if (!string.IsNullOrEmpty(data.data.RECK_DUE_FLG))
                sql += $" AND  RECK_DUE_FLG ='{ data.data.RECK_DUE_FLG }'";

            if (!string.IsNullOrEmpty(data.data.RECK_SAV_FLG))
                sql += $" AND  RECK_SAV_FLG ='{ data.data.RECK_SAV_FLG }'";

            // RECK_DUE_DATE3 
            sql += CommDAO.sql_ep_date(data.data.RECK_DUE_DATE3, "RECK_DUE_DATE3");
            // RECK_VOUNO 
            sql += CommDAO.sql_ep(data.data.RECK_VOUNO, "RECK_VOUNO");


            sql += " ORDER BY RECK_COMPID,RECK_NO ";
            if (pageNumber != 0)
                sql = CommDAO.getSQL_page(sql, pageNumber, pageSize);

            DataTable dt = comm.DB.RunSQL(sql);
            int TotalCount = CommDAO.getTotalCount(dt, pageNumber);

            List<AccReCheckSimple> rs = dt.ToList<AccReCheckSimple>();
            return new rs_AccReCheckQuery()
            {
                result = new rsItem() { retCode = 0, retMsg = "成功" },
                data = rs,
                pagination = pageNumber != 0 ? CommDAO.getPagination(TotalCount, pageNumber, pageSize, pageNumbers) : null
            };
        }

        public bool DeleteAll(string RECK_COMPID, string RECK_NO, CommDAO dao)
        {
            string sql = $@"DELETE {tableName} WHERE RECK_COMPID ='{RECK_COMPID}' AND RECK_NO ='{RECK_NO}'";
            return dao.DB.ExecSQL_T(sql);
        }

        public rs ACG040B_update(ACG040B_update data, string employeeNo, string name)
        {
            string sql = $@"UPDATE ACC_RE_CHECK SET ";

            if (data.KIND == 1)
            {
                sql += $@" RECK_DUE_FLG = '1', RECK_DUE_DATE3={data.RECK_DUE_DATE3}";
            }
            else if (data.KIND == 2)
            {
                sql += $@" RECK_DUE_FLG = '0', RECK_DUE_DATE3=null";
            }

            sql += $@", RECK_U_USER_ID='{employeeNo}', RECK_U_USER_NM='{name}', RECK_U_DATE=GETDATE() ";

            sql += $@" WHERE RECK_COMPID ='{data.RECK_COMPID }' AND RECK_NO='{data.RECK_NO}'";

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return new rs
                {
                    result = CommDAO.getRsItem()
                };

            return new rs
            {
                result = CommDAO.getRsItem1()
            };

        }

        public rsACG070B_qry ACG070B_Query(AccReCheck data)
        {
            string sql = $@"SELECT *
FROM ACC_RE_CHECK
LEFT JOIN ACC_ACCNAME ON 
RECK_COMPID=ACNM_COMPID
AND RECK_SAV_BANK = ISNULL(ACNM_ID1,'')+ISNULL(ACNM_ID2,'')+ISNULL(ACNM_ID3,'')
";
            return null;

        }


    }
}