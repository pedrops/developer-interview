using System;
using System.Collections.Generic;
using System.Reflection;
using Smartwyre.DeveloperTest.IncentiveTypesCalculations;
using Smartwyre.DeveloperTest.Interfaces.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    IProductDataStore _productDataStore;
    IRebateDataStore _rebateDataStore;
    private readonly IEnumerable<IIncentiveCalculationStrategy> _strategies;
    public RebateService(IProductDataStore productDataStore, IRebateDataStore rebateDataStore, IEnumerable<IIncentiveCalculationStrategy> strategies) {
        _productDataStore = productDataStore;
        _rebateDataStore = rebateDataStore;
        _strategies = strategies;
    }
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {

        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();

        var rebateAmount = 0m;

        foreach (var strategy in _strategies)
        {
            if (strategy.CanCalculate(rebate.Incentive))
            {
                var resultCalc = strategy.Calculate(rebate, product, request);
                rebateAmount = resultCalc.Amount;
                result.Success = resultCalc.Success;
            }
        }

        if (result.Success)
        {
            Console.WriteLine(rebate.Amount);
            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
            Console.WriteLine(rebateAmount);
        }

        return result;
    }

    // Prints result after the process
    public static void PrintObjectProperties(object obj)
    {
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            string propertyName = property.Name;
            object propertyValue = property.GetValue(obj);

            Console.WriteLine($"{propertyName}: {propertyValue}");
        }
    }
}
