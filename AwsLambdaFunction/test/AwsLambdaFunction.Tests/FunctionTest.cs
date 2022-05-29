using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

namespace AwsLambdaFunction.Tests;

public class FunctionTest
{
    private readonly MyLambdaFunction _function = new();
    private readonly TestLambdaContext _context = new();

    [Fact]
    public void TestToUpperFunction_WithValidStringAsInput_SendsEmailAndReturnsInputToUpper()
    {
        var result = _function.MyLambdaFunctionHandler(new APIGatewayHttpApiV2ProxyRequest
        {
            Body = "{ \"Body\": \"hello world 123\" }"
        }, _context);

        Assert.Equal("HELLO WORLD 123", result);
    }

    [Fact]
    public void TestToUpperFunction_WithInvalidInput_SendsEmailAndReturnsNoInputToUpper()
    {
        var result = _function.MyLambdaFunctionHandler(new APIGatewayHttpApiV2ProxyRequest(), _context);

        Assert.Equal("INPUT WAS EMPTY. THIS IS A DEFAULT EMAIL BODY.", result);
    }
}
