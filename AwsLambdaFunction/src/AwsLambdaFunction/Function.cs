using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AwsLambdaFunction.Core;
using AwsLambdaFunction.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AwsLambdaFunction;

// If class name is changed => update "function-handler" in aws-lambda-tools-defaults.json
public class MyLambdaFunction
{
    // If method name is changed => update "function-handler" in aws-lambda-tools-defaults.json
    public string MyLambdaFunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        var datePrefix = string.Concat(DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"), " | ");

        var subject = datePrefix + "Email sent from AWS Lambda!";
        var body = DeserializeRequest(request).Body ?? "Input was empty. This is a default email body.";

        try
        {
            EmailManager.SendAwsEmail(subject, body, Credentials.Sender.EmailAddress);

            context.Logger.LogInformation(datePrefix + "Email was sent successfully!");
        }
        catch (Exception ex)
        {
            context.Logger.LogError(datePrefix + $"Failed to send email! Error message: {ex.Message}.");
        }

        return body.ToUpper();
    }

    private static EmailModel DeserializeRequest(APIGatewayHttpApiV2ProxyRequest request)
    {
        var serializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        return !string.IsNullOrWhiteSpace(request.Body) ?
            JsonSerializer.Deserialize<EmailModel>(request.Body, serializationOptions) :
            new EmailModel();
    }
}
