using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.LesmateriaalViewModels {
    public class ThemaViewModel {
        
        public int Graad { get; private set; }
        public List<Thema> Themas { get; private set; }

        public ThemaViewModel(int graad, List<Thema> themas) {
            this.Graad = graad;
            this.Themas = themas;
        }
    }
}
