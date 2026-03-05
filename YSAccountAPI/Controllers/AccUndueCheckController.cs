using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccUndueCheckController : ApiController
    {
        AccUndueCheckDAO dao = new AccUndueCheckDAO();

        [Route("ACC_UNDUE_CHECK/AUD")]
        [HttpPost, ActionName("AUD")]
        public HttpResponseMessage Add(AccUndueCheck_Batch_ins data)
        {
            Log.Info("API : ACC_UNDUE_CHECK/AUD");

            rsAccUndueCheck_Batch rs = dao.AUD(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_UNDUE_CHECK/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccUndueCheck_ins data)
        {
            Log.Info("API : ACC_UNDUE_CHECK/Delete");

            rs rs = dao.Delete2( data.data );
            return CommDAO.getResponse(Request, rs);
        }
    }
}