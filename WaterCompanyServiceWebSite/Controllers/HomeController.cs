using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WaterCompanyServicesAPI;
using WaterCompanyServiceWebSite.Models;

namespace WaterCompanyServiceWebSite.Controllers
{
    public class HomeController : Controller
    {
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
            return View();
        }

        [HttpPost]
        public IActionResult Verify(User user)
        {
            User loginUser = DataAccess.Login2(user);
            if (loginUser == null || !loginUser.AccountActive)
            {
                return View("Login");
            }
            else
            {
                switch (loginUser.UserType)
                {
                    case "admin":
                        return View("AdminPanel");
                    case "employe":
                        return View("EmployePanel");
                    case "consumner":
                        return View("ConsumerPanel");
                    default:
                        return View("Error");
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