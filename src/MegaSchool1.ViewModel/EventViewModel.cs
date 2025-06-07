using MegaSchool1.Model;
using OneOf;
using OneOf.Types;

namespace MegaSchool1.ViewModel;

public class EventViewModel
{
    public string Header { get; set; } = default!;

    public string? Link { get; set; }

    public string? MeetingId { get; set; }

    public string Image { get; set; } = default!;

    public DayOfWeek? Day { get; set; }

    public TimeOnly Time { get; set; }

    public string ShareableHeader { get; set; } = default!;

    public string? ShareableHost { get; set; }

    public string? ShareableGuest { get; set; }

    public string? VanityLink { get; set; }

    public string HashTags { get; set; } = default!;

    public string Anchor { get; set; } = default!;

    public string? Archive { get; set; }

    public string? Password { get; set; }

    public LivestreamPlatform LivestreamPlatform { get; set; } = LivestreamPlatform.YouTube;

    public OneOf<DateTimeOffset, None> StartDate { get; set; } = new None();

    public OneOf<DateTimeOffset, None> EndDate { get; set; } = new None();
}
