using SDQRealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Interfaces.Repositories
{
    public interface IPostRepository : IGenericRepository<Propiedades>
    {
        Task<Propiedades> GetBywithRelationship(int id);
    }
}
