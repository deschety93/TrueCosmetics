using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueCosmetics.BootstrapApp.Areas.User.Models
{
    public class ProductFilterModel
    {
        public IEnumerable<int> ManufacturerIds { get; set; }
        public IEnumerable<string> CategoryIds { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
    }
}