using Serilog.Events;

namespace MegaSchool1.Model;

public class MemorySink : Serilog.Core.ILogEventSink, IDisposable
{
    private static readonly MemorySink LocalInstance = new();

    private readonly List<LogEvent> _logEvents;
    private readonly object _snapShotLock = new object();

    public MemorySink() : this(new List<LogEvent>())
    {
    }

    protected MemorySink(List<LogEvent> logEvents)
    {
        _logEvents = logEvents;
    }

    public static MemorySink Instance => LocalInstance;

    public IEnumerable<LogEvent> LogEvents => _logEvents.AsReadOnly();

    public void Dispose()
    {
        _logEvents.Clear();
    }

    public virtual void Emit(LogEvent logEvent)
    {
        lock (_snapShotLock)
        {
            _logEvents.Add(logEvent);
        }
    }
}