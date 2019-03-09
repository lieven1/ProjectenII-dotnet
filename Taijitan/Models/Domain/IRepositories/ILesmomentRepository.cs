using System.Collections.Generic;

namespace Taijitan.Models.Domain
{
    public interface ILesmomentRepository
    {
        void Save(Lesmoment lesmoment);
        List<Lesmoment> GetAll();
    }
}
