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
    
    public partial class ProductTask
    {
        public int ProductTaskID { get; set; }
        public int ProductID { get; set; }
        public int TaskID { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime RecordAdded { get; set; }
        public string RecordAddedBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Task Task { get; set; }
    }
}
