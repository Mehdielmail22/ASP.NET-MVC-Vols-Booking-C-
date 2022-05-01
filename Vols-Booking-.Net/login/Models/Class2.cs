using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class Class2
    {
        public Nullable<int> Nbr_max { get; set; }
        public string Destination { get; set; }
        public string Depart { get; set; }
        public Nullable<int> Prix { get; set; }
        public Nullable<System.DateTime> Time_depart { get; set; }
        public Nullable<System.DateTime> Time_arrive { get; set; }
        public Nullable<System.DateTime> Date_depart { get; set; }
        public Nullable<System.DateTime> Date_arrive { get; set; }
    }
}