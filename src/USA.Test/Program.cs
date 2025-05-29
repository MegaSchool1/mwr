using FakeItEasy;
using Microsoft.FeatureManagement;
using OneOf.Types;
using Stellar;
using StellarDotnetSdk.Accounts;
using StellarDotnetSdk.Transactions;
using TUnit.Core;
using USA.Model;

namespace USA.Test;

public class Program
{
    [Test]
    public async Task Test1()
    {
        await Protocol.RunAsync();
    }

    [Test]
    public async Task UniversalBasicIncome()
    {
        // arrange
        var featureManager = A.Fake<IFeatureManager>();
        
        A.CallTo(() => featureManager.IsEnabledAsync(A<string>.Ignored))
            .Returns(false);
        
        // act
        await Protocol.UniversalBasicIncomeAsync(featureManager);
    }
}
