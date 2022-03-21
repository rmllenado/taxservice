using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using TaxService.Controllers;
using TaxService.Models;
using TaxService.Services;
using Xunit;

namespace TaxService.Tests
{
    public class TaxServiceControllerUnitTests
    {
        [Fact]
        public void GetRates_Returns_Expected_Value_For_90404_Santa_Monica_US()
        {
            // arrange
            var expectedTaxRate = GetExpectedTaxRate();

            string zip = "90404";
            string city = "santa monica";
            string country = "us";

            var taxCalculator = A.Fake<ITaxCalculator>();
            A.CallTo(() => taxCalculator.GetTaxRates(zip, city, country)).Returns(Task.FromResult(expectedTaxRate));
            var controller = new TaxServiceController(taxCalculator);

            // act
            var actionResult = controller.GetRates(zip, city, country);

            // assert
            var result = actionResult.Result as OkObjectResult;
            var actualTaxRate = result.Value as RateResult;
            Assert.Equal(expectedTaxRate, actualTaxRate);
        }

        [Fact]
        public void GetTax_Returns_Expected_Value_()
        {
            // arrange
            var expectedTax = GetExpectedTax();

            var order = GetOrderInputPayload();
            
            var taxCalculator = A.Fake<ITaxCalculator>();
            A.CallTo(() => taxCalculator.GetTaxes(order)).Returns(Task.FromResult(expectedTax));
            var controller = new TaxServiceController(taxCalculator);

            // act
            var actionResult = controller.GetTax(order);

            // assert
            var result = actionResult.Result as OkObjectResult;
            var actualTax = result.Value as TaxResult;
            Assert.Equal(expectedTax, actualTax);
        }

        private RateResult GetExpectedTaxRate()
        {
            return new RateResult
            {
                rate = new Rate
                {
                    city = "SANTA MONICA",
                    city_rate = "0.0",
                    combined_district_rate = "0.03",
                    combined_rate = "0.1025",
                    country = "US",
                    country_rate = "0.0",
                    county = "LOS ANGELES",
                    county_rate = "0.01",
                    freight_taxable = false,
                    state = "CA",
                    state_rate = "0.0625",
                    zip = "90404"
                }
            };
        }

        private TaxResult GetExpectedTax()
        {
            var jsonTaxResult = @"{
    ""tax"": {
        ""amount_to_collect"": 1.09,
        ""breakdown"": {
            ""city_tax_collectable"": 0.0,
            ""city_tax_rate"": 0.0,
            ""city_taxable_amount"": 0.0,
            ""combined_tax_rate"": 0.06625,
            ""county_tax_collectable"": 0.0,
            ""county_tax_rate"": 0.0,
            ""county_taxable_amount"": 0.0,
            ""line_items"": [
                {
                    ""city_amount"": 0.0,
                    ""city_tax_rate"": 0.0,
                    ""city_taxable_amount"": 0.0,
                    ""combined_tax_rate"": 0.06625,
                    ""county_amount"": 0.0,
                    ""county_tax_rate"": 0.0,
                    ""county_taxable_amount"": 0.0,
                    ""id"": ""1"",
                    ""special_district_amount"": 0.0,
                    ""special_district_taxable_amount"": 0.0,
                    ""special_tax_rate"": 0.0,
                    ""state_amount"": 0.99,
                    ""state_sales_tax_rate"": 0.06625,
                    ""state_taxable_amount"": 15.0,
                    ""tax_collectable"": 0.99,
                    ""taxable_amount"": 15.0
                }
            ],
            ""shipping"": {
                ""city_amount"": 0.0,
                ""city_tax_rate"": 0.0,
                ""city_taxable_amount"": 0.0,
                ""combined_tax_rate"": 0.06625,
                ""county_amount"": 0.0,
                ""county_tax_rate"": 0.0,
                ""county_taxable_amount"": 0.0,
                ""special_district_amount"": 0.0,
                ""special_tax_rate"": 0.0,
                ""special_taxable_amount"": 0.0,
                ""state_amount"": 0.1,
                ""state_sales_tax_rate"": 0.06625,
                ""state_taxable_amount"": 1.5,
                ""tax_collectable"": 0.1,
                ""taxable_amount"": 1.5
            },
            ""special_district_tax_collectable"": 0.0,
            ""special_district_taxable_amount"": 0.0,
            ""special_tax_rate"": 0.0,
            ""state_tax_collectable"": 1.09,
            ""state_tax_rate"": 0.06625,
            ""state_taxable_amount"": 16.5,
            ""tax_collectable"": 1.09,
            ""taxable_amount"": 16.5
        },
        ""freight_taxable"": true,
        ""has_nexus"": true,
        ""jurisdictions"": {
            ""city"": ""RAMSEY"",
            ""country"": ""US"",
            ""county"": ""BERGEN"",
            ""state"": ""NJ""
        },
        ""order_total_amount"": 16.5,
        ""rate"": 0.06625,
        ""shipping"": 1.5,
        ""tax_source"": ""destination"",
        ""taxable_amount"": 16.5
    }
}";
            return JsonSerializer.Deserialize<TaxResult>(jsonTaxResult);
        }

        private Order GetOrderInputPayload()
        {
            var jsonOrder = @"{
  ""from_country"": ""US"",
  ""from_zip"": ""07001"",
  ""from_state"": ""NJ"",
  ""to_country"": ""US"",
  ""to_zip"": ""07446"",
  ""to_state"": ""NJ"",
  ""amount"": 16.50,
  ""shipping"": 1.5,
  ""line_items"": [
    {
      ""quantity"": 1,
      ""unit_price"": 15.0,
      ""product_tax_code"": ""31000""
    }
  ]
}";
           return JsonSerializer.Deserialize<Order>(jsonOrder);
        }
    }
}
