using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using Xunit;

namespace AwsLambdaFunctionSecond.Tests;

public class FunctionTest
{
    private readonly Function _function = new();
    private readonly TestLambdaContext _context = new();

    [Fact]
    public void FunctionHandler_WithValidInputHavingAuthorOnly_ReturnsOutputWithAuthorToUpper()
    {
        var result = _function.FunctionHandler(new APIGatewayHttpApiV2ProxyRequest
        {
            Body = "{ \"Author\": \"Petar\" }"
        }, _context);

        result.Should().Contain("AUTHOR: PETAR");
    }
}
