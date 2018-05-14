using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Dynamic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MarmitoFront.Models;

namespace MarmitoFront.Controllers
{
    public class MitoController : Controller
    {
        private API m_api = new API();
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("tokenValue"))
            {
                return RedirectToAction("Unauthorize", "Auth");
            }
            HttpClient client = m_api.getClient();
            client.DefaultRequestHeaders.Add("tokenValue", HttpContext.Request.Cookies["tokenValue"]);
            List<MarmitoAPI.Models.Mito> mitos = new List<MarmitoAPI.Models.Mito>();
            List<MarmitoAPI.Models.User> users = new List<MarmitoAPI.Models.User>();
            var res = await client.GetAsync("api/mito");
            dynamic model = new ExpandoObject();

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                mitos = JsonConvert.DeserializeObject<List<MarmitoAPI.Models.Mito>>(result);
            }

            res = await client.GetAsync("api/user");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<MarmitoAPI.Models.User>>(result);
            }

            model.Mitos = mitos;
            model.Users = users;

            return View(model);
        }
    }
}