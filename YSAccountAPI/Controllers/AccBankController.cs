using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;
using System.Web.Script.Serialization;

namespace YSAccountAPI.Controllers
{
    public class AccBankController : ApiController
    {
        AccBankDAO dao = new AccBankDAO();

        [Route("ACC_BANK/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccBankAdd data)
        {
            Log.Info("API : ACC_BANK/Add");

            rs_AccBankAdd rs = dao.addAccBank(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BANK/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccBankAdd data)
        {
            Log.Info("API : ACC_BANK/Update");

            rs rs = dao.updateAccBank(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BANK/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccBankDelete data)
        {
            Log.Info("API : ACC_BANK/Delete");

            rs rs = dao.deleteAccBank(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BANK/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccBankQuery data)
        {
            Log.Info("API : ACC_BANK/Query");

            string s = new JavaScriptSerializer().Serialize(data);
            Log.Info("dataIn string : " + s);
            rs_AccBankQuery rs = dao.queryAccBank(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BANK/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(AccBankQuery2 data)
        {
            Log.Info("API : ACC_BANK/Query2");

            string s = new JavaScriptSerializer().Serialize(data);
            Log.Info("dataIn string : " + s);
            rs_AccBankQuery rs = dao.queryAccBank_like(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}