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
    public class AccOtherAccountController : ApiController
    {
        AccOtherAccountDAO dao = new AccOtherAccountDAO();

        [Route("ACC_OTHER_ACCOUNT/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccOtherAccount_qry data)
        {
            Log.Info("API : ACC_OTHER_ACCOUNT/Query");

            rsAccOtherAccount_qry rs = dao.Query(data.data);
            return CommDAO.getResponse(Request, rs);
        }


    }
}