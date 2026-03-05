using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helpers;
using System.Data;

namespace DAO
{
    public class AccMonthSaleCostDAO:BaseClass
    {
        public rsAccMonthSaleCost_qry query(AccMonthSaleCost data)
        {
            string sql = "SELECT * from ACC_MONTH_SALE_COST where 1 =1 ";
            sql += CommDAO.sql_ep(data.MSCO_COMPID, "MSCO_COMPID");
            sql += CommDAO.sql_ep(data.MSCO_VOUNO, "MSCO_VOUNO");

            DataTable dt = comm.DB.RunSQL(sql);
            List<AccMonthSaleCost> rs = dt.ToList<AccMonthSaleCost>();

            return new rsAccMonthSaleCost_qry
            {
                result = CommDAO.getRsItem(),
                data = rs
            };
        }
    }
}