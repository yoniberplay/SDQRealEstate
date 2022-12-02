using Microsoft.EntityFrameworkCore;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Domain.Entities;
using SDQRealEstate.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDQRealEstate.Infrastructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Propiedades>, IPostRepository
    {
        private readonly ApplicationContext _dbContext;

        public PostRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual async Task<List<Propiedades>> GetAllAsync()  //Se sobrescribe pero sigue cumpliendo su misma funcion SOLID
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            return await _dbContext.Set<Propiedades>()
                //.Include(a => a.User)
                //.Include(c => c.Comments)
                .ToListAsync(); //Deferred execution
        }

        public Task<Propiedades> GetBywithRelationship(int id)
        {
            throw new NotImplementedException();
        }

        //public virtual async Task<Post> GetBywithRelationship(int id)
        //{
        //    var temp = await _dbContext.Set<Post>().Where(a => a.Id == id).Include(a => a.User).ToListAsync();
        //    return temp.First();

        //    //POR SI ACASO
        //    //  return _dbContext.Posts.Where(a => a.Id == id).Include(a => a.Fotos).Include(a => a.User).Include(a => a.Category).FirstOrDefault(); 
        //    //return a;
        //}


    }
}
