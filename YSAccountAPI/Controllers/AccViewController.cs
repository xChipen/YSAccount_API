using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class AccViewController : ApiController
    {
        AccViewDAO dao = new AccViewDAO();

        //下拉式 vw_ACCD
        [Route("ACC_VIEW/ACCD")]
        [HttpPost, ActionName("ACCD")]
        public HttpResponseMessage ACCD(ACCD_ins data)
        {
            Log.Info("API : ACC_VIEW/ACCD");

            rs_ACCD rs = dao.queryACCD(data);

            return CommDAO.getResponse(Request, rs);
        }

        // vw_ACCD
        [Route("ACC_VIEW/ACCD2")]
        [HttpPost, ActionName("ACCD2")]
        public HttpResponseMessage ACCD2(ACCD_ins data)
        {
            Log.Info("API : ACC_VIEW/ACCD2");

            rs_ACCD rs = dao.queryACCD(data, 2 );

            return CommDAO.getResponse(Request, rs);
        }

        // vw_ACCD1
        [Route("ACC_VIEW/ACCD3")]
        [HttpPost, ActionName("ACCD3")]
        public HttpResponseMessage ACCD3(ACCD_ins data)
        {
            Log.Info("API : ACC_VIEW/ACCD3");

            rs_ACCD rs = dao.queryACCD(data, 2, "vw_ACCD1");

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VIEW/ACCD4")]
        [HttpPost, ActionName("ACCD4")]
        public HttpResponseMessage ACCD4(ACCD_ins data)
        {
            Log.Info("API : ACC_VIEW/ACCD4");

            rs_ACCD rs = dao.queryACCD2(data, "VW_ACCD1");

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VIEW/ACCD5")]
        [HttpPost, ActionName("ACCD5")]
        public HttpResponseMessage ACCD5(ACCD_ins data)
        {
            Log.Info("API : ACC_VIEW/ACCD5");

            rs_ACCD rs = dao.queryACCD2(data, "VW_ACCD");

            return CommDAO.getResponse(Request, rs);
        }

        // ACE010M 
        [Route("ACC_VIEW/ACCD_ACE010M")]
        [HttpPost, ActionName("ACCD_ACE010M")]
        public HttpResponseMessage ACCD_ACE010M(ACCD_ins data)
        {
            Log.Info("API : ACC_VIEW/ACCD_ACE010M");

            rs_ACCD rs = dao.ACCD_ACE010M(data.data);

            return CommDAO.getResponse(Request, rs);
        }

        //下拉式
        [Route("ACC_VIEW/TRAIN")]
        [HttpPost, ActionName("TRAIN")]
        public HttpResponseMessage TRAIN(TRAIN_ins data)
        {
            Log.Info("API : ACC_VIEW/TRAIN");

            rs_TRAIN rs = dao.queryTRAIN(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VIEW/TRAIN2")]
        [HttpPost, ActionName("TRAIN2")]
        public HttpResponseMessage TRAIN2(TRAIN_ins2 data)
        {
            Log.Info("API : ACC_VIEW/TRAIN2");

            rs_TRAIN rs = dao.queryTRAIN_like(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VIEW/TRAIN3")]
        [HttpPost, ActionName("TRAIN3")]
        public HttpResponseMessage TRAIN3(TRAIN_ins data)
        {
            Log.Info("API : ACC_VIEW/TRAIN3");

            rs_TRAIN rs = dao.queryTRAIN_ACE040B(data);

            return CommDAO.getResponse(Request, rs);
        }


        //下拉式
        [Route("ACC_VIEW/DEPT")]
        [HttpPost, ActionName("DEPT")]
        public HttpResponseMessage DEPT(AccDept_ins data)
        {
            Log.Info("API : ACC_VIEW/DEPT");

            rs_AccDept_qry rs = dao.queryAccDept(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VIEW/ACC_DEPT_USE")]
        [HttpPost, ActionName("ACC_DEPT_USE")]
        public HttpResponseMessage ACC_DEPT_USE(AccDept_ins data)
        {
            Log.Info("API : ACC_VIEW/ACC_DEPT_USE");

            string sql = @"SELECT * FROM ACC_DEPT_USE,ACC_DEPT
WHERE DEPT_COMPID = DEUS_COMPID
AND DEPT_ID = DEUS_DEPTID ";

            rs_AccDept_qry rs = dao.queryAccDept(data, sql);

            return CommDAO.getResponse(Request, rs);
        }


        //下拉式
        [Route("ACC_VIEW/User")]
        [HttpPost, ActionName("User")]
        public HttpResponseMessage vwUser(USER_ins data)
        {
            Log.Info("API : ACC_VIEW/User");

            rs_USER rs = dao.queryAccUser(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_VIEW/Balance")]
        [HttpPost, ActionName("Balance")]
        public HttpResponseMessage Balance(BALANCE_qry data)
        {
            Log.Info("API : ACC_VIEW/Balance");

            rsBALANCE_qry rs = dao.vwBALANCE(data);

            return CommDAO.getResponse(Request, rs);
        }

    }
}