using FluentAssertions;
using Microsoft.VisualBasic;
using NUnit.Framework;

namespace MegaSchool1.Model.Test;

[TestFixture]
public class FinancialStrategyTests
{
    [Test]
    public void Foundation()
    {
        // act
        var sut = FinancialStrategy.Foundation();
        
        // assert

        // total startup cost
        sut.StartupCost.Value
            .Should().Be(50 * 1000);
        
        // entities
        sut.Entities.OfType<BusinessTrust>()
            .Should().HaveCount(1);
        
        sut.Entities.OfType<BeneficialTrust>()
            .Should().HaveCount(1);
        
        sut.Entities.OfType<PrivateFoundation>()
            .Should().HaveCount(1);

        sut.Entities
            .Should().HaveCount(
                sut.Entities.OfType<BusinessTrust>().Count()
                +
                sut.Entities.OfType<BeneficialTrust>().Count()
                +
                sut.Entities.OfType<PrivateFoundation>().Count()
                +
                sut.Entities.OfType<LLC>().Count());
        
        ValidateBusinessTrust(sut.Entities.OfType<BusinessTrust>().First());
        
        ValidateBeneficialTrust(sut.Entities.OfType<BeneficialTrust>().First());

        ValidatePrivateFoundation(sut.Entities.OfType<PrivateFoundation>().First());

        var llcs = sut.Entities.OfType<LLC>().ToArray();
        llcs.Should().SatisfyRespectively(
            first =>
            {
                var totalMonthlyExpense = GetTotalMonthlyExpense(first);
                var totalMonthlyIncome = GetTotalMonthlyIncome(first);
                
                totalMonthlyIncome.Value
                    .Should().Be(totalMonthlyExpense.Value);
                
                
            },
            second =>
            {
                var totalMonthlyExpense = GetTotalMonthlyExpense(second);
                var totalMonthlyIncome = GetTotalMonthlyIncome(second);
                
                totalMonthlyIncome.Value
                    .Should().Be(totalMonthlyExpense.Value);
                
            });
    }

    private static void ValidateBusinessTrust(BusinessTrust businessTrust)
    {
        // expenses
        var totalAnnualExpense = USD.From(businessTrust.Expenses.Sum(expense =>
            GetTransactionVolume((expense.Amount, expense.Frequency), Epoch.Annual).Value));

        // income
        var totalAnnualIncome = USD.From(businessTrust.Incomes.Sum(income =>
            GetTransactionVolume((income.Amount, income.Frequency), Epoch.Annual).Value));
        
        // cash flow
        totalAnnualIncome.Value
            .Should().BeGreaterThan(totalAnnualExpense.Value);
        
        
    }

    private static void ValidateBeneficialTrust(BeneficialTrust beneficialTrust)
    {
        // expenses
        var totalAnnualExpense = USD.From(beneficialTrust.Expenses.Sum(expense =>
            GetTransactionVolume((expense.Amount, expense.Frequency), Epoch.Annual).Value));

        // income
        var totalAnnualIncome = USD.From(beneficialTrust.Incomes.Sum(income =>
            GetTransactionVolume((income.Amount, income.Frequency), Epoch.Annual).Value));
        
        // cash flow
        totalAnnualIncome.Value
            .Should().BeGreaterThan(totalAnnualExpense.Value);
    }
    
    private static void ValidatePrivateFoundation(PrivateFoundation privateFoundation)
    {
        // expenses
        var totalAnnualExpense = USD.From(privateFoundation.Expenses.Sum(expense =>
            GetTransactionVolume((expense.Amount, expense.Frequency), Epoch.Annual).Value));

        // income
        var totalAnnualIncome = USD.From(privateFoundation.Incomes.Sum(income =>
            GetTransactionVolume((income.Amount, income.Frequency), Epoch.Annual).Value));
        
        // cash flow
        totalAnnualIncome.Value
            .Should().BeGreaterThan(totalAnnualExpense.Value);
    }

    private static void ValidateLLC(LLC llc)
    {
        // expenses
        var totalAnnualExpense = USD.From(llc.Expenses.Sum(expense =>
            GetTransactionVolume((expense.Amount, expense.Frequency), Epoch.Annual).Value));

        // income
        var totalAnnualIncome = USD.From(llc.Incomes.Sum(income =>
            GetTransactionVolume((income.Amount, income.Frequency), Epoch.Annual).Value));
        
        // cash flow
        totalAnnualIncome.Value
            .Should().BeGreaterThan(totalAnnualExpense.Value);
    }

    private static USD GetTotalMonthlyExpense(ICapitalEntity entity) => 
        USD.From(entity.Expenses.Sum(expense =>
            GetTransactionVolume((expense.Amount, expense.Frequency), Epoch.Monthly).Value));
 
    private static USD GetTotalMonthlyIncome(ICapitalEntity entity) => 
        USD.From(entity.Incomes.Sum(income =>
            GetTransactionVolume((income.Amount, income.Frequency), Epoch.Monthly).Value));

    private static USD GetTransactionVolume((USD Amount, Epoch Frequency) regularTransaction, Epoch targetFrequency)
    {
        const int NumMonthsInYear = 12;
        
        var regularMonthlyTransactionVolume = regularTransaction.Frequency == Epoch.Monthly 
            ? regularTransaction.Amount : USD.From(regularTransaction.Amount.Value * NumMonthsInYear); 
        
        return targetFrequency == Epoch.Monthly ? regularMonthlyTransactionVolume : USD.From(regularTransaction.Amount.Value * NumMonthsInYear);
    }
}