using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace POS_System.Business.Logger
{
    [ProviderAlias("FileProvider")]
    public sealed class ApplicationLoggerProvider : ILoggerProvider
    {
        private ApplicationLoggerOptions _options;
        private IDisposable? _updateToken;
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new();

        public ApplicationLoggerProvider(IOptionsMonitor<ApplicationLoggerOptions> options)
        {
            _options = options.CurrentValue;
            _updateToken = options.OnChange(updatedOptions => _options = updatedOptions);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, categoryName => new ApplicationLogger(
                categoryName,
                GetCurrentOptions
            ));
        }

        private ApplicationLoggerOptions GetCurrentOptions() => _options;

        public void Dispose()
        {
            _updateToken?.Dispose();
            _updateToken = null;
        }
    }
}
