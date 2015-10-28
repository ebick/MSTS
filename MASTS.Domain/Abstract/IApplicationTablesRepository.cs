using System.Collections.Generic;

namespace MASTS.Domain.Abstract
{
    public interface IApplicationTablesRepository
    {
        ApplicationTableField GetApplicationTableFieldByID(int ID);
        ApplicationTable GetApplicationTableByID(int ID);

        void SaveApplicationTable(ViewModels.ApplicationTableAndFieldsViewModel applicationTableVM);
        void AddApplicationTableField(ApplicationTableField applicationTableField);

        ApplicationTable DeleteApplicationTable(int applicationTableID);
        ApplicationTableField DeleteApplicationTableField(int applicationTableFieldID);

        List<ViewModels.ApplicationTableAndFieldsViewModel> ApplicationTablesVM { get; }
        List<ViewModels.ApplicationTableAndFieldsViewModel> ApplicationTablesVMAll { get; }
        List<ViewModels.ApplicationTableAndFieldsViewModel> ApplicationTablesVMDeleted { get; }

        List<string> FieldTypes { get; }

    }
}
