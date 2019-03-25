using System.Collections.Generic;
using Taijitan.Models.Domain;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentActiefViewModel
    {
        public List<Lesmoment> Lesmomenten { get; set; }

        public LesmomentActiefViewModel(List<Lesmoment> lesmomenten)
        {
            Lesmomenten = lesmomenten;
        }
    }
}
