using System.Collections.Generic;
using Smartwyre.DeveloperTest.Types;

public class SingletonDatabase
{
    private static SingletonDatabase _instance;

    // Locking the object to make the operation secure for subprecess
    private static readonly object _lock = new();

    // DB collection to emulate date
    public List<Product> Products { get; private set; }
    public List<Rebate> Rebates { get; private set; }

    public SingletonDatabase()
    {
        Products = new List<Product>();
        Rebates = new List<Rebate>();
    }

    // Method to obtain the instance of the singleton
    public static SingletonDatabase GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                _instance ??= new SingletonDatabase();
            }
        }
        return _instance;
    }
}
