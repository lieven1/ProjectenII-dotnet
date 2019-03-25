using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Models.Domain.IRepositories
{
    public interface ILesformuleRepository
    {
        IEnumerable<Lesformule> GetAll();
        Lesformule GetById(int id);
    }
}
