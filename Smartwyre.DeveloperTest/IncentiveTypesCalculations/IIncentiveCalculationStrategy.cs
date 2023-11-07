using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveTypesCalculations
{
    public interface IIncentiveCalculationStrategy
    {
        bool CanCalculate(IncentiveType incentiveType);
        CalculateRebateResultDto Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}