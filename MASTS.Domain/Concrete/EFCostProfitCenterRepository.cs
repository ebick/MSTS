using System;
using System.Collections.Generic;
using System.Linq;
using MASTS.Domain.Abstract;
using MASTS.Domain.ViewModels;

namespace MASTS.Domain.Concrete
{
    public class EFCostProfitCenterRepository : ICostProfitCentersRepository
    {
        private MSTSEntities context = new MSTSEntities();

        public IEnumerable<CostProfitCenter> CostProfitCenters
        {
            get { return context.CostProfitCenters.Where(a => a.IsDeleted == false); }
        }
        public IEnumerable<CostProfitCenter> CostProfitCentersDeleted
        {
            get { return context.CostProfitCenters.Where(a => a.IsDeleted == true); }
        }
        public IEnumerable<CostProfitCenter> CostProfitCentersAll
        {
            get { return context.CostProfitCenters; }
        }

        public void SaveCostProfitCenter(CostProfitCentersViewModel CostProfitCenterVM)
        {

            DateTime stamp = DateTime.UtcNow;

            if (CostProfitCenterVM.CostCenterID == 0)
            {
                CostProfitCenter c = new CostProfitCenter();
                c.LastModified = stamp;
                c.LastModifiedBy = "System";
                c.RecordAdded = stamp;
                c.RecordAddedBy = "System";
                c.Description = CostProfitCenterVM.Description;
                c.IsCostCenter = CostProfitCenterVM.IsCostCenter;
                c.IsDeleted= false;
                context.CostProfitCenters.Add(c);
                context.SaveChanges();
                Audit(c, stamp);
            }
            else
            {
                CostProfitCenter dbEntry = context.CostProfitCenters.Find(CostProfitCenterVM.CostCenterID);
                if (dbEntry != null)
                {
                    Audit(dbEntry, CostProfitCenterVM, stamp);
                    dbEntry.LastModified = stamp;
                    dbEntry.LastModifiedBy = "System";
                    dbEntry.Description = CostProfitCenterVM.Description;
                    dbEntry.IsCostCenter = CostProfitCenterVM.IsCostCenter;
                    context.SaveChanges();
                }
            }
        }

        private void Audit(CostProfitCenter InDB, CostProfitCentersViewModel NewData, DateTime stamp)
        {
            var query = from p in context.ApplicationTableFields
                        where p.IsDeleted == false && p.ApplicationTableID == 11 && p.IsAudited == true
                        orderby p.Description
                        select p;

            foreach (var item in query)
            {
                ApplicationTableField f = context.ApplicationTableFields.Find(item.ApplicationTableFieldID);

                ChangeLog cl = new ChangeLog();
                cl.ApplicationTableFieldID = item.ApplicationTableFieldID;
                cl.RecordID = InDB.CostProfitCenterID;
                cl.ChangeMade = stamp;
                cl.UserName = "System";

                switch (f.Description)
                {
                    case "Description":
                        cl.BeforeValue = InDB.Description;
                        cl.AfterValue = NewData.Description;
                        break;
                    case "IsCostCenter":
                        cl.BeforeValue = InDB.IsCostCenter.ToString();
                        cl.AfterValue = NewData.IsCostCenter.ToString();
                        break;
                }
                IChangeLogRepository repo = new EFChangeLogRepository();
                repo.SaveChangeLog(cl);
            }
        }

        private void Audit(CostProfitCenter c, DateTime stamp)
        {
            var query = from p in context.ApplicationTableFields
                        where p.IsDeleted == false && p.ApplicationTableID == 11 && p.IsAudited == true
                        orderby p.Description
                        select p;

            foreach (var item in query)
            {
                ApplicationTableField f = context.ApplicationTableFields.Find(item.ApplicationTableFieldID);

                ChangeLog cl = new ChangeLog();
                cl.ApplicationTableFieldID = item.ApplicationTableFieldID;
                cl.RecordID = c.CostProfitCenterID;
                cl.ChangeMade = stamp;
                cl.UserName = "System";
                cl.BeforeValue = "NewRecord";

                switch (f.Description)
                {
                    case "Description":
                        cl.AfterValue = c.Description;
                        break;
                    case "IsCostCenter":
                        cl.AfterValue = c.IsCostCenter.ToString();
                        break;
                }
                IChangeLogRepository repo = new EFChangeLogRepository();
                repo.SaveChangeLog(cl);
            }
        }

        public CostProfitCenter DeleteCostProfitCenter(int CostProfitCenterID)
        {
            CostProfitCenter dbEntry = context.CostProfitCenters.Find(CostProfitCenterID);

            if (dbEntry != null)
            {
                //context.CostProfitCenterLevels.Remove(dbEntry);
                dbEntry.IsDeleted = true;
                dbEntry.LastModified = DateTime.UtcNow;
                dbEntry.LastModifiedBy = "System";
                context.SaveChanges();
            }
            return dbEntry;
        }

        public List<ViewModels.CostProfitCentersViewModel> CostProfitCentersVM
        {
            get
            {
                List<ViewModels.CostProfitCentersViewModel> l = new List<ViewModels.CostProfitCentersViewModel>();
                var query = from p in context.CostProfitCenters
                            where p.IsDeleted == false
                            orderby p.Description
                            select p;

                foreach (var item in query)
                {
                    ViewModels.CostProfitCentersViewModel m = new ViewModels.CostProfitCentersViewModel();
                    m.CostCenterID = item.CostProfitCenterID;
                    m.Description = item.Description;
                    m.IsCostCenter = item.IsCostCenter;
                    l.Add(m);
                }
                return l;
            }

        }
    }
}
