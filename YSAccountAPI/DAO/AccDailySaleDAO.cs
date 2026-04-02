using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccDailySaleDAO:BaseClass
    {
        public rsAccDailySale_qry query(AccDailySale data)
        {
            string sql = "SELECT * from ACC_DAILY_SALE where 1=1";
            sql += CommDAO.sql_ep(data.DSAL_COMPID, "DSAL_COMPID");
            sql += CommDAO.sql_ep(data.DSAL_VOUNO, "DSAL_VOUNO");

            DataTable dt = comm.DB.RunSQL(sql);
            List<AccDailySale> rs = dt.ToList<AccDailySale>();

            return new rsAccDailySale_qry {
                result = CommDAO.getRsItem(),
                data = rs
            };
        }

        //                                     yyyy/mm/dd
        public rsAccDailySale_qry2 query2(string Kind, string DSAL_DATE)
        {
            string sql = $@"Select DSAL_DEPTID, DEPT_NAME FROM ACC_DAILY_SALE
LEFT JOIN ACC_DEPT ON DSAL_DEPTID = DEPT_ID
where
  DSAL_KIND = '{Kind}' 
  AND DSAL_DATE = '{DSAL_DATE}'
  AND DSAL_VOUNO = ''
GROUP BY DSAL_DEPTID, DEPT_NAME";

            DataTable dt = comm.DB.RunSQL(sql);

            return new rsAccDailySale_qry2 {
                result = CommDAO.getRsItem(),
                data = dt.ToList<rsAccDailySale2>()
            };
        }










    }
}