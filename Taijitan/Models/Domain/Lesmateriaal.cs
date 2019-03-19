using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain
{
    public class Lesmateriaal {
        // TO DO - Implement
        private string v1;
        private string v2;

        public Gradatie Graad { get; private set; }

        public Lesmateriaal(string v1, string v2, Gradatie graad) {
            this.v1 = v1;
            this.v2 = v2;
            this.Graad = graad;
        }
    }
}
