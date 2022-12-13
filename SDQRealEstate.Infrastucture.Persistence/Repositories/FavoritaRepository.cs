using Microsoft.EntityFrameworkCore;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Domain.Entities;
using SDQRealEstate.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDQRealEstate.Infrastructure.Persistence.Repositories
{
    public class FavoritaRepository : GenericRepository<Favorita>, IFavoritaRepository
    {
        private readonly ApplicationContext _dbContext;

        public FavoritaRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Favorita>> GetAllWithProperties()
        {
            var temp = await _dbContext.Set<Favorita>().Include(a => a.propiedad).ToListAsync();
            return temp;
        }
    }
}
