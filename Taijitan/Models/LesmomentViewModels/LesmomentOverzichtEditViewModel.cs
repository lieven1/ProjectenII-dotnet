using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentOverzichtEditViewmodel
    {
        public int Year { get; set; }
        public Maanden Month { get; set; }
        public IEnumerable<Lesmoment> Lesmomenten { get; set; }

        public LesmomentOverzichtEditViewmodel(int Year, Maanden Month, IEnumerable<Lesmoment> Lesmomenten)
        {
            this.Year = Year;
            this.Month = Month;
            this.Lesmomenten = Lesmomenten;              
        }

        public LesmomentOverzichtEditViewmodel(IEnumerable<Lesmoment> Lesmomenten)
        {
            this.Lesmomenten = Lesmomenten;
        }

        public LesmomentOverzichtEditViewmodel()
        {

        }

    }
}
