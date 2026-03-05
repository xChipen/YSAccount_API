using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using COMM;
using System.Data;
using System.Linq;
using Helpers;

namespace YSAccountAPI.Controllers
{
    public class TestController : ApiController
    {
        [Route("Home/Index")]
        [HttpGet]
        public HttpResponseMessage Hello()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Hello...");
        }

        [Route("TEST/Add1")]
        [HttpPost, ActionName("Add1")]
        public async Task<HttpResponseMessage> Add1()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                string fileSaveLocation = HttpContext.Current.Server.MapPath("~/Uploads");
                List<string> files = new List<string>();
                foreach (MultipartFileData file in provider.FileData)
                {
                    // 取檔案名稱
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileinfo = new FileInfo(file.LocalFileName);

                    // 另存檔案, 再將原檔案刪除
                    String ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    fileinfo.CopyTo(Path.Combine(fileSaveLocation, newFileName + ".bmp"), true);
                    fileinfo.Delete();
                    files.Add("http://192.168.1.129/UploadImages/" + newFileName + ".bmp");
                }

                // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        Trace.WriteLine(string.Format("{0}: {1}", key, val));
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Route("TEST/Add2")]
        [HttpPost, ActionName("Add2")]
        public HttpResponseMessage UploadFiles()
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            NameValueCollection form = HttpContext.Current.Request.Form;
            foreach (string key in form.AllKeys)
            {
                string value = form[key];
                // Do something with the key and value
            }

            //Fetch the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);

            //Save the File.
            postedFile.SaveAs(path + fileName);

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, fileName);
        }

        [Route("TEST/Add4")]
        [HttpGet, ActionName("Add4")]
        public HttpResponseMessage Add4()
        {
            string sConn = ConfigurationManager.AppSettings["TEST_DB"].ToString();
            SqlConnection conn = new SqlConnection(sConn);

            using (SqlCommand cmd = new SqlCommand(string.Empty, conn))
            {
                cmd.CommandText = "qrySaleM2";
                cmd.Parameters.Add(new SqlParameter("@saleno", "202112220001"));

                SqlParameter sp = new SqlParameter("@FK", SqlDbType.VarChar, 20);
                sp.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(sp);

                DataTable dt = commSQL.ExecSP(conn, cmd);

                return Request.CreateResponse(HttpStatusCode.OK, sp.Value);
            }
        }

        [Route("TEST/Add5")]
        [HttpGet, ActionName("Add5")]
        public HttpResponseMessage Add5()
        {
            string sConn = ConfigurationManager.AppSettings["TEST_DB"].ToString();
            SqlConnection conn = new SqlConnection(sConn);

            Dictionary<string, object> p = new Dictionary<string, object> {
                { "@saleno", "202112220001"}
            };

            DataTable dt = commSQL.ExecSP(conn, "qrySaleM", p);

            return Request.CreateResponse(HttpStatusCode.OK, dt.Rows[0]["SaleNo"]);
        }

        [Route("TEST/test")]
        [HttpGet]
        public HttpResponseMessage test()
        {
            List<Student> students = new List<Student> {
                new Student {
                    Name="Jack",
                    Year=2000,
                    ExamCourse=100
                },
                new Student {
                    Name="Jelly fish",
                    Year=2000,
                    ExamCourse=90
                },
                new Student {
                    Name="Jack",
                    Year=2001,
                    ExamCourse=100
                },
            };

            return Request.CreateResponse(HttpStatusCode.OK, students);
        }

        [Route("TEST/test2")]
        [HttpGet]
        public HttpResponseMessage test2()
        {
            List<Student> students = new List<Student> {
                new Student {
                    Name="Jack",
                    Year=2000,
                    ExamCourse=100
                },
                new Student {
                    Name="Jelly fish",
                    Year=2000,
                    ExamCourse=90
                },
                new Student {
                    Name="Jack",
                    Year=2001,
                    ExamCourse=100
                },
            };

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new JsonContent(students);

            return response;
        }



    }

    public class test {
    }

    public class Student {
        public string Name { get; set; }
        public int Year { get; set; }
        public int ExamCourse { get; set; }
    }
}