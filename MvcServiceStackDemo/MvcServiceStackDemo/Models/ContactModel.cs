using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcServiceStackDemo.Models
{
    public class ContactModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Comment { get; set; }
    }
}