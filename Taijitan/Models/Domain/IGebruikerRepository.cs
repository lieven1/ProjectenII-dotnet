namespace Taijitan.Models.Domain {
    public interface IGebruikerRepository {
        void SaveChanges();
        Gebruiker GetById(int v);
    }
}