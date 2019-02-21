using System;
using System.Text.RegularExpressions;

namespace Taijitan.Models.Domain
{
    public class Adres
    {
        #region Properties
        public String Land {
            get { return Land; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Land mag geen lege waarde bevatten.");
                }
                else
                {
                    Land = value;
                }
            }
        }
        public String Postcode
        {
            get { return Postcode; }
            private set
            {
                if(Regex.IsMatch(value, @"^\d{4}$"))
                {
                    Postcode = value;
                }
                else
                {
                    throw new ArgumentException("Ongeldige waarde voor postcode.");
                }
            }
        }
        public String Stad
        {
            get { return Stad; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Stad mag geen lege waarde bevatten.");
                }
                else
                {
                    Stad = value;
                }
            }
        }
        public String Straat
        {
            get { return Straat; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Straat mag geen lege waarde bevatten.");
                }
                else
                {
                    Straat = value;
                }
            }
        }
        public String Nummer
        {
            get { return Nummer; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nummer mag geen lege waarde bevatten.");
                }
                else
                {
                    Nummer = value;
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
