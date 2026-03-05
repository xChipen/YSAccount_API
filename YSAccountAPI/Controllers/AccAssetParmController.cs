using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;


namespace YSAccountAPI.Controllers
{
    public class AccAssetParmController : ApiController
    {
        AccAssetParmDAO dao = new AccAssetParmDAO();

        [Route("ACC_ASSET_PARAM/AUD")]
        [HttpPost]
        public HttpResponseMessage AUD(AccAssetParm_qry2 data)
        {
            Log.Info("API : ACC_ASSET_PARAM/AUD");

            rs rs = dao.AddAndUpdate(data.data, data.baseRequest.employeeNo, data.baseRequest.name);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_ASSET_PARAM/Query2")]
        [HttpPost]
        public HttpResponseMessage Query2(AccAssetParm_qry2 data)
        {
            Log.Info("API : ACC_ASSET_PARAM/Query2");

            rs_AccAccNameQuery rs = dao.Query2(data.data);
            return CommDAO.getResponse(Request, rs);
        }

        // 畫面查詢
        [Route("ACC_ASSET_PARAM/Query3")]
        [HttpPost]
        public HttpResponseMessage Query3(AccAssetParm_qry2 data)
        {
            Log.Info("API : ACC_ASSET_PARAM/Query3");

            rsAccAssetParm_query rs = dao.Query3(data.data);
            return CommDAO.getResponse(Request, rs);
        }

    }
}