using System.Collections.Generic;

namespace MASTS.Domain.Abstract
{
    public interface IManagementEntitiesRepository
    {
        //SortedList<int, string> CompanyList();
        IEnumerable<ManagementEntity> CompanyList { get; }
        IEnumerable<ManagementEntity> ManagementEntities { get; }

        void SaveManagementEntity(ManagementEntity managementEntity);

        ManagementEntity DeleteManagementEntity(int managementEntityID);
    }
}
