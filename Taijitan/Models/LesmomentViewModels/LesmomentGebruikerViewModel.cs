using Taijitan.Models.Domain;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentGebruikerViewModel
    {
        public Lesmoment Lesmoment;
        //public string Gebruikersnaam;
        //public string Voornaam;
        //public string Naam;

        //public LesmomentGebruikerViewModel(Lesmoment lesmoment, Gebruiker gebruiker) : this(lesmoment)
        //{
        //    Gebruikersnaam = gebruiker.Gebruikersnaam;
        //    Voornaam = gebruiker.Voornaam;
        //    Naam = gebruiker.Naam;
        //}

        public LesmomentGebruikerViewModel(Lesmoment lesmoment)
        {
            Lesmoment = lesmoment;

        }

        public LesmomentGebruikerViewModel()
        {

        }
    }
}
