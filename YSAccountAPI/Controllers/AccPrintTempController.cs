using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using Helpers;
using DAO;

namespace YSAccountAPI.Controllers
{
    public class AccPrintTempController : ApiController
    {
        AccPrintTempDAO dao = new AccPrintTempDAO();

        [Route("ACC_PRINT_TEMP/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccPrintTemp_ins data)
        {
            Log.Info("API : ACC_PRINT_TEMP/Add");

            rs rs = dao.Insert(data);

            return CommDAO.getResponse(Request, rs);
        }

        // Query PRTM_USERID
        [Route("ACC_PRINT_TEMP/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPrintTemp_qry data)
        {
            Log.Info("API : ACC_PRINT_TEMP/Add");

            rs_AccPrintTemp rs = dao.query(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}