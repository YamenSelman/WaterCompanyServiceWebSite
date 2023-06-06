using Microsoft.AspNetCore.Mvc;
using WaterCompanyServicesAPI;
using WaterCompanyServicesAPI.Models;
using WaterCompanyServiceWebSite.Models;

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
            ViewRequestObj obj = new ViewRequestObj();
            obj.Request = DataAccess.GetRequest(id);
            obj.Log = new RequestsLog();

            if (obj.Request != null)
            {
                return View(obj);
            }
            else
            {
                List<Request> pendingRequests = DataAccess.GetPendingRequests();
                return View("Index",pendingRequests);
            }
        }

        [HttpPost]
        public IActionResult ProcessRequest(ViewRequestObj obj)
        {
            if(obj.Request != null)
            {
                if(obj.Log.Decision)
                {
                    if (DataAccess.AcceptRequest(obj.Request.Id, obj.Log.Notes))
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
                    if(DataAccess.RejectRequest(obj.Request.Id,obj.Log.Notes))
                    {
                        ViewBag.Message = "Success";
                    }
                    else
                    {
                        ViewBag.Message = "Fail";
                    }
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
