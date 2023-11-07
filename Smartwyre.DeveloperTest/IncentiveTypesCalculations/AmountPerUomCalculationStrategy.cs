using Smartwyre.DeveloperTest.Types;
using System.Runtime.CompilerServices;

namespace Smartwyre.DeveloperTest.IncentiveTypesCalculations
{
    public class AmountPerUomCalculationStrategy : IIncentiveCalculationStrategy
    {
        public bool CanCalculate(IncentiveType incentiveType)
        {
            return incentiveType == IncentiveType.AmountPerUom;
        }


        // This was obtained instead of the case switch option for extensibility
        public CalculateRebateResultDto Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            var result = new CalculateRebateResultDto();

            if (rebate == null || product == null || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) || rebate.Amount == 0 || request.Volume == 0)
            {
                result.Success = false;
            }
            else
            {
                result.Success = true;
                result.Amount = rebate.Amount * request.Volume;
            }

            return result;
        }
    }
}