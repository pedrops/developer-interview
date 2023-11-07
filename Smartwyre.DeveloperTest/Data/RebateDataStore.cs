using System.Linq;
using Smartwyre.DeveloperTest.Interfaces.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    // SingletonDatabase _singletonDatabase;

    public RebateDataStore(){
        // _singletonDatabase = singletonDatabase;
    }
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity
        SingletonDatabase singletonDatabase = SingletonDatabase.GetInstance();
        return singletonDatabase.Rebates.FirstOrDefault();
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // TODO: Since this is using a simple singleton, this might be updating real data
        // Update account in database, code removed for brevity
        SingletonDatabase singletonDatabase = SingletonDatabase.GetInstance();
        var s = singletonDatabase.Rebates.FirstOrDefault();
        s.Amount = rebateAmount;
    }
}
