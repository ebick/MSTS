using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MASTS.Domain.ViewModels
{
    public class ApplicationTableAndFieldsViewModel
    {
        [Key]
        [Required]
        public int ParentTableID { get; set; }
        [Required]
        public string Description { get; set; }

        public List<ApplicationTableField> ApplicationTableFields { get; set; }
    }
}
