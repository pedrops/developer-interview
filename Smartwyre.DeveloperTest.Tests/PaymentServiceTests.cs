using System.Collections.Generic;
using Moq;
using Smartwyre.DeveloperTest.IncentiveTypesCalculations;
using Smartwyre.DeveloperTest.Interfaces.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private static RebateService MockRebateGeneralService(Rebate rebate, Product product)
        {
            var productMock = new Mock<IProductDataStore>();
            productMock.Setup(d => d.GetProduct(product.Identifier)).Returns(product);

            var rebateMock = new Mock<IRebateDataStore>();
            rebateMock.Setup(d => d.GetRebate(rebate.Identifier)).Returns(rebate);


            // Create a mock of IEnumerable<IIncentiveCalculationStrategy> strategies
            var fixedCashAmountCalculationStrategyMock = new Mock<IIncentiveCalculationStrategy>();
            var fixedRateRebateCalculationStrategyMock = new Mock<IIncentiveCalculationStrategy>();
            var amountPerUomCalculationStrategyMock = new Mock<IIncentiveCalculationStrategy>();

            // Put the strategy mocks in a list
            var strategies = new List<IIncentiveCalculationStrategy>
        {
            fixedCashAmountCalculationStrategyMock.Object,
            fixedRateRebateCalculationStrategyMock.Object,
            amountPerUomCalculationStrategyMock.Object
        };

            // Return the rebate service with the mocked data stores
            return new RebateService(productMock.Object, rebateMock.Object, strategies);
        }

        [Fact]
        public void Validate_InvalidRateRebate_NoSuccess()
        {
            var rebate = new Rebate
            {
                Percentage = 1,
                Identifier = "1",
                Incentive = IncentiveType.FixedRateRebate,
            };
            var product = new Product
            {
                Identifier = "11",
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 11
            };

            var rebateCalculator = MockRebateGeneralService(rebate, product);

            var rebateRequest = new CalculateRebateRequest
            {
                RebateIdentifier = rebate.Identifier,
                ProductIdentifier = product.Identifier,
                Volume = 1
            };

            var result = rebateCalculator.Calculate(rebateRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_AmountUOM_NoSuccess()
        {
            var rebate = new Rebate
            {
                Identifier = "2",
                Incentive = IncentiveType.AmountPerUom,
                Amount = 12
            };
            var product = new Product
            {
                Identifier = "12",
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            };

            var rebateCalculator = MockRebateGeneralService(rebate, product);

            var rebateRequest = new CalculateRebateRequest
            {
                RebateIdentifier = rebate.Identifier,
                ProductIdentifier = product.Identifier,
                Volume = 12
            };

            var result = rebateCalculator.Calculate(rebateRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_WrongIncentive_NoSuccess()
        {
            var rebate = new Rebate
            {
                Identifier = "3",
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 13
            };
            var product = new Product
            {
                Identifier = "13",
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            };

            var rebateCalculator = MockRebateGeneralService(rebate, product);

            var rebateRequest = new CalculateRebateRequest
            {
                RebateIdentifier = rebate.Identifier,
                ProductIdentifier = product.Identifier,
                Volume = 13
            };

            var result = rebateCalculator.Calculate(rebateRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_InvalidInfo_NoSuccess()
        {
            var rebate = new Rebate
            {
                Identifier = "4",
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = 4
            };
            var product = new Product
            {
                Identifier = "14",
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 0
            };

            var rebateCalculator = MockRebateGeneralService(rebate, product);
            var requestRebate = new CalculateRebateRequest
            {
                RebateIdentifier = rebate.Identifier,
                ProductIdentifier = product.Identifier,
                Volume = 14
            };

            var result = rebateCalculator.Calculate(requestRebate);

            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_CashAmount_NoSuccess()
        {
            var rebate = new Rebate
            {
                Percentage = 5,
                Identifier = "5",
                Amount = 5,
                Incentive = IncentiveType.FixedCashAmount,

            };
            var product = new Product
            {
                Identifier = "15",
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };

            var rebateCalc = MockRebateGeneralService(rebate, product);

            var requestCalculate = new CalculateRebateRequest
            {
                RebateIdentifier = rebate.Identifier,
                ProductIdentifier = product.Identifier,
                Volume = 5

            };

            var result = rebateCalc.Calculate(requestCalculate);

            Assert.False(result.Success);
        }
    }
}
