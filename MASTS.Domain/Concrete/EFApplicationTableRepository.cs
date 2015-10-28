using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MASTS.Domain.Abstract;

namespace MASTS.Domain.Concrete
{
    public class EFApplicationTableRepository : IApplicationTablesRepository
    {
        private MSTSEntities context = new MSTSEntities();

        public List<string> FieldTypes
        {
            get
            {
                List<string> l = new List<string>();
                l.Add("bit");
                l.Add("char");
                l.Add("date");
                l.Add("datetime");
                l.Add("decimal");
                l.Add("geography");
                l.Add("image");
                l.Add("int");
                l.Add("money");
                l.Add("time");
                l.Add("varchar");
                return l;
            }
        }

        public ApplicationTableField GetApplicationTableFieldByID(int ID)
        {
            ApplicationTableField atf = context.ApplicationTableFields.Find(ID);
            return atf;
        }
        public ApplicationTable GetApplicationTableByID(int ID)
        {
            ApplicationTable at = context.ApplicationTables.Find(ID);
            return at;
        }

        public List<ViewModels.ApplicationTableAndFieldsViewModel> ApplicationTablesVM
        {
            get
            {
                List<ViewModels.ApplicationTableAndFieldsViewModel> l = new List<ViewModels.ApplicationTableAndFieldsViewModel>();
                l = PopulateList("Active");
        
                return l;
            }
        }

        public List<ViewModels.ApplicationTableAndFieldsViewModel> ApplicationTablesVMAll
        {
            get
            {
                List<ViewModels.ApplicationTableAndFieldsViewModel> l = new List<ViewModels.ApplicationTableAndFieldsViewModel>();
                l = PopulateList("All");
                return l;
            }
        }

        public List<ViewModels.ApplicationTableAndFieldsViewModel> ApplicationTablesVMDeleted
        {
            get
            {
                List<ViewModels.ApplicationTableAndFieldsViewModel> l = new List<ViewModels.ApplicationTableAndFieldsViewModel>();
                l = PopulateList("Deleted");
                return l;
            }
        }

        private List<ViewModels.ApplicationTableAndFieldsViewModel> PopulateList(string Mode)
        {
            List<ViewModels.ApplicationTableAndFieldsViewModel> RetValue = new List<ViewModels.ApplicationTableAndFieldsViewModel>();
            var query = from p in context.ApplicationTables select p;

            if (Mode == "Active")
            {
                query = from p in context.ApplicationTables
                            where p.IsDeleted == false
                            orderby p.Description
                            select p;

            }
            else if (Mode =="All")
            {
                query = from p in context.ApplicationTables
                            orderby p.Description
                            select p;

            }
            else
            {
                query = from p in context.ApplicationTables
                            where p.IsDeleted == true
                            orderby p.Description
                            select p;

            }
            foreach (var item in query)
            {
                ViewModels.ApplicationTableAndFieldsViewModel m = new ViewModels.ApplicationTableAndFieldsViewModel();

                m.ParentTableID = item.ApplicationTableID;
                m.Description = item.Description;
                m.ApplicationTableFields = new List<ApplicationTableField>();

                var query2 = from c in context.ApplicationTableFields
                             where c.ApplicationTableID == m.ParentTableID && c.IsDeleted == false
                             orderby c.Description
                             select c;

                foreach (var c_item in query2)
                {
                    ApplicationTableField ct = new ApplicationTableField();
                    ct.ApplicationTableFieldID = c_item.ApplicationTableFieldID;
                    ct.Description = c_item.Description;
                    ct.ApplicationTableID = c_item.ApplicationTableID;
                    ct.FieldType = c_item.FieldType;
                    ct.IsAudited = c_item.IsAudited;
                    ct.LastModified = c_item.LastModified;
                    ct.LastModifiedBy = c_item.LastModifiedBy;
                    ct.RecordAdded = c_item.RecordAdded;
                    ct.RecordAddedBy = c_item.RecordAddedBy;
                    m.ApplicationTableFields.Add(ct);
                }
                RetValue.Add(m);
            }
            return RetValue;
        }

