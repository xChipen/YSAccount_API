using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;


namespace YSAccountAPI.Controllers
{
    public class AccVoumstDController : ApiController
    {
        AccVoumstDDAO dao = new AccVoumstDDAO();
        [Route("ACC_VOUMS_D/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccVoumstD_ins data)
        {
            Log.Info("API : ACC_VOUMS_D/Add");

            rs rs = dao.Add(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMS_D/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccVoumstD_ins data)
        {
            Log.Info("API : ACC_VOUMS_D/Update");

            rs rs = dao.Update(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMS_D/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccVoumstD_ins data)
        {
            Log.Info("API : ACC_VOUMS_D/Delete");

            rs rs = dao.Delete(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}