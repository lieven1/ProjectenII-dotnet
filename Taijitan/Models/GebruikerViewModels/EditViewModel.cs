using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.GebruikerViewModels {
    public class EditViewModel {
        private Gebruiker bruceLee;

        public EditViewModel(Gebruiker bruceLee) {
            this.bruceLee = bruceLee;
        }

        public Adres Adres { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
    }
}
