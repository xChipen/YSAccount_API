using System;
using System.Collections.Generic;
using Models;
using System.Data;
using Helpers;
using System.Reflection;
using COMM;

namespace DAO
{
    public class BaseClass2
    {
        public CommDAO comm = new CommDAO();

        // 更新資料表, 重取 PK
        string _tableName;
        protected string tableName {
            get { return _tableName; }
            set {
                _tableName = value;
                if (!string.IsNullOrEmpty(value))
                {
                    PK = CommDAO.getPK(comm, _tableName);
                }
            }
        }
        // Table primary key
        protected List<string> PK = null;
        // 不更新欄位 - 清單
        public List<string> outFields = new List<string>();

        #region 建構式
        public BaseClass2()
        {}
        public BaseClass2(string _tableName="")
        {
            if (!string.IsNullOrEmpty(_tableName))
            {
                tableName = _tableName;
                PK = CommDAO.getPK(comm, tableName);
            }
        }
        #endregion

        #region Batch call
        public bool isExist<T>(T data)
        {
            return CommDAO.isExist(comm, data, tableName, PK);
        }
        //public bool _AddBatch<T>(T data, CommDAO dao = null)
        //{
        //    return CommDAO._add(null, data, tableName, PK, dao);
        //}
        //public bool _UpdateBatch<T>(T data, CommDAO dao = null)
        //{
        //    return CommDAO._update(null, data, tableName, PK, null, dao);
        //}
        //public bool _DeleteBatch<T>(T data, CommDAO dao = null)
        //{
        //    return CommDAO._delete(null, data, tableName, PK, null, dao);
        //}
        #endregion

        // 取回傳值
        public static R getResult<R>(int retCode=0, string retMsg="成功") where R : Irs, new()
        {
            return new R { result = new rsItem { retCode = retCode, retMsg = retMsg } };
        }

        // 資料新增
        public virtual U Add<T, U>(T data) where U : Irs, new()
        {
            string errMsg="";
            if (PK == null) return getResult<U>(1, "Primary Key 不存在");

            if ( !isFields<T>(PK, data) ) return getResult<U>(1, "Primary Key 資料未填入");

            if ( !isExist(data) )
            {
                bool bOK = _add(comm, data, tableName, PK, outFields: outFields, errMsg:errMsg);
                if (bOK)
                    return getResult<U>();
            }
            return getResult<U>(1,  errMsg!="" ? errMsg: "資料已經存在");
        }

        // 資料更新
        public U Update<T, U>(T data) where U : rs, new()
        {
            string errMsg = "";
            if (PK == null) return getResult<U>(1, "Primary Key 不存在");

            if (!isFields<T>(PK, data)) return getResult<U>(1, "Primary Key 資料未填入");

            if ( isExist(data) )
            {
                bool bOK = _update(comm, data, tableName, PK, outFields: outFields, errMsg: errMsg);
                if (bOK)
                    return getResult<U>();
            }
            return getResult<U>(1, errMsg != "" ? errMsg : "資料不存在");
        }

        // 批次刪除 By PK
        public U Delete<T, U>(List<T> data) where U : Irs, new()
        {
            if (PK == null) return getResult<U>(1, "Primary Key 不存在");

            foreach (T item in data)
            {
                if (!isFields<T>(PK, item)) return getResult<U>(1, "Primary Key 資料未填入");

                _delete(comm, item, tableName, PK);
            }
            return getResult<U>();
        }

        // 依 PK 刪除
        public U Delete<T, U>(T data) where U : rs, new()
        {
            if (PK == null) return getResult<U>(1, "Primary Key 不存在");

            if (isFields<T>(PK, data)) return getResult<U>(1, "Primary Key 資料未填入");

            _delete(comm, data, tableName, PK);

            return getResult<U>();
        }

        // 取回傳值
        public static R getResult_rsList<R>(int retCode = 0, string retMsg = "成功") where R : rsList, new()
        {
            return new R { result = new rsItem { retCode = retCode, retMsg = retMsg } };
        }

