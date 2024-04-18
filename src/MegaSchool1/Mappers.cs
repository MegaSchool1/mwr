using MegaSchool1.Model;
using MegaSchool1.ViewModel;
using Riok.Mapperly.Abstractions;

namespace MWRCheatSheet;

[Mapper]
public partial class Mappers
{
    public partial EventViewModel EventDtoToEventViewModel(EventDto eventDto);
}