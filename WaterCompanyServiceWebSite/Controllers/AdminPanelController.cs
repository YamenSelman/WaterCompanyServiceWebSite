using Microsoft.AspNetCore.Mvc;

namespace WaterCompanyServiceWebSite.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            DataAccess.CurrentUser = null;
            return RedirectToAction("Index","Home");
        }

        public IActionResult UserManagement()
        {
            var data = DataAccess.GetUsers();
            return View(data);
        }
    }
}
