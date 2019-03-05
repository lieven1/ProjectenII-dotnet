using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.LesmomentViewModels
{
    public class GebruikerVM
    {
        public Gebruiker Gebruiker { get; set; }
        public bool IsSelected { get; set; }

        public GebruikerVM(Gebruiker gebruiker)
        {
            this.Gebruiker = gebruiker;
            this.IsSelected = false;
        }
    }
}
