using Microsoft.EntityFrameworkCore;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Domain.Entities;
using SDQRealEstate.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDQRealEstate.Infrastructure.Persistence.Repositories
{
    public class TipoPropiedadesRepository : GenericRepository<TipoPropiedades>, ITipoPropiedadesRepository
    {
        private readonly ApplicationContext _dbContext;

        public TipoPropiedadesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        

    }
}
