using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveTypesCalculations
{
    public class FixedRateRebateCalculationStrategy : IIncentiveCalculationStrategy
    {
        public bool CanCalculate(IncentiveType incentiveType)
        {
            return incentiveType == IncentiveType.FixedRateRebate;
        }

        public CalculateRebateResultDto Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            var result = new CalculateRebateResultDto();

            if (rebate == null || product == null || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) || rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            {
                result.Success = false;
            }
            else
            {
                result.Success = true;
                result.Amount = product.Price * rebate.Percentage * request.Volume;
            }

            return result;
        }
    }
}