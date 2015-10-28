using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MASTS.Domain.Abstract;
using MASTS.Domain;

namespace MASTS.Domain.Concrete
{
    public class EFManagementUnitRepository : IManagementUnitsRepository
    {
        private MSTSEntities context = new MSTSEntities();

        public IEnumerable<ManagementUnit> ManagementUnits
        {
            get { return context.ManagementUnits.Where(a => a.IsDeleted == false); }
        }

        public IEnumerable<ManagementUnit> ManagementUnitsAll
        {
            get { return context.ManagementUnits; }
        }
        public IEnumerable<ManagementUnit> ManagementUnitsDeleted
        {
            get { return context.ManagementUnits.Where(a => a.IsDeleted == true); }
        }

        public void SaveManagementUnit(ManagementUnit managementUnit)
        {


            if (managementUnit.ManagementUnitID == 0)
            {
                managementUnit.LastModified = DateTime.UtcNow;
                managementUnit.LastModifiedBy = "System";
                managementUnit.RecordAdded = DateTime.UtcNow;
                managementUnit.RecordAddedBy = "System";
                context.ManagementUnits.Add(managementUnit);
            }
            else
            {
                ManagementUnit dbEntry = context.ManagementUnits.Find(managementUnit.ManagementUnitID);
                if (dbEntry != null)
                {
                    dbEntry.LastModified = DateTime.UtcNow;
                    dbEntry.LastModifiedBy = "System";
                    dbEntry.Description = managementUnit.Description;
                    dbEntry.CostCenterID  = managementUnit.CostCenterID ;
                    dbEntry.ManagementEntityID  = managementUnit.ManagementEntityID;
                }
            }
            context.SaveChanges();
        }

        public ManagementUnit DeleteManagementUnit(int managementUnitID)
        {
            ManagementUnit dbEntry = context.ManagementUnits.Find(managementUnitID);
            if (dbEntry != null)
            {
                //context.ManagementUnitLevels.Remove(dbEntry);
                dbEntry.IsDeleted = true;
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
