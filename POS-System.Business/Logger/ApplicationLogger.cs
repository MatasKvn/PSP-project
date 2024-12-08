using Microsoft.Extensions.Logging;

namespace POS_System.Business.Logger
{
    public class ApplicationLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly Func<ApplicationLoggerOptions> _getOptions;
        private DateTime _lastEventFileCreatedTime;
        private DateTime _lastExceptionFileCreatedTime;
        private string? _currentFileName;

        public ApplicationLogger(string categoryName, Func<ApplicationLoggerOptions> getOptions)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException($"'{nameof(categoryName)}' cannot be null or empty.", nameof(categoryName));
            }

            _categoryName = categoryName;
            _getOptions = getOptions ?? throw new ArgumentNullException(nameof(getOptions));
            _lastEventFileCreatedTime = DateTime.UtcNow;
            _lastExceptionFileCreatedTime = DateTime.UtcNow;
            
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return new MemoryStream();
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var options = _getOptions();
            string logEntry = $"[{_categoryName}, {logLevel}] {formatter(state, exception)} [{DateTime.UtcNow}]";

            try
            {
                if (logLevel >= LogLevel.Warning)
                {
                    string exceptionFilePath = GetTimestampedFilePath(options.Exceptions, ref _lastExceptionFileCreatedTime);
                    WriteLogToFile(exceptionFilePath, logEntry);
                }
                else
                {
                    string eventFilePath = GetTimestampedFilePath(options.Events, ref _lastEventFileCreatedTime);
                    WriteLogToFile(eventFilePath, logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log: {ex.Message}");
            }
        }

        private string GetTimestampedFilePath(LogFileOptions logFileOptions, ref DateTime lastFileCreatedTime)
        {
            if (DateTime.UtcNow - lastFileCreatedTime > logFileOptions.FileCreationInterval || _currentFileName is null)
            {
                lastFileCreatedTime = DateTime.UtcNow;

                string directory = Path.GetDirectoryName(logFileOptions.Path) ?? string.Empty;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(logFileOptions.Path);
                string extension = Path.GetExtension(logFileOptions.Path);
                string timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                _currentFileName = Path.Combine(directory, $"{fileNameWithoutExtension}_{timeStamp}{extension}");
            }
            return _currentFileName;
        }

        private static void WriteLogToFile(string filePath, string logEntry)
        {
            using (Stream outputStream = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.None))
            using (StreamWriter writer = new StreamWriter(outputStream))
            {
                writer.WriteLine(logEntry);
            }
        }
    }

}
