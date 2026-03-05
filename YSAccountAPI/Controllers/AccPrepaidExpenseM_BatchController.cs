using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccPrepaidExpenseM_BatchController : ApiController
    {
        AccPrepaidExpenseM_BatchDAO dao = new AccPrepaidExpenseM_BatchDAO();

        // 新增 + 修改. 
        [Route("ACC_PREPAID_EXPENSE_M_BATCH/AddUpdate")]
        [HttpPost, ActionName("AddUpdate")]
        public HttpResponseMessage AddUpdate(AccPrepaidExpenseM_Batch_ins data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_M_BATCH/AddUpdate");

            rs rs = dao.addUpdate(data);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAID_EXPENSE_M_BATCH/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccPrepaidExpenseM_Batch_ins data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_M_BATCH/Delete");

            rs rs = dao.delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAID_EXPENSE_M_BATCH/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPrepaidExpenseM_Batch_qry data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_M_BATCH/Query");

            rs_AccPrepaidExpenseM_Batch rs = dao.queryAccPrepaidExpenseM_Batch(data);

            return CommDAO.getResponse(Request, rs);
        }

        //[Route("ACC_PREPAID_EXPENSE_M_BATCH/Query2")]
        //[HttpPost, ActionName("Query2")]
        //public HttpResponseMessage Query2(AccVoumstM_Batch_qry2 data)
        //{
        //    Log.Info("API : ACC_PREPAID_EXPENSE_M_BATCH/Query2");

        //    rs_AccVoumstM_Batch rs = AccVoumstM_BatchDAO.queryAccVoumstM_Batch2(data);

        //    return CommDAO.getResponse(Request, rs);
        //}
    }
}