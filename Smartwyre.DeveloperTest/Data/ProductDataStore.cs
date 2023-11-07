using System.Linq;
using Smartwyre.DeveloperTest.Interfaces.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    // SingletonDatabase _products;
    public ProductDataStore() {
        // _products = products;
    }
    public Product GetProduct(string productIdentifier)
    {
        SingletonDatabase singletonDatabase = SingletonDatabase.GetInstance();
        // Access database to retrieve account, code removed for brevity 

        return singletonDatabase.Products.FirstOrDefault();
    }
}
