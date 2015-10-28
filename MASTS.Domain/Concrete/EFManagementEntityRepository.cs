using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MASTS.Domain.Abstract;
using MASTS.Domain;

namespace MASTS.Domain.Concrete
{
    public class EFManagementEntityRepository : IManagementEntitiesRepository
    {
        private MSTSEntities context = new MSTSEntities();

        //public SortedList<int, string> CompanyList()
        //{
        //    SortedList<int, string> sl = new SortedList<int, string>();
        //    var query = from p in context.ManagementEntities
        //                join l in context.ManagementEntityLevels on p.ManagementEntityLevelID equals l.ManagementEntityLevelID where l.LevelNumber == 2 select p;

        //    foreach (var item in query)
        //    {
        //        sl.Add(item.CompanyID, item.Description);
        //    }
        //    return sl;
        //}
        public IEnumerable<ManagementEntity> CompanyList
        {
            get { return context.ManagementEntities.Where(a => a.IsDeleted == false && a.ManagementEntityLevelID == 2); }
        }

        public IEnumerable<ManagementEntity> ManagementEntities
        {
            get { return context.ManagementEntities.Where(a => a.IsDeleted == false); }
        }
        public IEnumerable<ManagementEntity> ManagementEntitiesDeleted
        {
            get { return context.ManagementEntities.Where(a => a.IsDeleted == true); }
        }
        public IEnumerable<ManagementEntity> ManagementEntitiesAll
        {
            get { return context.ManagementEntities; }
        }

        public void SaveManagementEntity(ManagementEntity managementEntity)
        {


            if (managementEntity.ManagementEntityID == 0)
            {
                managementEntity.LastModified = DateTime.UtcNow;
                managementEntity.LastModifiedBy = "System";
                managementEntity.RecordAdded = DateTime.UtcNow;
                managementEntity.RecordAddedBy = "System";
                context.ManagementEntities.Add(managementEntity);
            }
            else
            {
                ManagementEntity dbEntry = context.ManagementEntities.Find(managementEntity.ManagementEntityID);
                if (dbEntry != null)
                {
                    dbEntry.LastModified = DateTime.UtcNow;
                    dbEntry.LastModifiedBy = "System";
                    dbEntry.Description = managementEntity.Description;
                    dbEntry.CanContainManagementUnits = managementEntity.CanContainManagementUnits;
                    dbEntry.CompanyID = managementEntity.CompanyID;
                }
            }
            context.SaveChanges();
        }

        public ManagementEntity DeleteManagementEntity(int managementEntityID)
        {
            ManagementEntity dbEntry = context.ManagementEntities.Find(managementEntityID);
            if (dbEntry != null)
            {
                //context.ManagementEntityLevels.Remove(dbEntry);
                dbEntry.IsDeleted = true;
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
