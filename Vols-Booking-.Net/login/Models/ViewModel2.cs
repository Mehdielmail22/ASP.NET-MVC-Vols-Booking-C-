using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class ViewModel2
    {
        public Client Clients { get; set; }
        public IQueryable<Vol> Vols { get; set; }
    }
}