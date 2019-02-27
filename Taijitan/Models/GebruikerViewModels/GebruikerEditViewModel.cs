using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.GebruikerViewModels {
    public class GebruikerEditViewModel {
        

        public Adres Adres { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }

        public GebruikerEditViewModel()
        {
                
        }

        public GebruikerEditViewModel(Gebruiker gebruiker)
        {
            this.Adres = gebruiker.Adres;
            this.Gebruikersnaam = gebruiker.Gebruikersnaam;
            this.Naam = gebruiker.Naam;
            this.Voornaam = gebruiker.Voornaam;
            this.Email = gebruiker.Email;
            this.TelefoonNummer = gebruiker.Telefoonnummer;
        }
    }
}
