using System.Net.Http;
using System.Web.Http;
using Models;
using Helpers;
using DAO;

namespace YSAccountAPI.Controllers
{
    public class AccJouralMController : ApiController
    {
        AccJouralMDAO dao = new AccJouralMDAO();

        [Route("ACC_JOURAL_M/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccJouralM_ins data)
        {
            Log.Info("API : ACC_JOURAL_M/Add");

            rs rs = dao.addAccJouralM(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_JOURAL_M/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccJouralM_ins data)
        {
            Log.Info("API : ACC_JOURAL_M/Update");

            rs rs = dao.updateAccJouralM(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_JOURAL_M/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccJouralM_del data)
        {
            Log.Info("API : ACC_JOURAL_M/Delete");

            rs rs = dao.deleteAccJouralM(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_JOURAL_M/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccJouralM_del data)
        {
            Log.Info("API : ACC_JOURAL_M/Query");

            AccJouralM_query rs = dao.queryAccJouralM(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}