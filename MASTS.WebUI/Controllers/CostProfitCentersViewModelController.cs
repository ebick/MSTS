using System.Linq;
using System.Web.Mvc;
using MASTS.Domain;
using MASTS.Domain.Abstract;
using MASTS.Domain.ViewModels;
using MASTS.Domain.Concrete;
using System.Windows.Forms;

namespace MASTS.WebUI.Controllers
{
    public class CostProfitCentersViewModelController : Controller
    {
        private ICostProfitCentersRepository repository;
        // GET: CostProfitCenterViewModel
        public ActionResult Index()
        {
            repository = new EFCostProfitCenterRepository();
            ViewBag.CurrentCompanyID = System.Web.HttpContext.Current.Session["CurrentCompanyID"];
            return View(repository.CostProfitCentersVM);
        }

        public CostProfitCentersViewModelController()
        {
            repository = new EFCostProfitCenterRepository();
        }
        public ViewResult Edit(int CostCenterID)
        {
            CostProfitCentersViewModel costCenter = repository.CostProfitCentersVM 
                    .FirstOrDefault(p => p.CostCenterID == CostCenterID);
            return View(costCenter);
        }

        [HttpPost]
        public ActionResult Edit(CostProfitCentersViewModel costCenter)
        {
            if (ModelState.IsValid)
            {
                if (costCenter.CostCenterID == 0)
                {
                    repository.SaveCostProfitCenter(costCenter);
                    return RedirectToAction("Index");
                }
                else
                {
                    repository.SaveCostProfitCenter(costCenter);
                    TempData["message"] = string.Format("{0} has been saved", costCenter.Description);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                //there is something wrong with the data values
                return View(costCenter);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new CostProfitCentersViewModel());
        }

        public ActionResult Delete(int CostCenterID)
        {
            string MyTable = repository.CostProfitCenters.FirstOrDefault(p => p.CostProfitCenterID == CostCenterID).Description;
            if (MessageBox.Show("Are You Sure You Want To Delete " + MyTable + "?", "Delete Confirmation", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                CostProfitCenter deletedCostCenter = repository.DeleteCostProfitCenter(CostCenterID);
                if (deletedCostCenter!= null)
                {
                    TempData["message"] = string.Format("{0} was deleted", deletedCostCenter.Description);
                }
            }
            else
            {
                MessageBox.Show(MyTable + " Not Deleted");
            }
            return RedirectToAction("Index");
        }
    }
}