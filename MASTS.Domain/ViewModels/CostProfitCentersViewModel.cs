using System.ComponentModel.DataAnnotations;

namespace MASTS.Domain.ViewModels
{
    public class CostProfitCentersViewModel
    {
        [Key]
        [Required]
        public int CostCenterID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsCostCenter { get; set; }
    }
}