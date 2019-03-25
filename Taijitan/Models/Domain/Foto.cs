using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Models.Domain {
    public class Foto {
        public int id { get; private set; }
        public string bestandsnaam { get; set; }
        public string extensie { get; set; }
        public List<FotoLesmateriaal> lesmateriaal { get; private set; }

        public Foto(string bestandsnaam, string extensie) {
            this.bestandsnaam = bestandsnaam;
            this.extensie = extensie;
        }

        public Foto() {

        }
    }
}
