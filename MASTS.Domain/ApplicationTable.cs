//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MASTS.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class ApplicationTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApplicationTable()
        {
            this.CustomFields = new HashSet<CustomField>();
            this.ApplicationTableFields = new HashSet<ApplicationTableField>();
        }
    
        public int ApplicationTableID { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime RecordAdded { get; set; }
        public string RecordAddedBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomField> CustomFields { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationTableField> ApplicationTableFields { get; set; }
    }
}
