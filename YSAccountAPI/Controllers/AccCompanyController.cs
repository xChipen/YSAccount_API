using System.Net.Http;
using System.Web.Http;
using Models;
using Helpers;
using DAO;

namespace YSAccountAPI.Controllers
{
    public class AccCompanyController : ApiController
    {
        AccCompanyDAO dao = new AccCompanyDAO();

        [Route("ACC_COMPANY/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccCompany_ins data)
        {
            Log.Info("API : ACC_COMPANY/Add");

            rs rs = dao.addAccCompany(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_COMPANY/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccCompany_ins data)
        {
            Log.Info("API : ACC_COMPANY/Update");

            rs rs = dao.updateAccCompany(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_COMPANY/CloseDate")]
        [HttpPost, ActionName("CloseDate")]
        public HttpResponseMessage CloseDate(AccCompany_CloseDate data)
        {
            Log.Info("API : ACC_COMPANY/CloseDate");

            rs rs = dao.updateCompany_CloseDate(data);

            return CommDAO.getResponse(Request, rs);
        }


        [Route("ACC_COMPANY/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccCompany_del data)
        {
            Log.Info("API : ACC_COMPANY/Delete");

            rs rs = dao.deleteAccCompany(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_COMPANY/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccCompany_del data)
        {
            Log.Info("API : ACC_COMPANY/Query");

            AccCompany_query rs = dao.queryAccCompany(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}