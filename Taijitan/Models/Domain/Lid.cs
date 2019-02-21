using System;
using System.Text.RegularExpressions;

namespace Taijitan.Models.Domain
{
    public class Lid
    {
        #region Properties
        public String Gebruikersnaam { get; }
        public String Naam {
            get { return Naam; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Naam mag geen lege waarde bevatten.");
                }
                else
                {
                    Naam = value;
                }
            }
        }
        public String Voornaam
        {
            get { return Voornaam; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Voornaam mag geen lege waarde bevatten.");
                }
                else
                {
                    Voornaam = value;
                }
            }
        }
        public DateTime Geboortedatum
        {
            get { return Geboortedatum; }
            private set
            {
                if (value.CompareTo(DateTime.Today) >= 0)
                {
                    throw new ArgumentException("Geboortedatum kan niet in de toekomst liggen.");
                }
                else
                {
                    Geboortedatum = value;
                }
            }
        }
        public String Telefoonnummer
        {
            get { return Telefoonnummer; }
            private set
            {
                if (Regex.IsMatch(value, @"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))"))
                {
                    Telefoonnummer = value;
                }
                else
                {
                    throw new ArgumentException("Ongeldige waarde voor telefoonnummer.");
                }
            }
        }
        public String Email
        {
            get { return Email; }
            private set
            {
                if (Regex.IsMatch(value, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                {
                    Email = value;
                }
                else
                {
                    throw new ArgumentException("Ongeldige waarde voor e-mailadres.");
                }
            }
        }
        public Adres Adres { get; }
        #endregion

        #region Constructor
        public Lid(String gebruikersnaam, String naam, String voornaam, DateTime geboortedatum, String telefoonnummer, String email, Adres adres)
        {
            this.Gebruikersnaam = gebruikersnaam;
            this.Naam = naam;
            this.Voornaam = voornaam;
            this.Geboortedatum = geboortedatum;
            this.Telefoonnummer = telefoonnummer;
            this.Email = email;
            this.Adres = adres;
        }
        #endregion

        #region Methods
        public void WijzigGegevens(String naam, String voornaam, DateTime geboortedatum, String telefoonnummer, String email, String land, String postcode, String stad, String straat, String nummer)
        {
            this.Naam = naam;
            this.Voornaam = voornaam;
            this.Geboortedatum = geboortedatum;
            this.Telefoonnummer = telefoonnummer;
            this.Email = email;
            this.Adres.WijzigGegevens(land, postcode, stad, straat, nummer);
        }
        #endregion
    }
}
