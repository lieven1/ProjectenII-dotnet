using Taijitan.Models.Domain;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentGebruikerViewModel
    {
        public Lesmoment Lesmoment;

        public LesmomentGebruikerViewModel(Lesmoment lesmoment)
        {
            Lesmoment = lesmoment;
        }

        public LesmomentGebruikerViewModel(){}
    }
}
