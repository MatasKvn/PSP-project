namespace POS_System.Business.Logger
{
    public class ApplicationLoggerOptions
    {
        public LogFileOptions Events { get; set; } = new LogFileOptions();
        public LogFileOptions Exceptions { get; set; } = new LogFileOptions();
    }
}
