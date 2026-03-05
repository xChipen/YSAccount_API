using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccPrepaidExpenseDController : ApiController
    {
        AccPrepaidExpenseDDAO dao = new AccPrepaidExpenseDDAO();

        [Route("ACC_PREPAID_EXPENSE_D/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccPrepaidExpenseD_ins data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_D/Add");

            rs rs = dao.Add(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAID_EXPENSE_D/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccPrepaidExpenseD_ins data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_D/Update");

            rs rs = dao.Update(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAID_EXPENSE_D/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccPrepaidExpenseD_ins data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_D/Delete");

            rs rs = dao.Delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_PREPAID_EXPENSE_D/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccPrepaidExpenseD_ins data)
        {
            Log.Info("API : ACC_PREPAID_EXPENSE_D/Delete");

            rsAccPrepaidExpenseD_qry rs = dao.Query(data);

            return CommDAO.getResponse(Request, rs);
        }

    }
}