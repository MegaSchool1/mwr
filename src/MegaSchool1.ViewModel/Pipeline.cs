using MegaSchool1.Model;

namespace MegaSchool1.ViewModel;

public abstract class Pipeline<TSteps> : IPipeline<TSteps>
    where TSteps : struct, Enum
{
    public TSteps FirstStep { get; } = Enum.GetValues<TSteps>().First(step => (int)(object)step != 0);
    public abstract string DisplayName(TSteps step);
    public abstract Strategy Strategy { get; }
}
