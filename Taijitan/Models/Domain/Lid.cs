using System;

namespace Taijitan.Models.Domain
{
    public class Lid
    {
        #region Fields
        private readonly String _gebruikersnaam;
        private readonly String _wachtwoord;
        #endregion

        #region Properties
        public String Gebruikersnaam { get { return _gebruikersnaam; } }
        public String Naam { get; set; }
        public String Voornaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public String Telefoonnummer { get; set; }
        public String Email { get; set; }
        public Adres Adres { get; set; }
        #endregion

        #region Constructor
        public Lid(String gebruikersnaam, String wachtwoord, String naam, String voornaam, DateTime geboortedatum, string telefoonnummer, String email, Adres adres)
        {
            _gebruikersnaam = gebruikersnaam;
            _wachtwoord = wachtwoord;
            Naam = naam;
            Voornaam = voornaam;
            Geboortedatum = geboortedatum;
            Telefoonnummer = telefoonnummer;
            Email = email;
            Adres = adres;
        }
        #endregion

        #region Methods
        public Boolean Login(String wachtwoord)
        {
            return _wachtwoord.Equals(wachtwoord);
        }
        #endregion
    }
}
