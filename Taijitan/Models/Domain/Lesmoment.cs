using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Lesmoment
    {
        #region Properties
        public int LesmomentId { get; private set; }
        public DateTime StartTijd { get; private set; }
        public DateTime EindTijd { get; private set; }
        public List<Gebruiker> IngeschrevenLeden { get; private set; }
        public List<Gebruiker> AanwezigeLeden { get; private set; }
        #endregion

        #region Constructor
        public Lesmoment(int lesmomentId, DateTime startTijd, DateTime eindTijd, List<Gebruiker> ingeschrevenLeden, List<Gebruiker> aanwezigeLeden)
        {
            this.LesmomentId = lesmomentId;
            this.StartTijd = startTijd;
            this.EindTijd = eindTijd;
            // Maak nieuwe list als er geen list bestaat.
            if (aanwezigeLeden?.Any() != true)
            {
                this.AanwezigeLeden = new List<Gebruiker>();
            }
            else
            {
                this.AanwezigeLeden = aanwezigeLeden;
            }
            this.IngeschrevenLeden = ingeschrevenLeden;
        }

        public Lesmoment() {

        }
        #endregion

        #region Methods
        public void RegistreerLid(Gebruiker lid)
        {
            AanwezigeLeden.Add(lid);
        }
        public List<Gebruiker> geefLedenBinnenSessie()
        {
            return AanwezigeLeden.Intersect(IngeschrevenLeden).ToList();
        } 
        public List<Gebruiker> geefLedenBuitenSessie()
        {
            return AanwezigeLeden.Except(this.IngeschrevenLeden).ToList();
        }
        #endregion
    }
}
