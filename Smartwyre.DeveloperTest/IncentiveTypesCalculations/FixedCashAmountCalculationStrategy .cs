using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveTypesCalculations
{
    public class FixedCashAmountCalculationStrategy : IIncentiveCalculationStrategy
    {
        public bool CanCalculate(IncentiveType incentiveType)
        {
            return incentiveType == IncentiveType.FixedCashAmount;
        }

        // This was obtained instead of the case switch option for extensibility
        public CalculateRebateResultDto Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            var result = new CalculateRebateResultDto();

            if (rebate == null || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) || rebate.Amount == 0)
            {
                result.Success = false;
            }
            else
            {
                result.Amount = rebate.Amount;
                result.Success = true;
            }

            return result;
        }
    }
}