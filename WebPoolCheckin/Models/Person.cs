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
    
    public partial class Person
    {
        public Person()
        {
            this.Is_Guest = false;
            this.Entries = new HashSet<Entry>();
        }
    
        public int Id { get; internal set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
        public string MiddleName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string InsuranceCompany { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public Nullable<int> Family_Person { get; set; }
        public Nullable<int> FamilyMemberType_Person { get; set; }
        public Nullable<bool> Is_Guest { get; set; }
        public bool Is_Minor { get; set; }
        public Nullable<bool> Needs_Adult { get; set; }
    
        public virtual ICollection<Entry> Entries { get; set; }
        public virtual Family Family { get; set; }
        public virtual FamilyMemberType FamilyMemberType { get; set; }
    }
}
