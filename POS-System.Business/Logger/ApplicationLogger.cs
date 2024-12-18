using Microsoft.Extensions.Logging;

namespace POS_System.Business.Logger
{
    public class ApplicationLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly Func<ApplicationLoggerOptions> _getOptions;
        private static DateTime _lastEventFileCreatedTime = DateTime.MinValue;
        private static DateTime _lastExceptionFileCreatedTime = DateTime.MinValue;
        private static string _currentExceptionFileName = "";
        private static string _currentEventFileName = "";

        public ApplicationLogger(string categoryName, Func<ApplicationLoggerOptions> getOptions)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException($"'{nameof(categoryName)}' cannot be null or empty.", nameof(categoryName));
            }

            _categoryName = categoryName;
            _getOptions = getOptions ?? throw new ArgumentNullException(nameof(getOptions));
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
            logEntry = RedactPasswords(logEntry);

            try
            {
                if (logLevel >= LogLevel.Warning)
                {
                    string exceptionFilePath = GetTimestampedFilePath(options.Exceptions, ref _lastExceptionFileCreatedTime, ref _currentExceptionFileName);
                    WriteLogToFile(exceptionFilePath, logEntry);
                }
                else
                {
                    string eventFilePath = GetTimestampedFilePath(options.Events, ref _lastEventFileCreatedTime, ref _currentEventFileName);
                    WriteLogToFile(eventFilePath, logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log: {ex.Message}");
            }
        }

        private static string RedactPasswords(string str)
        {
            string pattern = "\"password\":\\s*\"[^\"]*\"";
            string replacement = "\"password\": \"[REDACTED]\"";

            return System.Text.RegularExpressions.Regex.Replace(str, pattern, replacement);
        }

        private string GetTimestampedFilePath(LogFileOptions logFileOptions, ref DateTime lastFileCreatedTime, ref string _currentFileName)
        {
            if (DateTime.UtcNow - lastFileCreatedTime > logFileOptions.FileCreationInterval)
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
            lock (filePath)
            {
                using (Stream outputStream = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.None))
                using (StreamWriter writer = new StreamWriter(outputStream))
                {
                    writer.WriteLine(logEntry);
                }
            }
        }
    }
}
