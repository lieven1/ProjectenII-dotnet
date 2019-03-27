using System.Collections.Generic;

namespace Taijitan.Models.Domain
{
    public interface ILesmomentRepository
    {
        void Save();
        void Save(Lesmoment lesmoment);
        List<Lesmoment> GetAll();
        IEnumerable<Lesmoment> GetAfgelopenLesmomentenByYear(int year);
        IEnumerable<Lesmoment> GetAfgelopenLesmomentenByYearAndMonth(int year, int month);
        IEnumerable<Gebruiker> GetAanwezigenLesmomenten(int id);
        List<int> GetJarenInDatabase();
        Lesmoment GetById(int id);
    }
}
