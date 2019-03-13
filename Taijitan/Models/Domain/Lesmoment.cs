using System;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Models.Domain
{
    public class Lesmoment
    {
        #region Fields
        private DateTime _datum;
        private DateTime _startTijd;
        private DateTime _eindTijd;
        private bool _actief;
        #endregion

        #region Properties
        public int LesmomentId { get; private set; }
        public DateTime Datum
        {
            get { return _datum; }
            private set
            {
                if (DateTime.Now.CompareTo(value) < 0)
                {
                    throw new ArgumentException("Een lesmoment in het verleden kan niet meer gestart worden.");
                }
                else
                {
                    _datum = value;
                }
            }
        }
        public DateTime StartTijd
        {
            get { return _startTijd; }
            private set
            {
                // Startijd van een les kan niet meer dan 2h in het verleden liggen
                if (DateTime.Now.Subtract(value).TotalHours > 2)
                {
                    throw new ArgumentException("Een nieuw lesmoment kan niet langer dan twee uur geleden gestart zijn.");
                }
                else
                {
                    _startTijd = value;
                }
            }
        }
        public DateTime EindTijd
        {
            get { return _eindTijd; }
            private set
            {
                if (StartTijd.CompareTo(value) >= 0)
                {
                    throw new ArgumentException("Eindtijd kan niet voor starttijd plaatsvinden.");
                }
                else
                {
                    _eindTijd = value;
                }
            }
        }

        public bool Actief { get { return _actief; } private set { _actief = value; } }

        public List<LesmomentLeden> Leden { get; set; }
        #endregion

        #region Constructor
        public Lesmoment(int lesmomentId, DateTime datum, DateTime startTijd, DateTime eindTijd, List<LesmomentLeden> lesmomentLeden) : this(datum, startTijd, eindTijd, lesmomentLeden)
        {
            this.LesmomentId = lesmomentId;
            Actief = false;
        }

        public Lesmoment(DateTime datum, DateTime startTijd, DateTime eindTijd, List<LesmomentLeden> lesmomentLeden)
        {
            this.Datum = datum;
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
        public Lesmoment(DateTime startTijd, DateTime eindTijd, List<Gebruiker> leden)
        {
            this.Datum = DateTime.Today;
            this.StartTijd = startTijd;
            this.EindTijd = eindTijd;
            Actief = false;
            if (leden == null)
            {
                throw new ArgumentNullException("De parameter leden mag niet null zijn.");
            }
            else
            {
                this.Leden = new List<LesmomentLeden>();
                leden.ForEach(lid => Leden.Add(new LesmomentLeden(this, lid, true, false)));
            }


        }

        public Lesmoment(DateTime datum, DateTime start, DateTime eind)
        {
            this.Datum = datum;
            this.StartTijd = start;
            this.EindTijd = eind;
            this.Actief = false;
        }

        public Lesmoment()
        {

        }
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

        public void ZetActief(Boolean actief) {
            Actief = actief;
        }

        #endregion
    }
}
