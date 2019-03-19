using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain {
    public class Thema {
        private string _naam;
        private List<Lesmateriaal> _lesmateriaal;

        public List<Lesmateriaal> Lesmateriaal { get { return _lesmateriaal; } private set { this._lesmateriaal = value; } }

        public Thema(string naam) {
            this._naam = naam;
            this._lesmateriaal = new List<Lesmateriaal>();
        }
        public Thema(string naam, List<Lesmateriaal> lesmateriaal) {
            this._naam = naam;
            this._lesmateriaal = lesmateriaal;
        }

        public void voegLesMateriaalToe(Lesmateriaal lesmateriaal) {
            this._lesmateriaal.Add(lesmateriaal);
        }
    }
}
