using System;
using System.Text;
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
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("tokenValue"))
            {
                return RedirectToAction("Unauthorize", "Auth");
            }
            HttpClient client = m_api.getClient();
            client.DefaultRequestHeaders.Add("tokenValue", HttpContext.Request.Cookies["tokenValue"]);

            List<MarmitoAPI.Models.MitoUser> mitos = new List<MarmitoAPI.Models.MitoUser>();

            var res = await client.GetAsync("api/mito");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                mitos = JsonConvert.DeserializeObject<List<MarmitoAPI.Models.MitoUser>>(result);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                mitos = mitos.Where(m => m.User.Name.Contains(searchString)).ToList();
            }

            return View(mitos);
        }

        [HttpGet("/Mito/Remove/{id}")]
        public async Task<IActionResult> Remove(long Id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("tokenValue"))
            {
                return RedirectToAction("Unauthorize", "Auth");
            }
            HttpClient client = m_api.getClient();
            client.DefaultRequestHeaders.Add("tokenValue", HttpContext.Request.Cookies["tokenValue"]);
            var res = await client.DeleteAsync("api/mito/" + Id.ToString());
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Mito");
            }
            else
            {
                return RedirectToAction("Unauthorize", "Auth");
            }
        }

        [HttpGet("/Mito/Update/{id}")]
        public async Task<IActionResult> Update(long Id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("tokenValue"))
            {
                return RedirectToAction("Unauthorize", "Auth");
            }

            HttpClient client = m_api.getClient();
            client.DefaultRequestHeaders.Add("tokenValue", HttpContext.Request.Cookies["tokenValue"]);
            var res = await client.GetAsync("api/mito/" + Id.ToString());
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                MarmitoAPI.Models.Mito mito = JsonConvert.DeserializeObject<MarmitoAPI.Models.Mito>(result);
                return View(mito);
            }
            else
            {
                return RedirectToAction("Unauthorize", "Auth");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(MarmitoAPI.Models.Mito mito)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("tokenValue"))
            {
                return RedirectToAction("Unauthorize", "Auth");
            }

            HttpClient client = m_api.getClient();
            client.DefaultRequestHeaders.Add("tokenValue", HttpContext.Request.Cookies["tokenValue"]);
            var res = await client.PutAsync("api/mito", new StringContent(JsonConvert.SerializeObject(mito), Encoding.UTF8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Mito");
            }
            else
            {
                return RedirectToAction("Unauthorize", "Auth");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Add(MarmitoAPI.Models.Mito mito)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("tokenValue") ||
                !HttpContext.Request.Cookies.ContainsKey("Id"))
            {
                return RedirectToAction("Unauthorize", "Auth");
            }

            HttpClient client = m_api.getClient();
            client.DefaultRequestHeaders.Add("tokenValue", HttpContext.Request.Cookies["tokenValue"]);
            mito.AuthorId = Convert.ToInt64(HttpContext.Request.Cookies["Id"]);
            var res = await client.PostAsync("api/mito", new StringContent(JsonConvert.SerializeObject(mito), Encoding.UTF8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Mito");
            }
            else
            {
                return RedirectToAction("Unauthorize", "Auth");
            }
        }
    }
}