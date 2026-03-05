using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccExpenseReceiptDController : ApiController
    {
        AccExpenseReceiptDDAO dao = new AccExpenseReceiptDDAO();

        [Route("ACC_EXPENSE_RECEIPT_D/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccExpenseReceiptD_ins data)
        {
            Log.Info("API : ACC_EXPENSE_RECEIPT_D/Add");

            rs rs = dao.Add(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_EXPENSE_RECEIPT_D/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccExpenseReceiptD_ins data)
        {
            Log.Info("API : ACC_EXPENSE_RECEIPT_D/Update");

            rs rs = dao.Update(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_EXPENSE_RECEIPT_D/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccExpenseReceiptD_ins data)
        {
            Log.Info("API : ACC_EXPENSE_RECEIPT_D/Delete");

            rs rs = dao.Delete(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}