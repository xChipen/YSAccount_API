using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Data;
using COMM;
using Helpers;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class CommStoreProcedure : BaseClass
    {

        // ACB050B 月過帳作業
        public rs runACB050B(ACB050B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.YearS))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "年未輸入" } };
            }
            else if (data.data.YearS.Length != 4)
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "年需4碼" } };
            }

            if (string.IsNullOrEmpty(data.data.MonthS))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "月未輸入" } };
            }
            else if (data.data.MonthS.Length != 2)
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "月需2碼" } };
            }

            if (string.IsNullOrEmpty(data.data.Date_EN))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "會計日期(迄)未輸入" } };
            }
            else if (data.data.Date_EN.Length != 10)
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "會計日期(迄)需10碼 YYYY/MM/DD" } };
            }
            #endregion

            Dictionary<string, object> ps = new Dictionary<string, object> {
                { "@COMP_ID", data.data.COMP_ID},
                { "@YearS"  , data.data.YearS},
                { "@MonthS" , data.data.MonthS},
                { "@Date_EN", data.data.Date_EN}
            };

            comm.DB.ExecSP("ACB050B", ps);

            return new rs() { result = new rsItem { retCode=0, retMsg="成功"} };
        }

        // ACB060B 過實帳戶月沖銷作業
        public ACB060B_rs runACB060B(ACB060B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB060B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.YearS))
            {
                return new ACB060B_rs() { result = new rsItem { retCode = 1, retMsg = "年未輸入" } };
            }
            else if (data.data.YearS.Length != 4)
            {
                return new ACB060B_rs() { result = new rsItem { retCode = 1, retMsg = "年需4碼" } };
            }

            if (string.IsNullOrEmpty(data.data.MonthS))
            {
                return new ACB060B_rs() { result = new rsItem { retCode = 1, retMsg = "月未輸入" } };
            }
            else if (data.data.MonthS.Length != 2)
            {
                return new ACB060B_rs() { result = new rsItem { retCode = 1, retMsg = "月需2碼" } };
            }

            if (string.IsNullOrEmpty(data.data.USER_ID))
            {
                return new ACB060B_rs() { result = new rsItem { retCode = 1, retMsg = "登錄者代號未輸入" } };
            }
            #endregion

            Dictionary<string, object> ps = new Dictionary<string, object> {
                { "@COMP_ID", data.data.COMP_ID},
                { "@YearS"  , data.data.YearS},
                { "@MonthS" , data.data.MonthS},
                { "@USER_ID", data.data.USER_ID}
            };

            commSQL.ExecSP(comm.DB._conn, "ACB060B", ps);

            string sql = $@"SELECT * from ACC_ERRLOG
