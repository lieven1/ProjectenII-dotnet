using System;
using System.Collections.Generic;

namespace Taijitan.Models.Domain
{
    public interface IGebruikerRepository
    {
        void SaveChanges();
        void Save(Gebruiker gebruiker);
        Gebruiker GetBy(String gebruikersnaam);
        List<Gebruiker> GetAllLeden();
    }
}