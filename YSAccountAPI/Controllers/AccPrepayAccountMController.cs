using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccPrepayAccountMController : ApiController
    {
        AccPrepayAccountMDAO dao = new AccPrepayAccountMDAO();

        [Route("ACC_PREPAY_ACCOUNT_M/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccPrepayAccountM_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_M /Add");

            rs rs = dao.Add(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_M/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccPrepayAccountM_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_M /Update");

            rs rs = dao.Update(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_M/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccPrepayAccountM_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_M /Delete");

            rs rs = dao.Delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_M/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPrepayAccountM_qry data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_M /Query");

            rsAccPrepayAccountM_qry rs = dao.Query(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}