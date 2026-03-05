using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Data;
using Helpers;

namespace DAO
{
    public class AccInvnoIdDAO:BaseClass
    {
        public rsACC_INVNO_ID Query(ACC_INVNO_ID data)
        {
            string sql = "Select * from ACC_INVNO_ID where 1=1";

            Dictionary<string, object> P = new Dictionary<string, object>();

            sql += BaseClass2.sql_ep_int(data.INID_YEAR, "INID_YEAR", ref P);
            sql += BaseClass2.sql_ep_int(data.INID_MONTH, "INID_MONTH", ref P);
            sql += BaseClass2.sql_ep(data.INID_FORMAT.ToString(), "INID_FORMAT", ref P);
            sql += BaseClass2.sql_ep(data.INID_ID.ToString(), "INID_ID", ref P);

            DataTable dt = comm.DB.RunSQL(sql, P);

            return new rsACC_INVNO_ID {
                result = CommDAO.getRsItem(),
                data = dt.ToList<ACC_INVNO_ID>()
            };
        }
    }
}