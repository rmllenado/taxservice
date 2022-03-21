using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaxService.Models
{
    public record Rate
    {
        public string city { get; init; }
        public string city_rate { get; init; }
        public string combined_district_rate { get; init; }
        public string combined_rate { get; init; }
        public string country { get; init; }
        public string country_rate { get; init; }
        public string county { get; init; }
        public string county_rate { get; init; }
        public Boolean freight_taxable { get; init; }
        public string state { get; init; }
        public string state_rate { get; init; }
        public string zip { get; init; }
    }
}
