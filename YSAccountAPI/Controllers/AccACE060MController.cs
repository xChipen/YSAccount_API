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
    public class AccACE060MController : ApiController
    {

        AccACE060MDAO dao = new AccACE060MDAO();

        [Route("ACE060M/Insert")]
        [HttpPost, ActionName("ACE060M")]
        public HttpResponseMessage Insert(AccACE060M_ins data)
        {
            Log.Info("API : ACE060M/Insert");

            rs rs = dao.Insert(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }
    }
}