        // 批次更新資料
        public U AUD<T, U>(List<T> data) where T : comm_in where U : rsList, new()
        {
            if (data.Count == 0)
                return getResult_rsList<U>(1, "無輸入資料");

            List<comm_in> rsData = new List<comm_in>();

            bool bOK = true;
            rs rsAUD = null;

            foreach (T item in data)
            {
                switch (item._State)
                {
                    case "A":
                        rsAUD = Add<T, rs>(item);
                        rsData.Add(new comm_in { _auto_id = item._auto_id, _State = rsAUD.result.retMsg });
                        break;
                    case "U":
                        rsAUD = Update<T, rs>(item);
                        rsData.Add(new comm_in { _auto_id = item._auto_id, _State = rsAUD.result.retMsg });
                        break;
                    case "D":
                        rsAUD = Delete<T, rs>(item);
                        rsData.Add(new comm_in { _auto_id = item._auto_id, _State = rsAUD.result.retMsg });
                        break;
                }

                if (rsAUD != null && rsAUD.result.retCode != 0)
                {
                    bOK = false;
                    break;
                }
            }

            return new U
            {
                result = bOK ? CommDAO.getRsItem() : CommDAO.getRsItem1(),
                data = rsData
            };
        }

        #region SQL
        public static void addKeyValue(ref Dictionary<string, object> P, string key, object value)
        {
            if (!P.ContainsKey(key))
                P.Add(key, value);
        }

        public static string sql_ep_int(int? data, string fileName,
            ref Dictionary<string, object> P, string tag = " AND ")
        {
            string paramName = fileName;
            if (fileName.Contains("."))
                paramName = paramName.Substring(fileName.IndexOf(".") + 1);

            if (data != null)
            {
                if (data.ToString() == "99999999")
                    return $" {tag} {fileName} <> 0";
                else
                {
                    addKeyValue(ref P, fileName, data);
                    return $" {tag} {fileName} = @{paramName}";
                }
            }
            else
                return "";
        }

        public static string sql_ep(string data, string fileName,
            ref Dictionary<string, object> P, string ep = "=")
        {
            string paramName = fileName;
            if (fileName.Contains("."))
                paramName = paramName.Substring(fileName.IndexOf(".") + 1);

            if (data == "empty")
                return $" AND {fileName} {ep} ''";
            else if (data == "notempty")
                return $" AND {fileName} <> ''";
            else if (!string.IsNullOrEmpty(data))
            {
                addKeyValue(ref P, fileName, data);

                return $" AND {fileName} {ep} @{paramName}";
            }
            else
                return "";
        }

        public static string sql_array<T>(List<T> data, string fileName,
            ref Dictionary<string, object> P, Boolean bNot = false)
        {

            if (data == null) return "";
            if (data.Count <= 0) return "";

            string sql = "";
            string apostrophe = "";

            // 字串或是數字
            //if (typeof(T) == typeof(string)) apostrophe = "'";

            int idx = 1;
            string key = "P";
            foreach (T item in data)
            {
                sql += $"{apostrophe}@{key}{idx}{apostrophe},";
                addKeyValue(ref P, $"@{key}{idx}", item);
                idx++;
            }
            sql = sql.Substring(0, sql.Length - 1);

            if (bNot)
                return $" AND {fileName} NOT IN ({sql}) ";
            else
                return $" AND {fileName} IN ({sql}) ";
        }

        public static string sql_like(string data, string fileName,
            ref Dictionary<string, object> P, string likeHead = "", string likeEnd = "%")
        {
            string paramName = fileName;
            if (fileName.Contains("."))
                paramName = paramName.Substring(fileName.IndexOf(".") + 1);

            if (!string.IsNullOrEmpty(data))
            {
                addKeyValue(ref P, fileName, $"{likeHead}{data}{likeEnd}");
                return $" AND {fileName} LIKE @{paramName}";
            }
            else
                return "";
        }

        public static string sql_decimal(decimal? data, string fileName,
            ref Dictionary<string, object> P, string tag = " AND ")
        {
            string paramName = fileName;
            if (fileName.Contains("."))
                paramName = paramName.Substring(fileName.IndexOf(".") + 1);

            if (data != null)
            {
                if (data.ToString() == "0.000")
                    return $" {tag} {fileName} <> 0";
                else
                {
                    addKeyValue(ref P, fileName, data);
                    return $" {tag} {fileName} = @{paramName}";
                }
            }
            else
                return "";
        }

        public static string sql_ep_date(DateTime? data, string fileName,
            ref Dictionary<string, object> P)
        {
            string paramName = fileName;
            if (fileName.Contains("."))
                paramName = paramName.Substring(fileName.IndexOf(".") + 1);

            if (data != null)
            {
                addKeyValue(ref P, fileName, string.Format("{0:yyyyMMdd}", data));
                return $" AND convert(char(8), {fileName} ,112) = @{paramName}";
            }
            else
                return "";
        }

