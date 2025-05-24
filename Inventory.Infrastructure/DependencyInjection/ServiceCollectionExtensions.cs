using Inventory.Application.Interfaces.Security;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Repositories;
using Inventory.Infrastructure.Services.Security;
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
            services.AddScoped<IBatchRepository, BatchRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
