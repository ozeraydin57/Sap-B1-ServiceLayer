using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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

            var client = new RestClient(login.ServiceLayerUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");

            var bodyParam = JsonConvert.SerializeObject(login);

            request.AddParameter("application/json", bodyParam, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var resp = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            return View(resp);
        }
    }
}