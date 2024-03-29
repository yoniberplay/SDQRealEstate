﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDQRealEstate.Infrastructure.Persistence.Repositories;

namespace SDQRealEstate.Infrastructure.Persistence
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m=> m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IFotosRepository, FotoRepository>();
            services.AddTransient<IMejoraRepository, MejoraRepository>();
            services.AddTransient<ITipoVentaRepository, TipoVentaRepository>();
            services.AddTransient<ITipoPropiedadesRepository, TipoPropiedadesRepository>();
            services.AddTransient<IPropiedadRepository, PropiedadRepository>();
            services.AddTransient<IFavoritaRepository, FavoritaRepository>();

            #endregion
        }
    }
}
