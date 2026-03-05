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
    public class AccAllowanceM_BatchController : ApiController
    {
        AccAllowanceM_BatchDAO dao = new AccAllowanceM_BatchDAO();

        [Route("ACC_ALLOWANCE_M_BATCH/AUD")]
        [HttpPost, ActionName("AUD")]
        public HttpResponseMessage AUD(AccAllowanceM_Batch_ins data)
        {
            Log.Info("API : ACC_ALLOWANCE_M_BATCH/AUD");

            rsAccAllowanceM_Batch rs = dao.AUD(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ALLOWANCE_M_BATCH/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccAllowanceM_Batch_qry data)
        {
            Log.Info("API : ACC_ALLOWANCE_M_BATCH/Query");

            rsAccAllowanceM_Batch_qry rs = dao.Query(data);
            return CommDAO.getResponse(Request, rs);
        }

    }
}