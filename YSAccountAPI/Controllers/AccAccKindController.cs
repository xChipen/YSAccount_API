using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;
using System.Web.Script.Serialization;

namespace YSAccountAPI.Controllers
{
    public class AccAccKindController : ApiController
    {
        AccAccKindDAO dao = new AccAccKindDAO();

        [Route("ACC_ACC_KIND/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccAccKindAdd data)
        {
            Log.Info("API : ACC_ACC_KIND/Add");

            rs_AccAccKindAdd rs = dao.addAccAccKind(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACC_KIND/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccAccKindAdd data)
        {
            Log.Info("API : ACC_ACC_KIND/Update");

            rs rs = dao.updateAccAccKind(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACC_KIND/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccAccKindDelete data)
        {
            Log.Info("API : ACC_ACC_KIND/Delete");

            rs rs = dao.deleteAccAccKind(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACC_KIND/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccAccKindQuery data)
        {
            Log.Info("API : ACC_ACC_KIND/Query");

            rs_AccAccKindQuery rs = dao.queryAccAccKind(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}