using System.Collections.Generic;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain.IRepositories {
    public interface IThemaRepository {
        List<Thema> GetAll();
        List<Lesmateriaal> GetLesmateriaal(Thema thema, Gradatie graad);
    }
}
