using MASTS.Domain.ViewModels;
using System.Collections.Generic;

namespace MASTS.Domain.Abstract
{
    public interface ICostProfitCentersRepository
    {
        IEnumerable<CostProfitCenter> CostProfitCenters { get; }

        void SaveCostProfitCenter(CostProfitCentersViewModel costProfitCenterVM);

        CostProfitCenter DeleteCostProfitCenter(int costProfitCenterID);

        List<CostProfitCentersViewModel> CostProfitCentersVM { get; }
    }
}
