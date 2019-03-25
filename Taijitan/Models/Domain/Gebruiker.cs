using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Taijitan.Models.Domain.Databindings;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain
{
    public class Gebruiker
    {
        #region Fields
        private string _naam;
        private string _voornaam;
        private string _telefoonnummer;
        private string _gsmnummer;
        private string _email;
        private string _emailouders;
        #endregion

        #region Properties
        // Niet aanpasbaar
        public string Gebruikersnaam { get; private set; }
        // Niet aanpasbaar
        public string Rijksregisternummer { get; private set; }
        // Niet aanpasbaar
        public DateTime Inschrijvingsdatum { get; private set; }
        public string Naam
        {
            get { return _naam; }
            private set
            {
                // Verplicht
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Naam mag geen lege waarde bevatten.");
                }
                else
                {
                    _naam = value;
                }
            }
        }
        public string Voornaam
        {
            get { return _voornaam; }
            private set
            {
                // Verplicht
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Voornaam mag geen lege waarde bevatten.");
                }
                else
                {
                    _voornaam = value;
                }
            }
        }
        // Niet aanpasbaar
        public Geslacht Geslacht { get; private set; }
        // Niet aanpasbaar
        public DateTime Geboortedatum { get; private set; }
        // Niet aanpasbaar
        public string Geboorteplaats { get; private set; }
        public string Telefoonnummer
        {
            get { return _telefoonnummer; }
            private set
            {
                // Niet verplicht
                if (string.IsNullOrWhiteSpace(value))
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
        public string Gsmnummer
        {
            get { return _gsmnummer; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Gsmnummer moet ingevuld zijn.");
                }
                // Verplicht en voldoet aan regels voor telefoonnummer
                else if (Regex.IsMatch(value, @"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))"))
                {
                    _gsmnummer = value;
                }
                else
                {
                    throw new ArgumentException("Ongeldige waarde voor telefoonnummer.");
                }
            }
        }
        public string Email
        {
            get { return _email; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email moet ingevuld zijn.");
                }
                // Verplicht en voldoet aan regels voor Email
                else if (Regex.IsMatch(value, @"^[\w-\.]+@([\w-]+\.)+[\w-]+$"))
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("Ongeldige waarde voor e-mailadres.");
                }
            }
        }
        public string EmailOuders
        {
            get { return _emailouders; }
            private set
            {
                // Niet verplicht
                if (string.IsNullOrWhiteSpace(value))
                {
                    _emailouders = null;
                }
                // Voldoet aan regels voor Email
                else if (Regex.IsMatch(value, @"^[\w-\.]+@([\w-]+\.)+[\w-]+$"))
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
        // Niet aanpasbaar
        public Lesformule Lesformule { get; private set; }
        // Property voor Intersectietabel
        public List<LesmomentLeden> Lesmomenten { get; private set; }
        #endregion

        #region Constructor
        public Gebruiker(string gebruikersnaam, string rijksregisternummer, DateTime inschrijvingsdatum, string naam, string voornaam, Geslacht geslacht, DateTime geboortedatum, string geboorteplaats, string telefoonnummer, string gsmnummer, string email, string emailOuders, Adres adres, int punten, Gradatie gradatie, TypeGebruiker typeGebruiker, Lesformule lesformule)
        {
            this.Gebruikersnaam = gebruikersnaam;
            this.Rijksregisternummer = rijksregisternummer;
            this.Inschrijvingsdatum = inschrijvingsdatum;
            this.Naam = naam;
            this.Voornaam = voornaam;
            this.Geslacht = geslacht;
            this.Geboortedatum = geboortedatum;
            this.Geboorteplaats = geboorteplaats;
            this.Telefoonnummer = telefoonnummer;
            this.Gsmnummer = gsmnummer;
            this.Email = email;
            this.EmailOuders = emailOuders;
            this.Adres = adres;
            this.Punten = punten;
            this.Gradatie = gradatie;
            this.TypeGebruiker = typeGebruiker;
            this.Lesformule = lesformule;
        }

        // ProeflesGebruiker
        public Gebruiker(DateTime inschrijvingsdatum, TypeGebruiker type, string gebruikersnaam, string naam, string voornaam, string email, string telefoonnummer)
        {
            this.Inschrijvingsdatum = inschrijvingsdatum;
            this.Gebruikersnaam = gebruikersnaam;
            this.Naam = naam;
            this.Voornaam = voornaam;
            this.Email = email;
            this.Telefoonnummer = telefoonnummer;
            this.TypeGebruiker = type;
        }

        public Gebruiker(){}
        #endregion

        #region Methods
        public void WijzigGegevens(string naam, string voornaam, string telefoonnummer, string gsmnummer, string email, string emailOuders, string land, string postcode, string stad, string straat, string nummer)
        {
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
