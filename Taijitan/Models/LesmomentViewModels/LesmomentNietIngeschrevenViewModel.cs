using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentNietIngeschrevenViewModel
    {
        public IEnumerable<Gebruiker> Gebruikers { get; set; }
        public Lesmoment Lesmoment  { get; set; }

        public LesmomentNietIngeschrevenViewModel(IEnumerable<Gebruiker> gebruikers, Lesmoment lesmoment)
        {
            this.Lesmoment = lesmoment;
            this.Gebruikers = gebruikers;
        }
    }
}
