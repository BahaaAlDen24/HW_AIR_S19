//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HW_AIR_S19.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EQUESTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EQUESTION()
        {
            this.EQUESTIONTERMs = new HashSet<EQUESTIONTERM>();
        }
    
        public System.Guid ID { get; set; }
        public string VALUE { get; set; }
        public string ANSWER { get; set; }
        public Nullable<int> Indexed { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EQUESTIONTERM> EQUESTIONTERMs { get; set; }
    }
}
