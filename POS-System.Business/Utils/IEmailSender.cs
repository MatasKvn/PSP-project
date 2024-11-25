namespace POS_System.Business.Utils
{
    public interface IEmailSender
    {
        public Task SendAsync(Message message); 
    }
}