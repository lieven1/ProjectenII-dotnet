using System;
using System.Text.RegularExpressions;

namespace Taijitan.Models.Domain
{
    public class Adres
    {
        #region Properties
        public String Land {
            get { return Land; }
            set
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
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Postcode mag geen lege waarde bevatten.");
                }
                else
                {
                    Postcode = value;
                }
            }
        }
        public String Stad
        {
            get { return Stad; }
            set
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
            set
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
            set
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
    }
}
