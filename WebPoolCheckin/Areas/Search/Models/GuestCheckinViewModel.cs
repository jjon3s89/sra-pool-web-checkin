using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPoolCheckin.Models;

namespace WebPoolCheckin.Areas.Search.Models
{
    public class GuestCheckinViewModel
    {
        public int Id { get; set; }
        public int[] PersonIdList { get; set; }
        public Person Person { get; set; }
    }
}