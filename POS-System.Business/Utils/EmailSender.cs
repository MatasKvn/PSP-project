using MailKit.Net.Smtp;
using MimeKit;
using POS_System.Business.Dtos;

namespace POS_System.Business.Utils
{
    public class EmailSender(EmailConfiguration emailConfiguration) : IEmailSender
    {
        public async Task SendAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            using var client = new SmtpClient();

            // Throws exception, add in handler ArgumentNullException
            try
            {
                await client.ConnectAsync(emailConfiguration.SmtpServer, emailConfiguration.Port, true);
                await client.AuthenticateAsync(emailConfiguration.UserName, emailConfiguration.Password);
                await client.SendAsync(emailMessage);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(message.Name, emailConfiguration.From));
            emailMessage.To.Add(message.SendTo);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }
    }
}