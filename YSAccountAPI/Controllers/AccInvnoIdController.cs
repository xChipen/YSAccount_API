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
    public class AccInvnoIdController : ApiController
    {
        AccInvnoIdDAO dao = new AccInvnoIdDAO();

        [Route("ACC_INVNO_ID/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Add(ACC_INVNO_ID_qry data)
        {
            Log.Info("API : ACC_INVNO_ID/Query");

            rsACC_INVNO_ID rs = dao.Query(data.data);
            return CommDAO.getResponse(Request, rs);
        }
    }
}