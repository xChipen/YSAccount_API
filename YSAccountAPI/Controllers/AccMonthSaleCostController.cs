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
    public class AccMonthSaleCostController : ApiController
    {
        AccMonthSaleCostDAO dao = new AccMonthSaleCostDAO();

        [Route("ACC_MONTH_SALE_COST/query")]
        [HttpPost, ActionName("query")]
        public HttpResponseMessage query(AccMonthSaleCost_qry data)
        {
            Log.Info("API : ACC_MONTH_SALE_COST/query");

            rsAccMonthSaleCost_qry rs = dao.query(data.data);
            return CommDAO.getResponse(Request, rs);
        }
    }
}