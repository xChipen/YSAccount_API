using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccPrepayAccountM_BatchController : ApiController
    {
        AccPrepayAccountM_BatchDAO dao = new AccPrepayAccountM_BatchDAO();

        // 新增 + 修改. 
        [Route("ACC_PREPAY_ACCOUNT_M_BATCH/AddUpdate")]
        [HttpPost, ActionName("AddUpdate")]
        public HttpResponseMessage AddUpdate(AccPrepayAccountM_Batch_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_M_BATCH/AddUpdate");

            rs rs = dao.addUpdate(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_M_BATCH/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccPrepayAccountM_Batch_ins data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_M_BATCH/Delete");

            rs rs = dao.delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAY_ACCOUNT_M_BATCH/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPrepayAccountM_Batch_qry data)
        {
            Log.Info("API : ACC_PREPAY_ACCOUNT_M_BATCH/Query");

            rs_AccPrepayAccountM_Batch rs = dao.queryAccPrepayAccountM_Batch(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}