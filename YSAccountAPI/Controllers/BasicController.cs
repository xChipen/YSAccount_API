using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;
using System.Collections.Generic;

namespace YSAccountAPI.Controllers
{
    public class BasicController : ApiController
    {
        //AccVendorDAO daoVendor = new AccVendorDAO();

        BaseClass2 daoDept     = new BaseClass2("ACC_DEPT");
        AccVendorDAO daoVendor = new AccVendorDAO("ACC_VENDOR");
        BaseClass2 daoCustom   = new BaseClass2("ACC_CUSTOM");
        BaseClass2 daoUser     = new BaseClass2("ACC_USER");

        #region Dept
        [Route("BASIC/Dept_add")]
        [HttpPost]
        public HttpResponseMessage Dept_add(AccDept_ins data)
        {
            Log.Info("API : BASIC/Dept_add");

            rs rs = daoDept.Add<AccDept, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        [Route("BASIC/Dept_update")]
        [HttpPost]
        public HttpResponseMessage Dept_update(AccDept_ins data)
        {
            Log.Info("API : BASIC/Dept_update");

            rs rs = daoDept.Update<AccDept, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        [Route("BASIC/Dept_delete")]
        [HttpPost]
        public HttpResponseMessage Dept_delete(AccDept_del data)
        {
            Log.Info("API : BASIC/Dept_delete");

            rs rs = daoDept.Delete<AccDept, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        #endregion

        #region CUSTOM
        [Route("BASIC/Custom_add")]
        [HttpPost]
        public HttpResponseMessage Custom_add(AccCustom_ins data)
        {
            Log.Info("API : BASIC/Custom_add");

            rs rs = daoCustom.Add<AccCustom, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        [Route("BASIC/Custom_update")]
        [HttpPost]
        public HttpResponseMessage Custom_update(AccCustom_ins data)
        {
            Log.Info("API : BASIC/Custom_update");

            rs rs = daoCustom.Update<AccCustom, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        [Route("BASIC/Custom_delete")]
        [HttpPost]
        public HttpResponseMessage Custom_delete(AccCustom_del data)
        {
            Log.Info("API : BASIC/Custom_update");

            rs rs = daoCustom.Delete<AccCustom, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        #endregion

        #region User
        [Route("BASIC/User_add")]
        [HttpPost]
        public HttpResponseMessage User_add(AccUser_ins data)
        {
            Log.Info("API : BASIC/User_add");

            rs rs = daoUser.Add<AccUser, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("BASIC/User_update")]
        [HttpPost]
        public HttpResponseMessage User_update(AccUser_ins data)
        {
            Log.Info("API : BASIC/User_update");

            rs rs = daoUser.Update<AccUser, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("BASIC/User_delete")]
        [HttpPost]
        public HttpResponseMessage User_delete(AccUser_del data)
        {
            Log.Info("API : BASIC/User_update");

            rs rs = daoUser.Delete<AccUser, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        #endregion

        #region Vender
        [Route("BASIC/Vendor_add")]
        [HttpPost]
        public HttpResponseMessage Vendor_add(AccVendor_ins data)
        {
            Log.Info("API : BASIC/Vendor_add");

            daoVendor.outFields.Clear();
            daoVendor.outFields.Add("SEQ");

            rs rs = daoVendor.Add<AccVendor, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        [Route("BASIC/Vendor_update")]
        [HttpPost]
        public HttpResponseMessage Vendor_update(AccVendor_ins data)
        {
            Log.Info("API : BASIC/Vendor_update");

            daoVendor.outFields.Clear();
            daoVendor.outFields.Add("SEQ");

            rs rs = daoVendor.Update<AccVendor, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        [Route("BASIC/Vendor_delete")]
        [HttpPost]
        public HttpResponseMessage Vendor_delete(AccVendor_del data)
        {
            Log.Info("API : BASIC/Vendor_update");

            rs rs = daoVendor.Delete<AccVendor, rs>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        [Route("BASIC/Vendor_Query")]
        [HttpPost,]
        public HttpResponseMessage Vendor_Query(AccVendor_qry data)
        {
            Log.Info("API : BASIC/Vendor_Query");

            rs_AccVendor_qry rs = daoVendor.Query(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        [Route("BASIC/Vendor_AUD")]
        [HttpPost,]
        public HttpResponseMessage Vendor_AUD(AccVendor_aud data)
        {
            Log.Info("API : BASIC/Vendor_Query");

            daoVendor.outFields.Clear();
            daoVendor.outFields.Add("SEQ");

            rsList rs = daoVendor.AUD<AccVendor_item, rsList>(data.data);

            return CommDAO.getResponse(Request, rs);
        }
        #endregion
    }
}