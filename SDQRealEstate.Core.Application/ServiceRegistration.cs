using SDQRealEstate.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Application.Interfaces.Services;
using System.Reflection;

namespace SDQRealEstate.Core.Application
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services,IConfiguration configuration)
        {
            #region Services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IPropiedadService, PropiedadService>();
            services.AddTransient<IFotosService, FotosService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMejoraService, MejoraService>();
            services.AddTransient<ITipoPropiedadService, TipoPropiedadService>();
            services.AddTransient<ITipoVentaService, TipoVentaService>();

            #endregion
        }
    }
}
