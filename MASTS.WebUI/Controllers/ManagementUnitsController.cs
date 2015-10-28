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
    public class ManagementUnitsController : Controller
    {
        private IManagementUnitsRepository repository;
        private IManagementEntitiesRepository  repoMgmtEntities;
        private ICostProfitCentersRepository repoCostCenter;


        // GET: ApplicationTable

        public ActionResult Index()
        {
            repository = new MASTS.Domain.Concrete.EFManagementUnitRepository();
            SortedList<int, string> EntityList = BuildTableList("Entity");
            ViewBag.EntityList = EntityList;
            SortedList<int, string> CostCenterList = BuildTableList("CostCenter");
            ViewBag.CostCenterList = CostCenterList;
            return View(repository.ManagementUnits);
        }

        private SortedList<int, string> BuildTableList(string mode)
        {
            SortedList<int, string> X = new SortedList<int, string>();
            if (mode =="Entity")
            {
                repoMgmtEntities = new MASTS.Domain.Concrete.EFManagementEntityRepository();
                foreach (ManagementEntity  a in repoMgmtEntities.ManagementEntities)
                {
                    X.Add(a.ManagementEntityID, a.Description);
                }
            }
            else
            {
                repoCostCenter = new MASTS.Domain.Concrete.EFCostProfitCenterRepository();
                foreach (CostProfitCenter a in repoCostCenter.CostProfitCenters )
                {
                    X.Add(a.CostProfitCenterID, a.Description);
                }
            }
            return X;

        }

        public ManagementUnitsController()
        {
            repository = new MASTS.Domain.Concrete.EFManagementUnitRepository();
        }
        //public ViewResult Index()
        //{
        //    return View(repository.ApplicationTables);
        //}
        public ViewResult Edit(int ManagementUnitID)
        {
            repoMgmtEntities = new MASTS.Domain.Concrete.EFManagementEntityRepository();
            repoCostCenter = new MASTS.Domain.Concrete.EFCostProfitCenterRepository();

            ManagementUnit ManagementUnit = repository.ManagementUnits
                .FirstOrDefault(p => p.ManagementUnitID == ManagementUnitID);
            ViewBag.ManagementEntityID = new SelectList(repoMgmtEntities.ManagementEntities, "ManagementEntityID", "Description", ManagementUnit.ManagementEntityID);
            ViewBag.CostCenterID = new SelectList(repoCostCenter.CostProfitCenters, "CostProfitCenterID", "Description", ManagementUnit.CostCenterID);
            return View(ManagementUnit);
        }

        [HttpPost]
        public ActionResult Edit(ManagementUnit ManagementUnit)
        {
            if (ModelState.IsValid)
            {

                repository.SaveManagementUnit(ManagementUnit);
                TempData["message"] = string.Format("{0} has been saved", ManagementUnit.Description);
                return RedirectToAction("Index");
            }
            else
            {
                //there is something wrong with the data values
                return View(ManagementUnit);
            }
        }
        public ViewResult Create()
        {
            repoMgmtEntities = new MASTS.Domain.Concrete.EFManagementEntityRepository();
            repoCostCenter = new MASTS.Domain.Concrete.EFCostProfitCenterRepository();

            ViewBag.ManagementEntityID = new SelectList(repoMgmtEntities.ManagementEntities, "ManagementEntityID", "Description");
            ViewBag.CostCenterID = new SelectList(repoCostCenter.CostProfitCenters, "CostProfitCenterID", "Description");

            return View("Edit", new ManagementUnit());
        }

        [HttpPost]
        public ActionResult Delete(int ManagementUnitId)
        {
            ManagementUnit deletedManagementUnit = repository.DeleteManagementUnit(ManagementUnitId);
            if (deletedManagementUnit != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedManagementUnit.Description);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Save(int ManagementUnitId)
        {
            return RedirectToAction("Index");
        }
    }

}