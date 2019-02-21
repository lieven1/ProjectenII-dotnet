namespace Taijitan.Models.Domain {
    public interface ILidRepository {
        void SaveChanges();
        Lid GetById(int v);
    }
}