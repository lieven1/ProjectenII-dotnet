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

        private Dictionary<Lesformule, string> lesformuleToStringDictionary;

        public string LesformuleTekst { get { return lesformuleToStringDictionary.GetValueOrDefault(Lesformule); } }

        public bool isGebruikerAanwezig(Gebruiker gebruiker)
        {
            return Lesmoment.geefAanwezigeLeden().Contains(gebruiker);
        }

        public LesmomentGebruikersInFormuleViewModel(Lesmoment lesmoment, Lesformule lesformule, List<Gebruiker> gebruikers)
        {
            Lesmoment = lesmoment;
            Lesformule = lesformule;
            Gebruikers = gebruikers;

            lesformuleToStringDictionary = new Dictionary<Lesformule, string>
            {
                { Lesformule.Dinsdag, "Dinsdag"},
                { Lesformule.DinsdagDonderdag, "Dinsdag en donderdag" },
                { Lesformule.DinsdagZaterdag, "Dinsdag en zaterdag"},
                { Lesformule.Woensdag, "Woensdag"},
                { Lesformule.WoensdagZaterdag, "Woensdag en zaterdag"},
                { Lesformule.Zaterdag, "Zaterdag" }
            };
        }
    }
}
