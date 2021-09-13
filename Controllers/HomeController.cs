using MVCTesterCPI2.Infrastructure;
using MVCTesterCPI2.Infrastructure.Interfaces;
using MVCTesterCPI2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCTesterCPI2.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient _cpiClient;
        private readonly EntityUpdater _entityUpdater;
        public HomeController(HttpClient client, EntityUpdater eUpdater)
        {
            _cpiClient = client;
            _entityUpdater = eUpdater;
        }

        public HomeController()
        {

        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            //var content = "";
            //HttpResponseMessage response = await _cpiClient.GetAsync(_cpiClient.BaseAddress);
            //if (response.IsSuccessStatusCode)
            //{
            //    content = await response.Content.ReadAsStringAsync();
            //}
            //ViewBag.Message = content;
            return View();
        }

        public ActionResult Contact()
        {
            return View(HttpContext.Application["events"]);
        }
    }
}