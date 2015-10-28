using System;
using System.Collections.Generic;
using System.Linq;
using MASTS.Domain.Abstract;
using MASTS.Domain.ViewModels;

namespace MASTS.Domain.Concrete
{
    public class EFManagementEntityLevelRepository : IManagementEntityLevelsRepository
    {
        private MSTSEntities context = new MSTSEntities();

        public IEnumerable<ManagementEntityLevel> ManagementEntityLevels
        {
            get { return context.ManagementEntityLevels.Where(a => a.IsDeleted == false); }
        }
        public IEnumerable<ManagementEntityLevel> ManagementEntityLevelsDeleted
        {
            get { return context.ManagementEntityLevels.Where(a => a.IsDeleted == true); }
        }
        public IEnumerable<ManagementEntityLevel> ManagementEntityLevelsAll
        {
            get { return context.ManagementEntityLevels; }
        }

        public void SaveManagementEntityLevel(ManagementEntityLevelsViewModel managementEntityLevelVM)
        {


            if (managementEntityLevelVM.ManagementEntityLevelID == 0)
            {
                ManagementEntityLevel o = new ManagementEntityLevel();
                o.LastModified = DateTime.UtcNow;
                o.LastModifiedBy = "System";
                o.RecordAdded = DateTime.UtcNow;
                o.RecordAddedBy = "System";
                o.IsDeleted = false;

                o.CompanyID = managementEntityLevelVM.CompanyID;
                o.Description = managementEntityLevelVM.Description;
                o.LevelNumber = managementEntityLevelVM.LevelNumber;

                context.ManagementEntityLevels.Add(o);
            }
            else
            {
                ManagementEntityLevel dbEntry = context.ManagementEntityLevels.Find(managementEntityLevelVM.ManagementEntityLevelID);
                if (dbEntry != null)
                {
                    dbEntry.LastModified = DateTime.UtcNow;
                    dbEntry.LastModifiedBy = "System";

                    dbEntry.Description = managementEntityLevelVM.Description;
                    dbEntry.CompanyID = managementEntityLevelVM.CompanyID;
                    dbEntry.LevelNumber = managementEntityLevelVM.LevelNumber;

                }
            }
            context.SaveChanges();
        }

        public ManagementEntityLevel DeleteManagementEntityLevel(int managementEntityLevelID)
        {
            ManagementEntityLevel dbEntry = context.ManagementEntityLevels.Find(managementEntityLevelID);
            if (dbEntry != null)
            {
                //context.ManagementEntityLevels.Remove(dbEntry);
                dbEntry.IsDeleted = true;
                dbEntry.LastModified = DateTime.UtcNow;
                dbEntry.LastModifiedBy = "System";
                context.SaveChanges();
            }
            return dbEntry;
        }
        public List<ManagementEntityLevelsViewModel> ManagementEntityLevelVM
        {
            get
            {
                List<ManagementEntityLevelsViewModel> l = new List<ManagementEntityLevelsViewModel>();
                var query = from p in context.ManagementEntityLevels
                            where p.IsDeleted == false
                            orderby p.Description
                            select p;

                foreach (var item in query)
                {
                    ManagementEntityLevelsViewModel m = new ManagementEntityLevelsViewModel();
                    m.ManagementEntityLevelID  = item.ManagementEntityLevelID;
                    m.Description = item.Description;
                    m.CompanyID= item.CompanyID;
                    m.LevelNumber = item.LevelNumber;
                    l.Add(m);
                }
                return l;
            }

        }

        public List<ManagementEntityLevelsViewModel> ManagementEntityLevelByCompanyVM(int CompanyID)
        {
            List<ManagementEntityLevelsViewModel> l = new List<ManagementEntityLevelsViewModel>();
            var query = from p in context.ManagementEntityLevels
                        where p.IsDeleted == false && p.CompanyID == CompanyID 
                        orderby p.Description
                        select p;

            foreach (var item in query)
            {
                ManagementEntityLevelsViewModel m = new ManagementEntityLevelsViewModel();
                m.ManagementEntityLevelID = item.ManagementEntityLevelID;
                m.Description = item.Description;
                m.CompanyID = item.CompanyID;
                m.LevelNumber = item.LevelNumber;
                l.Add(m);
            }
            return l;
        }
    }
}
