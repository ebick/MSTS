using System.Linq;
using System.Web.Mvc;
using MASTS.Domain;
using MASTS.Domain.Abstract;
using MASTS.Domain.ViewModels;
using MASTS.Domain.Concrete;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MASTS.WebUI.Controllers
{
    public class ApplicationTableAndFieldsViewModelController : Controller
    {
        private IApplicationTablesRepository repository;
        // GET: ApplicationTable

        public ActionResult Index()
        {
            repository = new EFApplicationTableRepository();
            return View(repository.ApplicationTablesVM);
        }
        public ApplicationTableAndFieldsViewModelController()
        {
            repository = new EFApplicationTableRepository();
        }
        public ViewResult Edit(int parentTableID)
        {
            repository = new EFApplicationTableRepository();
            ApplicationTableAndFieldsViewModel applicationTable = repository.ApplicationTablesVM
                    .FirstOrDefault(p => p.ParentTableID == parentTableID);

            ViewBag.FieldTypeList = GetFieldTypeSelectList();
            return View(applicationTable);
        }

        private List<SelectListItem> GetFieldTypeSelectList()
        {
            IEnumerable<string> FieldTypeDrop = repository.FieldTypes;


            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var c in FieldTypeDrop)
            {
                SelectListItem i = new SelectListItem();
                i.Text = c;
                i.Value = c;
                selectList.Add(i);
            }
            return selectList;

        }

        [HttpPost]
        public ActionResult Edit(ApplicationTableAndFieldsViewModel a)
        {
            if (ModelState.IsValid)
            {
                if (a.ParentTableID == 0)
                {
                    repository.SaveApplicationTable(a);
                    return RedirectToAction("Index");
                }
                else
                {
                    //ApplicationTable at = repository.GetApplicationTableByID(a.ParentTableID);
                    //at.Description = a.Description;
                    repository.SaveApplicationTable(a);
                    TempData["message"] = string.Format("{0} has been saved", a.Description);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                //there is something wrong with the data values
                return View(a);
            }
        }

        public ViewResult EditChild(int id)
        {
            ApplicationTableField atf = new ApplicationTableField();
            atf.ApplicationTableID = id;
            atf.IsDeleted = false;
            ViewBag.FieldTypeList = GetFieldTypeSelectList();
            return View(atf);
        }

        [HttpPost]
        public ActionResult EditChild(ApplicationTableField applicationTableField)
        {
            if (ModelState.IsValid)
            {

                repository.AddApplicationTableField(applicationTableField);
                TempData["message"] = string.Format("{0} has been saved", applicationTableField.Description);
                return RedirectToAction("EditChild");
            }
            else
            {
                //there is something wrong with the data values
                return View(applicationTableField);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new ApplicationTableAndFieldsViewModel());
        }

        public ActionResult Delete(int id)
        {
            string MyTable = repository.GetApplicationTableByID(id).Description;
            if (MessageBox.Show("Are You Sure You Want To Delete " + MyTable + "?", "Delete Confirmation", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ApplicationTable deletedApplicationTable = repository.DeleteApplicationTable(id);
                if (deletedApplicationTable != null)
                {
                    TempData["message"] = string.Format("{0} was deleted", deletedApplicationTable.Description);
                }
            }
            else
            {
                MessageBox.Show(MyTable + " Not Deleted");
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteChild(int ApplicationTableFieldID)
        {
            string MyField = repository.GetApplicationTableFieldByID(ApplicationTableFieldID).Description;
            if (MessageBox.Show("Are You Sure You Want To Delete " + MyField + "?", "Delete Confirmation", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ApplicationTableField deletedApplicationTableField = repository.DeleteApplicationTableField(ApplicationTableFieldID);
                if (deletedApplicationTableField != null)
                {
                    TempData["message"] = string.Format("{0} was deleted", deletedApplicationTableField.Description);
                }
            }
            else
            {
                MessageBox.Show(MyField + " Not Deleted");
            }
            return RedirectToAction("Index");
        }
    }

}