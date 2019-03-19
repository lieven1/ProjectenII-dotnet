using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain
{
    public class Lesmateriaal {
        private string _naam;
        private string _type;
        public Gradatie Graad { get; private set; }

        public Lesmateriaal(string naam, string type, Gradatie graad) {
            this._naam = naam;
            this._type = type;
            this.Graad = graad;
        }
    }
}
