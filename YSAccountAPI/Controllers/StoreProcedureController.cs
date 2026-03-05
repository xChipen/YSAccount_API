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
    public class StoreProcedureController : ApiController
    {
        CommStoreProcedure dao = new CommStoreProcedure();

        [Route("SP/ACB050B")]
        [HttpPost, ActionName("ACB050B")]
        public HttpResponseMessage ACB050B(ACB050B_ins data)
        {
            Log.Info("API : SP/ACB050B");

            rs rs = dao.runACB050B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB060B")]
        [HttpPost, ActionName("ACB060B")]
        public HttpResponseMessage ACB060B(ACB060B_ins data)
        {
            Log.Info("API : SP/ACB060B");

            ACB060B_rs rs = dao.runACB060B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB070B")]
        [HttpPost, ActionName("ACB070B")]
        public HttpResponseMessage ACB070B(ACB070B_ins data)
        {
            Log.Info("API : SP/ACB070B  ");

            rs rs = dao.runACB070B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB110B")]
        [HttpPost, ActionName("ACB110B")]
        public HttpResponseMessage ACB110B(ACB110B_ins data)
        {
            Log.Info("API : SP/ACB110B");

            ACB110B_rs rs = dao.runACB110B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB120B")]
        [HttpPost, ActionName("ACB120B")]
        public HttpResponseMessage ACB120B(ACB120B_ins data)
        {
            Log.Info("API : SP/ACB120B");

            rs rs = dao.runACB120B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB140B")]
        [HttpPost, ActionName("ACB140B")]
        public HttpResponseMessage ACB140B(ACB120B_ins data)
        {
            Log.Info("API : SP/ACB140B");

            rs rs = dao.runACB140B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACG070B")]
        [HttpPost, ActionName("ACG070B")]
        public HttpResponseMessage ACG070B(ACG070B_ins data)
        {
            Log.Info("API : SP/ACG070B");

            ACB110B_rs rs = dao.runACG070B(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACD040M1")]
        [HttpPost, ActionName("ACD040M1")]
        public HttpResponseMessage ACD040M1(ACD040M2_ins data)
        {
            Log.Info("API : SP/ACD040M1");

            rs rs = dao.runACD040M2(data, "ACD040M1");
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACD040M2")]
        [HttpPost, ActionName("ACD040M2")]
        public HttpResponseMessage ACD040M2(ACD040M2_ins data)
        {
            Log.Info("API : SP/ACD040M2");

            rs rs = dao.runACD040M2(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACD080B")]
        [HttpPost, ActionName("ACD080B")]
        public HttpResponseMessage ACD080B(ACD080B_ins data)
        {
            Log.Info("API : SP/ACD080B");

            ACB110B_rs rs = dao.runACD080B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACE050B")]
        [HttpPost, ActionName("ACE050B")]
        public HttpResponseMessage ACE050B(ACE050B_ins data)
        {
            Log.Info("API : SP/ACE050B");

            ACB110B_rs rs = dao.runACE050B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACH120B")]
        [HttpPost, ActionName("ACH120B")]
        public HttpResponseMessage ACH120B(ACH120B_ins data)
        {
            Log.Info("API : SP/ACH120B");

            rs rs = dao.runACH120B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACK030B")]
        [HttpPost, ActionName("ACK030B")]
        public HttpResponseMessage ACK030B(ACK030B_ins data)
        {
            Log.Info("API : SP/ACK030B");

            ACB110B_rs rs = dao.runACK030B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACK060B")]
        [HttpPost, ActionName("ACK060B")]
        public HttpResponseMessage ACK060B(ACK060B_ins data)
        {
            Log.Info("API : SP/ACK060B");

            ACB110B_rs rs = dao.runACK060B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACK070B")]
        [HttpPost, ActionName("ACK070B")]
        public HttpResponseMessage ACK070B(ACK060B_ins data)
        {
            Log.Info("API : SP/ACK070B");

            ACB110B_rs rs = dao.runACK070B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACJ020B")]
        [HttpPost, ActionName("ACJ020B")]
        public HttpResponseMessage ACJ020B(ACH120B_ins data)
        {
            Log.Info("API : SP/ACJ020B");

            rs rs = dao.runACH120B(data, "ACJ020B");
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACJ040B")]
        [HttpPost, ActionName("ACJ040B")]
        public HttpResponseMessage ACJ040B(ACH120B_ins data)
        {
            Log.Info("API : SP/ACJ020B");

            rs rs = dao.runACJ040B(data, "ACJ040B");
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACF050B")]
        [HttpPost, ActionName("ACF050B")]
        public HttpResponseMessage ACF050B(ACK030B_ins data)
        {
            Log.Info("API : SP/ACF050B");

            ACB110B_rs rs = dao.runACF050B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB160B1")]
        [HttpPost, ActionName("ACB160B1")]
        public HttpResponseMessage ACB160B1(ACB160B1_ins data)
        {
            Log.Info("API : SP/ACB160B1");

            ACB110B_rs rs = dao.runACB160B1(data);
            return CommDAO.getResponse(Request, rs);
        }
        //[Route("SP/ACB180B")]
        //[HttpPost, ActionName("ACB180B")]
        //public HttpResponseMessage ACB180B(ACB160B1_ins data)
        //{
        //    Log.Info("API : SP/ACB180B");

        //    ACB110B_rs rs = dao.runACB160B1(data, "ACB180B"); // 20240823 StoreProcedure 名稱待確認
        //    return CommDAO.getResponse(Request, rs);
        //}

        [Route("SP/ACB160B2")]
        [HttpPost, ActionName("ACB160B2")]
        public HttpResponseMessage ACB160B2(ACB160B1_ins data)
        {
            Log.Info("API : SP/ACB160B2");

            rs rs = dao.runACB160B2(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB160B_BATCH")]
        [HttpPost, ActionName("ACB160B_BATCH")]
        public HttpResponseMessage ACB160B_BATCH(ACB160B_BATCH_ins data)
        {
            Log.Info("API : SP/ACB160B_BATCH");

            rs rs = dao.runACB160B_BATCH(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB170B_BATCH")]
        [HttpPost, ActionName("ACB170B_BATCH")]
        public HttpResponseMessage ACB170B_BATCH(ACB170B_BATCH_ins data)
        {
            Log.Info("API : SP/ACB170B_BATCH");

            rs rs = dao.runACB170B_BATCH(data);
            return CommDAO.getResponse(Request, rs);
        }
        [Route("SP/ACB170B1")]
        [HttpPost, ActionName("ACB170B1")]
        public HttpResponseMessage ACB170B1(ACB170B_BATCH_ins data)
        {
            Log.Info("API : SP/ACB170B1");

            ACB110B_rs rs = dao.runACB170B1(data);
            return CommDAO.getResponse(Request, rs);
        }
        [Route("SP/ACB170B2")]
        [HttpPost, ActionName("ACB170B2")]
        public HttpResponseMessage ACB170B2(ACB170B_BATCH_ins data)
        {
            Log.Info("API : SP/ACB170B2");

            rs rs = dao.runACB170B_BATCH(data, "ACB170B2");
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB010M_REVERSAL")]
        [HttpPost, ActionName("ACB010M_REVERSAL")]
        public HttpResponseMessage ACB010M_REVERSAL(ACB010M_REVERSAL_in data)
        {
            Log.Info("API : SP/ACB010M_REVERSAL");

            bool bOK = dao.runACB010M_REVERSAL(data.data.VOMD_COMPID, data.data.VOMD_NO, data.baseRequest.employeeNo, data.baseRequest.name);

            return CommDAO.getResponse(Request, bOK ? CommDAO.getRsItem() : CommDAO.getRsItem1());
        }

        [Route("SP/ACD010B1")]
        [HttpPost, ActionName("ACD010B1")]
        public HttpResponseMessage ACD010B1(ACD010B1_ins data)
        {
            Log.Info("API : SP/ACD010B1");

            ACB110B_rs rs = dao.runACD010B1(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACB180B")]
        [HttpPost, ActionName("ACB180B")]
        public HttpResponseMessage ACB180B(ACB180B_in data)
        {
            Log.Info("API : SP/ACB180B");

            ACB110B_rs rs = dao.runACB180B(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACC160B1")]
        [HttpPost, ActionName("ACC160B1")]
        public HttpResponseMessage ACC160B1(ACH120B_ins data)
        {
            Log.Info("API : SP/ACC160B1");

            rsACB160B1_result rs = dao.runACC160B1(data, "ACC160B1");
            return CommDAO.getResponse(Request, rs);
        }
        [Route("SP/ACC160B2")]
        [HttpPost, ActionName("ACC160B2")]
        public HttpResponseMessage ACC160B2(ACH120B_ins data)
        {
            Log.Info("API : SP/ACC160B2");

            rsACB160B2_result rs = dao.runACC160B2(data, "ACC160B2");
            return CommDAO.getResponse(Request, rs);
        }
        [Route("SP/ACC160B3")]
        [HttpPost, ActionName("ACC160B3")]
        public HttpResponseMessage ACC160B3(ACH120B_ins data)
        {
            Log.Info("API : SP/ACC160B3");

            rsACB160B2_result rs = dao.runACC160B2(data, "ACC160B3");
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACD060B")]
        [HttpPost, ActionName("ACD060B")]
        public HttpResponseMessage ACD060B(ACD060B_ins data)
        {
            Log.Info("API : SP/ACD060B");

            ACD060B_rs rs = dao.runACD060B(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACE030B")]
        [HttpPost, ActionName("ACE030B")]
        public HttpResponseMessage ACE030B(ACE030B_ins data)
        {
            Log.Info("API : SP/ACE030B");

            rs rs = dao.runACE030B(data);
            return CommDAO.getResponse(Request, rs);
        }

        // 20251029
        [Route("SP/ACB190B")]
        [HttpPost, ActionName("ACB190B")]
        public HttpResponseMessage ACB190B(ACB190B_ins data)
        {
            Log.Info("API : SP/ACB190B");

            rs rs = dao.runACB190B(data.data, data.baseRequest.companyId, data.baseRequest.name);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACD030B1")]
        [HttpPost, ActionName("ACD030B1")]
        public HttpResponseMessage ACD030B1(ACK030B_ins data)
        {
            Log.Info("API : SP/ACD030B1");

            ACB140B_rs rs = dao._runACD030B(data.data, data.baseRequest.employeeNo, data.baseRequest.name, "ACD030B1");

            return CommDAO.getResponse(Request, rs);
        }
        [Route("SP/ACD030B2")]
        [HttpPost, ActionName("ACD030B2")]
        public HttpResponseMessage ACD030B2(ACK030B_ins data)
        {
            Log.Info("API : SP/ACD030B2");

            ACB140B_rs rs = dao._runACD030B(data.data, data.baseRequest.companyId, data.baseRequest.name, "ACD030B2");

            return CommDAO.getResponse(Request, rs);
        }
        [Route("SP/ACD030B3")]
        [HttpPost, ActionName("ACD030B3")]
        public HttpResponseMessage ACD030B3(ACK030B_ins data)
        {
            Log.Info("API : SP/ACD030B3");

            ACB140B_rs rs = dao._runACD030B(data.data, data.baseRequest.companyId, data.baseRequest.name, "ACD030B3");

            return CommDAO.getResponse(Request, rs);
        }

        [Route("SP/ACD030B4")]
        [HttpPost, ActionName("ACD030B4")]
        public HttpResponseMessage ACD030B4(ACK030B_ins data)
        {
            Log.Info("API : SP/ACD030B4");

            ACB140B_rs rs = dao._runACD030B(data.data, data.baseRequest.companyId, data.baseRequest.name, "ACD030B4");

            return CommDAO.getResponse(Request, rs);
        }

    }
}