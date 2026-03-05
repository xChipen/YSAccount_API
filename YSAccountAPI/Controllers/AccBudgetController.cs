using System.Net.Http;
using System.Web.Http;
using Models;
using DAO;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccBudgetController : ApiController
    {
        AccBudgetDAO dao = new AccBudgetDAO();

        [Route("ACC_BUDGET/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccBudgetAdd data)
        {
            Log.Info("API : ACC_BUDGET/Add");

            rs_AccBudgetAdd rs = dao.addAccBudget(data);
            //string s = new JavaScriptSerializer().Serialize(rs);
            //Log.Info("haveAccBalance rs_AccBalanceAdd : " + s);
            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BUDGET/batch")]
        [HttpPost, ActionName("batch")]
        public HttpResponseMessage batch(AccBudget_ins data)
        {
            Log.Info("API : ACC_BUDGET/batch");

            rs_AccBudgetAdd rs = dao.batch(data);
            return CommDAO.getResponse(Request, rs);
        }


        [Route("ACC_BUDGET/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccBudgetAdd data)
        {
            Log.Info("API : ACC_BUDGET/Update");

            rs rs = dao.updateAccBudget(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BUDGET/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccBudgetDelete data)
        {
            Log.Info("API : ACC_BUDGET/Delete");

            rs rs = dao.deleteAccBudget(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_BUDGET/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccBudgetQuery data)
        {
            Log.Info("API : ACC_BUDGET/Query");

            rs_AccBudgetQuery rs = dao.queryAccBudget(data);

            return CommDAO.getResponse(Request, rs);
        }
    }
}