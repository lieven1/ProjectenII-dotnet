namespace Taijitan.Models.Domain
{
    public interface IGebruikerRepository
    {
        void SaveChanges();
        Gebruiker GetBy(string email);
    }
}