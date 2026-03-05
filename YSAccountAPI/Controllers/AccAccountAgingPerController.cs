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
    public class AccAccountAgingPerController : ApiController
    {
        AccAccountAgingPerDAO dao = new AccAccountAgingPerDAO();

        [Route("ACC_ACCOUNT_AGING_PER/AUD")]
        [HttpPost, ActionName("AUD")]
        public HttpResponseMessage Add(AccAccountAgingPer_Batch_ins data)
        {
            Log.Info("API : ACC_ACCOUNT_AGING_PER/AUD");

            rsAccAccountAgingPer_Batch rs = dao.AUD(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACCOUNT_AGING_PER/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccAccountAgingPer_qry data)
        {
            Log.Info("API : ACC_ACCOUNT_AGING_PER/Delete");

            rsAccAccountAgingPer_qry rs = dao.Query(data);
            return CommDAO.getResponse(Request, rs);
        }

        // ACF010M_APER_KIND
        [Route("ACC_ACCOUNT_AGING_PER/ACF010M_APER_KIND")]
        [HttpPost, ActionName("ACF010M_APER_KIND")]
        public HttpResponseMessage ACF010M_APER_KIND()
        {
            Log.Info("API : ACC_ACCOUNT_AGING_PER/ACF010M_APER_KIND");

            rsACF010M_rs rs = dao.ACF010M_APER_KIND();
            return CommDAO.getResponse(Request, rs);
        }

    }
}