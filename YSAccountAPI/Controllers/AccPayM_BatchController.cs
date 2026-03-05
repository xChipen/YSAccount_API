using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccPayM_BatchController : ApiController
    {
        AccPayM_BatchDAO dao = new AccPayM_BatchDAO();

        // 新增 + 修改. 
        [Route("ACC_PAY_M_BATCH/AddUpdate")]
        [HttpPost, ActionName("AddUpdate")]
        public HttpResponseMessage AddUpdate(AccPayM_Batch_ins data)
        {
            Log.Info("API : ACC_PAY_M_BATCH/AddUpdate");

            rs rs = dao.addUpdate(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PAY_M_BATCH/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccPayM_Batch_ins data)
        {
            Log.Info("API : ACC_PAY_M_BATCH/Delete");

            rs rs = dao.delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PAY_M_BATCH/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPayM_Batch_qry data)
        {
            Log.Info("API : ACC_PAY_M_BATCH/Query");

            rs_AccPayM_Batch rs = dao.queryAccPayM_Batch(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMST_M_BATCH/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(AccPayM_qry2_in data)
        {
            Log.Info("API : ACC_VOUMST_M_BATCH/Query2");

            rsAccPayM_qry2_rs rs = dao.Query2(data.data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMST_M_BATCH/Query3")]
        [HttpPost, ActionName("Query3")]
        public HttpResponseMessage Query3(AccPayM_qry3_in data)
        {
            Log.Info("API : ACC_VOUMST_M_BATCH/Query3");

            rsAccPayM_qry3_rs rs = dao.Query3(data.data);

            return CommDAO.getResponse(Request, rs);
        }

    }
}