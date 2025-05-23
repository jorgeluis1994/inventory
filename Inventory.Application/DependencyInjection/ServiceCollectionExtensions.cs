using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBatchService, BatchService>();
            return services;
        }
    }
}
