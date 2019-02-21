using System;
using System.Text.RegularExpressions;

namespace Taijitan.Models.Domain
{
    public class Adres
    {
        #region Properties
        public String Land { get; set; }
        public String Postcode { get; set; }
        public String Stad { get; set; }
        public String Straat { get; set; }
        public String Nummer { get; set; }
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
