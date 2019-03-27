using System;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Models.Domain
{
    public class Lesmoment
    {
        #region Properties
        public int LesmomentId { get; private set; }

        public DateTime StartTijd { get; private set; }
        public DateTime EindTijd { get; private set; }

        public bool Actief { get; private set; }

        public List<LesmomentLeden> Leden { get; set; }
        #endregion

        #region Constructor
        public Lesmoment(DateTime startTijd, DateTime eindTijd, List<LesmomentLeden> lesmomentLeden)
        {
            this.StartTijd = startTijd;
            this.EindTijd = eindTijd;
            Actief = false;
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
        public Lesmoment(DateTime startTijd, DateTime eindTijd, List<Gebruiker> leden) {
            this.StartTijd = startTijd;
            this.EindTijd = eindTijd;
            Actief = false;
            if (leden == null) {
                throw new ArgumentNullException("De parameter leden mag niet null zijn.");
            } else {
                this.Leden = new List<LesmomentLeden>();
                leden.ForEach(lid => Leden.Add(new LesmomentLeden(this, lid, true, false)));
            }
        }

        public Lesmoment(DateTime start, DateTime eind)
        {
            this.StartTijd = start;
            this.EindTijd = eind;
            this.Actief = false;
        }

        public Lesmoment(){}
        #endregion

        #region Methods
        public void RegistreerLid(Gebruiker lid)
        {
            // null check
            if (lid == null)
            {
                throw new ArgumentNullException("De parameter mag niet null zijn.");
            }

            if (Leden.Exists(t => t.Gebruiker.Equals(lid)))
            {
                Leden.Single(t => t.Gebruiker.Equals(lid)).Aanwezig = true;
            }
            else
            {
                // Niet ingeschreven + wel aanwezig
                Leden.Add(new LesmomentLeden(this, lid, false, true));
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

        public void ZetActief(bool actief) {
            Actief = actief;
        }

        public bool EersteHelftIsVoorbij()
        {
            var start = StartTijd;
            var eind = EindTijd;
            return start.Add(eind.Subtract(start) / 2).CompareTo(DateTime.Now) < 0;
        }
        #endregion
    }
}
