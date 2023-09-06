using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginRequest(LoginRequest login)
        {
            //ssl hatalarını kaypak geçmek için
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            #region login kısmı

            var client = new RestClient(login.ServiceLayerUrl + "/b1s/v1/Login");

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var bodyParam = JsonConvert.SerializeObject(login);
            request.AddParameter("application/json", bodyParam, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var resp = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            resp.RouteId = response.Cookies.Where(k => k.Name == "ROUTEID").FirstOrDefault().Value;

            #endregion


            #region file ulaşma

            var client3 = new RestClient(login.ServiceLayerUrl);
            var request3 = new RestRequest("/b1s/v1/Attachments2(3)");
            request3.AddHeader("Content-Type", "application/json");
            request3.AddCookie("B1SESSION", resp.SessionId);
            request3.AddCookie("ROUTEID", resp.RouteId);
            IRestResponse response3 = client3.Execute(request3);
            var resp3 = JsonConvert.DeserializeObject<FileResponse>(response3.Content);




            var client2 = new RestClient(login.ServiceLayerUrl);
            var request2 = new RestRequest("/b1s/v1/Attachments2(3)/$value");
            request2.AddHeader("Content-Type", "application/json");
            request2.AddCookie("B1SESSION", resp.SessionId);
            request2.AddCookie("ROUTEID", resp.RouteId);
            IRestResponse response2 = client2.Execute(request2);

            byte[] veriBytes = Encoding.UTF8.GetBytes(response2.Content);


            System.IO.File.WriteAllBytes($"C:\\ODS\\{resp3.Attachments2_Lines.FirstOrDefault().FileName}.{resp3.Attachments2_Lines.FirstOrDefault().FileExtension}", response2.RawBytes);



            //string dosyaYolu = "C:\\ODS\\deneme.txt";
            //// FileStream kullanarak dosyayı oluşturun veya açın
            //using (FileStream dosyaAkisi = new FileStream(dosyaYolu, FileMode.Create))
            //{

            //    // Veriyi akışa yazın
            //    dosyaAkisi.Write(veriBytes, 0, veriBytes.Length);
            //}

            #endregion


            return View(resp);
        }
    }
}