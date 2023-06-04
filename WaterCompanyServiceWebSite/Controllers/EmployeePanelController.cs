using Microsoft.AspNetCore.Mvc;
using WaterCompanyServicesAPI;

namespace WaterCompanyServiceWebSite.Controllers
{
    public class EmployeePanelController : Controller
    {
        public IActionResult Index()
        {
            List<Request> pendingRequests = DataAccess.GetPendingRequests();
            return View(pendingRequests);
        }
    }
}
