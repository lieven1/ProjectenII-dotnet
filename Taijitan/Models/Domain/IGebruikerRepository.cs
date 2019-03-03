using System;

namespace Taijitan.Models.Domain
{
    public interface IGebruikerRepository
    {
        void SaveChanges();
        Gebruiker GetBy(String gebruikersnaam);
    }
}