        public static string sql_ep_date_between(DateTime? sData, DateTime? eData, string fileName,
            ref Dictionary<string, object> P)
        {
            string paramName = fileName;
            if (fileName.Contains("."))
                paramName = paramName.Substring(fileName.IndexOf(".") + 1);

            if (sData != null && eData != null)
            {
                addKeyValue(ref P, fileName + "_S", string.Format("{0:yyyyMMdd}", sData));
                addKeyValue(ref P, fileName + "_E", string.Format("{0:yyyyMMdd}", eData));
                return $@" 
AND convert(char(8), {fileName} ,112) >= @{paramName}{"_S"}
AND convert(char(8), {fileName} ,112) <= @{paramName}{"_E"}
";
            }
            else
                return "";
        }

        public static string sql_ep_between(string sNO, string eNO, string fileName,
            ref Dictionary<string, object> P)
        {
            string paramName = fileName;
            if (fileName.Contains("."))
                paramName = paramName.Substring(fileName.IndexOf(".") + 1);

            if (!string.IsNullOrEmpty(sNO) && !string.IsNullOrEmpty(eNO))
            {
                addKeyValue(ref P, fileName + "_S", sNO);
                addKeyValue(ref P, fileName + "_E", eNO);

                return $@" AND {fileName} >= @{paramName}{"_S"} AND {fileName} <= @{paramName}{"_E"}";
            }
            else
                return "";
        }
        #endregion

        #region 自動新增刪除修改
        public static List<string> getPK(CommDAO comm, string tableName)
        {
            return commSQL.getPrimaryKey(comm.DB._conn, tableName);
        }

        // SQL查詢式 : PK 檢查資料是否存在 
        public static string existSQL(string tableName, List<string> data)
        {
            string sql = $"SELECT * FROM {tableName} WHERE 1 = 1";
            foreach (string item in data)
                sql += $" AND {item}=@{item} ";
            return sql;
        }

        // 檢查物件中欄位是否有輸入
        public static bool isFields<T>(List<string> PKs, T obj)
        {
            if (obj == null) return false;

            foreach (string pk in PKs)
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(pk);
                var value = propertyInfo.GetValue(obj, null);

                string sType = propertyInfo.PropertyType.Name;
                if (sType == "String")
                {
                    if (String.IsNullOrEmpty(value.ToString())) return false;
                }
                else
                {
                    if (value == null) return false;
                }
            }

            return true;
        }

        // 物件中取出 欄位 對應的資料
        public static object[] getParamsValues<T>(List<string> PKs, T sour)
        {
            List<object> obj = new List<object>();

            foreach (string pk in PKs)
            {
                PropertyInfo propertyInfo = sour.GetType().GetProperty(pk);
                if (propertyInfo != null)
                {
                    var value = propertyInfo.GetValue(sour, null);
                    obj.Add(value);
                }
            }

            return obj.ToArray();
        }

        // 取 "資料表" 欄位名稱
        public static List<string> getTableData(CommDAO dao, string tableName, List<string> outFields = null)
        {
            string sSQL = $"SELECT * from {tableName} where 1<>1";
            DataTable dt = dao.DB.RunSQL(sSQL);
            return commSQL.getTable_Title(dt, outFields);
        }

        // 取 "物件" 欄位名稱
        public static List<string> getObjectFields<T>(T obj)
        {

            List<string> sl = new List<string>();

            PropertyInfo[] Mcols = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (Mcols != null)
            {
                for (int i = 0; i < Mcols.Length; i++)
                {
                    PropertyInfo col = Mcols[i];

                    if (col.GetValue(obj, null) != null)
                    {
                        sl.Add(col.Name);
                    }
                }
                return sl;
            }
            return null;
        }

        // 使用前提 class 欄位名稱 與 資料表 名稱相符
        // 用PK檢查資料是否存在
        public static bool isExist<T>(CommDAO dao, T data, string tableName, List<string> PK)
        {
            if (PK == null || PK.Count == 0)
            {
                Log.Info(tableName + " : 無PK資料");
                return false;
            }

            // 檢查欄位是否有資料
            if (!CommDAO.isFields(PK, data)) return false;

            // 產生 查詢 SQL
            string sql = CommDAO.existSQL(tableName, PK);

            //                                 填值
            DataTable dt = dao.DB.RunSQL(sql, CommDAO.getParamsValues(PK, data));

            // 資料是否存在
            return dt.Rows.Count != 0;
        }

        // 新增
        public static string insertSQL(string tableName, List<string> fields)
        {
            string p1 = string.Join(",", fields.ToArray());
            string p2 = "@" + string.Join(",@", fields.ToArray());

            return $"INSERT INTO {tableName} ({p1}) VALUES({p2})";
        }

