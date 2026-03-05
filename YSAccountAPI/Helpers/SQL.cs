using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Helpers;
using System.Configuration;

namespace COMM
{
    public class commSQL
    {
        public SqlConnection _conn;
        public SqlCommand cmd;

        public commSQL(string connectionName)
        {
            _conn = new SqlConnection(sConn(connectionName));
            cmd = new SqlCommand();
        }
        /// <summary>
        /// SQL 查詢資料
        /// </summary>
        /// <param name="sSQL">SQL 語法</param>
        /// <param name="sqlP">參數, 可以不填入</param>
        /// <returns>回傳資料表</returns>
        public DataTable RunSQL(string sSQL, params object[] sqlP)
        {
            return RunSQL(_conn, sSQL, sqlP);
        }

        // 避免被攻擊
        public DataTable RunSQL(string sSQL, Dictionary<string, object> sqlP)
        {
            return RunSQL(_conn, sSQL, sqlP);
        }

        /// <summary>
        /// SQL 查詢資料
        /// </summary>
        /// <param name="dt">ref 資料表</param>
        /// <param name="sSQL">SQL 語法</param>
        /// <param name="sqlP">參數, 可以不填入</param>
        /// <returns>回傳是否有資料</returns>
        public bool RunSQL(out DataTable dt, string sSQL, params object[] sqlP)
        {
            return RunSQL(_conn, out dt, sSQL, sqlP);
        }
        /// <summary>
        /// SQL 更新 
        /// </summary>
        /// <param name="sSQL">SQL語法</param>
        /// <param name="sqlP">參數, 可以不填入</param>
        /// <returns>回傳是否成功</returns>
        public bool ExecSQL(string sSQL, params object[] sqlP)
        {
            return ExecSQL(_conn, sSQL, sqlP);
        }
        public bool ExecSQL(string sSQL, Dictionary<string, object> sqlP)
        {
            return ExecSQL(_conn, sSQL, sqlP);
        }

        public DataTable ExecSP(string spName, Dictionary<string, object> p = null)
        {
            return ExecSP(_conn, spName, p);
        }
        public DataTable ExecSP(SqlCommand cmd)
        {
            return ExecSP(_conn, cmd);
        }

        /// <summary>
        /// 取資料表 Primary key
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        public List<string> getPrimaryKey(string sTableName)
        {
            return getPrimaryKey(_conn, sTableName);
        }
        public DataTable getTableInfo_dr(string sTableName)
        {
            return getTableInfo_dr(_conn, sTableName);
        }
        public List<string> getPrimaryKey_dr(string sTableName)
        {
            return getPrimaryKey_dr(_conn, sTableName);
        }

        #region Transaction 配合 物件使用
        public void BeginTransaction()
        {
            BeginTransaction(ref _conn, ref cmd);
        }
        public void Rollback()
        {
            Rollback(cmd);
        }
        public void Commit()
        {
            Commit(cmd);
        }
        public bool ExecSQL_T(string sSQL, params object[] sqlP)
        {
            return ExecSQL_T(cmd, sSQL, sqlP);
        }
        #endregion


//--- static -------------------------------------------------------------------------------------

        // 連線字串 : 從 設定檔 AppSettings
        public static string sConn(string connectionName)
        {
            return ConfigurationManager.AppSettings[connectionName].ToString();
        }
        // 連線字串 : 透過參數
        public static string sConn(string sIP, string sDB, string sUser, string sPW)
        {
            return String.Format("Data Source={0};Initial Catalog = {1}; User ID = {2}; Password = {3}",
                      sIP, sDB, sUser, sPW);
        }
        // 連接資料庫
        public static bool openDB(SqlConnection conn, string sConn, ref string sError)
        {
            try
            {
                conn.Close();
                conn.ConnectionString = sConn;

                conn.Open();
                return true;
            }
            catch (SqlException ex)
            {
                sError = ex.Message;
                Log.Info("[openDB] 錯誤訊息:" + sError);
                return false;
            }
        }
        // SQL 參數
        private static List<string> getParam(string sData)
        {
            List<string> sl = new List<string>();

            // 將 @ 帶頭視為參數
            string[] slData = sData.Split(new char[] {'@'});
            string[] sl_sub = { };

            string ss = "";
            for (int ii=1; ii < slData.Count(); ii++)
            {
                ss = slData[ii];
                // 取@後面字串. 逗號與空白視為 '結尾'
                sl_sub = slData[ii].Split(new char[] { ',', ' ', '}', '"', ')', '\r'});
                if (sl_sub.Length != 0) ss = sl_sub[0];

                sl.Add("@" + ss.Replace('\r', ' ').Replace('\n', ' ').Trim());
            }
            return sl;
        }

        // 參數 : add to SqlCommand
        private static void addParams(SqlCommand cmd, string sSQL, object[] sqlP)
        {
            if (sqlP == null || sqlP.Count() == 0) return;

            List<string> sl = getParam(sSQL);

            if (sl.Count() != sqlP.Length)  // 參數與資料必須相符
            {
                Log.Info("參數與資料必須相符");
                return;
            }

            for (int ii = 0; ii < sl.Count(); ii++)
            {
                cmd.Parameters.AddWithValue(sl[ii], sqlP[ii] ?? DBNull.Value);
            }
        }

