using System.Collections.Generic;

namespace Taijitan.Models.Domain {
    public class Thema {

        public int ThemaId { get; internal set; }
        public string Naam { get; private set; }

        public Thema(string naam) {
            this.Naam = naam;
        }
        public Thema() {

        }
    }
}
