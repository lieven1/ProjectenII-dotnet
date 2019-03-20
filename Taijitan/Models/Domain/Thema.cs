using System.Collections.Generic;

namespace Taijitan.Models.Domain {
    public class Thema {

        public int ThemaId { get; set; }
        public string Naam { get; private set; }
        public List<Lesmateriaal> Lesmateriaal { get; private set; }

        public Thema(string naam) {
            this.Naam = naam;
            this.Lesmateriaal = new List<Lesmateriaal>();
        }
        public Thema(string naam, List<Lesmateriaal> lesmateriaal) {
            this.Naam = naam;
            this.Lesmateriaal = lesmateriaal;
        }
        public Thema() {

        }
    }
}
