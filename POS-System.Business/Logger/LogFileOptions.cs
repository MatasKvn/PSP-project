namespace POS_System.Business.Logger
{
    public class LogFileOptions
    {
        public String Path { get; set; } = "Logs/log.log";
        public TimeSpan FileCreationInterval { get; set; } = TimeSpan.FromHours(24);
    }
}
