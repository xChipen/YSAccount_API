using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccAccKind_BatchController : ApiController
    {
        AccAccKind_BatchDAO dao = new AccAccKind_BatchDAO();

        [Route("ACC_ACC_KIND_BATCH/AddUpdate")]
        [HttpPost, ActionName("AddUpdate")]
        public HttpResponseMessage AddUpdate(AccAccKind_Batch_ins data)
        {
            Log.Info("API : ACC_ACC_KIND_BATCH/AddUpdate");

            //
            rs rs = dao.addUpdate(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACC_KIND_BATCH/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccAccKind_Batch_ins data)
        {
            Log.Info("API : ACC_ACC_KIND_BATCH/Delete");

            //
            rs rs = dao.deleteAccAccKind_Batch(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACC_KIND_BATCH/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccAccKindQuery data)
        {
            Log.Info("API : ACC_ACC_KIND_BATCH/Query");

            rs_AccAccKind_Batch rs = dao.queryAccAccKind_Batch(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}