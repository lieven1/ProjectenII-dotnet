using System;
using System.Collections.Generic;

namespace Taijitan.Models.Domain
{
    public interface IGebruikerRepository
    {
        void SaveChanges();
        Gebruiker GetBy(String gebruikersnaam);
        List<Gebruiker> GetAllLeden();
    }
}