WHERE ELOG_USERID ='{data.data.USER_ID}'
";
            DataTable dt = comm.DB.RunSQL(sql);
            if (dt.Rows.Count != 0)
            {
                List<ACB060B_rsItem> rs = dt.ToList<ACB060B_rsItem>();
                return new ACB060B_rs() {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs
                };
            }

            return new ACB060B_rs() { result = new rsItem { retCode = 0, retMsg = "失敗" } };
        }

        // ACB070B 年結作業
        public rs runACB070B(ACB070B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.YearS))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "年未輸入" } };
            }
            else if (data.data.YearS.Length != 4)
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "年需4碼" } };
            }
            #endregion

            Dictionary<string, object> ps = new Dictionary<string, object> {
                { "@COMP_ID", data.data.COMP_ID},
                { "@YearS"  , data.data.YearS}
            };

            comm.DB.ExecSP("ACB070B", ps);

            return new rs() { result = new rsItem { retCode = 0, retMsg = "成功" } };
        }

        // ACB110B 遞延費用迴轉傳票自動開立
        public ACB110B_rs runACB110B(ACB110B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.VOU_DAT))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "傳票日期未輸入" } };
            }
            else if (data.data.VOU_DAT.Length != 10)
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "傳票日期需10碼" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACB110B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@VOU_DATE", data.data.VOU_DAT));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 15);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                return new ACB110B_rs() {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = new List<string> { sp.Value.ToString() }
                };
            }
        }

        // ACB120B 評價資料產生處理
        private bool checkACB120B(string COMP_ID, string YearS, string MonthS)
        {
            string sql = $@"
SELECT MOAC_CURRID,MAX(ISNULL(EXRA_RATE_E,0))
	FROM ACC_MONTH_ACCOUNT
            LEFT JOIN ACC_EXRATE ON MOAC_YEAR = EXRA_YEAR AND MOAC_MONTH=EXRA_MONTH AND 
                                    MOAC_CURRID = EXRA_CURRID
            WHERE MOAC_COMPID = '{COMP_ID}'
	AND   MOAC_YEAR   = '{YearS}'
	AND   MOAC_MONTH  = '{MonthS}'
	AND   MOAC_CURRID <> 'NTD'
	AND   MOAC_BAL + MOAC_D_AMT - MOAC_C_AMT > 0
	GROUP BY MOAC_CURRID
";
            DataTable dt = comm.DB.RunSQL(sql);
            return dt.Rows.Count != 0;
        }
        public rs runACB120B(ACB120B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.YearS))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "年未輸入" } };
            }
            else if (data.data.YearS.Length != 4)
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "年需4碼" } };
            }

            if (string.IsNullOrEmpty(data.data.MonthS))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "月未輸入" } };
            }
            else if (data.data.MonthS.Length != 2)
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "月需2碼" } };
            }
            #endregion

            if (!checkACB120B(data.data.COMP_ID, data.data.YearS, data.data.MonthS))
            {
                return new rs() { result = new rsItem { retCode = 0, retMsg = "相關資料未建立" } };
            }

            Dictionary<string, object> ps = new Dictionary<string, object> {
                { "@COMP_ID", data.data.COMP_ID},
                { "@YearS"  , data.data.YearS},
                { "@MonthS" , data.data.MonthS},
                { "@USER_ID" , data.data.USER_ID},
                { "@USER_NM" , data.data.USER_NM}
            };

            comm.DB.ExecSP("ACB120B", ps);

            return new rs() { result = new rsItem { retCode = 0, retMsg = "成功" } };
        }

        // 140B 評價傳票自動開立
        private bool checkACB140B(string COMP_ID, string YearS, string MonthS)
        {
            string sql = $@"
SELECT * From ACC_MONTH_VALUATION
WHERE 
MOVA_COMPID='{COMP_ID}'
AND MOVA_YEAR='{YearS}'
AND MOVA_MONTH='{MonthS}'
";
            DataTable dt = comm.DB.RunSQL(sql);
            return dt.Rows.Count != 0;
        }
        public ACB140B_rs runACB140B(ACB120B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB140B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.YearS))
            {
                return new ACB140B_rs() { result = new rsItem { retCode = 1, retMsg = "年未輸入" } };
            }
            else if (data.data.YearS.Length != 4)
            {
                return new ACB140B_rs() { result = new rsItem { retCode = 1, retMsg = "年需4碼" } };
            }

            if (string.IsNullOrEmpty(data.data.MonthS))
            {
                return new ACB140B_rs() { result = new rsItem { retCode = 1, retMsg = "月未輸入" } };
            }
            else if (data.data.MonthS.Length != 2)
            {
                return new ACB140B_rs() { result = new rsItem { retCode = 1, retMsg = "月需2碼" } };
            }
            #endregion

            //if (!checkACB140B(data.data.COMP_ID, data.data.YearS, data.data.MonthS))
            //{
            //    return new rs() { result = new rsItem { retCode = 0, retMsg = "ACC_MONTH_VALUATION 資料未建立" } };
            //}

            //Dictionary<string, object> ps = new Dictionary<string, object> {
            //    { "@COMP_ID", data.data.COMP_ID},
            //    { "@YearS"  , data.data.YearS},
            //    { "@MonthS" , data.data.MonthS}
            //};

            //comm.DB.ExecSP("ACB140B", ps);

            //return new rs() { result = new rsItem { retCode = 0, retMsg = "成功" } };


            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACB140B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@YearS", data.data.YearS));
                cmd.Parameters.Add(new SqlParameter("@MonthS", data.data.MonthS));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.data.USER_ID));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.data.USER_NM));

                //SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 15);
                //sp.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                return new ACB140B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = new ACB140B_item { VOU_NO = getACB140B_VOU_NO(data.data.USER_ID) }  // 20260326
                };
            }

        }

        private string getACB140B_VOU_NO(string USER_ID)
        {
            string sql = $@"SELECT  PRTM_PARAMETER1 
FROM  ACC_PRINT_TEMP
WHERE PRTM_PROGID ='ACB140B'
And PRTM_USERID = '{USER_ID}'
";
            DataTable dt = comm.DB.RunSQL(sql);

            if (dt.Rows.Count != 0)
                return dt.Rows[0]["PRTM_PARAMETER1"].ToString();

            return "";
        }

        // ACG070B 應收票據兌現轉傳票處理
        public ACB110B_rs runACG070B(ACG070B data, string employeeId, string name)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.DUE_DATE))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "兌現日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACG070B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@DUE_DATE", data.DUE_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", employeeId));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }

        // ACD040M1, 2 
        public rs runACD040M2(ACD040M2_ins data, string SP_NAME= "ACD040M2")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.PAY_KIND))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "付款方式" } };
            }
            else if (data.data.DATE == null)
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "付款日期 未輸入" } };
            }
            #endregion

            Dictionary<string, object> ps = new Dictionary<string, object> {
                { "@COMP_ID" , data.data.COMP_ID},
                { "@PAY_KIND", data.data.PAY_KIND},
                { "@DATE", data.data.DATE},
                { "@USER_ID", data.baseRequest.employeeNo},
                { "@USER_NM", data.baseRequest.name}
            };

            comm.DB.ExecSP(SP_NAME, ps);

            return new rs() { result = new rsItem { retCode = 0, retMsg = "成功" } };
        }

        // ACD080B 應收票據兌現轉傳票處理
        public ACB110B_rs runACD080B(ACD080B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (data.data.PAY_DATE == null)
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "付款日期未輸入" } };
            }
            if (data.data.VOU_DATE == null)
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "傳票日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACD080B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@PAY_DATE", data.data.PAY_DATE));
                cmd.Parameters.Add(new SqlParameter("@VOU_DATE", data.data.VOU_DATE));
                cmd.Parameters.Add(new SqlParameter("@PAY_KIND", data.data.PAY_KIND));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }

        // ACE050B 應收票據兌現轉傳票處理
        public ACB110B_rs runACE050B(ACE050B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (data.data.DUE_DATE == null)
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "兌現日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACE050B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@DUE_DATE", data.data.DUE_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }

        public rs runACH120B(ACH120B_ins data, string SP_NAME = "ACH120B")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.YM))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "會計年月未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = SP_NAME;
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@YM", data.data.YM));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));
                comm.DB.ExecSP(cmd);

                return new rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                };
            }
        }
        
        public rsACJ040B_result runACJ040B(ACH120B_ins data, string SP_NAME = "ACJ040B")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new rsACJ040B_result() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.YM))
            {
                return new rsACJ040B_result() { result = new rsItem { retCode = 1, retMsg = "會計年月未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                #region Call StoreProcedure
                cmd.CommandText = SP_NAME;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@YM", data.data.YM));
                cmd.Parameters.Add(new SqlParameter("@UNNO", data.data.UNNO));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                List<rsACJ040B> rs = new List<rsACJ040B>();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    comm.DB._conn.Open();

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    #region fill data
                    foreach (DataRow row in dt.Rows)
                    {
                        rs.Add(new rsACJ040B
                        {
                            Item1 = row[0].ToString(),
                            Item2 = row[1].ToString()
                        });
                    }
                    #endregion
                }
                #endregion

                return new rsACJ040B_result()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs
                };
            }
        }


        // ACK030B_零用金立帳轉傳票
        public ACB110B_rs runACK030B(ACK030B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (data.data.VOU_DATE == null)
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "立帳傳票日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACK030B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@VOU_DATE", data.data.VOU_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }

        // ACK060B_零用金匯款檔案生成處理
        public ACB110B_rs runACK060B(ACK060B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (data.data.PAY_DATE == null)
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "付款日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACK060B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@PAY_DATE", data.data.PAY_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }

        // ACK070B_零用金付帳轉傳票處理
        public ACB110B_rs runACK070B(ACK060B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (data.data.PAY_DATE == null)
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "付款日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACK070B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@PAY_DATE", data.data.PAY_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }

        // ACF050B_收款轉傳票處理
        public ACB110B_rs runACF050B(ACK030B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (data.data.VOU_DATE == null)
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "傳票日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACF050B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@VOU_DATE", data.data.VOU_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }


        public bool runACB010M_ADD(AccVoumstD_item data, string employeeId, string name, string SP_NAME = "ACB010M_ADD")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.VOMD_COMPID))
                return false;
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                try
                {
                    cmd.CommandText = SP_NAME;

                    cmd.Parameters.Add(new SqlParameter("@VOMD_COMPID", data.VOMD_COMPID));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_NO", data.VOMD_NO));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_SEQ", data.VOMD_SEQ));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_ACCD", data.VOMD_ACCD));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_D_NT_AMT", data.VOMD_D_NT_AMT));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_C_NT_AMT", data.VOMD_C_NT_AMT));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_EXPENSE", data.VOMD_EXPENSE));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_MEMO", data.VOMD_MEMO));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_CURR", data.VOMD_CURR));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_EXRATE", data.VOMD_EXRATE));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_AMT", data.VOMD_AMT));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_DEPTID", data.VOMD_DEPTID));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_TRANID", data.VOMD_TRANID));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_INVNO", data.VOMD_INVNO));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_DUEFLG", data.VOMD_DUEFLG));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_TAXCD", data.VOMD_TAXCD));

                    if (data.VOMD_DUEDATE != null)
                        cmd.Parameters.Add(new SqlParameter("@VOMD_DUEDATE", data.VOMD_DUEDATE));
                    else
                        cmd.Parameters.Add(new SqlParameter("@VOMD_DUEDATE", ""));

                    cmd.Parameters.Add(new SqlParameter("@VOMD_PAY_KIND", data.VOMD_PAY_KIND));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_DUE_BANK", data.VOMD_DUE_BANK));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_ACNO", data.VOMD_ACNO));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_SAV_BANK", data.VOMD_SAV_BANK));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_CVOUNO", data.VOMD_CVOUNO));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_CSEQ", data.VOMD_CSEQ));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_CNT", data.VOMD_CNT));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_STYM", data.VOMD_STYM));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_ENYM", data.VOMD_ENYM));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_D_ACCD", data.VOMD_D_ACCD));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_D_DEPTID", data.VOMD_D_DEPTID));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_D_INVNO", data.VOMD_D_INVNO));
                    cmd.Parameters.Add(new SqlParameter("@USER_ID", employeeId));
                    cmd.Parameters.Add(new SqlParameter("@USER_NM", name));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_TAXCD", name));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_RELATIVE_NO", data.VOMD_RELATIVE_NO));

                    comm.DB.ExecSP(cmd);
                }
                catch(Exception ex)
                {
                    Log.Info(ex.Message);
                    return false;
                }
                return true;
            }
        }
        public bool runACB010M_DEL(string VOMD_COMPID, string VOMD_NO, string FLGVARCHAR, string employeeId, string name, string SP_NAME = "ACB010M_DEL")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(VOMD_COMPID))
                return false;
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                try
                {
                    cmd.CommandText = SP_NAME;

                    cmd.Parameters.Add(new SqlParameter("@VOMD_COMPID", VOMD_COMPID));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_NO", VOMD_NO));
                    cmd.Parameters.Add(new SqlParameter("@FLG", FLGVARCHAR));
                    cmd.Parameters.Add(new SqlParameter("@USER_ID", employeeId));
                    cmd.Parameters.Add(new SqlParameter("@USER_NM", name));

                    comm.DB.ExecSP(cmd);
                }
                catch(Exception ex)
                {
                    Log.Info(ex.Message);
                    return false;
                }
                return true;
            }
        }

        // 20240823
        public bool runACB010M_REVERSAL(string VOMD_COMPID, string VOMD_NO, string employeeId, string name)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(VOMD_COMPID))
                return false;
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                try
                {
                    cmd.CommandText = "ACB010M_REVERSAL";

                    cmd.Parameters.Add(new SqlParameter("@VOMD_COMPID", VOMD_COMPID));
                    cmd.Parameters.Add(new SqlParameter("@VOMD_NO", VOMD_NO));
                    cmd.Parameters.Add(new SqlParameter("@USER_ID", employeeId));
                    cmd.Parameters.Add(new SqlParameter("@USER_NM", name));

                    comm.DB.ExecSP(cmd);
                }
                catch (Exception ex)
                {
                    Log.Info(ex.Message);
                    return false;
                }
                return true;
            }
        }

        public ACB110B_rs runACB160B1(ACB160B1_ins data, string StoreProcedureName= "ACB160B1")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.SALE_DATE))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = StoreProcedureName;
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@SALE_DATE", data.data.SALE_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }

        public rs runACB160B2(ACB160B1_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
                return new rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };

            if (string.IsNullOrEmpty(data.data.SALE_DATE))
                return new rs() { result = new rsItem { retCode = 1, retMsg = "日期未輸入" } };
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACB160B2";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@SALE_DATE", data.data.SALE_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                comm.DB.ExecSP(cmd);

                return new rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" }
                };
            }
        }

        public rs runACB160B_BATCH(ACB160B_BATCH_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
                return new rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };

            if (string.IsNullOrEmpty(data.data.SALE_DATE))
                return new rs() { result = new rsItem { retCode = 1, retMsg = "日期未輸入" } };
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACB160B_BATCH";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@SALE_DATE", data.data.SALE_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                comm.DB.ExecSP(cmd);

                return new rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" }
                };
            }
        }

        public rs runACB170B_BATCH(ACB170B_BATCH_ins data, string spName = "ACB170B_BATCH")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
                return new rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };

            if (string.IsNullOrEmpty(data.data.YM))
                return new rs() { result = new rsItem { retCode = 1, retMsg = "年月未輸入" } };
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = spName;
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@YM", data.data.YM));
                cmd.Parameters.Add(new SqlParameter("@VOU_DATE", data.data.VOU_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                comm.DB.ExecSP(cmd);

                return new rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" }
                };
            }
        }

        public ACB110B_rs runACB170B1(ACB170B_BATCH_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };

            if (string.IsNullOrEmpty(data.data.YM))
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "年月未輸入" } };

            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACB170B1";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@YM", data.data.YM));
                cmd.Parameters.Add(new SqlParameter("@VOU_DATE", data.data.VOU_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }

        //20241204
        public ACB110B_rs runACD010B1(ACK030B data, string employeeId, string name)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.VOU_DATE))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                if (comm.DB._conn.State == ConnectionState.Closed)
                    comm.DB._conn.Open();

                SqlTransaction transaction = comm.DB._conn.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = "ACD010B1";
                    cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.COMP_ID));
                    cmd.Parameters.Add(new SqlParameter("@VOU_DATE", data.VOU_DATE));
                    cmd.Parameters.Add(new SqlParameter("@USER_ID", employeeId));
                    cmd.Parameters.Add(new SqlParameter("@USER_NM", name));

                    SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                    sp.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(sp);

                    comm.DB.ExecSP(cmd);

                    string[] rs = null;
                    if (sp.Value.ToString() != "")
                    {
                        transaction.Commit();

                        rs = sp.Value.ToString().Split(',');

                        return new ACB110B_rs()
                        {
                            result = new rsItem { retCode = 0, retMsg = "成功" },
                            data = rs != null ? rs.ToList() : new List<string>()
                        };
                    }
                }
                catch(Exception ex) 
                {
                    Log.Info("runACD010B1 error : " + ex.Message);
                }

                transaction.Rollback();

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 1, retMsg = "失敗" }
                };
            }
        }

        //20241205, 20250822
        public ACB110B_rs runACB180B(ACH120B data, string employeeId, string name)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.COMP_ID))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.YM))
            {
                return new ACB110B_rs() { result = new rsItem { retCode = 1, retMsg = "發票年月未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACB180B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@YM", data.YM));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", employeeId));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB110B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs != null ? rs.ToList() : new List<string>()
                };
            }
        }

        public rsACB160B1_result runACC160B1(ACH120B_ins data, string SP_NAME = "ACC160B1")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new rsACB160B1_result() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.YM))
            {
                return new rsACB160B1_result() { result = new rsItem { retCode = 1, retMsg = "會計年月未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = SP_NAME;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@ACC_YM", data.data.YM));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                List<rsACB160B1> rs = new List<rsACB160B1>();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    comm.DB._conn.Open();

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    #region fill data
                    foreach (DataRow row in dt.Rows)
                    {
                        rs.Add(new rsACB160B1 {
                            Item1 = row[0].ToString(),
                            Item2 = row[1].ToString(),
                            Item3 = row[2].ToString(),
                            Item4 = row[3].ToString(),
                            Item5 = row[4].ToString(),
                            Item6 = row[5].ToString(),
                            Item7 = row[6].ToString(),
                            Item8 = row[7].ToString(),
                            Item9 = row[8].ToString(),
                            Item10 = row[9].ToString(),
                            Item11 = row[10].ToString()
                        });
                    }
                    #endregion
                }

                return new rsACB160B1_result()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs
                };
            }
        }

        public rsACB160B2_result runACC160B2(ACH120B_ins data, string SP_NAME = "ACC160B2")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new rsACB160B2_result() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.YM))
            {
                return new rsACB160B2_result() { result = new rsItem { retCode = 1, retMsg = "會計年月未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = SP_NAME;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@ACC_YM", data.data.YM));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                List<rsACB160B2> rs = new List<rsACB160B2>();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    comm.DB._conn.Open();

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    #region fill data
                    foreach (DataRow row in dt.Rows)
                    {
                        rs.Add(new rsACB160B2
                        {
                            Item1 = row[0].ToString(),
                            Item2 = row[1].ToString(),
                            Item3 = row[2].ToString(),
                            Item4 = row[3].ToString(),
                            Item5 = row[4].ToString(),
                            Item6 = row[5].ToString(),
                            Item7 = row[6].ToString(),
                            Item8 = row[7].ToString(),
                            Item9 = row[8].ToString(),
                            Item10 = row[9].ToString(),
                            Item11 = row[10].ToString(),
                            Item12 = row[11].ToString(),
                            Item13 = row[12].ToString(),
                            Item14 = row[13].ToString(),
                            Item15 = row[14].ToString(),
                            Item16 = row[15].ToString(),
                            Item17 = row[16].ToString(),
                            Item18 = row[17].ToString(),
                            Item19 = row[18].ToString(),
                            Item20 = row[19].ToString(),
                            Item21 = row[20].ToString(),
                            Item22 = row[21].ToString(),
                            Item23 = row[22].ToString(),
                            Item24 = row[23].ToString(),
                            Item25 = row[24].ToString(),
                            Item26 = row[25].ToString(),
                            Item27 = row[26].ToString(),
                            Item28 = row[27].ToString(),
                            Item29 = row[28].ToString(),
                            Item30 = row[29].ToString(),
                            Item31 = row[30].ToString(),
                            Item32 = row[31].ToString(),
                            Item33 = row[32].ToString(),
                            Item34 = row[33].ToString(),
                            Item35 = row[34].ToString(),
                            Item36 = row[35].ToString(),
                            Item37 = row[36].ToString(),
                            Item38 = row[37].ToString(),
                            Item39 = row[38].ToString(),
                            Item40 = row[39].ToString(),
                            Item41 = row[40].ToString(),
                            Item42 = row[41].ToString(),
                            Item43 = row[42].ToString(),
                            Item44 = row[43].ToString(),
                            Item45 = row[44].ToString(),
                            Item46 = row[45].ToString(),
                            Item47 = row[46].ToString(),
                            Item48 = row[47].ToString(),
                            Item49 = row[48].ToString(),
                            Item50 = row[49].ToString(),
                            Item51 = row[50].ToString(),
                            Item52 = row[51].ToString(),
                            Item53 = row[52].ToString(),
                            Item54 = row[53].ToString(),
                            Item55 = row[54].ToString()
                        });
                    }
                    #endregion
                }

                return new rsACB160B2_result()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = rs
                };
            }
        }


        // ACD060B
        public ACD060B_rs runACD060B(ACD060B_ins data)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new ACD060B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }
            if (string.IsNullOrEmpty(data.data.PAY_DATE))
            {
                return new ACD060B_rs() { result = new rsItem { retCode = 1, retMsg = "付款日未輸入" } };
            }
            if (string.IsNullOrEmpty(data.data.PAY_KIND))
            {
                return new ACD060B_rs() { result = new rsItem { retCode = 1, retMsg = "付款方式未輸入" } };
            }
            #endregion

            Dictionary<string, object> ps = new Dictionary<string, object> {
                { "@COMP_ID", data.data.COMP_ID},
                { "@PAY_DATE"  , data.data.PAY_DATE},
                { "@PAY_KIND" , data.data.PAY_KIND},
                { "@USER_ID", data.data.USER_ID},
                { "@USER_NM", data.data.USER_NM}
            };

            DataTable dt = commSQL.ExecSP(comm.DB._conn, "ACD060B", ps);

            return new ACD060B_rs() {
                result = new rsItem { retCode = 0, retMsg = "成功" },
                data = dt.ToList<ACD060B_rs_item>()
            };
        }

        public rs runACE030B(ACE030B_ins data, string SP_NAME = "ACE030B")
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.data.PAY_DATE))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "日期未輸入" } };
            }
            if (string.IsNullOrEmpty(data.data.CHKNO))
            {
                return new rs() { result = new rsItem { retCode = 1, retMsg = "CHKNO 未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = SP_NAME;
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@PAY_DATE", data.data.PAY_DATE));
                cmd.Parameters.Add(new SqlParameter("@CHKNO", data.data.CHKNO));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", data.baseRequest.employeeNo));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", data.baseRequest.name));

                comm.DB.ExecSP(cmd);

                return new rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                };
            }
        }

        //20241205, 20250822
        public ACB140B_rs runACB190B(ACB190B data, string employeeId, string name)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.COMP_ID))
            {
                return new ACB140B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }

            if (string.IsNullOrEmpty(data.ISM_NO))
            {
                return new ACB140B_rs() { result = new rsItem { retCode = 1, retMsg = "ISM_NO 未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = "ACB190B";
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@YM", data.ISM_NO));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", employeeId));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 1000);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                string[] rs = null;
                if (sp.Value.ToString() != "")
                    rs = sp.Value.ToString().Split(',');

                return new ACB140B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = new ACB140B_item { VOU_NO = rs != null ? sp.Value.ToString() : "" }  
                };
            }
        }

        // 20251121
        public ACB140B_rs _runACD030B(ACK030B data, string employeeId, string name, string SP)
        {
            #region 檢查資料
            if (string.IsNullOrEmpty(data.COMP_ID))
            {
                return new ACB140B_rs() { result = new rsItem { retCode = 1, retMsg = "公司代碼未輸入" } };
            }
            if (string.IsNullOrEmpty(data.VOU_DATE))
            {
                return new ACB140B_rs() { result = new rsItem { retCode = 1, retMsg = "日期未輸入" } };
            }
            #endregion

            using (SqlCommand cmd = new SqlCommand(string.Empty, comm.DB._conn))
            {
                cmd.CommandText = SP;
                cmd.Parameters.Add(new SqlParameter("@COMP_ID", data.COMP_ID));
                cmd.Parameters.Add(new SqlParameter("@VOU_DATE", data.VOU_DATE));
                cmd.Parameters.Add(new SqlParameter("@USER_ID", employeeId));
                cmd.Parameters.Add(new SqlParameter("@USER_NM", name));

                SqlParameter sp = new SqlParameter("@VOU_NO", SqlDbType.VarChar, 15);
                sp.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(sp);

                comm.DB.ExecSP(cmd);

                return new ACB140B_rs()
                {
                    result = new rsItem { retCode = 0, retMsg = "成功" },
                    data = new ACB140B_item { VOU_NO = sp.Value.ToString() }
                };
            }

        }


    }
}