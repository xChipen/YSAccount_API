using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccPrepayAccountDController : ApiController
    {
        AccPrepayAccountDDAO dao = new AccPrepayAccountDDAO();

        [Route("ACC_PREPAY_ACCOUNT_D/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccPrepayAccountD_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_D/Add");

            rs rs = dao.Add(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_D/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccPrepayAccountD_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_D/Update");

            rs rs = dao.Update(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_D/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccPrepayAccountD_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_D/Delete");

            rs rs = dao.Delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_D/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPrepayAccountD_qry data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_D /Query");

            rsAccPrepayAccountD_qry rs = dao.Query(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}