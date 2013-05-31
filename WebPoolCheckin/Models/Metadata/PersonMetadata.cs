using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPoolCheckin.Models
{
    [MetadataType(typeof(PersonMetadata))]
    public partial class Person
    {
        //This class will be overridden by the entity framework generated class
        public virtual string FullName { get { return this.FirstName + " " + this.LastName; } }
    }

    internal sealed class PersonMetadata
    {
        [Required]
        public string FirstName;
        [Required]
        public string LastName;
    }
}