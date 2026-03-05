using DAO;
using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Controllers
{
    public class ACC_EXRATEController : ApiController
    {
        AccExrateDAO dao = new AccExrateDAO();

        [Route("ACC_EXRATE/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccExrateAdd2 model)
        {
            Log.Info("API : ACC_EXRATE/Add");

            rs rs = dao.add_Batch(model);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_EXRATE/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccExrateAdd model)
        {
            Log.Info("API : ACC_EXRATE/Update");

            rs rs = dao.update(model);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_EXRATE/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccExrateAdd model)
        {
            Log.Info("API : ACC_EXRATE/Delete");

            rs rs = dao.delete(model);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_EXRATE/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccExrateAdd data)
        {
            Log.Info("API : ACC_EXRATE/Query");

            rs_AccExrateQuery rs = dao.select_data(data);
            return CommDAO.getResponse(Request, rs);
        }
    }
}
