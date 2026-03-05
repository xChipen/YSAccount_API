using DAO;
using Helpers;
using System.Net.Http;
using Models;
using System.Web.Http;

namespace Controllers
{
    public class AccRemitController : ApiController
    {
        AccRemitDAO dao = new AccRemitDAO();

        [Route("ACC_REMIT_CONTROL/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccRemitAdd2 model)
        {
            Log.Info("API : ACC_REMIT_CONTROL/Add");

            rs rs = dao.add_Batch(model);
            
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_REMIT_CONTROL/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccRemitAdd model)
        {
            Log.Info("API : ACC_REMIT_CONTROL/Update");

            rs rs = dao.update(model);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_REMIT_CONTROL/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccRemitAdd model)
        {
            Log.Info("API : ACC_REMIT_CONTROL/Delete");

            rs rs = dao.delete(model);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_REMIT_CONTROL/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccRemitAdd data)
        {
            Log.Info("API : ACC_REMIT_CONTROL/Query");

            rs_AccRemitQuery rs = dao.select_data(data);
            return CommDAO.getResponse(Request, rs);
        }
    }
}