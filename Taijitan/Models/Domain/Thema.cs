using System.Collections.Generic;

namespace Taijitan.Models.Domain {
    public class Thema {

        public int ThemaId { get; set; }
        public string Naam { get; private set; }
        public List<Lesmateriaal> Lesmateriaal { get; private set; }

        public Thema(int id, string naam, List<Lesmateriaal> lesmateriaal) : this(naam, lesmateriaal) {
            this.ThemaId = id;
        }

        public Thema(string naam, List<Lesmateriaal> lesmateriaal) {
            this.Naam = naam;
            if (lesmateriaal == null) {
                this.Lesmateriaal = new List<Lesmateriaal>();
            } else {
                this.Lesmateriaal = lesmateriaal;
            }
        }
        public Thema() {

        }
    }
}
