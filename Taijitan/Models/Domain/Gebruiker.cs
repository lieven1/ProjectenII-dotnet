using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Taijitan.Models.Domain.Databindings;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain {
    public class Gebruiker : IdentityUser<Guid>{
        #region Fields
        private String _naam;
        private String _voornaam;
        private String _telefoonnummer;
        private String _gsmnummer;
        private String _email;
        private String _emailouders;
        #endregion

        #region Properties
        // Niet aanpasbaar
        public String Gebruikersnaam { get; private set; }
        // Niet aanpasbaar
        public String Rijksregisternummer { get; private set; }
        // Niet aanpasbaar
        public DateTime Inschrijvingsdatum { get; private set; }
        public String Naam {
            get { return _naam; }
            set {
                // Verplicht
                if (String.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Naam mag geen lege waarde bevatten.");
                } else {
                    _naam = value;
                }
            }
        }
        public String Voornaam {
            get { return _voornaam; }
            set {
                // Verplicht
                if (String.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Voornaam mag geen lege waarde bevatten.");
                } else {
                    _voornaam = value;
                }
            }
        }
        // Niet aanpasbaar
        public Geslacht Geslacht { get; private set; }
        // Niet aanpasbaar
        public DateTime Geboortedatum { get; private set; }
        // Niet aanpasbaar
        public String Geboorteplaats { get; private set; }
        public String Telefoonnummer
        {
            get { return _telefoonnummer; }
            set
            {
                // Niet verplicht
                if (String.IsNullOrWhiteSpace(value))
                {
                    _telefoonnummer = null;
                }
                // Voldoet aan regels voor telefoonnummer
                else if (Regex.IsMatch(value, @"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))"))
                {
                    _telefoonnummer = value;
                }
                else
                {
                    throw new ArgumentException("Ongeldige waarde voor telefoonnummer.");
                }
            }
        }
        public String Gsmnummer
        {
            get { return _gsmnummer; }
            set
            {
                // Verplicht en voldoet aan regels voor telefoonnummer
                if (Regex.IsMatch(value, @"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))"))
                {
                    _gsmnummer = value;
                }
                else
                {
                    throw new ArgumentException("Ongeldige waarde voor telefoonnummer.");
                }
            }
        }
        public String Email
        {
            get { return _email; }
            set
            {
                // Verplicht en voldoet aan regels voor Email
                if (Regex.IsMatch(value, @"^[\w-\.]+@([\w-]+\.)+[\w-]+$"))
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("Ongeldige waarde voor e-mailadres.");
                }
            }
        }
        public String EmailOuders
        {
            get { return _emailouders; }
            set
            {
                // Niet verplicht
                if (String.IsNullOrWhiteSpace(value))
                {
                    _emailouders = null;
                }
                // Voldoet aan regels voor Email
                if (Regex.IsMatch(value, @"^[\w-\.]+@([\w-]+\.)+[\w-]+$"))
                {
                    _emailouders = value;
                }
                else
                {
                    throw new ArgumentException("Ongeldige waarde voor e-mailadres.");
                }
            }
        }
        // Aanpasbaar via Adres.WijzigGegevens(...)
        public Adres Adres { get; private set; }
        // Niet aanpasbaar
        public int Punten { get; private set; }
        // Niet aanpasbaar
        public Gradatie Gradatie { get; private set; }
        // Niet aanpasbaar - vb=" Beheerder , Lid "
        public TypeGebruiker TypeGebruiker { get; private set; }
        // Property voor Intersectietabel
        //public List<LesmomentLeden> LesmomentLeden { get; set; }
        #endregion

        #region Constructor
        public Gebruiker(String gebruikersnaam, String rijksregisternummer, DateTime inschrijvingsdatum, String naam, String voornaam, Geslacht geslacht, DateTime geboortedatum, String geboorteplaats, String telefoonnummer, String gsmnummer, String email, String emailOuders, Adres adres, int punten, Gradatie gradatie, TypeGebruiker typeGebruiker) {
            this.Gebruikersnaam = gebruikersnaam;
            this.Rijksregisternummer = rijksregisternummer;
            this.Inschrijvingsdatum = inschrijvingsdatum;
            this._naam = naam;
            this._voornaam = voornaam;
            this.Geslacht = geslacht;
            this.Geboortedatum = geboortedatum;
            this.Geboorteplaats = geboorteplaats;
            this._telefoonnummer = telefoonnummer;
            this._gsmnummer = gsmnummer;
            this._email = email;
            this._emailouders = emailOuders;
            this.Adres = adres;
            this.Punten = punten;
            this.Gradatie = gradatie;
            this.TypeGebruiker = typeGebruiker;
        }

        public Gebruiker() {
        }
        #endregion

        #region Methods
        public void WijzigGegevens(String naam, String voornaam, String telefoonnummer, String gsmnummer, String email, String emailOuders, String land, String postcode, String stad, String straat, String nummer) {
            this.Naam = naam;
            this.Voornaam = voornaam;
            this.Telefoonnummer = telefoonnummer;
            this.Gsmnummer = gsmnummer;
            this.Email = email;
            this.EmailOuders = emailOuders;
            this.Adres.WijzigGegevens(land, postcode, stad, straat, nummer);
        }
        #endregion
    }
}
