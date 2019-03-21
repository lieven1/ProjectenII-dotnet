using System;
using System.Collections.Generic;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain
{
    public interface IGebruikerRepository
    {
        void SaveChanges();
        void Save(Gebruiker gebruiker);
        Gebruiker GetBy(String gebruikersnaam);
        List<Gebruiker> GetAllLeden();
        List<Gebruiker> GetAllLedenInFormule(Lesformule formule);
    }
}