using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAO;
using Helpers;
using Models;

namespace YSAccountAPI.Controllers
{
    public class AccMonthAccountController : ApiController
    {
        AccMonthAccountDAO dao = new AccMonthAccountDAO();

        [Route("ACC_MONTH_ACCOUNT/query_max")]
        [HttpPost, ActionName("query_max")]
        public HttpResponseMessage Add(AccMonthAccount_query data)
        {
            Log.Info("API : ACC_MONTH_ACCOUNT/query_max");

            rs_AccMonthAccount_query rs = dao.check(data);
            return CommDAO.getResponse(Request, rs);
        }


    }
}