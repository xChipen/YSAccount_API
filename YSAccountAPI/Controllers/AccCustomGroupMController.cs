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
    public class AccCustomGroupMController : ApiController
    {
        AccCustomGroupMDAO dao = new AccCustomGroupMDAO();

        [Route("ACC_CUSTOM_GROUP_M/Query")]
        [HttpPost]
        public HttpResponseMessage Query(AccCustomGroupM_ins data)
        {
            Log.Info("API : ACC_CUSTOM_GROUP_M/Query");

            rsAccCustomGroupM_qry rs = dao.Query2(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CUSTOM_GROUP_M/Query3")]
        [HttpPost]
        public HttpResponseMessage Query3(AccCustomGroupM_ins data)
        {
            Log.Info("API : ACC_CUSTOM_GROUP_M/Query2");

            rsAccCustomGroupM_qry3 rs = dao.Query3(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CUSTOM_GROUP_M/AUD")]
        [HttpPost]
        public HttpResponseMessage AUD(AccCustomGroupM_Batch data)
        {
            Log.Info("API : ACC_CUSTOM_GROUP_M/AUD");

            rs rs = dao.AUD(data.data, data.baseRequest.employeeNo, data.baseRequest.name);

            return CommDAO.getResponse(Request, rs);
        }


    }
}