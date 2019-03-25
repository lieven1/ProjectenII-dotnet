using System.Collections.Generic;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain.IRepositories {
    public interface IThemaRepository {
        List<Thema> GetAll();
        Thema GetBy(int id);
        void SaveChanges();
    }
}
