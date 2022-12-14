using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDQRealEstate.Infrastructure.Identity.Context;
using SDQRealEstate.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SDQRealEstate.Infrastructure.Identity.Services;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SDQRealEstate.Core.Application.Dtos.Account;

namespace SDQRealEstate.Infrastructure.Identity
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                m=> m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication();

            //services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.RequireHttpsMetadata = false;
            //    o.SaveToken = false;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        ValidateIssuer = true,
            //        ValidateLifetime = true,
            //        ValidateAudience = true,
            //        ClockSkew = TimeSpan.Zero,
            //        ValidIssuer = configuration["JWTSettings:Issuer"],
            //        ValidAudience = configuration["JWTSettings:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"])),
            //    };
            //    o.Events = new JwtBearerEvents()
            //    {
            //        OnAuthenticationFailed = c =>
            //        {
            //            c.NoResult();
            //            c.Response.StatusCode = 500;
            //            c.Response.ContentType = "text/plain";
            //            return c.Response.WriteAsync(c.Exception.ToString());
            //        },
            //        OnChallenge = c =>
            //        {
            //            c.HandleResponse();
            //            c.Response.StatusCode = 401;
            //            c.Response.ContentType = "application/json";
            //            var result = JsonConvert.SerializeObject(new JwtResponse() { HasError = true, Error = "Acceso denegado" });
            //            return c.Response.WriteAsync(result);
            //        },
            //        OnForbidden = c =>
            //        {
            //            c.Response.StatusCode = 403;
            //            c.Response.ContentType = "application/json";
            //            var result = JsonConvert.SerializeObject(new JwtResponse() { HasError = true, Error = "No tiene autorizacion para acceder a esta area" });
            //            return c.Response.WriteAsync(result);
            //        },

            //    };
            //});

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath ="/User";
                options.AccessDeniedPath = "/User/AccessDenied";
            }
            );

            
            #endregion

            #region services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IManageUsersService, ManageUsersService>();
            #endregion
        }
    }
}
