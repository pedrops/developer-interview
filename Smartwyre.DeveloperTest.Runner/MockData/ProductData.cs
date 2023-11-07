using System;
using System.Collections.Generic;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner.MockData
{
    public static class ProductData
    {
        public static List<Product> GetProducts()
        {
            Random random = new();

            Product product = new()
            {
                Identifier = "1",
                Id = random.Next(1, 100),
                Price = random.Next(1, 100),
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate | SupportedIncentiveType.FixedCashAmount,
                Uom = Guid.NewGuid().ToString()
            };

            List<Product> products = new()
            {
                product,
                new() {
                    Id = 1,
                    Identifier = "Product1",
                    Price = 100.0m,
                    Uom = "kg",
                    SupportedIncentives = SupportedIncentiveType.FixedRateRebate | SupportedIncentiveType.FixedCashAmount
                },
                new() {
                    Id = 2,
                    Identifier = "Product2",
                    Price = 200.0m,
                    Uom = "kg",
                    SupportedIncentives = SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.FixedCashAmount
                }
            };

            return products;
        }
    }
}
