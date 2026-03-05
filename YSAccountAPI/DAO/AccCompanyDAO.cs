using System.Collections.Generic;
using System.Linq;
using Models;
using System.Data;
using Helpers;

namespace DAO
{
    public class AccCompanyDAO : BaseClass
    {

        // 資料是否存在
        public  DataTable haveAccCompany(string COMP_ID)
        {
            string sql = "SELECT * FROM ACC_COMPANY WHERE COMP_ID = @CODM_ID1 ";

            return comm.DB.RunSQL(sql, new object[] { COMP_ID });
        }
        // 新增
        public  rs addAccCompany(AccCompany_ins data)
        {
            if (string.IsNullOrEmpty(data.data.COMP_ID))
                return CommDAO.getRs(1, "未輸入 公司代號");

            DataTable dt = haveAccCompany(data.data.COMP_ID);
            if (dt != null && dt.Rows.Count != 0)
                return CommDAO.getRs(1, "資料已經存在");

            string sql = $@"INSERT INTO ACC_COMPANY(

COMP_ID,COMP_NAME,COMP_ABBR,COMP_RESPON,COMP_UNNO,COMP_TAXNO,COMP_ADDRESS,COMP_BASE,COMP_TAXNOID,
COMP_CONNECT,COMP_TEL,COMP_FAX,COMP_MAIL_SERVER,COMP_PORT,COMP_ACCOUNT,COMP_PASSWORD,COMP_TAX_RATE,
COMP_LIMIT_MONTHS,COMP_A_USER_ID,COMP_A_USER_NM,COMP_A_DATE,COMP_U_USER_ID,
COMP_U_USER_NM,COMP_U_DATE
) VALUES (
@COMP_ID,@COMP_NAME,@COMP_ABBR,@COMP_RESPON,@COMP_UNNO,@COMP_TAXNO,@COMP_ADDRESS,@COMP_BASE,@COMP_TAXNOID,
@COMP_CONNECT,@COMP_TEL,@COMP_FAX,@COMP_MAIL_SERVER,@COMP_PORT,@COMP_ACCOUNT,@COMP_PASSWORD,@COMP_TAX_RATE,
@COMP_LIMIT_MONTHS,@COMP_A_USER_ID,@COMP_A_USER_NM,GetDate(),@COMP_U_USER_ID,
@COMP_U_USER_NM,GetDate()
) ";
            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.COMP_ID,
                data.data.COMP_NAME,
                data.data.COMP_ABBR,
                data.data.COMP_RESPON,
                data.data.COMP_UNNO,
                data.data.COMP_TAXNO,
                data.data.COMP_ADDRESS,
                data.data.COMP_BASE,
                data.data.COMP_TAXNOID,
                data.data.COMP_CONNECT,
                data.data.COMP_TEL,
                data.data.COMP_FAX,
                data.data.COMP_MAIL_SERVER,
                data.data.COMP_PORT,
                data.data.COMP_ACCOUNT,
                data.data.COMP_PASSWORD,
                data.data.COMP_TAX_RATE,
                data.data.COMP_LIMIT_MONTHS,
                data.baseRequest.employeeNo,
                data.baseRequest.name,
                data.baseRequest.employeeNo,
                data.baseRequest.name
            });

            if (bOK)
                return CommDAO.getRs(0, "成功");

            return CommDAO.getRs(1, "失敗");
        }
        // 修改
        public  rs updateAccCompany(AccCompany_ins data)
        {
            if (string.IsNullOrEmpty(data.data.COMP_ID))
                return CommDAO.getRs(1, "未輸入 公司代號");

            DataTable dt = haveAccCompany(data.data.COMP_ID);
            if (dt == null || dt.Rows.Count == 0)
                return CommDAO.getRs(1, " 資料不存在");

            string sql = $@"UPDATE ACC_COMPANY SET
COMP_NAME=@COMP_NAME,COMP_ABBR=@COMP_ABBR,COMP_RESPON=@COMP_RESPON,COMP_UNNO=@COMP_UNNO,
COMP_TAXNO=@COMP_TAXNO,COMP_ADDRESS=@COMP_ADDRESS,COMP_BASE=@COMP_BASE,COMP_TAXNOID=@COMP_TAXNOID,
COMP_CONNECT=@COMP_CONNECT,COMP_TEL=@COMP_TEL,COMP_FAX=@COMP_FAX,COMP_MAIL_SERVER=@COMP_MAIL_SERVER,
COMP_PORT=@COMP_PORT,COMP_ACCOUNT=@COMP_ACCOUNT,COMP_PASSWORD=@COMP_PASSWORD,COMP_TAX_RATE=@COMP_TAX_RATE,
COMP_LIMIT_MONTHS=@COMP_LIMIT_MONTHS,COMP_U_USER_ID=@COMP_U_USER_ID,
COMP_U_USER_NM=@COMP_U_USER_NM,COMP_U_DATE=GetDate() 
WHERE COMP_ID='{data.data.COMP_ID}' ";

            bool bOK = comm.DB.ExecSQL(sql, new object[] {
                data.data.COMP_NAME,
                data.data.COMP_ABBR,
                data.data.COMP_RESPON,
                data.data.COMP_UNNO,
                data.data.COMP_TAXNO,
                data.data.COMP_ADDRESS,
                data.data.COMP_BASE,
                data.data.COMP_TAXNOID,
                data.data.COMP_CONNECT,
                data.data.COMP_TEL,
                data.data.COMP_FAX,
                data.data.COMP_MAIL_SERVER,
                data.data.COMP_PORT,
                data.data.COMP_ACCOUNT,
                data.data.COMP_PASSWORD,
                data.data.COMP_TAX_RATE,
                data.data.COMP_LIMIT_MONTHS,
                data.baseRequest.employeeNo,
                data.baseRequest.name
            });
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }
        // 刪除
        public  rs deleteAccCompany(AccCompany_del data)
        {
            if (string.IsNullOrEmpty(data.data.COMP_ID ))
                return CommDAO.getRs(1, "未輸入 資料類別代號");

            string sql = $"DELETE ACC_COMPANY WHERE COMP_ID='{data.data.COMP_ID}'";

            bool bOK = comm.DB.ExecSQL(sql);
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }
        // 查詢
        public  AccCompany_query queryAccCompany(AccCompany_del data)
        {
            string sql = "Select * from ACC_COMPANY where 1=1 ";

            if (!string.IsNullOrEmpty(data.data.COMP_ID))
                sql += $" and COMP_ID='{data.data.COMP_ID}'";

            DataTable dt = comm.DB.RunSQL(sql);
            if (dt.Rows.Count != 0)
            {
                List<AccCompany> rs = dt.ToList<AccCompany>();
                return rsAccCompany(rs, 0, "成功");
            }
            return new AccCompany_query() { result = new rsItem() { retCode = 1, retMsg = "失敗" } };
        }

        // Query return
        public static AccCompany_query rsAccCompany(List<AccCompany> data = null, int retCode = 1, string retMsg = "失敗")
        {
            return new AccCompany_query()
            {
                result = new rsItem() { retCode = retCode, retMsg = retMsg },
                data = data != null ? data[0] : null
            };
        }

        // 關帳日更新
        public rs updateCompany_CloseDate(AccCompany_CloseDate data)
        {
            #region 檢查
            if (string.IsNullOrEmpty(data.data.COMP_ID))
            {
                return CommDAO.getRs(0, "公司代碼未輸入");
            }
            if (data.data.COMP_GL_CLOSE_DATE == null)
            {
                return CommDAO.getRs(0, "總帳關脹迄止日期未輸入");
            }
            if (data.data.COMP_TAX_CLOSE_DATE == null)
            {
                return CommDAO.getRs(0, "申報資料關脹迄止日期未輸入");
            }

            DataTable dt = haveAccCompany(data.data.COMP_ID);
            if (dt.Rows.Count == 0)
            {
                return CommDAO.getRs(0, "查無公司代碼");
            }
            #endregion

            string sql = $@"UPDATE ACC_COMPANY set COMP_GL_CLOSE_DATE = @COMP_GL_CLOSE_DATE,
COMP_TAX_CLOSE_DATE = @COMP_TAX_CLOSE_DATE";

            List<object> obj = new List<object> {
                data.data.COMP_GL_CLOSE_DATE,
                data.data.COMP_TAX_CLOSE_DATE
            };

            if (data.data.COMP_COST_CLOSE_DATE != null)
            {
                sql += ",COMP_COST_CLOSE_DATE=@COMP_COST_CLOSE_DATE";
                obj.Add(data.data.COMP_COST_CLOSE_DATE);
            }

            sql += " WHERE COMP_ID=@COMP_ID";
            obj.Add(data.data.COMP_ID);

            bool bOK = comm.DB.ExecSQL(sql, obj.ToArray());
            if (bOK)
                return CommDAO.getRs();

            return CommDAO.getRs(0, "失敗");
        }
    }
}