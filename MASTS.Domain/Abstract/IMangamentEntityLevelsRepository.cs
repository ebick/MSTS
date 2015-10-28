using MASTS.Domain.ViewModels;
using System.Collections.Generic;

namespace MASTS.Domain.Abstract
{
    public interface IManagementEntityLevelsRepository
    {
        IEnumerable<ManagementEntityLevel> ManagementEntityLevels { get; }

        void SaveManagementEntityLevel(ManagementEntityLevelsViewModel managementEntityLevelVM);

        ManagementEntityLevel DeleteManagementEntityLevel(int managementEntityLevelID);

        List<ManagementEntityLevelsViewModel> ManagementEntityLevelVM { get; }
        List<ManagementEntityLevelsViewModel> ManagementEntityLevelByCompanyVM(int CompanyID);
    }
}
