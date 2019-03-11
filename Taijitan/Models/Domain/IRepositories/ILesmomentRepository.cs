using System.Collections.Generic;

namespace Taijitan.Models.Domain
{
    public interface ILesmomentRepository
    {
        void Save();
        void Save(Lesmoment lesmoment);
        List<Lesmoment> GetAll();
        Lesmoment GetById(int id);
    }
}
