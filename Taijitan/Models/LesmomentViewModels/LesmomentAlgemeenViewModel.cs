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
                { Lesformule.Dinsdag, "Les op dinsdag"},
                { Lesformule.DinsdagDonderdag, "Les op dinsdag en donderdag" },
                { Lesformule.DinsdagZaterdag, "Les op dinsdag en zaterdag"},
                { Lesformule.Woensdag, "Les op woensdag"},
                { Lesformule.WoensdagZaterdag, "Les op dinsdag en zaterdag"},
                { Lesformule.Zaterdag, "Les op zaterdag"}
            };
        }

        public LesmomentAlgemeenViewModel(){}
    }
}
