using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Sessie
    {
        #region Fields
        private List<Gebruiker> _leden;
        private List<Gebruiker> _ledenBuitenSessie;
        #endregion

        #region Properties
        public DateTime StartTijd { get; }
        public DateTime EindTijd { get; }
        public List<Gebruiker> IngeschrevenLeden { get; }
        #endregion

        #region Constructor
        public Sessie(DateTime startTijd, DateTime eindTijd, List<Gebruiker> ingeschrevenLeden)
        {
            this.StartTijd = startTijd;
            this.EindTijd = eindTijd;
            this._leden = new List<Gebruiker>();
            this.IngeschrevenLeden = ingeschrevenLeden;
        }
        #endregion

        #region Methods
        public void RegistreerLid(Gebruiker lid)
        {
            if (IngeschrevenLeden.Contains(lid))
            {
                _leden.Add(lid);
            }
            else
            {
                _ledenBuitenSessie.Add(lid);
            }
        }
        public List<Gebruiker> geefLeden()
        {
            return this._leden;
        } 
        public List<Gebruiker> geefLedenBuitenSessie()
        {
            return this._ledenBuitenSessie;
        }
        #endregion
    }
}
