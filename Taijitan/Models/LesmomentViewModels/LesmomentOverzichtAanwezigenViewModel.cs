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
        public IEnumerable<Gebruiker> AanwezigeGebruikers { get; set; }
        public IEnumerable<Gebruiker> AfwezigeGebruikers { get; set; }

        public LesmomentOverzichtAanwezigenViewModel(Lesmoment les, IEnumerable<Gebruiker> aanwezigeGebruikers, IEnumerable<Gebruiker> afwezigeGebruikers)
        {
            Lesmoment = les;
            this.AanwezigeGebruikers = aanwezigeGebruikers;
            this.AfwezigeGebruikers = afwezigeGebruikers;
        }

    }
}
