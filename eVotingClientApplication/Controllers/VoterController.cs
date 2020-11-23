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
    public class VoterController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AdminController));
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");
                return RedirectToAction("Login", "User");
            }
            else
            {
                _log4net.Info("Voting Page");
                return View();
            }
        }

        public async Task<IActionResult> Contenders()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");
                return RedirectToAction("Login", "User");
            }
            else
            {
                _log4net.Info("Contenders Page");
                List<Contender> contenders = new List<Contender>();
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44349/Contender/GetAll"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        contenders = JsonConvert.DeserializeObject<List<Contender>>(apiResponse);
                    }
                }

                return View(contenders);
            }
        }
        public IActionResult Vote(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");
                return RedirectToAction("Login", "User");

            }
            else
            {
                _log4net.Info("Vote Page");
                ViewBag.VoterID =HttpContext.Session.GetInt32("userID");
                ViewBag.ContenderID = id;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Vote(Vote vote)
        {
            _log4net.Info("Voting in progress");
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
                    StringContent content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync("https://localhost:44327/Vote/Add", content))
                    {
                        _log4net.Info(response);
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        _log4net.Info(apiResponse);
                        Vote vote1 = JsonConvert.DeserializeObject<Vote>(apiResponse);
                        //_log4net.Info("Vote with Id - " + vote1.VoteID.ToString() + " Added");
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
                _log4net.Info("Vote Page");
                return View();
            }
        }
    }
}