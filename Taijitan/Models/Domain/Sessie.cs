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
        #endregion

        #region Properties
        public DateTime StartTijd { get; private set; }
        public DateTime EindTijd { get; private set; }
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

        public Sessie() {

        }
        #endregion

        #region Methods
        public void RegistreerLid(Gebruiker lid)
        {
            _leden.Add(lid);
        }
        public List<Gebruiker> geefLeden()
        {
            return this._leden;
        } 
        public List<Gebruiker> geefLedenBuitenSessie()
        {
            return this._leden.Except(this.IngeschrevenLeden).ToList();
        }
        #endregion
    }
}
