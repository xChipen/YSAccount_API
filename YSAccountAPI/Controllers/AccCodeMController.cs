using System.Net.Http;
using System.Web.Http;
using Models;
using Helpers;
using DAO;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Data;
using System.Diagnostics;

namespace YSAccountAPI.Controllers
{
    public class AccCodeMController : ApiController
    {
        AccCodeMDAO dao = new AccCodeMDAO();

        [Route("ACC_CODE_M/Add")]
        [HttpPost, ActionName("Add")]
        public HttpResponseMessage Add(AccCodeM_ins data)
        {
            Log.Info("API : ACC_CODE_M/Add");

            rs rs = dao.addAccCodeM(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CODE_M/Update")]
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(AccCodeM_ins data)
        {
            Log.Info("API : ACC_CODE_M/Update");

            rs rs = dao.updateAccCodeM(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CODE_M/Delete")]
        [HttpPost, ActionName("Delete")]
        public HttpResponseMessage Delete(AccCodeM_del data)
        {
            Log.Info("API : ACC_CODE_M/Delete");

            rs rs = dao.deleteAccCodeM(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CODE_M/Query")]
        [HttpPost, ActionName("Query")]
        public HttpResponseMessage Query(AccCodeM_del data)
        {
            Log.Info("API : ACC_CODE_M/Query");

            AccCodeM_query rs = dao.queryAccCodeM(data);

            return CommDAO.getResponse(Request, rs);
        }
        //--------------------------------------------------------------
        [Route("ACC_CODE_M/AddBatch")]
        [HttpPost, ActionName("AddBatch")]
        public HttpResponseMessage AddBatch(AccCodeM_MD_ins data)
        {
            Log.Info("API : ACC_CODE_M/AddBatch");

            rs rs = dao.addAccCodeM_MD(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CODE_M/UpdateBatch")]
        [HttpPost, ActionName("UpdateBatch")]
        public HttpResponseMessage UpdateBatch(AccCodeM_MD_ins data)
        {
            Log.Info("API : ACC_CODE_M/UpdateBatch");

            rs rs = dao.updateAccCodeM_MD(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CODE_M/AddUpdateBatch")]
        [HttpPost, ActionName("AddUpdateBatch")]
        public HttpResponseMessage AddUpdateBatch(AccCodeM_MD_ins data)
        {
            Log.Info("API : ACC_CODE_M/AddUpdateBatch");

            rs rs = dao.addAndUpdate_MD(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CODE_M/DeleteBatch")]
        [HttpPost, ActionName("DeleteBatch")]
        public HttpResponseMessage DeleteBatch(AccCodeM_MD_ins data)
        {
            Log.Info("API : ACC_CODE_M/DeleteBatch");

            rs rs = dao.deleteAccCodeM_MD(data);

            return CommDAO.getResponse(Request, rs);
        }

        [Route("ACC_CODE_M/QueryBatch")]
        [HttpPost, ActionName("QueryBatch")]
        public HttpResponseMessage QueryBatch(AccCodeM_del data)
        {
            Log.Info("API : ACC_CODE_M/QueryBatch");

            //var sw = new Stopwatch();
            //sw.Start();

            AccCodeM_MD_query rs = dao.queryAccCodeM_MD(data);

            //sw.Stop();
            //Log.Info(sw.Elapsed.ToString("s\\.f"));

            return CommDAO.getResponse(Request, rs);
        }

        //--------------------------------------------------------------
        [Route("ACC_CODE_M/Excel")]
        [HttpGet, ActionName("Excel")]
        public HttpResponseMessage Excel()
        {
            Log.Info("API : ACC_CODE_M/Excel");

            MemoryStream data = ExcelHelper.exampleImportExcel(new string[] 
            { "JOUM_COMPID-公司代號", "JOUM_CODE-常用分錄代號", "JOUM_VALID-有效區分", "JOUM_MEMO-傳票說明" });
            string fileName = "ACC_CODE_M.xls";

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(data.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/vnd.ms-excel");

            return result;
        }

        [Route("ACC_CODE_M/uploadExcel")]
        [HttpPost, ActionName("uploadExcel")]
        public HttpResponseMessage uploadExcel(HttpPostedFileBase file)
        {
            DataTable dt = ExcelHelper.excelToDataTable(file);

            return CommDAO.getResponse(Request, dao.importAccCodeM(dt));
        }
    }
}