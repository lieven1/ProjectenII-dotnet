using System;
using System.ComponentModel.DataAnnotations;
using Taijitan.Models.Domain;

namespace Taijitan.Models.GebruikerViewModels {
    public class GebruikerEditViewModel {
        
        public string Gebruikersnaam { get; }
        [Required]
        public String Naam { get; set; }
        [Required]
        public String Voornaam { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }
        [Required]
        public String TelefoonNummer { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Land { get; set; }
        [Required]
        public String Postcode { get; set; }
        [Required]
        public String Stad { get; set; }
        [Required]
        public String Straat { get; set; }
        [Required]
        public String Nummer { get; set; }

        public GebruikerEditViewModel()
        {

        }

        public GebruikerEditViewModel(Gebruiker gebruiker)
        {
            this.Gebruikersnaam = gebruiker.Gebruikersnaam;
            this.Naam = gebruiker.Naam;
            this.Voornaam = gebruiker.Voornaam;
            this.TelefoonNummer = gebruiker.Telefoonnummer;
            this.Email = gebruiker.Email;
            this.Land = gebruiker.Adres.Land;
            this.Postcode = gebruiker.Adres.Postcode;
            this.Stad = gebruiker.Adres.Stad;
            this.Straat = gebruiker.Adres.Straat;
            this.Nummer = gebruiker.Adres.Nummer;
        }
    }
}
