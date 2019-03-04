namespace Taijitan.Models.Domain
{
    public interface IGebruikerRepositoryExtern
    {
        Gebruiker GetByEmail(string email);
        void UpdateGebruiker(Gebruiker gebruiker);
    }
}