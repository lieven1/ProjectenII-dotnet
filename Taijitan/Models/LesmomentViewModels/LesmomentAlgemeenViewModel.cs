using System;
using System.Collections.Generic;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentAlgemeenViewModel
    {
        public Lesmoment Lesmoment;
        private Dictionary<Lesformule, string> LesformuleToStringDictionary;
        public HashSet<Lesformule> Lesformules { get; set; }

        public String LesformuleToStringMapper(Lesformule key)
        {
            return LesformuleToStringDictionary.GetValueOrDefault(key);
        }

        public LesmomentAlgemeenViewModel(Lesmoment lesmoment, HashSet<Lesformule> formules)
        {
            Lesmoment = lesmoment;
            Lesformules = formules;

            LesformuleToStringDictionary = new Dictionary<Lesformule, string>
            {
                { Lesformule.Dinsdag, "Ik volg enkel les op dinsdag."},
                { Lesformule.DinsdagDonderdag, "Ik volg les op dinsdag en donderdag." },
                { Lesformule.DinsdagZaterdag, "Ik volg les op dinsdag en zaterdag."},
                { Lesformule.Woensdag, "Ik volg enkel les op woensdag."},
                { Lesformule.WoensdagZaterdag, "Ik volg les op dinsdag en zaterdag"},
                { Lesformule.Zaterdag, "Ik volg enkel les op zaterdag." }
            };
        }

        public LesmomentAlgemeenViewModel()
        {}
    }
}
