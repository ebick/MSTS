using System.Collections.Generic;

namespace MASTS.Domain.Abstract
{
    public interface IManagementUnitsRepository
    {
        IEnumerable<ManagementUnit> ManagementUnits { get; }

        void SaveManagementUnit(ManagementUnit managementUnit);

        ManagementUnit DeleteManagementUnit(int managementUnitID);
    }
}
