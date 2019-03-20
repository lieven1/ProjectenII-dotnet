using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain
{
    public class Lesmateriaal {

        public int LesmateriaalId { get; set; }
        public string Naam { get; private set; }
        public string Type { get; private set; }
        public Gradatie Graad { get; private set; }

        public Lesmateriaal(string naam, string type, Gradatie graad) {
            this.Naam = naam;
            this.Type = type;
            this.Graad = graad;
        }
        public Lesmateriaal() {

        }
    }
}
