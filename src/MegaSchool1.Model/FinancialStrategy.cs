using Foundation.Model;
using ValueOf;

namespace MegaSchool1.Model;

public enum Epoch
{
    None = 0,
    Monthly = 1,
    Annual = 2,
}

public class USD : ValueOf.ValueOf<int, USD>;

public interface ICapitalEntity
{
    public IEnumerable<(string Description, USD Amount)> StartupExpenses { get; }
    
    public IEnumerable<(string Description, USD Amount, Epoch Frequency)> Expenses { get; }
    
    public IEnumerable<(string Description, USD Amount, Epoch Frequency)> Incomes { get; }
    
    public IEnumerable<(string Description, USD Limit, Percentage APR, USD Balance)> CreditLines { get; }
}

public class CapitalEntityBase : ICapitalEntity
{
    private readonly List<(string Description, USD amount, Epoch Frequency)> _expenses = new();
    private readonly List<(string Description, USD amount, Epoch Frequency)> _incomes = new();

    public IEnumerable<(string Description, USD Amount)> StartupExpenses { get; }
    public IEnumerable<(string Description, USD Amount, Epoch Frequency)> Expenses => _expenses;
    public IEnumerable<(string Description, USD Amount, Epoch Frequency)> Incomes => _incomes;
    public IEnumerable<(string Description, USD Limit, Percentage APR, USD Balance)> CreditLines { get; }
    
    protected void AddExpense(string description, USD amount, Epoch frequency) => _expenses.Add((description, amount, frequency));
    
    protected void AddIncome(string description, USD amount, Epoch frequency) => _incomes.Add((description, amount, frequency));
}

public class BusinessTrust : CapitalEntityBase
{
    public BusinessTrust(USD startCost)
    {
        //StartupExpenses = new ;
        
        AddExpense("Starting cost", USD.From(1), Epoch.Annual);
        
        AddIncome("Starting cost", USD.From(2), Epoch.Annual);
    }
}

public class BeneficialTrust : CapitalEntityBase
{
    public BeneficialTrust(USD startCost)
    {
        //StartupExpenses = new ;
        
        AddExpense("Starting cost", USD.From(1), Epoch.Annual);
        
        AddIncome("Starting cost", USD.From(2), Epoch.Annual);
    } 
}

public class PrivateFoundation : CapitalEntityBase
{
    public PrivateFoundation(USD startCost)
    {
        //StartupExpenses = new ;
        
        AddExpense("Starting cost", USD.From(1), Epoch.Annual);
        
        AddIncome("Starting cost", USD.From(2), Epoch.Annual);
    } 
}

public class LLC : CapitalEntityBase
{
    public LLC(USD startCost)
    {
        //StartupExpenses = new ;
        
        AddExpense("Starting cost", USD.From(1), Epoch.Annual);
        
        AddIncome("Starting cost", USD.From(2), Epoch.Annual);
    } 
}

public class Organization
{
    private readonly List<ICapitalEntity> _entities = new();

    public USD StartupCost => USD.From(Entities.SelectMany(entity => entity.StartupExpenses).Sum(expense => expense.Amount.Value));
        
    public IEnumerable<ICapitalEntity> Entities => _entities;
    
    public void AddEntity(ICapitalEntity entity) => _entities.Add(entity);
}

public class FinancialStrategy
{
    public static Organization Foundation()
    {
        var organization = new Organization();
        
        organization.AddEntity(new BusinessTrust(USD.From(0)));
        organization.AddEntity(new BeneficialTrust(USD.From(0)));
        organization.AddEntity(new PrivateFoundation(USD.From(0)));
        
        return organization;
    }
}