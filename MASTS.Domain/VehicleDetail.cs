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
    
    public partial class VehicleDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleDetail()
        {
            this.DriverVehicles = new HashSet<DriverVehicle>();
        }
    
        public int VehicleDetailID { get; set; }
        public Nullable<int> VehicleInsurancePolicyID { get; set; }
        public string RegistrationState { get; set; }
        public string LicensePlate { get; set; }
        public System.DateTime RegistrationExpiration { get; set; }
        public bool IsYearRequired { get; set; }
        public Nullable<int> VehicleYear { get; set; }
        public bool IsMakeRequired { get; set; }
        public string VehicleMake { get; set; }
        public bool IsModelRequired { get; set; }
        public string VehicleModel { get; set; }
        public bool IsTrimRequired { get; set; }
        public string VehicleTrim { get; set; }
        public bool IsMileageRequired { get; set; }
        public Nullable<int> VehicleMileage { get; set; }
        public bool IsVehiclePriority { get; set; }
        public bool IsFreeFormDetailRequired { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime RecordAdded { get; set; }
        public string RecordAddedBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverVehicle> DriverVehicles { get; set; }
        public virtual VehicleInsurancePolicy VehicleInsurancePolicy { get; set; }
    }
}
