using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eVotingClientApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eVotingClientApplication.Controllers
{
    public class AdminController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AdminController));
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");
                return RedirectToAction("Login","User");
            }
            else
            {
                _log4net.Info("Admin welcome Page");
                return View();
            }
        }

        public IActionResult AddContender()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");
                return RedirectToAction("Login", "User");
            }
            else
            {
                _log4net.Info("Add Contender Page");
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContender(Contender contender)
        {
            _log4net.Info("Adding Contender in progress"+contender.ContenderID);
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(contender), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync("https://localhost:44349/Contender/Add", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        contender = JsonConvert.DeserializeObject<Contender>(apiResponse);
                        _log4net.Info("Contender with Name - " +contender.ContenderID+" Added");
                    }
                }
                return RedirectToAction("Success");
            }
        }

        public IActionResult Success()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");

                return RedirectToAction("Login", "User");

            }
            else
            {
                _log4net.Info("Added successfully");
                return View();
            }
        }
    }
}