using Inventory.Application.Security;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Repositories;
using Inventory.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.DependencyInjection
{
    public static  class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Registra UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
