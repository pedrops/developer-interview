using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Interfaces.Data
{
    public interface IProductDataStore
    {
        public Product GetProduct(string productIdentifier);
    }
}