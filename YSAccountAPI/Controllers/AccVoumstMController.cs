using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccVoumstMController : ApiController
    {
        AccVoumstMDAO dao = new AccVoumstMDAO();

        [Route("ACC_VOUMST_M/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccVoumstM_ins data)
        {
            Log.Info("API : ACC_VOUMST_M /Add");

            rs rs = dao.Add(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMST_M/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccVoumstM_update data)
        {
            Log.Info("API : ACC_VOUMST_M /Update");

            rs rs = dao.update_ACB030M(data.data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMST_M/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccVoumstM_ins data)
        {
            Log.Info("API : ACC_VOUMST_M /Delete");

            rs rs = dao.Delete(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMST_M/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccVoumstM2_qry_ins data)
        {
            Log.Info("API : ACC_VOUMST_M/Query");

            rsAccVoumstM2_qry rs = dao.Query_ACB030M(data.data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMST_M/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(AccVoumstM_qry2_ins data)
        {
            Log.Info("API : ACC_VOUMST_M/Query2");

            rsAccVoumstM_qry2 rs = dao.Query2_ACB030M(data.data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMST_M/Query3")]
        [HttpPost, ActionName("Query3")]
        public HttpResponseMessage Query3(AccVoumstM_qry2_ins data)
        {
            Log.Info("API : ACC_VOUMST_M/Query");

            rsAccVoumstM_qry2 rs = dao.Query2_ACB030M(data.data, false);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VOUMST_M/ACB090Q")]
        [HttpPost, ActionName("ACB090Q")]
        public HttpResponseMessage ACB090Q(AccVoumstM_ACB090Q_qry data)
        {
            Log.Info("API : ACC_VOUMST_M/ACB090Q");

            rsAccVoumstM_ACB090Q_rs rs = dao.AccVoumstM_ACB090Q(data.data);

            return CommDAO.getResponse(Request, rs);
        }

    }
}