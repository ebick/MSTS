using System;
using System.Collections.Generic;
using System.Linq;
using MASTS.Domain.Abstract;
using MASTS.Domain.ViewModels;

namespace MASTS.Domain.Concrete
{
    public class EFChangeLogRepository : IChangeLogRepository
    {
        private MSTSEntities context = new MSTSEntities();

        public IEnumerable<ChangeLog> ChangeLogs(int id)
        {
            return context.ChangeLogs.Where(a => a.ApplicationTableFieldID == id);
        }

        public void SaveChangeLog(ChangeLog changeLog)
        {
            context.ChangeLogs.Add(changeLog);
            context.SaveChanges();
        }

    }
}
