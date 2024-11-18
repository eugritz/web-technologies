using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace WebTechnologies.Importer
{
    internal class NoopDisposable : IDisposable
    {
        public void Dispose()
        {
        }
    }

    internal class SimpleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);
            Console.WriteLine($"{logLevel,-12} {message}");
        }
    }
}
