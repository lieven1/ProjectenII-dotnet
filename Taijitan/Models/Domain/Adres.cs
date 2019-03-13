using System;
using System.Text.RegularExpressions;

namespace Taijitan.Models.Domain
{
    public class Adres
    {
        #region Fields
        private String _land;
        private String _postcode;
        private String _stad;
        private String _straat;
        private String _nummer;
        #endregion

        #region Properties
        public int AdresId { get; set; }
        public String Land {
            get { return _land; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Land mag geen lege waarde bevatten.");
                }
                else
                {
                    _land = value;
                }
            }
        }
        public String Postcode
        {
            get { return _postcode; }
            private set
            {
                if(String.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^\d{4}$"))
                {
                    throw new ArgumentException("Ongeldige waarde voor postcode.");
                }
                else
                {
                    _postcode = value;
                }
            }
        }
        public String Stad
        {
            get { return _stad; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Stad mag geen lege waarde bevatten.");
                }
                else
                {
                    _stad = value;
                }
            }
        }
        public String Straat
        {
            get { return _straat; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Straat mag geen lege waarde bevatten.");
                }
                else
                {
                    _straat = value;
                }
            }
        }
        public String Nummer
        {
            get { return _nummer; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nummer mag geen lege waarde bevatten.");
                }
                else
                {
                    _nummer = value;
                }
            }
        }
        #endregion

        #region Constructor
        public Adres(String land, String postcode, String stad, String straat, String nummer)
        {
            this.Land = land;
            this.Postcode = postcode;
            this.Stad = stad;
            this.Straat = straat;
            this.Nummer = nummer;
        }
        public Adres() {

        }
        #endregion
        
        #region Methods
        public void WijzigGegevens(String land, String postcode, String stad, String straat, String nummer)
        {
            this.Land = Land;
            this.Postcode = postcode;
            this.Stad = stad;
            this.Straat = straat;
            this.Nummer = nummer;
        }
        #endregion
    }
}
