using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Models.Domain
{
    public class Lesmoment
    {
        #region Properties
        public int LesmomentId { get; private set; }
        public DateTime StartTijd { get; private set; }
        public DateTime EindTijd { get; private set; }
        public List<LesmomentLeden> Leden { get; private set; }
        #endregion

        #region Constructor
        public Lesmoment(int lesmomentId, DateTime startTijd, DateTime eindTijd, List<LesmomentLeden> lesmomentLeden)
        {
            this.LesmomentId = lesmomentId;
            this.StartTijd = startTijd;
            this.EindTijd = eindTijd;
            // Maak nieuwe list als er geen list bestaat.
            if (lesmomentLeden?.Any() != true)
            {
                this.Leden = new List<LesmomentLeden>();
            }
            else
            {
                this.Leden = lesmomentLeden;
            }
        }

        public Lesmoment() {

        }
        #endregion

        #region Methods
        public void RegistreerLid(Gebruiker lid)
        {
            if(Leden.Exists(t => t.Gebruiker.Equals(lid)))
            {
                Leden.Single(t => t.Gebruiker.Equals(lid)).Aanwezig = true;
            }
            else
            {
                Leden.Add(new LesmomentLeden(this, lid));
            }
        }
        public List<Gebruiker> geefAanwezigeLeden()
        {
            return Leden.Where(t => t.Aanwezig).Select(t => t.Gebruiker).ToList();
        } 
        public List<Gebruiker> geefIngeschrevenLeden()
        {
            return Leden.Where(t => t.Ingeschreven).Select(t => t.Gebruiker).ToList();
        }
        #endregion
    }
}
