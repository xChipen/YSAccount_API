using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;
using System.Web.Script.Serialization;

namespace YSAccountAPI.Controllers
{
    public class AccMemoController : ApiController
    {
        AccMemoDAO dao = new AccMemoDAO();

        [Route("ACC_MEMO/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccMemoAdd data)
        {
            Log.Info("API : ACC_MEMO/Add");

            rs_AccMemoAdd rs = dao.addAccMemo(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_MEMO/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccMemoAdd data)
        {
            Log.Info("API : ACC_MEMO/Update");

            rs rs = dao.updateAccMemo(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_MEMO/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccMemoDelete data)
        {
            Log.Info("API : ACC_MEMO/Delete");

            rs rs = dao.deleteAccMemo(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_MEMO/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccMemoQuery data)
        {
            Log.Info("API : ACC_MEMO/Query");

            string s = new JavaScriptSerializer().Serialize(data);
            Log.Info("dataIn string : " + s);
            rs_AccMemoQuery rs = dao.queryAccMemo(data);

            return CommDAO.getResponse(Request, rs);
        }
        [Route("ACC_MEMO/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(AccMemoQuery2 data)
        {
            Log.Info("API : ACC_MEMO/Query2");
            
            string s = new JavaScriptSerializer().Serialize(data);
            Log.Info("dataIn string : " + s);
            rs_AccMemoQuery rs = dao.queryAccMemo2(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}