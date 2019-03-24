using System.Collections.Generic;

namespace Taijitan.Models.Domain.IRepositories {
    public interface ILesmateriaalRepository {
        List<Lesmateriaal> GetAll();
        Lesmateriaal GetById(int id);
    }
}
