using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccDailySaleController : ApiController
    {
        AccDailySaleDAO dao = new AccDailySaleDAO();

        [Route("ACC_DAILY_SALE/query")]
        [HttpPost, ActionName("query")]
        public HttpResponseMessage query(AccDailySale_qry data)
        {
            Log.Info("API : ACC_DAILY_SALE/query");

            rsAccDailySale_qry rs = dao.query(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        // 20260402 銷貨收入轉傳票處理
        [Route("ACC_DAILY_SALE/query2")]
        [HttpPost, ActionName("query2")]
        public HttpResponseMessage query2(AccDailySale_qry2 data)
        {
            Log.Info("API : ACC_DAILY_SALE/query2");

            rsAccDailySale_qry2 rs = dao.query2(data.data.Kind, data.data.DSAL_DATE);
            return CommDAO.getResponse(Request, rs);
        }



    }
}