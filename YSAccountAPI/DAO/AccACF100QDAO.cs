using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccACF100QDAO:BaseClass
    {


        // ACF100Q_應收帳款明細查詢
        public rsAccACF100Q Query_ACD120Q(AccACF100Q_item_qry data)
        {
            DataTable dt = _Query_AccACF100Q(data);

            return new rsAccACF100Q
            {
                result = CommDAO.getRsItem(),
                data = dt.ToList<rsAccACF100Q_qry>()
            };
        }

        public DataTable _Query_AccACF100Q(AccACF100Q_item_qry data)
        {
            switch (data.QueryType)
            {
                case 1: return Query_AccACF100Q1(data);
                case 2: return Query_AccACF100Q2(data);
                case 3: return Query_AccACF100Q3(data);
                default: return null;
            }
        }

        // 1: 未收款
        public DataTable Query_AccACF100Q1(AccACF100Q_item_qry data)
        {
            string sql = $@"SELECT
a.REAC_INV_DATE as INV_DATE,
a.REAC_INVNO as INV_NO,
b.TRAN_NAME,
a.REAC_VOUNO  as VOUNO,
a.REAC_NT_TOT_AMT,
a.REAC_NT_TOT_AMT-a.REAC_NT_BAL as REC_AMT,
a.REAC_REC_DATE as REC_DATE,
a.REAC_CUSTID,
'' as RECW_NO
from ACC_RE_ACCOUNT a 
left join vw_TRAIN b on a.REAC_COMPID=b.TRAN_COMPID and a.REAC_CUSTID=b.TRAN_ID
WHERE 1=1
";
            sql += " AND a.REAC_NT_BAL<>0";

            sql += CommDAO.sql_ep(data.COMPID, "a.REAC_COMPID");
            sql += CommDAO.sql_ep(data.CUSTID, "a.REAC_CUSTID");
            sql += CommDAO.sql_like(data.CUSTID_NAME, "b.TRAN_NAME");

            sql += CommDAO.sql_ep_date_between(data.INV_DATE, data.INV_DATE_E, "a.REAC_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.REC_DATE, data.REC_DATE_E, "a.REAC_DUE_DATE");

            return comm.DB.RunSQL(sql);
        }
        // 2: 已收款
        public DataTable Query_AccACF100Q2(AccACF100Q_item_qry data)
        {
            string sql = $@"SELECT
b.RECW_INV_DATE as INV_DATE,
b.RECW_INVNO as INV_NO,
d.TRAN_NAME,
a.RECM_VOUNO as VOUNO,
c.REAC_NT_TOT_AMT,
b.RECW_NT_AMT as REC_AMT,
a.RECM_DATE as REC_DATE,
c.REAC_CUSTID,
b.RECW_NO
from ACC_RECEIVE_M a 
left join ACC_RECEIVE_WRITEOFF b on a.RECM_COMPID=b.RECW_COMPID and a.RECM_NO=b.RECW_NO
left join ACC_RE_ACCOUNT c on a.RECM_COMPID=c.REAC_COMPID and a.RECM_CUSTID=c.REAC_CUSTID and b.RECW_INVNO=c.REAC_INVNO
left join vw_TRAIN d on a.RECM_COMPID=d.TRAN_COMPID and c.REAC_CUSTID=d.TRAN_ID
WHERE 1=1 
";
            sql += " AND a.RECM_VALID ='Y'";

            sql += CommDAO.sql_ep(data.RTPW_NO, "b.RTPW_NO");   // 20241122 收款單號

            sql += CommDAO.sql_ep(data.COMPID, "a.RECM_COMPID");
            sql += CommDAO.sql_ep(data.CUSTID, "a.RECM_CUSTID");
            sql += CommDAO.sql_like(data.CUSTID_NAME, "d.TRAN_NAME");

            sql += CommDAO.sql_ep_date_between(data.INV_DATE, data.INV_DATE_E, "b.RECW_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.REC_DATE, data.REC_DATE_E, "a.RECM_DATE");

            return comm.DB.RunSQL(sql);
        }
        // 3: 全部
        public DataTable Query_AccACF100Q3(AccACF100Q_item_qry data)
        {
            string sql = $@"SELECT
a.REAC_INV_DATE as INV_DATE,
a.REAC_INVNO as INV_NO,
b.TRAN_NAME,
a.REAC_VOUNO  as VOUNO,
a.REAC_NT_TOT_AMT,
a.REAC_NT_TOT_AMT-a.REAC_NT_BAL as REC_AMT,
a.REAC_REC_DATE as REC_DATE,
a.REAC_CUSTID,
'' as RECW_NO
from ACC_RE_ACCOUNT a 
left join vw_TRAIN b on a.REAC_COMPID=b.TRAN_COMPID and a.REAC_CUSTID=b.TRAN_ID
WHERE 1=1 ";

            sql += CommDAO.sql_ep(data.COMPID, "a.REAC_COMPID");
            sql += CommDAO.sql_ep(data.CUSTID, "a.REAC_CUSTID");
            sql += CommDAO.sql_like(data.CUSTID_NAME, "b.TRAN_NAME");

            sql += CommDAO.sql_ep_date_between(data.INV_DATE, data.INV_DATE_E, "a.REAC_INV_DATE");
            sql += CommDAO.sql_ep_date_between(data.REC_DATE, data.REC_DATE_E, "a.REAC_DUE_DATE");

            return comm.DB.RunSQL(sql);
        }



    }
}