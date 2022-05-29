using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using AwsLambdaFunction.Models;
using System.Net;
using System.Net.Mail;

namespace AwsLambdaFunction.Core;

public static class EmailManager
{
    // The configuration set to use for this email. If you do not want to use a configuration set,
    // comment out the following property and the ConfigurationSetName = configSet argument below. 
    private static readonly string configSet = "ConfigSet";

    // The HTML body of the email.
    private static readonly string htmlBody = @"<html>
                                        <head></head>
                                        <body>
                                          <h1>Amazon SES Test (AWS SDK for .NET)</h1>
                                          <p>This email was sent with
                                            <a href='https://aws.amazon.com/ses/'>Amazon SES</a> using the
                                            <a href='https://aws.amazon.com/sdk-for-net/'>
                                              AWS SDK for .NET</a>.</p>
                                        </body>
                                        </html>";

    public static void SendGmailEmail(string subject, string body, string receiver = Credentials.Receiver.EmailAddress)
    {
        MailAddress fromAddress = new(Credentials.Sender.EmailAddress);
        MailAddress toAddress = new(receiver);

        SmtpClient smtp = new()
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, Credentials.Sender.EmailPassword)
        };

        using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
        {
            smtp.Send(message);
        }
    }

    public static void SendAwsEmail(string subject, string body, string receiver = Credentials.Receiver.EmailAddress)
    {
        using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.EUCentral1))
        {
            var sendRequest = new SendEmailRequest
            {
                Source = Credentials.Sender.EmailAddress,
                Destination = new Destination { ToAddresses = new List<string> { receiver } },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        /*Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = htmlBody
                        },*/
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = body
                        }
                    }
                }
                // If you are not using a configuration set, comment or remove the following line:
                // ConfigurationSetName = configSet
            };

            try
            {
                Console.WriteLine("Sending email using Amazon SES...");
                var response = client.SendEmailAsync(sendRequest).GetAwaiter().GetResult();
                Console.WriteLine("The email was sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The email was not sent.");
                Console.WriteLine("Error message: " + ex.Message);
            }
        }
    }
}
