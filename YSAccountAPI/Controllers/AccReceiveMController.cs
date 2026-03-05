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
    public class AccReceiveMController : ApiController
    {
        AccReceiveMDAO dao = new AccReceiveMDAO();

        [Route("ACC_RECEIVE_M/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccReceiveM_qry_ACF050B data)
        {
            Log.Info("API : ACC_RECEIVE_M/Query");

            rsAccReceiveM_qry_ACF050B rs = dao.Query_ACF050B(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        //ACF100Q_應收帳款明細查詢
        [Route("ACC_RECEIVE_M/ACF100Q_Query")]
        [HttpPost, ActionName("ACF100Q_Query")]
        public HttpResponseMessage ACF100Q_Query(ACF100Q_qry_ins data)
        {
            Log.Info("API : ACC_RECEIVE_M/ACF100Q_Query");

            rsACF100Q_rs rs = dao.ACF100Q_Query(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        //ACF030M
        [Route("ACC_RECEIVE_M/ACF030M_Query")]
        [HttpPost, ActionName("ACF030M_Query")]
        public HttpResponseMessage ACF030M_Query(AccReceiveM_ACF030M_ins data)
        {
            Log.Info("API : ACC_RECEIVE_M/ACF030M_Query");

            rsAccReceiveM_ACF030M rs = dao.ACF030M(data.data);
            return CommDAO.getResponse(Request, rs);
        }



    }
}