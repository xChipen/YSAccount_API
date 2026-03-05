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
    public class AccMonthValuationController : ApiController
    {
        AccMonthValuationDAO dao = new AccMonthValuationDAO();

        [Route("ACC_MONTH_VALUATION/query_max")]
        [HttpPost, ActionName("query")]
        public HttpResponseMessage Add(AccMonthValuation_qry_max data)
        {
            Log.Info("API : ACC_MONTH_VALUATION/query_max");

            rsAccMonthValuation_query rs = dao.AccMonthValuation_query_max(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_MONTH_VALUATION/check")]
        [HttpPost, ActionName("check")]
        public HttpResponseMessage check(AccMonthValuation_qry_max data)
        {
            Log.Info("API : ACC_MONTH_VALUATION/check");

            rsAccMonthValuation_query rs = dao.AccMonthValuation_query_max(data);
            return CommDAO.getResponse(Request, rs);
        }

    }
}