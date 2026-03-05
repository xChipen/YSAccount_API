using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccAccCodeController : ApiController
    {
        AccAccCodeDAO dao = new AccAccCodeDAO();

        [Route("ACC_ACC_CODE/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccAccCodeAdd data)
        {
            Log.Info("API : ACC_ACC_CODE/Add");

            rs_AccAccCodeAdd rs = dao.addAccAccCode(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACC_CODE/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccAccCodeAdd data)
        {
            Log.Info("API : ACC_ACC_CODE/Update");

            rs rs = dao.updateAccAccCode(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACC_CODE/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccAccCodeDelete data)
        {
            Log.Info("API : ACC_ACC_CODE/Delete");

            rs rs = dao.deleteAccAccCode(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACC_CODE/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccAccCodeQuery data)
        {
            Log.Info("API : ACC_ACC_CODE/Query");

            //Log.Info("dataIn string : " + s);
            rs_AccAccCodeQuery rs = dao.queryAccAccCode(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}