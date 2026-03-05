using System.Net.Http;
using System.Web.Http;
using Models;
using Helpers;
using DAO;

namespace YSAccountAPI.Controllers
{
    public class AccBalanceControlController : ApiController
    {
        AccBalanceControlDAO dao = new AccBalanceControlDAO();

        [Route("ACC_BALANCE_CONTROL/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccBalanceControl_ins2 data)
        {
            Log.Info("API : ACC_BALANCE_CONTROL/Add");

            rs rs = dao.addAccBalanceControl_Batch(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BALANCE_CONTROL/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccBalanceControl_ins data)
        {
            Log.Info("API : ACC_BALANCE_CONTROL/Update");

            rs rs = dao.updateAccBalanceControl(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BALANCE_CONTROL/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccBalanceControl_del data)
        {
            Log.Info("API : ACC_BALANCE_CONTROL/Delete");

            rs rs = dao.deleteAccBalanceControl(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BALANCE_CONTROL/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccBalanceControl_del data)
        {
            Log.Info("API : ACC_BALANCE_CONTROL/Query");

            AccBalanceControl_query rs = dao.queryAccBalanceControl(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}