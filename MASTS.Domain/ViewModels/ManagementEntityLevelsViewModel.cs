using System.ComponentModel.DataAnnotations;

namespace MASTS.Domain.ViewModels
{
    public class ManagementEntityLevelsViewModel
    {
        [Key]
        public int ManagementEntityLevelID { get; set; }
        public int CompanyID { get; set; }
        public int LevelNumber { get; set; }
        public string Description { get; set; }
    }
}
