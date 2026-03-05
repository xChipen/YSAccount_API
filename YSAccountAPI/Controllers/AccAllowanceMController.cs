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
    public class AccAllowanceMController : ApiController
    {
        AccAllowanceMDAO dao = new AccAllowanceMDAO();

        [Route("ACC_ALLOWANCE_M/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(AccAllowanceM_qry2 data)
        {
            Log.Info("API : ACC_ALLOWANCE_M/Query2");

            rsAccAllowanceM_qry2 rs = dao.rsQuery2(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ALLOWANCE_M/Update1")]
        [HttpPost, ActionName("Update1")]
        public HttpResponseMessage Update1(rsAccAllowanceM2_upd data)
        {
            Log.Info("API : ACC_ALLOWANCE_M/Update1");

            rs rs = dao.update1(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }
        [Route("ACC_ALLOWANCE_M/Update2")]
        [HttpPost, ActionName("Update2")]
        public HttpResponseMessage Update2(rsAccAllowanceM2_upd data)
        {
            Log.Info("API : ACC_ALLOWANCE_M/Update2");

            rs rs = dao.update2(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

    }
}