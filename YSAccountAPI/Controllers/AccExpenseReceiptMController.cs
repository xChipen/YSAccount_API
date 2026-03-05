using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;


namespace YSAccountAPI.Controllers
{
    public class AccExpenseReceiptMController : ApiController
    {
        AccExpenseReceiptMDAO dao = new AccExpenseReceiptMDAO();

        [Route("ACC_EXPENSE_RECEIPT_M/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccExpenseReceiptM_ins data)
        {
            Log.Info("API : ACC_EXPENSE_RECEIPT_M /Add");

            rs rs = dao.Add(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_EXPENSE_RECEIPT_M/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccExpenseReceiptM_ins data)
        {
            Log.Info("API : ACC_EXPENSE_RECEIPT_M /Update");

            rs rs = dao.Update(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_EXPENSE_RECEIPT_M/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccExpenseReceiptM_ins data)
        {
            Log.Info("API : ACC_EXPENSE_RECEIPT_M /Delete");

            rs rs = dao.Delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_EXPENSE_RECEIPT_M/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccExpenseReceiptM_qry data)
        {
            Log.Info("API : ACC_EXPENSE_RECEIPT_M /Query");

            rsAccExpenseReceiptM_qry rs = dao.Query(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}