        public void SaveApplicationTable(ViewModels.ApplicationTableAndFieldsViewModel applicationTableAndFieldVM)
        {


            if (applicationTableAndFieldVM.ParentTableID == 0)
            {
                ApplicationTable at = new ApplicationTable();
                at.Description = applicationTableAndFieldVM.Description;
                at.LastModified = DateTime.UtcNow;
                at.LastModifiedBy = "System";
                at.RecordAdded = DateTime.UtcNow;
                at.RecordAddedBy = "System";
                at.IsDeleted = false;

                List<ApplicationTableField> l = (List <ApplicationTableField>)at.ApplicationTableFields;
                context.ApplicationTables.Add(at);
                context.SaveChanges();

                foreach (ApplicationTableField atf in l)
                {
                    atf.ApplicationTableID = at.ApplicationTableID;
                    atf.LastModified = DateTime.UtcNow;
                    atf.LastModifiedBy = "System";
                    atf.RecordAdded = DateTime.UtcNow;
                    atf.RecordAddedBy = "System";
                    atf.IsDeleted = false;
                    context.ApplicationTableFields.Add(atf);
                }
                context.SaveChanges();
            }
            else
            {
                ApplicationTable dbEntry = context.ApplicationTables.Find(applicationTableAndFieldVM.ParentTableID);
                if (dbEntry != null)
                {
                    bool SaveIsRequired = false;
                    if (applicationTableAndFieldVM.Description != dbEntry.Description)
                    {
                        SaveIsRequired = true;
                        dbEntry.Description = applicationTableAndFieldVM.Description;

                        dbEntry.LastModified = DateTime.UtcNow;
                        dbEntry.LastModifiedBy = "System";
                    }

                    foreach (ApplicationTableField atf in applicationTableAndFieldVM.ApplicationTableFields)
                    {
                        if (atf.ApplicationTableFieldID == 0)
                        {
                            SaveIsRequired = true;
                            //this is a new entry                            
                            atf.LastModified = DateTime.UtcNow;
                            atf.LastModifiedBy = "System";
                            atf.RecordAdded = DateTime.UtcNow;
                            atf.RecordAddedBy = "System";
                            atf.IsDeleted = false;
                            context.ApplicationTableFields.Add(atf);
                        }
                        else
                        {
                            //first we have to find the original record and compare it and see if any of the 
                            //user controlled fields have changed
                            ApplicationTableField dbAtf = context.ApplicationTableFields.Find(atf.ApplicationTableFieldID);

                            if (atf.Description != dbAtf.Description || atf.FieldType != null || atf.IsAudited != dbAtf.IsAudited)
                            {
                                SaveIsRequired = true;
                                //there's been some relevant change
                                dbAtf.Description = atf.Description;
                                dbAtf.IsAudited = atf.IsAudited;

                                if (atf.FieldType != null)
                                {
                                    dbAtf.FieldType = atf.FieldType;
                                }

                                dbAtf.LastModified = DateTime.UtcNow;
                                dbAtf.LastModifiedBy = "System";

                            }
                        }
                    }

                    if (SaveIsRequired)
                    {
                        context.SaveChanges();
                    }
                }
                
            }
            
        }

        public void AddApplicationTableField(ApplicationTableField applicationTableField)
        {
            applicationTableField.LastModified = DateTime.UtcNow;
            applicationTableField.LastModifiedBy = "System";
            applicationTableField.RecordAdded = DateTime.UtcNow;
            applicationTableField.RecordAddedBy = "System";
            applicationTableField.IsDeleted = false;
            context.ApplicationTableFields.Add(applicationTableField);
            context.SaveChanges();
        }


        public ApplicationTable DeleteApplicationTable(int applicationTableID)
        {
            ApplicationTable dbEntry = context.ApplicationTables.Find(applicationTableID);
            if (dbEntry != null)
            {
                //context.ApplicationTables.Remove(dbEntry);
                dbEntry.IsDeleted = true;
                dbEntry.LastModified = DateTime.UtcNow;
                dbEntry.LastModifiedBy = "System";
                var query = from c in context.ApplicationTableFields
                            where c.ApplicationTableID == applicationTableID && c.IsDeleted == false
                            select c;

                foreach (var item in query)
                {
                    item.IsDeleted = true;
                    item.LastModified = DateTime.UtcNow;
                    item.LastModifiedBy = "System";
                }
                context.SaveChanges();
            }
            return dbEntry;
        }
        public ApplicationTableField DeleteApplicationTableField(int applicationTableFieldID)
        {
            ApplicationTableField dbEntry = context.ApplicationTableFields.Find(applicationTableFieldID);
            if (dbEntry != null)
            {
                //context.ApplicationTables.Remove(dbEntry);
                dbEntry.IsDeleted = true;
                dbEntry.LastModified = DateTime.UtcNow;
                dbEntry.LastModifiedBy = "System";
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
