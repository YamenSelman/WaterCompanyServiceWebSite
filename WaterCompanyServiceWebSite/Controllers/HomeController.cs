﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WaterCompanyServicesAPI;
using WaterCompanyServiceWebSite.Models;

namespace WaterCompanyServiceWebSite.Controllers
{
    public class HomeController : Controller
    {
        Random rnd = new Random();
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if(DataAccess.CurrentUser != null)
            {
                return RedirectUser(DataAccess.CurrentUser);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            User loginUser = DataAccess.Login(user);
            if (loginUser == null)
            {
                ViewBag.Message = "Username or password incorrect";
                return View();
            }
            else if (!loginUser.AccountActive)
            {
                ViewBag.Message = "This account is not active";
                return View();
            }
            else
            {
                DataAccess.CurrentUser = loginUser;
                return RedirectUser(loginUser);
            }
        }

        private IActionResult RedirectUser(User user)
        {
            switch (user.UserType)
            {
                case "admin":
                    return RedirectToAction("Index", "AdminPanel");
                case "employee":
                    return RedirectToAction("Index", "EmployeePanel");
                case "consumer":
                    return RedirectToAction("Index", "ConsumerPanel");
                default:
                    return View("Error");
            }
        }

        public IActionResult Register()
        {
            if (DataAccess.CurrentUser != null)
            {
                return RedirectUser(DataAccess.CurrentUser);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(Consumer consumer,String passwordConfirm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if(DataAccess.UserNameExists(consumer.User.UserName))
                {
                    ViewBag.Message = "User name already exists";
                    return View(consumer);
                }
                else if (consumer.User.Password != passwordConfirm)
                {
                    ViewBag.Message = "Password does not match";
                    return View(consumer);
                }
                else
                {
                    consumer.User.UserType = "consumer";
                    consumer.User.AccountActive = false;
                    DataAccess.AddConsumer(consumer);
                    return View("RegisterSuccess");
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}