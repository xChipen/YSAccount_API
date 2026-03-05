using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccAccNameController : ApiController
    {
        AccAccNameDAO dao = new AccAccNameDAO();

        [Route("ACC_ACCNAME/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccAccNameAdd data)
        {
            Log.Info("API : ACC_ACCNAME/Add");

            rs_AccAccNameAdd rs = dao.addAccAccName(data);
            //string s = new JavaScriptSerializer().Serialize(rs);
            //Log.Info("haveAccBalance rs_AccBalanceAdd : " + s);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACCNAME/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccAccNameAdd data)
        {
            Log.Info("API : ACC_ACCNAME/Update");

            rs rs = dao.updateAccAccName(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACCNAME/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccAccNameDelete data)
        {
            Log.Info("API : ACC_ACCNAME/Delete");

            rs rs = dao.deleteAccAccName(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACCNAME/Delete2")]
        [HttpPost, ActionName("Delete2")]
        public HttpResponseMessage Delete2(AccAccNameDelete2 data)
        {
            Log.Info("API : ACC_ACCNAME/Delete2");

            rs rs = dao.deleteAccAccName2(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACCNAME/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccAccNameQuery data)
        {
            Log.Info("API : ACC_ACCNAME/Query");

            rs_AccAccNameQuery rs = dao.queryAccAccName(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACCNAME/QueryByRange")]
        [HttpPost, ActionName("QueryByRange")]
        public HttpResponseMessage QueryByRange(AccAccNameQueryByRange data)
        {
            Log.Info("API : ACC_ACCNAME/QueryByRange");

            rs_AccAccNameQuery rs = dao.queryAccAccNameByRange(data);

            return CommDAO.getResponse(Request, rs);
        }

        //下拉式
        [Route("ACC_ACCNAME/Query2")]
        [HttpPost, ActionName("Query2")]
        public HttpResponseMessage Query2(AccAccNameQuery23 data)
        {
            Log.Info("API : ACC_ACCNAME/Query2");

            rs_AccAccNameQuery23 rs = dao.query23(data, 2);

            return CommDAO.getResponse(Request, rs);
        }

        //下拉式
        [Route("ACC_ACCNAME/Query3")]
        [HttpPost, ActionName("Query3")]
        public HttpResponseMessage Query3(AccAccNameQuery23 data)
        {
            Log.Info("API : ACC_ACCNAME/Query3");

            rs_AccAccNameQuery23 rs = dao.query23(data, 3);

            return CommDAO.getResponse(Request, rs);
        }

        //下拉式
        [Route("ACC_ACCNAME/Query4")]
        [HttpPost, ActionName("Query4")]
        public HttpResponseMessage Query4(AccAccNameQuery23 data)
        {
            Log.Info("API : ACC_ACCNAME/Query4");

            rs_AccAccNameQuery23 rs = dao.query23(data, 4);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACCNAME/Query5")]
        [HttpPost, ActionName("Query5")]
        public HttpResponseMessage Query5(AccAccNameQuery23 data)
        {
            Log.Info("API : ACC_ACCNAME/Query5");

            rs_AccAccNameQuery rs = dao.query5(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ACCNAME/Query5_2")]
        [HttpPost, ActionName("Query5_2")]
        public HttpResponseMessage Query5_2(AccAccNameQuery23 data)
        {
            Log.Info("API : ACC_ACCNAME/Query5_2");

            rs_AccAccNameQuery rs = dao.query5_2(data);

            return CommDAO.getResponse(Request, rs);
        }


        [Route("ACC_ACCNAME/Query6")]
        [HttpPost, ActionName("Query6")]
        public HttpResponseMessage Query6(AccAccNameQuery23 data)
        {
            Log.Info("API : ACC_ACCNAME/Query6");

            rs_AccAccNameQuery rs = dao.query6(data);

            return CommDAO.getResponse(Request, rs);
        }


    }
}