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
    
    public partial class MobileUnit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MobileUnit()
        {
            this.DriverMobileAssignements = new HashSet<DriverMobileAssignement>();
        }
    
        public int MobileUnitID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string AccessNumber { get; set; }
        public bool IsPrivatelyOwned { get; set; }
        public bool IsCellularCapable { get; set; }
        public bool IsScanCapable { get; set; }
        public bool IsPhotoCapable { get; set; }
        public bool IsSignatureCaptureCapable { get; set; }
        public int YearManufactured { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsDamaged { get; set; }
        public bool IsBeingRepaired { get; set; }
        public bool IsUnderWarranty { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime RecordAdded { get; set; }
        public string RecordAddedBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverMobileAssignement> DriverMobileAssignements { get; set; }
    }
}
