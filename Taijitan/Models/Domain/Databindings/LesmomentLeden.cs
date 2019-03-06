using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain.Databindings
{
    public class LesmomentLeden
    {
        public int LesmomentId { get; set; }
        public Lesmoment Lesmoment { get; set; }
        public string Gebruikersnaam { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public Boolean Ingeschreven { get; set; }
        public Boolean Aanwezig { get; set; }

        public LesmomentLeden(Lesmoment lesmoment, Gebruiker gebruiker, Boolean ingeschreven = false, Boolean aanwezig = false)
        {
            this.Lesmoment = lesmoment;
            this.Gebruiker = gebruiker;
            this.Ingeschreven = ingeschreven;
            this.Aanwezig = aanwezig;
        }
        public LesmomentLeden() {

        }
    }
}
