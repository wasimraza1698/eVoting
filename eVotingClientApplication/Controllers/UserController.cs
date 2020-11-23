using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using eVotingClientApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eVotingClientApplication.Controllers
{
    public class UserController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UserController));
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            _log4net.Info("User Login");
            User item;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:44322/User/Get", content);
                string apiResponse = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<User>(apiResponse);
                //StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response1 = await httpClient.PostAsync("https://localhost:44322/User/Login", content))
                {
                    _log4net.Info(response1.Content.ReadAsStringAsync().Result);
                    if (!response1.IsSuccessStatusCode)
                    {
                        _log4net.Info("Again User Login");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (user.UserName == "Admin")
                        {
                            _log4net.Info("into Admin");
                            //string apiResponse1 = await response1.Content.ReadAsStringAsync();
                            string stringJWT = response1.Content.ReadAsStringAsync().Result;
                            JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);

                            HttpContext.Session.SetString("token", jwt.Token);
                            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(item));
                            HttpContext.Session.SetInt32("userID", item.UserID);
                            HttpContext.Session.SetString("userName", item.UserName);
                            ViewBag.Message = "User logged in successfully!";

                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            string apiResponse1 = await response1.Content.ReadAsStringAsync();
                            string stringJWT = response1.Content.ReadAsStringAsync().Result;
                            JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);

                            HttpContext.Session.SetString("token", jwt.Token);
                            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(item));
                            HttpContext.Session.SetInt32("userID", item.UserID);
                            HttpContext.Session.SetString("userName", item.UserName);
                            ViewBag.Message = "User logged in successfully!";

                            return RedirectToAction("Index", "Voter");
                        }
                    }
                }
            }
        }

        public IActionResult Logout()
        {
            _log4net.Info("User Log Out");
            HttpContext.Session.Remove("token");
            // HttpContext.Session.SetString("user", null);

            return View("Login");
        }
    }
}