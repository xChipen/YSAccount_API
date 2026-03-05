using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using COMM;
using Models;
using System.Net.Http;
using System.Net;
using Helpers;
using System.Reflection;

namespace DAO
{
    public class CommDAO
    {
        // 多連線
        public commSQL DB = new commSQL("COMM_DB");

        // 成功
        public static rsItem getRsItem(int code = 0, string msg = "成功")
        {
            return new rsItem { retCode = code, retMsg = msg };
        }
        public static rs getRs(int code = 0, string msg = "成功")
        {
            return new rs()
            {
                result = getRsItem(code, msg)
            };
        }

        // 失敗
        public static rsItem getRsItem1(string msg = "失敗")
        {
            return new rsItem { retCode = 1, retMsg = msg };
        }
        public static rs getRs1(string msg = "失敗")
        {
            return new rs()
            {
                result = getRsItem(1, msg)
            };
        }

        public static rsComm getRsComm(int code = 0, string msg = "成功", string id="")
        {
            return new rsComm()
            {
                result = new rsItem() { retCode = code, retMsg = msg },
                data = new Id() { id = id}
            };
        }

        public static HttpResponseMessage getResponse(HttpRequestMessage request, object data)
        {
            var response = request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new JsonContent(data);

            return response;
        }

        #region 處理分頁
        public static string getSQL_page(string sSQL, int page, int pageSize)
        {
            string head = $@"DECLARE @pageSize INT, @pageNo INT;
SET @pageSize = {pageSize};
SET @pageNo = {page}; ";

            string end = $@" OFFSET(@pageNo - 1) * @pageSize ROWS
FETCH NEXT @pageSize ROWS ONLY; ";

            sSQL = sSQL.Replace("SELECT", "SELECT TotalCount = COUNT(1) OVER (),");

            return $"{head} {sSQL} {end}";
        }

        public static Pagination getPagination(int allPage, int pageNumber, int pageSize, int pageNumbers) {
            return new Pagination()
            {
                pageNumber = pageNumber,
                pageSize = pageSize,
                pageNumbers = allPage % pageSize == 0 ? allPage / pageSize : allPage / pageSize + 1,
                pages = allPage
            };
        }

        public static void initPagination(Pagination pagination, out int pageNumber, out int pageSize, out int pageNumbers) {
            pageNumber = 1;
            pageSize = 10;
            pageNumbers = 0;
            if (pagination != null)
            {
                pageNumber = pagination.pageNumber;
                pageSize = pagination.pageSize;
            }
        }
        #endregion

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
        public static List<string> getObjectFields<T>(T obj) {

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
        public static string getWherePK(List<string> PK) {

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
            for (int ii=sl.Count-1; ii>=0; ii--)
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

        // SQL
        public static string sql_array<T>(List<T> data, string fileName, Boolean bNot = false)
        {

            if (data == null) return "";
            if (data.Count <= 0) return "";

            string sql = "";
            string apostrophe = "";

            // 字串或是數字
            if (typeof(T) == typeof(string)) apostrophe = "'";

            foreach (T item in data)
            {
                sql += $"{apostrophe}{item}{apostrophe},";
            }
            sql = sql.Substring(0, sql.Length - 1);

            if (bNot)
                return $" AND {fileName} NOT IN ({sql}) ";
            else
                return $" AND {fileName} IN ({sql}) ";
        }
        public static string sql_like(string data, string fileName, string likeHead="", string likeEnd="%")
        {
            if (!string.IsNullOrEmpty(data))
                return $" AND {fileName} LIKE '{likeHead}{data}{likeEnd}'";
            else
                return "";
        }
        public static string sql_ep(string data, string fileName, string ep="=")
        {
            if (data == "empty")
                return $" AND {fileName} {ep} ''";
            else if (data == "notempty")
                return $" AND {fileName} <> ''";
            else if (!string.IsNullOrEmpty(data))
                return $" AND {fileName} {ep} '{data}'";
            else
                return "";
        }
        public static string sql_ep_int(int? data, string fileName, string tag=" AND ")
        {
            if (data != null)
            {
                if (data.ToString()=="99999999")
                    return $" {tag} {fileName} <> 0";
                else
                    return $" {tag} {fileName} = {data}";
            }
            else
                return "";
        }
        public static string sql_decimal(decimal? data, string fileName, string tag = " AND ")
        {
            if (data != null)
            {
                if (data.ToString() == "0.000")
                    return $" {tag} {fileName} <> 0";
                else
                    return $" {tag} {fileName} = {data}";
            }
            else
                return "";
        }
        public static string sql_ep_date(DateTime? data, string fileName)
        {
            if (data != null)
                return $" AND convert(char(8), {fileName} ,112) = '{ string.Format("{0:yyyyMMdd}", data)}'";
            else
                return "";
        }
        public static string sql_ep_date_between(DateTime? sData, DateTime? eData, string fileName)
        {
            if (sData != null && eData != null)
                return $@" 
AND convert(char(8), {fileName} ,112) >= '{ string.Format("{0:yyyyMMdd}", sData)}'
AND convert(char(8), {fileName} ,112) <= '{ string.Format("{0:yyyyMMdd}", eData)}'
";
            else
                return "";
        }
        public static string sql_ep_between(string sNO, string eNO, string fileName)
        {
            if (!string.IsNullOrEmpty(sNO) && !string.IsNullOrEmpty(eNO))
                return $@" AND {fileName} >= '{sNO}' AND {fileName} <= '{eNO}'";
            else
                return "";
        }

        //
        public static int getTotalCount(DataTable dt, int pageNumber)
        {
            return (dt.Rows.Count != 0 && pageNumber != 0) ? (int)dt.Rows[0]["TotalCount"] : 0;
        }

    }
}