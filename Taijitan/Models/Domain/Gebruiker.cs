using System;
using System.Text.RegularExpressions;

namespace Taijitan.Models.Domain {
    public class Gebruiker {
        #region Fields
        private String _naam;
        private String _voornaam;
        private DateTime _geboortedatum;
        private String _telefoonnummer;
        private String _email;
        #endregion

        #region Properties
        public String Naam {
            get { return _naam; }
            set {
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
                if (String.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Voornaam mag geen lege waarde bevatten.");
                } else {
                    _voornaam = value;
                }
            }
        }
        public DateTime Geboortedatum {
            get { return _geboortedatum; }
            set {
                if (value.CompareTo(DateTime.Today) >= 0) {
                    throw new ArgumentException("Geboortedatum kan niet in de toekomst liggen.");
                } else {
                    _geboortedatum = value;
                }
            }
        }
        public String Telefoonnummer {
            get { return _telefoonnummer; }
            set {
                if (Regex.IsMatch(value, @"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))")) {
                    _telefoonnummer = value;
                } else {
                    throw new ArgumentException("Ongeldige waarde voor telefoonnummer.");
                }
            }
        }
        public String Email {
            get { return _email; }
            set {
                if (Regex.IsMatch(value, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")) {
                    _email = value;
                } else {
                    throw new ArgumentException("Ongeldige waarde voor e-mailadres.");
                }
            }
        }
        public Adres Adres { get; set; }
        #endregion

        #region Constructor
        public Gebruiker(String naam, String voornaam, DateTime geboortedatum, String telefoonnummer, String email, Adres adres) {
            this._naam = naam;
            this._voornaam = voornaam;
            this._geboortedatum = geboortedatum;
            this._telefoonnummer = telefoonnummer;
            this._email = email;
            this.Adres = adres;
        }

        public Gebruiker() {

        }
        #endregion

        #region Methods
        public void WijzigGegevens(String naam, String voornaam, DateTime geboortedatum, String telefoonnummer, String email, String land, String postcode, String stad, String straat, String nummer) {
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
