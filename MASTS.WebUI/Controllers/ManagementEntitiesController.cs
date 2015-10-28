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
    public class ManagementEntitiesController : Controller
    {
        private IManagementEntitiesRepository repository;
        private IManagementEntitiesRepository repo2;
        // GET: ApplicationTable

        public ActionResult Index()
        {
            repository = new MASTS.Domain.Concrete.EFManagementEntityRepository();
            return View(repository.ManagementEntities);
        }

        public ManagementEntitiesController()
        {
            repository = new MASTS.Domain.Concrete.EFManagementEntityRepository();
            repo2      = new MASTS.Domain.Concrete.EFManagementEntityRepository();
        }
        //public ViewResult Index()
        //{
        //    return View(repository.ApplicationTables);
        //}
        public ViewResult Edit(int ManagementEntityID)
        {
            ManagementEntity ManagementEntity = repository.ManagementEntities
                .FirstOrDefault(p => p.ManagementEntityID == ManagementEntityID);
            //SelectList CompanyNameList = new SelectList(repo2.CompanyList);
            //ViewBag.CompanyList = CompanyNameList;
            return View(ManagementEntity);
        }

        [HttpPost]
        public ActionResult Edit(ManagementEntity ManagementEntity)
        {
            if (ModelState.IsValid)
            {

                repository.SaveManagementEntity(ManagementEntity);
                TempData["message"] = string.Format("{0} has been saved", ManagementEntity.Description);
                return RedirectToAction("Index");
            }
            else
            {
                //there is something wrong with the data values
                return View(ManagementEntity);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new ManagementEntity());
        }

        [HttpPost]
        public ActionResult Delete(int ManagementEntityId)
        {
            ManagementEntity deletedManagementEntity = repository.DeleteManagementEntity(ManagementEntityId);
            if (deletedManagementEntity != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedManagementEntity.Description);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Save(int ManagementEntityId)
        {
            return RedirectToAction("Index");
        }
    }

}