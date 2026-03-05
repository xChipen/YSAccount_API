using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccPrepayAccountShareController : ApiController
    {
        AccPrepayAccountShareDAO dao = new AccPrepayAccountShareDAO();

        [Route("ACC_PREPAY_ACCOUNT_SHARE/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccPrepayAccountShare_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_SHARE /Add");

            rs rs = dao.Add(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_SHARE/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccPrepayAccountShare_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_SHARE /Update");

            rs rs = dao.Update(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_SHARE/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccPrepayAccountShare_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_SHARE /Delete");

            rs rs = dao.Delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_SHARE/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPrepayAccountShare_qry data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_SHARE /Query");

            rsAccPrepayAccountShare_qry rs = dao.Query(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}