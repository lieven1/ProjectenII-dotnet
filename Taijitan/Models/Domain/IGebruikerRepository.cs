using System.Collections.Generic;

namespace Taijitan.Models.Domain {
    public interface IGebruikerRepository {
        void SaveChanges();
        Gebruiker GetById(int v);
        void AddGebruiker(Gebruiker gebruiker);
        void RemoveGebruiker(Gebruiker gebruiker);
        IEnumerable<Gebruiker> getAll();

    }
}