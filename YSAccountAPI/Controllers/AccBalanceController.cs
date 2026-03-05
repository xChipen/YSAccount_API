using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;
using System.Web.Script.Serialization;

namespace YSAccountAPI.Controllers
{
    public class AccBalanceController : ApiController
    {
        AccBalanceDAO dao = new AccBalanceDAO();

        [Route("ACC_BALANCE/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccBalanceAdd data)
        {
            Log.Info("API : ACC_BALANCE/Add");

            rs_AccBalanceAdd rs = dao.addAccBalance(data);
            //string s = new JavaScriptSerializer().Serialize(rs);
            //Log.Info("haveAccBalance rs_AccBalanceAdd : " + s);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BALANCE/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccBalanceAdd data)
        {
            Log.Info("API : ACC_BALANCE/Update");

            rs rs = dao.updateAccBalance(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BALANCE/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccBalanceDelete data)
        {
            Log.Info("API : ACC_BALANCE/Delete");

            rs rs = dao.deleteAccBalance(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BALANCE/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccBalanceQuery data)
        {
            Log.Info("API : ACC_BALANCE/Query");

            rs_AccBalanceQuery rs = dao.queryAccBalance(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}