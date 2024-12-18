using Amazon;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;

namespace POS_System.Business.Utils
{
    public class SmsService(IConfiguration configuration)
    {
        public async Task SendMessageAsync(string phoneNumber, int cartId)
        {
            var accessKeyId = configuration["AWS:AccessKeyId"];
            var secretAccessKey = configuration["AWS:SecretAccessKey"];
            var region = configuration["AWS:Region"];
            var sessionToken = configuration["AWS:SessionToken"];
            var snsClient = new AmazonSimpleNotificationServiceClient(
                new SessionAWSCredentials(accessKeyId, secretAccessKey, sessionToken),
                RegionEndpoint.GetBySystemName(region)
            );

            var request = new PublishRequest
            {

                PhoneNumber = $"+{phoneNumber}",
                Message = $"You order {cartId} was completed successfully. Thank you for purchasing!"
            };

            try
            {
                await snsClient.PublishAsync(request);
            }
            catch(Exception)
            {
                Console.WriteLine("Number was not verified.");
            }
        }
    }
}