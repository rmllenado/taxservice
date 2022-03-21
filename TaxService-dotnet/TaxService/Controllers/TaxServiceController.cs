using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TaxService.Models;
using TaxService.Services;

namespace TaxService.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class TaxServiceController : ControllerBase
    {
        private readonly ITaxCalculator taxCalculator;

        public TaxServiceController(ITaxCalculator taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }

        /// <summary>
        /// Returns tax rates for a location
        /// </summary>
        /// <param name="zipcode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("rates/{zipcode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRates(string zipcode, [FromQuery] string city, [FromQuery] string country)
        {
            var rateResult = await this.taxCalculator.GetTaxRates(zipcode, city, country);

            if (rateResult == null || rateResult.rate == null)
                return NotFound();

            return Ok(rateResult);
        }

        /// <summary>
        /// Returns taxes for an order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("taxes")]
        public async Task<IActionResult> GetTax([FromBody]Order order)
        {
            var taxResult = await this.taxCalculator.GetTaxes(order);

            if (taxResult == null || taxResult.tax == null)
                return BadRequest();

            return Ok(taxResult);
        }
    }
}
