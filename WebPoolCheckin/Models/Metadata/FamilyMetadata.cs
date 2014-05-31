using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPoolCheckin.Models
{
    [MetadataType(typeof(FamilyMetadata))]
    public partial class Family
    {
        //This class will be overridden by the entity framework generated class
        public virtual string NameAndShare
        {
            get
            {
                if (this.ShareFamilies.Any() && this.ShareFamilies.Single().Share != null)
                {
                    return this.ShareFamilies.Single().Share.Id + " - " + this.FamilyName;
                }
                else
                {
                    return this.FamilyName;
                }
            }
        }
    }

    internal sealed class FamilyMetadata
    {
    }
}