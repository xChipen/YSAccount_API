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
    public class AccPaCheckController : ApiController
    {
        AccPaCheckDAO dao = new AccPaCheckDAO();

        [Route("ACC_PA_CHECK/AUD")]
        [HttpPost, ActionName("AUD")]
        public HttpResponseMessage AUD(AccPaCheck_ins data)
        {
            Log.Info("API : ACC_PA_CHECK/AUD");

            rsAUD rs = dao.addAndUpdate(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PA_CHECK/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPaCheck_qry data)
        {
            Log.Info("API : ACC_PA_CHECK/Query");

            rsAccPaCheck_qry rs = dao.Query(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PA_CHECK/ReCreateData")]
        [HttpPost, ActionName("ReCreateData")]
        public HttpResponseMessage ReCreateData(AccPaCheck_ReCreate data)
        {
            Log.Info("API : ACC_PA_CHECK/ReCreateData");

            rs rs = dao.ReCreateData(data.baseRequest.companyId, data.data.PACK_NO);
            return CommDAO.getResponse(Request, rs);
        }

    }
}