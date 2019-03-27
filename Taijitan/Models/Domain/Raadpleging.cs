using System;

namespace Taijitan.Models.Domain
{
    public class Raadpleging
    {
        public int RaadplegingId { get; private set; }
        public DateTime Tijdstip { get; private set; }
        public Gebruiker Gebruiker { get; private set; }
        public Lesmateriaal Lesmateriaal { get; private set; }

        public Raadpleging(Lesmateriaal lesmateriaal, Gebruiker gebruiker, DateTime tijdstip) {
            this.Lesmateriaal = lesmateriaal;
            this.Gebruiker = gebruiker;
            this.Tijdstip = tijdstip;
        }

        public Raadpleging() { }
    }
}
