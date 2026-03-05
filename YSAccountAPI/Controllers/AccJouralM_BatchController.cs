using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccJouralM_BatchController : ApiController
    {
        AccJouralM_BatchDAO dao = new AccJouralM_BatchDAO();

        // 新增 + 修改. 
        [Route("ACC_JOURAL_M_BATCH/AddUpdate")]
        [HttpPost, ActionName("AddUpdate")]
        public HttpResponseMessage AddUpdate(AccJouralM_Batch_ins data)
        {
            Log.Info("API : ACC_JOURAL_M_BATCH/AddUpdate");

            rs rs = dao.addUpdate(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_JOURAL_M_BATCH/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccJouralM_Batch_ins data)
        {
            Log.Info("API : ACC_JOURAL_M_BATCH/Delete");

            rs rs = dao.delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_JOURAL_M_BATCH/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccJouralM_del data)
        {
            Log.Info("API : ACC_JOURAL_M_BATCH/Query");

            rs_AccJouralM_Batch rs = dao.queryAccJouralM_Batch(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}