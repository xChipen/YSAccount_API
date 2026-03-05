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
    public class AccReceiveM_BatchController : ApiController
    {
        AccReceiveM_BatchDAO dao = new AccReceiveM_BatchDAO();

        [Route("ACC_RECEIVE_M_BATCH/AUD")]
        [HttpPost, ActionName("AUD")]
        public HttpResponseMessage AUD(AccReceiveM_Batch_ins data)
        {
            Log.Info("API : ACC_RECEIVE_M_BATCH/AUD");

            rsAccReceiveM_Batch_ins rs = dao.AUD(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_RECEIVE_M_BATCH/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccReceiveM_Batch_qry data)
        {
            Log.Info("API : ACC_RECEIVE_M_BATCH/Query");

            rsAccReceiveM_Batch_qry rs = dao.Query(data.data);
            return CommDAO.getResponse(Request, rs);
        }

    }
}