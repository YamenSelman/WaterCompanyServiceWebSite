using Microsoft.AspNetCore.Mvc;
using WaterCompanyServicesAPI;

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

        public IActionResult EditUser(int id)
        {
            User user = DataAccess.GetUser(id);
            user.AccountActive = !user.AccountActive;
            DataAccess.UpdateUser(user);
            return RedirectToAction("UserManagement", "AdminPanel");
        }

    }
}
