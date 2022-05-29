using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AwsLambdaFunction;

public class Function
{
    public string MyLambdaFunctionHandler(string input, ILambdaContext context)
    {
        return input.ToUpper();
    }
}
