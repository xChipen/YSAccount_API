using DAO;
using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YSAccountAPI.Controllers
{
    public class AccQueryController : ApiController
    {
        [Route("ACC_QUERY/ACD030B")]
        [HttpPost, ActionName("ACD030B")]
        public HttpResponseMessage ACD030B(ACD030B_Query_in data)
        {
            Log.Info("API : SP/ACD030B");

            AccQueryDAO dao = new AccQueryDAO();

            rsACD030B_Query_result rs = dao._ACD030B_Query(data.data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_QUERY/ACC_PURCH_VOUNO_Query")]
        [HttpPost, ActionName("ACC_PURCH_VOUNO_Query")]
        public HttpResponseMessage ACC_PURCH_VOUNO_Query(ACC_PURCH_VOUNO_Query_in data)
        {
            Log.Info("API : SP/ACC_PURCH_VOUNO_Query");

            AccQueryDAO dao = new AccQueryDAO();

            rsACC_PURCH_VOUNO_result rs = dao.ACC_PURCH_VOUNO_Query(data.data);

            return CommDAO.getResponse(Request, rs);
        }



    }
}