using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MASTS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MASTS.Domain.Concrete.EFManagementEntityRepository repo = new MASTS.Domain.Concrete.EFManagementEntityRepository();
            System.Web.HttpContext.Current.Session["CurrentCompanyID"] = 1;
            ViewBag.CompanyID = new SelectList(repo.CompanyList, "CompanyID", "Description");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}