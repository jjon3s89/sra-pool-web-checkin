//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPoolCheckin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Entry
    {
        public int Id { get; set; }
        public System.DateTime Time { get; set; }
        public int Entry_Person { get; set; }
        public Nullable<bool> Is_exit { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
