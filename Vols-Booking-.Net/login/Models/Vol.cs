//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace login.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vol
    {
        public int Id { get; set; }
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
