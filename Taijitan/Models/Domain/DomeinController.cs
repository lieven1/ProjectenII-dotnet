using System;
using System.Collections.Generic;

namespace Taijitan.Models.Domain
{
    public class DomeinController
    {
        #region Fields
        private List<Gebruiker> _leden;
        private Sessie _sessie;
        private Gebruiker _actiefLid;
        #endregion

        #region Constructor
        public DomeinController()
        {
            // Change to repository
            _leden = new List<Gebruiker>() {
                new Gebruiker("LastName1", "FirstName1", new DateTime(1998,01,01), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1")),
                new Gebruiker("LastName2", "FirstName2", new DateTime(1998,05,15), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1")),
                new Gebruiker("LastName3", "FirstName3", new DateTime(1998,03,12), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1")),
                new Gebruiker("LastName4", "FirstName4", new DateTime(1998,07,12), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1")),
                new Gebruiker("LastName5", "FirstName5", new DateTime(1998,05,11), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1"))};
            DateTime now = new DateTime();
            // Sessie met alle leden behalve laatste 2
            _sessie = new Sessie(now, now.AddHours(4), _leden.GetRange(0, _leden.Count-2));
        }
        #endregion

        #region Methods
        public void Login(String gebruikersnaam, String wachtwoord)
        {
            // identity framework
        }
        public void WijzigGegevens(String naam, String voornaam, DateTime geboortedatum, String telefoonnummer, String email, String land, String postcode, String stad, String straat, String nummer)
        {
            _actiefLid.WijzigGegevens(naam, voornaam, geboortedatum, telefoonnummer, email, land, postcode, stad, straat, nummer);
        }
        public void RegistreerAanwezigheid(String email)
        {
            _sessie.RegistreerLid(_leden.Find(l => l.Email.Equals(email)));
        }
        public Sessie geefHuidigeSessie()
        {
            return this._sessie;
        }
        public Gebruiker geefActiefLid()
        {
            return this._actiefLid;
        }
        #endregion
    }
}
