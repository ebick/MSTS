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
    
    public partial class DriverMobileAssignement
    {
        public int DriverMobileAssignementID { get; set; }
        public int DriverID { get; set; }
        public int MobileUnitID { get; set; }
        public System.DateTime DateAssigned { get; set; }
        public Nullable<decimal> LeaseCostPerSettlementPeriod { get; set; }
        public Nullable<decimal> LeasePaymentMaximum { get; set; }
        public Nullable<decimal> LeasePaymentCollected { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime RecordAdded { get; set; }
        public string RecordAddedBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    
        public virtual MobileUnit MobileUnit { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
