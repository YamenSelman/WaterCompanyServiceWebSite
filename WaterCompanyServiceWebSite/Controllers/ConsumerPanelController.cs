using Microsoft.AspNetCore.Mvc;
using WaterCompanyServicesAPI;

namespace WaterCompanyServiceWebSite.Controllers
{
    public class ConsumerPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            DataAccess.CurrentUser = null;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddSubscription()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSubscription(Subscription sub)
        {
            sub = DataAccess.getSubscriptionByBarcode(sub.ConsumerBarCode);
            if(sub == null)
            {
                ViewBag.Message = "No subscription with this barcode";
            }
            return View(sub);
        }
    }
}
