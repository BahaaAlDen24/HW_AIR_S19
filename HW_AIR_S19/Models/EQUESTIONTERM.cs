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
    
    public partial class EQUESTIONTERM
    {
        public System.Guid ID { get; set; }
        public System.Guid QUESTIONID { get; set; }
        public System.Guid TERMID { get; set; }
        public string TF { get; set; }
        public string WEIGHT { get; set; }
    
        public virtual EQUESTION EQUESTION { get; set; }
        public virtual ETERM ETERM { get; set; }
    }
}
