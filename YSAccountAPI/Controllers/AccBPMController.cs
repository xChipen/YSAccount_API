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
    public class AccBPMController : ApiController
    {
        AccBPMDAO dao = new AccBPMDAO();

        [Route("ACC_BPM/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Add(BPM_Query_in data)
        {
            Log.Info("API : ACC_BPM/Query");

            rsBPM_Query rs = dao.Query(data.data);
            return CommDAO.getResponse(Request, rs);
        }
    }
}