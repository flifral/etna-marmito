using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Dynamic;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MarmitoFront.Models;

namespace MarmitoFront.Controllers
{
    public class AuthController : Controller
    {
        private API m_api = new API();

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MarmitoAPI.Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterError");
            }

            if (user != null)
            {
                if (user.Email == null ||
                    user.Password == null ||
                    user.Name == null)
                {
                    return View("RegisterError");
                }

                var sha256 = System.Security.Cryptography.SHA256.Create();
                byte[] pbytes = Encoding.ASCII.GetBytes(user.Password);
                byte[] hash = sha256.ComputeHash(pbytes);
                user.Password = Encoding.ASCII.GetString(hash, 0, hash.Length);
                
                HttpClient client = m_api.getClient();
                var res = await client.PostAsync("api/register", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
                if (!res.IsSuccessStatusCode)
                {
                    return View("RegisterError");
                }
                return RedirectToAction("Login","Auth");
            }
            else
            {
                return View("RegisterError");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(MarmitoAPI.Models.Login login)
        {
            if (login != null)
            {
                if (login.Email == null ||
                    login.Password == null)
                {
                    return View("LoginError");
                }
            }

            var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] pbytes = Encoding.ASCII.GetBytes(login.Password);
            byte[] hash = sha256.ComputeHash(pbytes);
            login.Password = Encoding.ASCII.GetString(hash, 0, hash.Length);
            HttpClient client = m_api.getClient();
            var res = await client.PostAsync("api/login", new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"));

            if (!res.IsSuccessStatusCode)
            {
                return View("LoginError");
            }

            var tokens = res.Content.ReadAsStringAsync().Result;
            MarmitoAPI.Models.Token token = JsonConvert.DeserializeObject<MarmitoAPI.Models.Token>(tokens);


            Response.Cookies.Append("tokenValue", token.TokenValue);
            Response.Cookies.Append("Id", token.Id.ToString());

            return RedirectToAction("Index", "Mito");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("tokenValue");
            Response.Cookies.Delete("Id");
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Unauthorize()
        {
            return View();
        }

        public async Task<IActionResult> Manage()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("tokenValue") ||
                !HttpContext.Request.Cookies.ContainsKey("Id"))
            {
                return RedirectToAction("Unauthorize", "Auth");
            }
            HttpClient client = m_api.getClient();
            client.DefaultRequestHeaders.Add("tokenValue", HttpContext.Request.Cookies["tokenValue"]);
            var res = await client.GetAsync("api/user/" + HttpContext.Request.Cookies["Id"].ToString());

            if (!res.IsSuccessStatusCode)
            {
                return RedirectToAction("Unauthorize", "Auth");
            }

            var user = res.Content.ReadAsStringAsync().Result;
            MarmitoAPI.Models.User u = JsonConvert.DeserializeObject<MarmitoAPI.Models.User>(user);

            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUpdate(MarmitoAPI.Models.User user)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("tokenValue") ||
                !HttpContext.Request.Cookies.ContainsKey("Id"))
            {
                return RedirectToAction("Unauthorize", "Auth");
            }

            var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] pbytes = Encoding.ASCII.GetBytes(user.Password);
            byte[] hash = sha256.ComputeHash(pbytes);
            user.Password = Encoding.ASCII.GetString(hash, 0, hash.Length);

            HttpClient client = m_api.getClient();
            client.DefaultRequestHeaders.Add("tokenValue", HttpContext.Request.Cookies["tokenValue"]);

            var res = await client.PutAsync("api/user", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            if (!res.IsSuccessStatusCode)
            {
                return RedirectToAction("Unauthorize", "Auth");
            }

            return RedirectToAction("Index", "Mito");
        }
    }
}