using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IncentiveTypesCalculations;
using Smartwyre.DeveloperTest.Interfaces.Data;
using Smartwyre.DeveloperTest.Runner.MockData;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;

// Create a Service Collection
IServiceCollection services = new ServiceCollection();

services.AddSingleton<SingletonDatabase>();
services.AddSingleton<IRebateService, RebateService>();
services.AddSingleton<IRebateDataStore, RebateDataStore>();
services.AddSingleton<IProductDataStore, ProductDataStore>();
services.AddSingleton<IIncentiveCalculationStrategy, FixedCashAmountCalculationStrategy>();
services.AddSingleton<IIncentiveCalculationStrategy, FixedRateRebateCalculationStrategy>();
services.AddSingleton<IIncentiveCalculationStrategy, AmountPerUomCalculationStrategy>();

var build = services.BuildServiceProvider();

var rebateService = build.GetRequiredService<IRebateService>();


SingletonDatabase db = SingletonDatabase.GetInstance();

List<Product> products = ProductData.GetProducts();
List<Rebate> rebates = RebateData.GetRebates();

db.Products.AddRange(products);
db.Rebates.AddRange(rebates);

// Watch for input and then act on it
while (true)
{
    Console.WriteLine("================");
    Console.WriteLine("Write the RebateIdentifier: ");
    var input1 = Console.ReadLine();
    Console.WriteLine("Write the ProductIdentifier: ");
    var input2 = Console.ReadLine();
    Console.WriteLine("Write the Volume: ");
    var input3 = Console.ReadLine();

    // Create the request and parse the input
    CalculateRebateRequest request = new(){
        RebateIdentifier = input1,
        ProductIdentifier = input2,
        Volume = decimal.Parse(input3)
    };

    // Validating the inputs
    if (!string.IsNullOrWhiteSpace(request.ProductIdentifier)
        && !string.IsNullOrWhiteSpace(request.RebateIdentifier)
        && request.Volume >= 0)
    {
        // Run Calculation
        var rebateResult = rebateService.Calculate(request);

        // Return response
        if (rebateResult.Success)
            Console.WriteLine("======= Verified product =======");
        else
            Console.WriteLine("We are sorry, this product is not valid for rebate process..");
    }
}