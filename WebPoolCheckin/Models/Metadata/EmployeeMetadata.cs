using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPoolCheckin.Models
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
        public virtual string FullName { get { return this.FirstName + " " + this.LastName; } }
    }

    internal sealed class EmployeeMetadata
    {
        [Required]
        public string FirstName;
        [Required]
        public string LastName;
        [Required]
        [Phone]
        public string PhoneNumber;
        [Required]
        public string EmployeeType;
    }
}