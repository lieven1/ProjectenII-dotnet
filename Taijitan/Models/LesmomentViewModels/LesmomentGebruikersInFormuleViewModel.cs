using System.Collections.Generic;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentGebruikersInFormuleViewModel
    {
        public List<Gebruiker> Gebruikers { get; set; }
        public Lesformule Lesformule { get; set; }
        public Lesmoment Lesmoment { get; set; }

        public bool isGebruikerAanwezig(Gebruiker gebruiker)
        {
            return Lesmoment.geefAanwezigeLeden().Contains(gebruiker);
        }

        public LesmomentGebruikersInFormuleViewModel(Lesmoment lesmoment, Lesformule lesformule, List<Gebruiker> gebruikers)
        {
            Lesmoment = lesmoment;
            Lesformule = lesformule;
            Gebruikers = gebruikers;
        }
    }
}
