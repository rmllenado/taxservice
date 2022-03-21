using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxService.Models
{
    public record Order
    {
        public string from_country { get; init; }
        public string from_zip { get; init; }
        public string from_state { get; init; }
        public string to_country { get; init; }
        public string to_zip { get; init; }
        public string to_state { get; init; }
        public decimal amount { get; init; }
        public decimal shipping { get; init; }
        public IEnumerable<Item> line_items { get; init; }
    }

    public record Item
    {
        public int quantity { get; init; }
        public decimal unit_price { get; init; }
        public string product_tax_code { get; init; }
    }
}
