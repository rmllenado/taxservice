using System.Collections.Generic;
using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService.Services
{
    public interface ITaxCalculator
    {
        Task<TaxResult> GetTaxes(Order order);
        Task<RateResult> GetTaxRates(string zipcode, string city, string country);
    }
}