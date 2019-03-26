using System.Collections.Generic;
using Taijitan.Models.Domain.Databindings;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain
{
    public class Lesmateriaal {
        public int LesmateriaalId { get; internal set; }
        public string Naam { get; private set; }
        public string Beschrijving { get; private set; }
        public Gradatie Graad { get; private set; }
        public Thema Thema { get; private set; }
        public string VideoId { get; private set; }
        public List<FotoLesmateriaal> Fotos { get; set; }

        public Lesmateriaal(string naam, Gradatie graad, Thema thema, string beschrijving = null, string videoId = null, List<FotoLesmateriaal> fotos = null) {
            this.Naam = naam;
            this.Graad = graad;
            this.Thema = thema;
            if (beschrijving != null)
                this.Beschrijving = beschrijving;
            if (videoId != null)
                this.VideoId = videoId;
            if (fotos != null)
                this.Fotos = fotos;
        }

        public Lesmateriaal() {}
    }
}
