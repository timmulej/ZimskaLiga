using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZimskaLiga.repository;
using ZimskaLiga.services;

namespace ZimskaLiga.Controllers
{
    public class HomeController : Controller
    {
        //private Int32 IdUser = new();

        private readonly ILogin _loginUser;

        public HomeController(ILogin loguser)
        {
            _loginUser = loguser;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string passcode)
        {
            var issuccess = _loginUser.AuthenticateUser(username, passcode); 

            if (issuccess.Result != null)
            {
                Console.WriteLine(username);
                if (username == "merilec")
                {
                    ViewBag.username = string.Format("Successfully logged-in", username);

                    return RedirectToAction("Index", "Measure");
                }
                else
                {
                    int idUser = AuthenticateLogin.GetUserId(username, passcode);
                    ViewBag.username = string.Format("Successfully logged-in", username);


                    return RedirectToAction("Index", "Layout", new { IdUser = idUser });
                }
            }
            else
            {
                ViewBag.username = string.Format("Login Failed ", username);
                return View();
            }
        }

    }
}