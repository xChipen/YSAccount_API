using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccReCheckController : ApiController
    {
        AccReCheckDAO dao = new AccReCheckDAO();

        [Route("ACC_RE_CHECK/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccReCheckAdd data)
        {
            Log.Info("API : ACC_RE_CHECK/Add");

            rs_AccReCheckAdd rs = dao.addAccReCheck(data);
            //string s = new JavaScriptSerializer().Serialize(rs);
            //Log.Info("haveAccBalance rs_AccBalanceAdd : " + s);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_RE_CHECK/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccReCheck_ins data)
        {
            Log.Info("API : ACC_RE_CHECK/Update");

            rs rs = dao.updateAccReCheck(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_RE_CHECK/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccReCheckDelete data)
        {
            Log.Info("API : ACC_RE_CHECK/Delete");

            rs rs = dao.deleteAccReCheck(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_RE_CHECK/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccReCheckQuery data)
        {
            Log.Info("API : ACC_RE_CHECK/Query");

            rs_AccReCheckQuery rs = dao.queryAccReCheck(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_RE_CHECK/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(AccReCheckQuery2 data)
        {
            Log.Info("API : ACC_RE_CHECK/Query2");

            rs_AccReCheckQuery rs = dao.queryAccReCheck2(data);

            return CommDAO.getResponse(Request, rs);
        }

        // ACG040B_update
        [Route("ACC_RE_CHECK/ACG040B_update")]
        [HttpPost, ActionName("ACG040B_update")]
        public HttpResponseMessage ACG040B_update(ACG040B_update_ins data)
        {
            Log.Info("API : ACC_RE_CHECK/ACG040B_update");

            rs rs = dao.ACG040B_update(data.data, data.baseRequest.employeeNo, data.baseRequest.name);

            return CommDAO.getResponse(Request, rs);
        }

    }
}