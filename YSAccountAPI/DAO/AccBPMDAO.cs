using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Helpers;

namespace DAO
{
    public class AccBPMDAO : BaseClass
    {
        public rsBPM_Query Query(BPM_Query_item data)
        {
            //if (string.IsNullOrEmpty(data.RPUSER2))
            //    return new rsBPM_Query { result = CommDAO.getRsItem1("無輸入 歸檔人代號") };

            string no = "";
            string no2 = "";
            if (!string.IsNullOrEmpty(data.RPVR01) && data.RPVR01.Length >= 2)
            {
                no = $"AND substring(B.RPVR01,1,2) = '{data.RPVR01.Substring(0, 2)}'";

                no2 = $"AND substring(RPVINV,1,2) = '{data.RPVR01.Substring(0, 2)}'";
            }

            string user = "";
            if (!string.IsNullOrEmpty(data.RPUSER2))
                user = " AND RPUSER2 =  '{data.RPUSER2}' ";

            string sql = $@"
SELECT '預付' AS 資料別,
RPVINV 單據編號,
RPIVDA 申請日,
RPGDC6  作帳日,
RPUSER 申請人代碼,
ISNULL(EMPL_NAME,'') 申請人名稱,
RPACTNM 摘要,
RPAG 請款金額
FROM EP2
LEFT JOIN ACC_EMPLOYEE ON RPUSER = EMPL_ID
WHERE ISNULL(TRFLG,'') <> 'Y' and RPGDC6='{data.RPGDC6}' and RPCO='{data.RPCO}' 
{user}
{no2}
UNION ALL
SELECT '費用請款' AS 資料別,
B.RPVR01 單據編號,
MAX(B.RPGDC5)申請日,
MAX(B.RPGDC6)  作帳日,
MAX(B.RPUSER) 申請人代碼,
MAX(ISNULL(EMPL_NAME, '')) 申請人名稱,
MAX(RPACTNM) 摘要,
SUM(RPAG) 請款金額
FROM EPD A, EPM B
LEFT JOIN ACC_EMPLOYEE ON B.RPUSER = EMPL_ID
WHERE ISNULL(TRFLG, '') <> 'Y'
{no}
AND A.RPUSER = B.RPUSER
AND A.RPVR01 = B.RPVR01 and B.RPGDC6='{data.RPGDC6}' and RPCO='{data.RPCO}' 
{user}
GROUP BY B.RPVR01, B.RPUSER";

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsBPM_Query
            {
                result = CommDAO.getRsItem(),
                data = dt.ToList<BPM_Query>()
            };
        }
    }
}