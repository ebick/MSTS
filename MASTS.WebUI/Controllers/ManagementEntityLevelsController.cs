using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MASTS.Domain;
using MASTS.Domain.Abstract;
using MASTS.WebUI.Models;

namespace MASTS.WebUI.Controllers
{
    public class ManagementEntityLevelsController : Controller
    {
        private IManagementEntityLevelsRepository repository;
        // GET: ApplicationTable

        public ActionResult Index()
        {
            repository = new MASTS.Domain.Concrete.EFManagementEntityLevelRepository();
            return View(repository.ManagementEntityLevelByCompanyVM(1));
        }

        public ManagementEntityLevelsController()
        {
            repository = new MASTS.Domain.Concrete.EFManagementEntityLevelRepository();
        }
        //public ViewResult Index()
        //{
        //    return View(repository.ApplicationTables);
        //}
        public ViewResult Edit(int ManagementEntityLevelID)
        {
            ManagementEntityLevel ManagementEntityLevel = repository.ManagementEntityLevels
                .FirstOrDefault(p => p.ManagementEntityLevelID == ManagementEntityLevelID);
            return View(ManagementEntityLevel);
        }

        [HttpPost]
        public ActionResult Edit(ManagementEntityLevel ManagementEntityLevel)
        {
            if (ModelState.IsValid)
            {

                //repository.SaveManagementEntityLevel(ManagementEntityLevel);
                TempData["message"] = string.Format("{0} has been saved", ManagementEntityLevel.Description);
                return RedirectToAction("Index");
            }
            else
            {
                //there is something wrong with the data values
                return View(ManagementEntityLevel);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new ManagementEntityLevel());
        }

        [HttpPost]
        public ActionResult Delete(int ManagementEntityLevelId)
        {
            ManagementEntityLevel deletedManagementEntityLevel = repository.DeleteManagementEntityLevel(ManagementEntityLevelId);
            if (deletedManagementEntityLevel != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedManagementEntityLevel.Description);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Save(int ManagementEntityLevelId)
        {
            return RedirectToAction("Index");
        }
    }

}