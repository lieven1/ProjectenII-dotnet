using System;
using System.ComponentModel.DataAnnotations;
using Taijitan.Models.Domain;

namespace Taijitan.Models.GebruikerViewModels {
    public class GebruikerEditViewModel {
        [Required(ErrorMessage = "Naam is verplicht.")]
        public String Naam { get; set; }
        [Required(ErrorMessage = "Voornaam is verplicht.")]
        public String Voornaam { get; set; }
        [Phone(ErrorMessage = "Ongeldige waarde voor telefoonnummer.")]
        [RegularExpression(@"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))", ErrorMessage = "Ongeldige waarde voor telefoonnummer.")]
        public String TelefoonNummer { get; set; }
        [Required(ErrorMessage = "Gsmnummer is verplicht.")]
        [Phone(ErrorMessage = "Ongeldige waarde voor gsmnummer.")]
        [RegularExpression(@"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))", ErrorMessage = "Ongeldige waarde voor gsmnummer.")]
        public String Gsmnummer { get; set; }
        [Required(ErrorMessage = "Email is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldige waarde voor e-mailadres.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Ongeldige waarde voor e-mailadres.")]
        public String Email { get; set; }
        [Display(Name = "Email ouders")]
        [EmailAddress(ErrorMessage = "Ongeldige waarde voor e-mailadres.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Ongeldige waarde voor e-mailadres.")]
        public String EmailOuders { get; set; }
        [Required(ErrorMessage = "Land is verplicht.")]
        public String Land { get; set; }
        [Required(ErrorMessage = "Postcode is verplicht.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Ongeldige waarde voor postcode.")]
        public String Postcode { get; set; }
        [Required(ErrorMessage = "Stad is verplicht.")]
        public String Stad { get; set; }
        [Required(ErrorMessage = "Straat is verplicht.")]
        public String Straat { get; set; }
        [Required(ErrorMessage = "Nummer is verplicht.")]
        public String Nummer { get; set; }

        public GebruikerEditViewModel()
        {

        }

        public GebruikerEditViewModel(Gebruiker gebruiker)
        {
            this.Naam = gebruiker.Naam;
            this.Voornaam = gebruiker.Voornaam;
            this.TelefoonNummer = gebruiker.Telefoonnummer;
            this.Gsmnummer = gebruiker.Gsmnummer;
            this.Email = gebruiker.Email;
            this.EmailOuders = gebruiker.EmailOuders;
            this.Land = gebruiker.Adres.Land;
            this.Postcode = gebruiker.Adres.Postcode;
            this.Stad = gebruiker.Adres.Stad;
            this.Straat = gebruiker.Adres.Straat;
            this.Nummer = gebruiker.Adres.Nummer;
        }
    }
}
