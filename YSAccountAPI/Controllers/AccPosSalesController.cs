using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAO;
using Models;
using Helpers;
using System.Data;

namespace YSAccountAPI.Controllers
{
    public class AccPosSalesController : ApiController
    {
        AccPosSalesDAO dao = new AccPosSalesDAO();

        [Route("ACC_POS_SALES/AUD")]
        [HttpPost, ActionName("AUD")]
        public HttpResponseMessage Add(ACC_POS_SALES_in data)
        {
            Log.Info("API : ACC_POS_SALES/AUD");

            List<ACC_POS_SALES> _data = data.data;

            bool bOK = dao._AUD(ref _data, data.baseRequest.employeeNo, data.baseRequest.name);

            return CommDAO.getResponse(Request, new ACC_POS_SALES_in_rs
            {
                result = bOK ? CommDAO.getRsItem() : CommDAO.getRsItem1(),
                data = _data
            });
        }

        [Route("ACC_POS_SALES/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(ACC_POS_SALES_qry data)
        {
            Log.Info("API : ACC_POS_SALES/Query");

            DataTable dt = dao._QueryBatch(data.data);

            List<ACC_POS_SALES_item> rs = dt.ToList<ACC_POS_SALES_item>() ;

            return CommDAO.getResponse(Request, new ACC_POS_SALES_qry_out {
                result = CommDAO.getRsItem(),
                data = rs
            });
        }


    }
}