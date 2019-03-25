using System.Collections.Generic;

namespace Taijitan.Models.Domain.IRepositories
{
    public interface IThemaRepository {
        List<Thema> GetAll();
        Thema GetBy(int id);
        void SaveChanges();
    }
}
