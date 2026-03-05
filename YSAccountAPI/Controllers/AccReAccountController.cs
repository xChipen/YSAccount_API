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
    public class AccReAccountController : ApiController
    {
        AccReAccountDAO dao = new AccReAccountDAO();

        [Route("ACC_RE_ACCOUNT/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccReAccount_item2_qry data)
        {
            Log.Info("API : ACC_RE_ACCOUNT/Query");

            rsAccReAccount_qry rs = dao.Query_ACF030M(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_RE_ACCOUNT/UpdateAmt")]
        [HttpPost, ActionName("UpdateAmt")]
        public HttpResponseMessage UpdateAmt(AccReAccount_update_ins data)
        {
            Log.Info("API : ACC_RE_ACCOUNT/UpdateAmt");

            rs rs = dao.update(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }
    }
}