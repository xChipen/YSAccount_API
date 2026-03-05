using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccVoumstM_BatchController : ApiController
    {
        public static readonly object lockObject = new object();

        AccVoumstM_BatchDAO dao = new AccVoumstM_BatchDAO();

        // 新增 + 修改. 
        [Route("ACC_VOUMST_M_BATCH/AUD")]
        [HttpPost, ActionName("AUD")]
        public HttpResponseMessage AUD( AccVoumstM_Batch_ins data)
        {
            Log.Info("API : ACC_VOUMST_M_BATCH/AUD");

            lock (lockObject)
            {
                rs_AccVoumstM_Batch rs = dao._AUD(data);
                return CommDAO.getResponse(Request, rs);
            }
        }

        //[Route("ACC_VOUMST_M_BATCH/Delete")]
        //[HttpPost, ActionName("Delete")]
        //public HttpResponseMessage Delete(AccVoumstM_Batch_ins data)
        //{
        //    Log.Info("API : ACC_VOUMST_M_BATCH/Delete");

        //    rs rs = null; //dao.delete(data);

        //    return CommDAO.getResponse(Request, rs);
        //}

        [Route("ACC_VOUMST_M_BATCH/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccVoumstM_qry2_ins data)
        {
            Log.Info("API : ACC_VOUMST_M_BATCH/Query");

            rsAccVoumstM_Batch_qry rs = dao.queryAccVoumstM_Batch(data.data);

            return CommDAO.getResponse(Request, rs);
        }

        //[Route("ACC_VOUMST_M_BATCH/Query2")]
        //[HttpPost, ActionName("Query2")]
        //public HttpResponseMessage Query2(AccVoumstM_Batch_qry2 data)
        //{
        //    Log.Info("API : ACC_VOUMST_M_BATCH/Query2");

        //    rs_AccVoumstM_Batch rs = null; //dao.queryAccVoumstM_Batch2(data);

        //    return CommDAO.getResponse(Request, rs);
        //}
    }
}