        public static bool _add<T>(CommDAO dao2, T data, string tableName, List<string> PK, CommDAO dao = null, List<string> outFields = null, string errMsg = "")
        {
            errMsg = "";
            // 檢查欄位是否有資料
            if (!CommDAO.isFields(PK, data))
            {
                errMsg = "欄位對照錯誤";
                return false;
            }

            // 取出資料表 欄位名稱
            // 欄位 依 資料表
            List<string> sl = CommDAO.getTableData(dao2, tableName, outFields);
            if (sl == null || sl.Count == 0)
            {
                errMsg = "取資料庫欄位錯誤";
                return false;
            }

            string sql = CommDAO.insertSQL(tableName, sl);

            if (dao == null)
                return dao2.DB.ExecSQL(sql, CommDAO.getParamsValues(sl, data));
            else
                return dao.DB.ExecSQL_T(sql, CommDAO.getParamsValues(sl, data));
        }

        // 產生 Update SQL
        public static string getWherePK(List<string> PK)
        {

            string p2 = "";
            foreach (string fsName in PK)
            {
                p2 += $"{fsName}=@{fsName} AND ";
            }
            p2 = p2.Substring(0, p2.Length - " AND ".Length);
            return p2;
        }
        public static string updateSQL(string tableName, List<string> fields, List<string> PK)
        {
            #region Get Fields PK, 排除 PK 欄位
            string p1 = "";
            foreach (string fsName in fields)
            {
                if (!PK.Contains(fsName))
                    p1 += $"{fsName}=@{fsName},";
            }
            p1 = p1.Substring(0, p1.Length - 1);
            #endregion

            string p2 = getWherePK(PK);

            return $"UPDATE {tableName} set {p1} Where {p2}";
        }

        // 使用前提 class 欄位名稱 與 資料表 名稱相符
        // 更新                                                 PK 清單          排除清單                                              
        public static bool _update<T>(CommDAO dao2, T data, string tableName, List<string> PK, List<string> outFields = null, CommDAO dao = null, string errMsg = "")
        {
            // 1.檢查欄位是否有資料
            errMsg = "";
            // 檢查欄位是否有資料
            if (!CommDAO.isFields(PK, data))
            {
                errMsg = "欄位對照錯誤";
                return false;
            }

            // 取出資料表 欄位名稱 - 依資料表欄位更新
            List<string> sl = CommDAO.getTableData(dao2, tableName, outFields);
            if (sl == null || sl.Count == 0)
            {
                errMsg = "取資料庫欄位錯誤";
                return false;
            }

            #region 因為是 Update, 所以將 PK 欄位 移至最後
            // 移除
            for (int ii = sl.Count - 1; ii >= 0; ii--)
            {
                if (PK.Contains(sl[ii]))
                    sl.Remove(sl[ii]);
            }
            // 重新加回 PK 欄位
            sl.AddRange(PK);
            #endregion

            // 產生 Update SQL
            string sql = CommDAO.updateSQL(tableName, sl, PK);

            if (dao == null)                // 產生更新資料
                return dao2.DB.ExecSQL(sql, CommDAO.getParamsValues(sl, data));
            else
                return dao.DB.ExecSQL_T(sql, CommDAO.getParamsValues(sl, data));
        }

        public static string deleteSQL(string tableName, List<string> PK)
        {
            return $"DELETE {tableName} WHERE { getWherePK(PK) }";
        }

        public static bool _delete<T>(CommDAO dao2, T data, string tableName, List<string> PK, List<string> outFields = null, CommDAO dao = null)
        {
            string sql = deleteSQL(tableName, PK);

            if (dao == null)                // 產生更新資料
                return dao2.DB.ExecSQL(sql, CommDAO.getParamsValues(PK, data));
            else
                return dao.DB.ExecSQL_T(sql, CommDAO.getParamsValues(PK, data));
        }


        public static bool checkDataIsLive(string state, bool isLive, ref string errMsg)
        {
            errMsg = "";
            switch (state)
            {
                case "A":
                    if (isLive)
                    {
                        errMsg = "新增:資料已經存在";
                        return false;
                    }
                    break;
                case "U":
                    if (!isLive)
                    {
                        errMsg = "修改:資料不存在";
                        return false;
                    }
                    break;
                case "D":
                    if (!isLive)
                    {
                        errMsg = "刪除:資料不存在";
                        return false;
                    }
                    break;
            }
            return true;
        }
        #endregion




    }
}