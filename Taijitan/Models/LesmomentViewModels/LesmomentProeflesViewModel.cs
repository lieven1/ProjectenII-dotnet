using System;
using System.ComponentModel.DataAnnotations;
using Taijitan.Models.Domain;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentProeflesViewModel
    {
        [Required(ErrorMessage = "Naam is verplicht."), MaxLength(50)]
        //naam begint met kleine letter of hoofdletter
        //naam kan tekens (,.'-) bevatten
        //naam kan uit meerdere woorden bestaan
        [RegularExpression("^([A-Za-z]{1}[a-z ,.'-]+)+$", ErrorMessage = "Ongeldige waarde voor naam.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Voornaam is verplicht."), MaxLength(50)]
        //voornaam is zoals naam, maar moet met hoofdletter beginnen
        [RegularExpression("^([A-Z]{1}[a-z ,.'-]+)+$", ErrorMessage = "Ongeldige waarde voor voornaam.")]
        public String Voornaam { get; set; }

        [Required(ErrorMessage = "Email is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldige waarde voor e-mailadres.")]
        //email pre/post @ kan . of - en letters bevatten en minstens 1 karakter
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]+$", ErrorMessage = "Ongeldige waarde voor e-mailadres.")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is verplicht.")]
        [RegularExpression(@"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))", ErrorMessage = "Ongeldige waarde voor telefoonnummer")]
        public String Telefoonnummer { get; set; }

        public int LesmomentId { get; set; }

        public LesmomentProeflesViewModel(Lesmoment lesmoment)
        {
            this.LesmomentId = lesmoment.LesmomentId;
        }

        public LesmomentProeflesViewModel(){}
    }
}
