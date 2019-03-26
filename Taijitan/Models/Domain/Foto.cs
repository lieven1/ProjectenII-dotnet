using CloudinaryDotNet;
using System.Collections.Generic;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Models.Domain
{
    public class Foto {
        public int Id { get; private set; }
        public string Bestandsnaam { get; set; }
        public string Extensie { get; set; }
        public List<FotoLesmateriaal> Lesmateriaal { get; private set; }

        public Cloudinary Cloudinary { get; } = new Cloudinary(new Account("dkbfdmful", "863719494787761", "DpGFihSrXeYjXljkQiaFcaqR14c"));

        public Foto(string bestandsnaam, string extensie) {
            this.Bestandsnaam = bestandsnaam;
            this.Extensie = extensie;
        }

        public Foto() {}
    }
}
