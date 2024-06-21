using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Text.Json.Serialization;
using System.Text.Json;
using AwsLambdaFunctionSecond.Models;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AwsLambdaFunctionSecond;

// If class name is changed => update "function-handler" in aws-lambda-tools-defaults.json
public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    // If method name is changed => update "function-handler" in aws-lambda-tools-defaults.json
    public string FunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        var stringToReturn = new StringBuilder()
            .AppendLine(string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH/mm/ss")))
            .AppendLine("-------------------")
            .AppendLine(DeserializeRequest(request).ToString())
            .ToString()
            .ToUpper();

        return stringToReturn;
    }

    private static Note DeserializeRequest(APIGatewayHttpApiV2ProxyRequest request)
    {
        var serializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        return request == null || string.IsNullOrWhiteSpace(request?.Body)
            ? new Note()
            : JsonSerializer.Deserialize<Note>(request.Body, serializationOptions);
    }
}
