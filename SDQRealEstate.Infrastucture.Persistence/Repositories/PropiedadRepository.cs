using Microsoft.EntityFrameworkCore;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Domain.Entities;
using SDQRealEstate.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDQRealEstate.Infrastructure.Persistence.Repositories
{
    public class PropiedadRepository : GenericRepository<Propiedad>, IPropiedadRepository
    {
        private readonly ApplicationContext _dbContext;

        public PropiedadRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<List<Propiedad>> GetAllAsync()  //Se sobrescribe pero sigue cumpliendo su misma funcion SOLID
        {
            return await _dbContext.Set<Propiedad>()
                //.Include(a => a.User)
                //.Include(c => c.Comments)
                .ToListAsync(); //Deferred execution
        }

        public Task<Propiedad> GetBywithRelationship(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<Propiedad>> GetAllViewModelIcnlude()
        {
            var temp = await _dbContext.Set<Propiedad>().Include(a => a.tipoVenta).Include(a => a.tipoPropiedades).Include(a => a.Mejoras).Include(a => a.fotos)
                .ToListAsync();
            return temp;

            //POR SI ACASO
            //  return _dbContext.Posts.Where(a => a.Id == id).Include(a => a.Fotos).Include(a => a.User).Include(a => a.Category).FirstOrDefault(); 
            //return a;
        }


    }
}
