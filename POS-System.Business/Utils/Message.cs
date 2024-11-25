using MimeKit;

namespace POS_System.Business.Utils
{
    public class Message(string sendTo, string subject, string content)
    {
        public MailboxAddress SendTo => new (Name, sendTo);
        public string Name = "POS-System.Recovery";
        public string Subject => subject;
        public string Content => content;
    }
}