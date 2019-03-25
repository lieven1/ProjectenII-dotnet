using System;
using System.Collections.Generic;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentAlgemeenViewModel
    {
        public Lesmoment Lesmoment;
        public HashSet<Lesformule> Lesformules { get; set; }

        public LesmomentAlgemeenViewModel(Lesmoment lesmoment, HashSet<Lesformule> formules)
        {
            Lesmoment = lesmoment;
            Lesformules = formules;
        }

        public LesmomentAlgemeenViewModel(){}
    }
}
