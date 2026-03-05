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
    public class AccCheckCounterController : ApiController
    {
        AccCheckCounterDAO dao = new AccCheckCounterDAO();

        [Route("ACC_CHECK_COUNTER/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccCheckCounter_ins data)
        {
            Log.Info("API : ACC_CHECK_COUNTER/Add");

            rs rs = dao.Add(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CHECK_COUNTER/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccCheckCounter_ins data)
        {
            Log.Info("API : ACC_CHECK_COUNTER/Update");

            rs rs = dao.Update(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CHECK_COUNTER/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccCheckCounter_ins data)
        {
            Log.Info("API : ACC_CHECK_COUNTER/Delete");

            rs rs = dao.Delete(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CHECK_COUNTER/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccCheckCounter_ins data)
        {
            Log.Info("API : ACC_CHECK_COUNTER/Query");

            rsAccCheckCounter_qry rs = dao.Query(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CHECK_COUNTER/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(BaseIn data)
        {
            Log.Info("API : ACC_CHECK_COUNTER/Query2");

            rsAccCheckCounter_qry rs = dao.Query2(data.baseRequest.companyId);
            return CommDAO.getResponse(Request, rs);
        }

        // ACE010M Query
        [Route("ACC_CHECK_COUNTER/ACE010M_Query")]
        [HttpPost, ActionName("ACE010M_Query")]
        public HttpResponseMessage ACE010M_Query(AccCheckCounter_ins data)
        {
            Log.Info("API : ACC_CHECK_COUNTER/ACE010M_Query");

            rsACE010M_qryAll rs = dao.ACE010M_qry(data.data);
            return CommDAO.getResponse(Request, rs);
        }


    }
}