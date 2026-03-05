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
    public class AccJouralDController : ApiController
    {
        AccJouralDDAO dao = new AccJouralDDAO();

        [Route("ACC_JOURAL_D/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccJouralDAdd model)
        {
            Log.Info("API : AccJouralD/Add");

            rs rs = dao.add(model);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_JOURAL_D/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccJouralDAdd model)
        {
            Log.Info("API : AccJouralD/Update");

            rs rs = dao.update(model);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_JOURAL_D/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccJouralD_del model)
        {
            Log.Info("API : AccJouralD/Delete");

            rs rs = dao.delete(model);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_JOURAL_D/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccJouralD_ins data)
        {
            Log.Info("API : AccJouralD/Query");

            rs_AccJouralDQuery rs = dao.select_data(data);
            return CommDAO.getResponse(Request, rs);
        }
    }
}
