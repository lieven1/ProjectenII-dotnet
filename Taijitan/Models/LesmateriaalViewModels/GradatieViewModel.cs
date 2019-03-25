using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.LesmateriaalViewModels {
    public class GradatieViewModel {
        public int graadInt { get; private set; }
        public Gradatie gradatie { get; private set; }

        public GradatieViewModel(int graadInt) {
            this.graadInt = graadInt;
            this.gradatie = (Gradatie)graadInt;
        }
    }
}
