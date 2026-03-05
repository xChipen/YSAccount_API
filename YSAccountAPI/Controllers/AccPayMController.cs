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
    public class AccPayMController : ApiController
    {
        AccPayMDAO dao = new AccPayMDAO();

        [Route("ACC_PAY_M/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPayM_ACD040M_qry_ins data)
        {
            Log.Info("API : ACC_PAY_M/Query");

            rsAccPayM_ACD040M_1_qry rs = dao.queryACD040M_Query1(data.data);
            return CommDAO.getResponse(Request, rs);
        }
        [Route("ACC_PAY_M/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(AccPayM_ACD040M_qry_ins data)
        {
            Log.Info("API : ACC_PAY_M/Query2");

            rsAccPayM_ACD040M_1_qry rs = dao.queryACD040M_Query2(data.data);
            return CommDAO.getResponse(Request, rs);
        }
        [Route("ACC_PAY_M/Query3")]
        [HttpPost, ActionName("Query3")]
        public HttpResponseMessage Query3(AccPayM_ACD040M_qry_ins data)
        {
            Log.Info("API : ACC_PAY_M/Query3");

            rsAccPayM_ACD040M_qry rs = dao.queryACD040M_Query3(data.data);
            return CommDAO.getResponse(Request, rs);
        }
        [Route("ACC_PAY_M/Query4")]
        [HttpPost, ActionName("Query4")]
        public HttpResponseMessage Query4(AccPayM_ACD040M_qry_ins data)
        {
            Log.Info("API : ACC_PAY_M/Query4");

            rsAccPayM_ACD040M_qry rs = dao.queryACD040M_Query4(data.data);
            return CommDAO.getResponse(Request, rs);
        }
        [Route("ACC_PAY_M/Query5")]
        [HttpPost, ActionName("Query5")]
        public HttpResponseMessage Query5(AccPayM_ACD040M_qry_ins data)
        {
            Log.Info("API : ACC_PAY_M/Query5");

            rsAccPayM_ACD040M_qry rs = dao.queryACD040M_Query5(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PAY_M/ACD120Q_Query")]
        [HttpPost, ActionName("ACD120Q_Query")]
        public HttpResponseMessage ACD120Q_Query(ACD120Q_qry_ins data)
        {
            Log.Info("API : ACC_PAY_M/ACD120Q_Query");

            rsACD120Q_rs rs = dao.ACD120Q_Query(data.data);
            return CommDAO.getResponse(Request, rs);
        }



    }
}