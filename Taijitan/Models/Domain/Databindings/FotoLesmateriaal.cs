using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain.Databindings {
    public class FotoLesmateriaal {
        public int LesmateriaalId { get; set; }
        public Lesmateriaal Lesmateriaal { get; set; }
        public int FotoId { get; set; }
        public Foto Foto { get; set; }

        public FotoLesmateriaal(Lesmateriaal lesmateriaal, Foto foto) {
            this.Lesmateriaal = lesmateriaal;
            this.Foto = foto;
        }

        public FotoLesmateriaal() {

        }
    }
}
