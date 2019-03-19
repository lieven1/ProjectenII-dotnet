using System.Collections.Generic;

namespace Taijitan.Models.Domain {
    public class Thema {
        private string _naam;
        public List<Lesmateriaal> Lesmateriaal { get; private set; }

        public Thema(string naam) {
            this._naam = naam;
            this.Lesmateriaal = new List<Lesmateriaal>();
        }
        public Thema(string naam, List<Lesmateriaal> lesmateriaal) {
            this._naam = naam;
            this.Lesmateriaal = lesmateriaal;
        }
    }
}
