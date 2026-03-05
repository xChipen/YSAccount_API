using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccBankBalanceController : ApiController
    {
        AccBankBalanceDAO dao = new AccBankBalanceDAO();

        [Route("ACC_BANK_BALANCE/AUD")]
        [HttpPost, ActionName("AUD")]
        public HttpResponseMessage Add(AccBankBalance_Batch_ins data)
        {
            Log.Info("API : ACC_BANK_BALANCE/AUD");

            rsAccBankBalance_Batch rs = dao.AUD(data.data, data.baseRequest.employeeNo, data.baseRequest.name);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BANK_BALANCE/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccBankBalance_ins data)
        {
            Log.Info("API : ACC_BANK_BALANCE/Delete");

            rs rs = dao.Delete2(data.data);
            return CommDAO.getResponse(Request, rs);
        }
    }
}