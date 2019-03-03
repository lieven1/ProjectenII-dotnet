using System;
using System.ComponentModel.DataAnnotations;
using Taijitan.Models.Domain;

namespace Taijitan.Models.GebruikerViewModels {
    public class GebruikerEditViewModel {
        
        [Required(ErrorMessage = "Naam is verplicht.")]
        public String Naam { get; set; }
        [Required(ErrorMessage = "Voornaam is verplicht.")]
        public String Voornaam { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))", ErrorMessage = "Ongeldige waarde voor telefoonnummer.")]
        public String TelefoonNummer { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Ongeldige waarde voor e-mailadres.")]
        public String Email { get; set; }
        [Required]
        public String Land { get; set; }
        [Required]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Ongeldige waarde voor postcode.")]
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
            this.Naam = gebruiker.Naam;
            this.Voornaam = gebruiker.Voornaam;
            this.Geboortedatum = gebruiker.Geboortedatum;
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
