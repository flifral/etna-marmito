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

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("tokenValue");
            Response.Cookies.Delete("Id");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Unauthorize()
        {
            return View();
        }
    }
}