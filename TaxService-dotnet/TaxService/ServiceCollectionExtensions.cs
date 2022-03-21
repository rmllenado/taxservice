using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxService.Services;

namespace TaxService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTaxCalculator(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TaxCalculatorOptions>(configuration.GetSection("TaxCalculator"));
            services.AddHttpClient<ITaxCalculator, TaxCalculator>();
            return services;
        }
    }
}
