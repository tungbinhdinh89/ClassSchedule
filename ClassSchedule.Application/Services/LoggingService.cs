using Microsoft.Extensions.Logging;

public class LoggerService : ILogger
{
    public void LogInformation(string message, params object[] args)
    {
        Console.WriteLine($"[INFO]: {FormatMessage(message, args)}");
    }

    public void LogWarning(string message, params object[] args)
    {
        Console.WriteLine($"[WARN]: {FormatMessage(message, args)}");
    }

    public void LogError(string message, params object[] args)
    {
        Console.WriteLine($"[ERROR]: {FormatMessage(message, args)}");
    }

    private string FormatMessage(string message, params object[] args)
    {
        try
        {
            return string.Format(message.Replace("{", "{0:"), args);
        }
        catch
        {
            return message;
        }
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine(formatter(state, exception));
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
}
