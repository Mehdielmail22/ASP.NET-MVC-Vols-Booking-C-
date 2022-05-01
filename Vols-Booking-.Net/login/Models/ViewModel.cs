using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace login.Models
{
    public class ViewModel
    { 
        public Client Clients { get; set; }
        public Vol Vols { get; set; }
    }
}