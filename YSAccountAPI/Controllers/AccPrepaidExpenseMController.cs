using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccPrepaidExpenseMController : ApiController
    {
        AccPrepaidExpenseMDAO dao = new AccPrepaidExpenseMDAO();

        [Route("ACC_PREPAID_EXPENSE_M/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccPrepaidExpenseM_ins data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_M /Add");

            rs rs = dao.Add(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAID_EXPENSE_M/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccPrepaidExpenseM_ins data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_M /Update");

            rs rs = dao.Update(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAID_EXPENSE_M/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccPrepaidExpenseM_ins data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_M /Delete");

            rs rs = dao.Delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAID_EXPENSE_M/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPrepaidExpenseM_qry data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_M /Query");

            rsAccPrepaidExpenseM_qry rs = dao.Query(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}