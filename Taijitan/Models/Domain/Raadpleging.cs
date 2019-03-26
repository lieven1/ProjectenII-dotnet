using System;

namespace Taijitan.Models.Domain
{
    public class Raadpleging
    {
        public int RaadplegingId { get; private set; }
        public DateTime Tijdstip { get; private set; }
        public string Gebruikersnaam { get; private set; }
        public int LesmateriaalId { get; private set; }

        public Raadpleging(int lesmateriaalId, string gebruikersnaam, DateTime tijdstip) {
            this.LesmateriaalId = lesmateriaalId;
            this.Gebruikersnaam = gebruikersnaam;
            this.Tijdstip = tijdstip;
        }
    }
}
