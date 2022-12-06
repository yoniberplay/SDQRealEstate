using SDQRealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Interfaces.Repositories
{
    public interface IPropiedadRepository : IGenericRepository<Propiedad>
    {
        Task<Propiedad> GetBywithRelationship(int id);
        Task<List<Propiedad>> GetAllViewModelIcnlude();
    }
}
