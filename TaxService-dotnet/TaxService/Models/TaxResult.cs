using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxService.Models
{
    public record TaxResult
    {
        public Tax tax { get; init; }
    }

    public record Tax
    {
        public decimal amount_to_collect { get; init; }
        public Breakdown breakdown { get; init; }
        public bool freight_taxable { get; init; }
        public bool has_nexus { get; init; }
        public Jurisdictions jurisdictions { get; init; }
        public decimal order_total_amount { get; init; }
        public decimal rate { get; init; }
        public decimal shipping { get; init; }
        public string tax_source { get; init; }
        public decimal taxable_amount { get; init; }
    }

    public record Breakdown
    {
        public decimal city_tax_collectable { get; init; }
        public decimal city_tax_rate { get; init; }
        public decimal city_taxable_amount { get; init; }
        public decimal combined_tax_rate { get; init; }
        public decimal county_tax_collectable { get; init; }
        public decimal county_tax_rate { get; init; }
        public decimal county_taxable_amount { get; init; }
        public IEnumerable<ItemTax> line_items { get; init; }
        public ShippingTax shipping { get; init; }
        public decimal special_district_tax_collectable { get; init; }
        public decimal special_district_taxable_amount { get; init; }
        public decimal special_tax_rate { get; init; }
        public decimal state_tax_collectable { get; init; }
        public decimal state_tax_rate { get; init; }
        public decimal state_taxable_amount { get; init; }
        public decimal tax_collectable { get; init; }
        public decimal taxable_amount { get; init; }
    }

    public record ItemTax
    {
        public decimal city_amount { get; init; }
        public decimal city_tax_rate { get; init; }
        public decimal city_taxable_amount { get; init; }
        public decimal combined_tax_rate { get; init; }
        public decimal county_amount { get; init; }
        public decimal county_tax_rate { get; init; }
        public decimal county_taxable_amouont { get; init; }
        public string id { get; init; }
        public decimal special_district_amount { get; init; }
        public decimal special_district_taxable_amount { get; init; }
        public decimal special_tax_rate { get; init; }
        public decimal state_amount { get; init; }
        public decimal state_sales_tax_rate { get; init; }
        public decimal state_taxable_amount { get; init; }
        public decimal tax_collectable { get; init; }
        public decimal taxable_amount { get; init; }
    }

    public record ShippingTax
    {
        public decimal city_amount { get; init; }
        public decimal city_tax_rate { get; init; }
        public decimal city_taxable_amount { get; init; }
        public decimal combined_tax_rate { get; init; }
        public decimal county_amount { get; init; }
        public decimal county_tax_rate { get; init; }
        public decimal county_taxable_amouont { get; init; }
        public decimal special_district_amount { get; init; }
        public decimal special_tax_rate { get; init; }
        public decimal special_taxable_amount { get; init; }
        public decimal state_amount { get; init; }
        public decimal state_sales_tax_rate { get; init; }
        public decimal state_taxable_amount { get; init; }
        public decimal tax_collectable { get; init; }
        public decimal taxable_amount { get; init; }
    }

    public record Jurisdictions
    {
        public string city { get; init; }
        public string country { get; init; }
        public string county { get; init; }
        public string state { get; init; }
    }
}
