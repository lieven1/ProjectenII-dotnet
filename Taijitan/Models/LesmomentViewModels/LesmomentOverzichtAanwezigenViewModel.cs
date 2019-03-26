using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.LesmomentViewModels
{
  
  

    public class LesmomentOverzichtAanwezigenViewModel
    {
        public Lesmoment Lesmoment { get; set; }
        public IEnumerable<Gebruiker> Gebruikers { get; set; }

        public LesmomentOverzichtAanwezigenViewModel(Lesmoment les, IEnumerable<Gebruiker> gebruikers)
        {
            Lesmoment = les;
            this.Gebruikers = gebruikers;
        }

    }
}
