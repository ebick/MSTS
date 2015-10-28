using MASTS.Domain.ViewModels;
using System.Collections.Generic;

namespace MASTS.Domain.Abstract
{
    public interface IChangeLogRepository
    {
        IEnumerable<ChangeLog> ChangeLogs(int id);

        void SaveChangeLog(ChangeLog changeLog);

    }
}
