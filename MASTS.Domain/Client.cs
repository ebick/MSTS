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
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.ClientLocationAliasTypes = new HashSet<ClientLocationAliasType>();
            this.ClientManagementUnits = new HashSet<ClientManagementUnit>();
        }
    
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientShortName { get; set; }
        public string HeadQuartersAddress1 { get; set; }
        public string HeadQuartersAddress2 { get; set; }
        public string HeadQuartersCity { get; set; }
        public string HeadQuartersState { get; set; }
        public string HeadQuartersZipCode { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime RecordAdded { get; set; }
        public string RecordAddedBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientLocationAliasType> ClientLocationAliasTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientManagementUnit> ClientManagementUnits { get; set; }
    }
}