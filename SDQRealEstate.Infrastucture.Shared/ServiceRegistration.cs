using SDQRealEstate.Infrastucture.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDQRealEstate.Core.Domain.Settings;
using SDQRealEstate.Core.Application.Interfaces.Services;

namespace SDQRealEstate.Infrastucture.Shared.Services
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddShareInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
