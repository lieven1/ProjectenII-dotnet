using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Sessie
    {
        #region Fields
        private List<Lid> _leden;
        private List<Lid> _ledenBuitenSessie;
        #endregion

        #region Properties
        public DateTime StartTijd { get; }
        public DateTime EindTijd { get; }
        public List<Lid> IngeschrevenLeden { get; }
        #endregion

        #region Constructor
        public Sessie(DateTime startTijd, DateTime eindTijd, List<Lid> ingeschrevenLeden)
        {
            this.StartTijd = startTijd;
            this.EindTijd = eindTijd;
            this._leden = new List<Lid>();
            this.IngeschrevenLeden = ingeschrevenLeden;
        }
        #endregion

        #region Methods
        public void RegistreerLid(Lid lid)
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
        public List<Lid> geefLeden()
        {
            return this._leden;
        } 
        public List<Lid> geefLedenBuitenSessie()
        {
            return this._ledenBuitenSessie;
        }
        #endregion
    }
}
