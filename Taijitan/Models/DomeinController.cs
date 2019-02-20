﻿using System;
using System.Collections.Generic;

namespace Taijitan.Models
{
    public class DomeinController
    {
        #region Fields
        private List<Lid> _leden;
        private Sessie _sessie;
        private Lid _actiefLid;
        #endregion

        #region Constructor
        public DomeinController()
        {
            // Change to repository
            _leden = new List<Lid>() {
                new Lid("user1", "pin", "LastName1", "FirstName1", new DateTime(1998,01,01), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1")),
                new Lid("user2", "pin2", "LastName2", "FirstName2", new DateTime(1998,05,15), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1")),
                new Lid("user3", "pin3", "LastName3", "FirstName3", new DateTime(1998,03,12), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1")),
                new Lid("user4", "pin4", "LastName4", "FirstName4", new DateTime(1998,07,12), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1")),
                new Lid("user5", "pin5", "LastName5", "FirstName5", new DateTime(1998,05,11), "052525252", "email1@email.be", new Adres("Belgium", "9000", "Gent", "Straat", "1"))};
            DateTime now = new DateTime();
            // Sessie met alle leden behalve laatste 2
            _sessie = new Sessie(now, now.AddHours(4), _leden.GetRange(0, _leden.Count-2));
        }
        #endregion

        #region Methods
        public void Login(String gebruikersnaam, String wachtwoord)
        {
            Lid lid = _leden.Find(l => l.Gebruikersnaam.Equals(gebruikersnaam));
            if (lid.Login(wachtwoord))
                _actiefLid = lid;
        }
        public void WijzigGegevens(String naam, String voornaam, DateTime geboortedatum, String telefoonnummer, String email, Adres adres)
        {
            _actiefLid.Naam = naam;
            _actiefLid.Voornaam = voornaam;
            _actiefLid.Geboortedatum = geboortedatum;
            _actiefLid.Telefoonnummer = telefoonnummer;
            _actiefLid.Email = email;
            _actiefLid.Adres = adres;
        }
        public void RegistreerAanwezigheid(String gebruikersnaam)
        {
            _sessie.RegistreerLid(_leden.Find(l => l.Gebruikersnaam.Equals(gebruikersnaam)));
        }
        public Sessie geefHuidigeSessie()
        {
            return this._sessie;
        }
        public Lid geefActiefLid()
        {
            return this._actiefLid;
        }
        #endregion
    }
}
