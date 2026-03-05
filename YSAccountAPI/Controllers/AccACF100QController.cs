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
    public class AccACF100QController : ApiController
    {
        AccACF100QDAO dao = new AccACF100QDAO();

        [Route("ACC_ACF100Q/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccACF100Q_qry data)
        {
            Log.Info("API : ACC_ACF100Q/Query");

            rsAccACF100Q rs = dao.Query_ACD120Q(data.data);
            return CommDAO.getResponse(Request, rs);
        }
    }
}