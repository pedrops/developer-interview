using System;
using System.Collections.Generic;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner.MockData
{
    public static class RebateData
    {
        public static List<Rebate> GetRebates()
        {
            Random random = new();

            Rebate rebate = new()
            {
                Identifier = "1",
                Amount = random.Next(1, 100),
                Incentive = (IncentiveType)random.Next(0, Enum.GetNames(typeof(IncentiveType)).Length),
                Percentage = (decimal)random.NextDouble()
            };
            List<Rebate> rebates = new()
            {
                rebate,
                new Rebate(){
                    Identifier = "Identifier1",
                    Incentive = IncentiveType.AmountPerUom,
                    Amount = 2.1m,
                    Percentage = 0.4m,
                },
                new Rebate(){
                    Identifier = "Identifier2",
                    Incentive = IncentiveType.FixedCashAmount,
                    Amount = 10.4m,
                    Percentage = 0.2m,
                }
            };

            return rebates;
        }
    }
}
