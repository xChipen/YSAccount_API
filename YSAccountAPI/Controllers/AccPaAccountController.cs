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
    public class AccPaAccountController : ApiController
    {
        AccPaAccountDAO dao = new AccPaAccountDAO();

        [Route("ACC_PA_ACCOUNT/AUD")]
        [HttpPost, ActionName("AUD")]
        public HttpResponseMessage AUD(AccPaAccount_ins data)
        {
            Log.Info("API : ACC_PA_ACCOUNT/AUD");

            rsAUD rs = dao.addAndUpdate(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PA_ACCOUNT/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPaAccount_qry data)
        {
            Log.Info("API : ACC_PA_ACCOUNT/Query");

            rsAccPaAccount_qry rs = dao.Query(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PA_ACCOUNT/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(AccPaAccount_query2_in data)
        {
            Log.Info("API : ACC_PA_ACCOUNT/Query2");

            rsAccPaAccount_query2_rs rs = dao.Query2(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PA_ACCOUNT/Query3")]
        [HttpPost, ActionName("Query3")]
        public HttpResponseMessage Query3(AccPaAccount_query2_in data)
        {
            Log.Info("API : ACC_PA_ACCOUNT/Query3");

            rsAccPaAccount_qry3_rs rs = dao.Query3(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PA_ACCOUNT/Query_ACD120Q")]
        [HttpPost, ActionName("Query_ACD120Q")]
        public HttpResponseMessage Query_ACD120Q(AccPaAccount_QueryForm_ins data)
        {
            Log.Info("API : ACC_PA_ACCOUNT/Query_ACD120Q");

            AccPaAccount_QueryForm_qry rs = dao.Query_ACD120Q(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        // Query_ACD040M
        [Route("ACC_PA_ACCOUNT/Query_ACD040M")]
        [HttpPost, ActionName("Query_ACD040M")]
        public HttpResponseMessage Query_ACD040M(AccPayM_ACD040M_qry_ins data)
        {
            Log.Info("API : ACC_PA_ACCOUNT/Query_ACD040M");

            AccPaAccount_ACD040M_qry_ins rs = dao.Query_ACD040M(data.data);
            return CommDAO.getResponse(Request, rs);
        }


    }
}