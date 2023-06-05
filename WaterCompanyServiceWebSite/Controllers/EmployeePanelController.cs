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

        public IActionResult ViewRequest(int id)
        {
            Request req = DataAccess.GetRequest(id);
            if(req != null)
            {
                return View(req);
            }
            else
            {
                List<Request> pendingRequests = DataAccess.GetPendingRequests();
                return View("Index",pendingRequests);
            }
        }

        public IActionResult AcceptRequest(int rid)
        {
            var m = DataAccess.GetRequest(rid);
            if(m != null)
            {
                if(DataAccess.AcceptRequest(m.Id))
                {
                    ViewBag.Message = "Success";
                }
                else
                {
                    ViewBag.Message = "Fail";
                }
            }
            else
            {
                ViewBag.Message = "RequestNotFound";
            }
            return RedirectToAction("index");
        }
    }
}