        private static bool addParams(SqlCommand cmd, string sSQL, Dictionary<string, object> sqlP)
        {
            if (sqlP == null || sqlP.Count() == 0) return false;

            List<string> sl = getParam(sSQL);

            if (sl.Count() != sqlP.Count)
            {
                Log.Info("addParams : 參數與資料必須相符");
                return false;
            }

            string key;
            object value;
            for (int ii = 0; ii < sl.Count(); ii++)
            {
                value = DBNull.Value;
                key = sl[ii].Replace("@", "");

                if (sqlP.ContainsKey( key ))
                    sqlP.TryGetValue( key, out value);

                cmd.Parameters.AddWithValue("@" + key, value);
            }
            return true;
        }

        // 查詢 : overload SQL: Select ; result DataSet
        public static bool RunSQL(SqlConnection conn, string sSQL, ref DataSet ds, params object[] sqlP)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sSQL, conn))
                {
                    addParams(cmd, sSQL, sqlP);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.Info(ex.Message);
                Log.Info("RunSQL: " + sSQL);

                return false;
            }
        }
        // 查詢 : overload Result DataTable
        public static DataTable RunSQL(SqlConnection conn, string sSQL, params object[] sqlP)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand(sSQL, conn))
                {
                    addParams(cmd, sSQL, sqlP);
                    //Log.Info("RunSQL 111: " + sSQL);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.Info(ex.Message);
                Log.Info("RunSQL: " + sSQL);

                return dt;
            }
        }

        public static DataTable RunSQL(SqlConnection conn, string sSQL, Dictionary<string, object> sqlP)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand(sSQL, conn))
                {
                    bool bOK = addParams(cmd, sSQL, sqlP);
                    if (!bOK) return dt;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.Info(ex.Message);
                Log.Info("RunSQL: " + sSQL);

                return dt;
            }
        }

        public static bool RunSQL(SqlConnection conn, out DataTable dt, string sSQL, params object[] sqlP)
        {
            dt =  RunSQL(conn, sSQL, sqlP);
            return dt.Rows.Count != 0;
        }
        // 更新
        public static bool ExecSQL(SqlConnection conn, string sSQL, params object[] sqlP)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sSQL, conn))
                {
                    addParams(cmd, sSQL, sqlP);

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Log.Info(ex.Message);
                Log.Info(sSQL);

                return false;
            }
        }
        public static bool ExecSQL(SqlConnection conn, string sSQL, Dictionary<string, object> sqlP)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sSQL, conn))
                {
                    addParams(cmd, sSQL, sqlP);

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Log.Info(ex.Message);
                Log.Info(sSQL);

                return false;
            }
        }

        #region Static Transaction 可以獨立使用
        public static void BeginTransaction(ref SqlConnection _conn, ref SqlCommand cmd)
        {
            //連接Open
            if (_conn.State != ConnectionState.Open)
                _conn.Open();
            if (cmd == null)
                cmd = new SqlCommand();

            cmd.Connection = _conn;

            cmd.Transaction = _conn.BeginTransaction();
        }
        public static void Rollback(SqlCommand cmd)
        {
            cmd.Transaction.Rollback();
        }
        public static void Commit(SqlCommand cmd)
        {
            cmd.Transaction.Commit();
        }
        public static bool ExecSQL_T(SqlCommand cmd, string sSQL, params object[] sqlP)
        {
            try
            {
                cmd.CommandText = sSQL;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();

                addParams(cmd, sSQL, sqlP);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                Log.Info(ex.Message);
                Log.Info(sSQL);

                return false;
            }
        }

        public static bool ExecSQL_T(SqlCommand cmd, string sSQL, Dictionary<string, object> sqlP)
        {
            try
            {
                cmd.CommandText = sSQL;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();

                addParams(cmd, sSQL, sqlP);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                Log.Info(ex.Message);
                Log.Info(sSQL);

                return false;
            }
        }

        #endregion

        #region Store Procedure
        // Example
        // Dictionary<string, object> p = new Dictionary<string, object> {
        //        { "@saleno", "202112220001"}
        // };
        //DataTable dt = commSQL.ExecSP(conn, "qrySaleM", p);

        /// <summary>
        /// 執行 Store Procedure
        /// </summary>
        /// <param name="conn">連線物件</param>
        /// <param name="spName">Store Procedure 名稱</param>
        /// <param name="p">參數</param>
        /// <returns></returns>
        public static DataTable ExecSP(SqlConnection conn, string spName, Dictionary<string, object> p = null)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(string.Empty, conn))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Parameters.Clear();

                if (p != null)
                {
                    foreach (var item in p)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // 當參數需要設定回傳值時使用
        // using (SqlCommand cmd = new SqlCommand(string.Empty, conn)) {
        //   cmd.CommandText = "qrySaleM2";
        //   cmd.Parameters.Add(new SqlParameter("@saleno", "202112220001"));
        //   SqlParameter sp = new SqlParameter("@FK", SqlDbType.VarChar, 20);
        //   sp.Direction = ParameterDirection.Output;
        //   cmd.Parameters.Add(sp);
        //   DataTable dt = commSQL.ExecSP(conn, cmd);
        // }

        public static DataTable ExecSP(SqlConnection conn, SqlCommand cmd)
        {
            DataTable dt = new DataTable();

            if (conn.State == ConnectionState.Closed) conn.Open();

            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
                return dt;
            }
        }
        #endregion


        /// <summary>
        /// DataTable Filter Data
        /// </summary>
        /// <param name="oTable"></param>
        /// <param name="filterExpression"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public static DataTable DataTable_Filter(DataTable oTable, string filterExpression, string sortExpression = "")
        {
            DataView dv = new DataView();
            dv.Table = oTable;
            dv.RowFilter = filterExpression;
            if (sortExpression != string.Empty)
                dv.Sort = sortExpression;
            DataTable nTable = dv.ToTable();
            return nTable;
        }

        /// <summary>
        /// DataTable Select Data
        /// </summary>
        /// <param name="oTable"></param>
        /// <param name="filterExpression"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public static DataTable DataTable_Select(DataTable oTable, string filterExpression, string sortExpression = "")
        {
            DataTable nTable;
            if (sortExpression == string.Empty)
                nTable = oTable.Select(filterExpression).CopyToDataTable();
            else
                nTable = oTable.Select(filterExpression, sortExpression).CopyToDataTable();

            return nTable;
        }

        /// <summary>
        /// get Table primary key
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        public static List<string> getPrimaryKey(SqlConnection conn, string sTableName)
        {
            string ss = @"SELECT ColumnName = col.column_name 
                        FROM information_schema.table_constraints tc 
                        INNER JOIN information_schema.key_column_usage col 
                              ON col.Constraint_Name = tc.Constraint_Name 
                              AND col.Constraint_schema = tc.Constraint_schema 
                        WHERE tc.Constraint_Type = 'Primary Key' AND col.Table_name = '{0}'";
            ss = string.Format(ss, sTableName);
            DataTable dt = commSQL.RunSQL(conn, ss);

            List<string> sl = new List<string>();

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sl.Add(dr["ColumnName"].ToString());
                }
            }
            return sl;
        }

        /// <summary>
        /// 取資料表資訊
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        public static DataTable getTableInfo_dr(SqlConnection conn, string sTableName)
        {
            string ss = string.Format("Select * from {0}", sTableName);
            SqlCommand cmd = new SqlCommand(ss, conn);
            System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo);

            DataTable dt = dr.GetSchemaTable();
            if (dr.HasRows)
            {
                for (int ii = dt.Columns.Count - 1; ii >= 0; ii--)
                {
                    if (!(dt.Columns[ii].ColumnName == "ColumnName" ||
                          dt.Columns[ii].ColumnName == "ColumnOrdinal" ||
                          dt.Columns[ii].ColumnName == "ColumnSize" ||
                          dt.Columns[ii].ColumnName == "NumericPrecision" ||
                          dt.Columns[ii].ColumnName == "NumericScale" ||
                          dt.Columns[ii].ColumnName == "IsKey" ||
                          dt.Columns[ii].ColumnName == "AllowDBNull" ||
                          dt.Columns[ii].ColumnName == "DataTypeName"
                          ))
                    {
                        dt.Columns.Remove(dt.Columns[ii]);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// get Tbale primary key by DataReader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        public static List<string> getPrimaryKey_dr(SqlConnection conn, string sTableName)
        {
            DataTable dt = getTableInfo_dr(conn, sTableName);

            List<string> sl = new List<string>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if ((bool)dr["IsKey"])
                        sl.Add(dr["ColumnName"].ToString());
                }
            }
            return sl;
        }

        /// <summary>
        /// get Table title list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<string> getTable_Title(DataTable dt, List<string> outFields= null)
        {
            List<string> ls = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                if (outFields != null)
                {
                    if (!outFields.Contains(dc.ColumnName))
                        ls.Add(dc.ColumnName);
                }
                else
                    ls.Add(dc.ColumnName);
            }
            return ls;
        }

        public static string getTable_Title_Str(DataTable dt)
        {
            List<string> sl = getTable_Title(dt);
            return string.Join(",", sl.ToArray());
        }

        public static void saveToFile(DataTable dt, string sPath, string sTableName)
        {
            if (File.Exists(sPath))
                File.Delete(sPath);

            string sTitle = commSQL.getTable_Title_Str(dt);

            using (StreamWriter sw = new StreamWriter(sPath, true))
            {
                string ss = string.Empty;

                sw.WriteLine(sTableName);       // 資料表名稱
                sw.WriteLine("");
                sw.WriteLine(dt.Rows.Count);    // 筆數     
                sw.WriteLine(sTitle);           // 欄位名稱

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ss = string.Empty;
                        foreach (DataColumn dc in dt.Columns)
                        {
                            ss += dr[dc].ToString() + ",";
                        }
                        ss = ss.Substring(0, ss.Length - 1);
                        sw.WriteLine(ss);
                    }
                }
            }
        }


    }
}
