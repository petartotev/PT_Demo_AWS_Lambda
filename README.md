# PT_Demo_AWS_Lambda

## Contents

- [AwsLambdaFunction (2022)](#awslambdafunction-2022)
- [AwsLambdaFunctionSecond (2024)](#awslambdafunctionsecond-2024)
    - [Configure New Lambda](#configure-new-lambda)
    - [Implement Lambda](#implement-lambda)
    - [Deploy Lambda](#deploy-lambda)
    - [Execute Lambda using Function URL](#execute-lambda-using-function-url)
    - [Delete Lambda](#delete-lambda)
- [Links](#links)

## AwsLambdaFunction (2022)

## AwsLambdaFunctionSecond (2024)

### Configure New Lambda

1. In cmd.exe, configure AWS Profile:

```
C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond>aws configure --profile meterboteff.biz

AWS Access Key ID [None]: your-aws-account-id-here
AWS Secret Access Key [None]: your-pass-here
Default region name [None]:
Default output format [None]:
```

2. In cmd.exe, install Dotnet Tool `Amazon.Lambda.Tools`:

```
C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond>dotnet tool install -g Amazon.Lambda.Tools --add-source https://api.nuget.org/v3/index.json
You can invoke the tool using the following command: dotnet-lambda
Tool 'amazon.lambda.tools' (version '5.10.6') was successfully installed.
```

3. In cmd.exe, install `Amazon.Lambda.Templates`:

```
C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond>dotnet new --install Amazon.Lambda.Templates

Warning: use of 'dotnet new --install' is deprecated. Use 'dotnet new install' instead.
For more information, run:
   dotnet new install -h

The following template packages will be installed:
   Amazon.Lambda.Templates

Success: Amazon.Lambda.Templates::7.2.0 installed the following templates:
Template Name                                     Short Name                        Language  Tags
------------------------------------------------  --------------------------------  --------  ---------------------
...
Lambda Empty Function                             lambda.EmptyFunction              [C#],F#   AWS/Lambda/Function
Lambda Empty Function (.NET .8 Container Image)   lambda.image.EmptyFunction        [C#],F#   AWS/Lambda/Function
Lambda Empty Serverless                           serverless.EmptyServerless        [C#],F#   AWS/Lambda/Serverles
Lambda Empty Serverless (.NET 8 Container Image)  serverless.image.EmptyServerless  [C#],F#   AWS/Lambda/Serverles
...
```

4. In cmd.exe, create new Lambda using template `lambda.EmptyFunction`:

```
C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond>dotnet new lambda.EmptyFunction --name AwsLambdaFunctionSecond

The template "Lambda Empty Function" was created successfully.
```

### Implement Lambda

5. CREATE NEW BLANK .NET SOLUTION AND ADD EXISTING PROJECTS CREATED IN STEP 4:
- src/AwsLambdaFunctionSecond.csproj
- src/AwsLambdaFunctionSecond.Tests.csproj

6. In the 2 Projects, install the following NuGet packages:
- AwsLambdaFunctionSecond
    - `Amazon.Lambda.APIGatewayEvents`
- AwsLambdaFunctionSecond.Tests
    - `FluentAssertions`

7. IMPLEMENT YOUR CODE IN ENTRY POINT `Function.cs`:

### Deploy Lambda

8. In cmd.exe, change current directory to Lambda's `Function.cs`:

```
C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond>cd src\AwsLambdaFunctionSecond
```

9. In cmd.exe, deploy Lambda remotely in AWS:

```
C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond>dotnet lambda deploy-function AwsLambdaFunctionSecond --add-source https://api.nuget.org/v3/index.json
Amazon Lambda Tools for .NET Core applications (5.10.6)
Project Home: https://github.com/aws/aws-extensions-for-dotnet-cli, https://github.com/aws/aws-lambda-dotnet

Executing publish command
Deleted previous publish folder
... invoking 'dotnet publish', working folder 'C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\publish'
... dotnet publish "C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond" --output "C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\publish" --configuration "Release" --framework "net8.0" /p:GenerateRuntimeConfigurationFiles=true --runtime linux-x64 --self-contained False
... publish:   Determining projects to restore...
... publish:   All projects are up-to-date for restore.
... publish:   AwsLambdaFunctionSecond -> C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\linux-x64\AwsLambdaFunctionSecond.dll
... publish:   AwsLambdaFunctionSecond -> C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\publish\
Zipping publish folder C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\publish to C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\AwsLambdaFunctionSecond.zip
... zipping: Amazon.Lambda.APIGatewayEvents.dll
... zipping: Amazon.Lambda.Core.dll
... zipping: Amazon.Lambda.Serialization.SystemTextJson.dll
... zipping: AwsLambdaFunctionSecond.deps.json
... zipping: AwsLambdaFunctionSecond.dll
... zipping: AwsLambdaFunctionSecond.pdb
... zipping: AwsLambdaFunctionSecond.runtimeconfig.json
Created publish archive (C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\AwsLambdaFunctionSecond.zip).
Creating new Lambda function AwsLambdaFunctionSecond
Select IAM Role that to provide AWS credentials to your code:
    1) MyRole1
    2) MyLambdaRole123
    3) myHelloWorld-role-3
    ...
    8) *** Create new IAM Role *** ########## THIS!!!
8
Enter name of the new IAM Role:
AwsLambdaFunctionSecondIamRole
Select IAM Policy to attach to the new role and grant permissions
    1) AWSLambdaExecute (Provides Put, Get access to S3 and full access to CloudWatch Logs.)
    2) AWSLambdaInvocation-DynamoDB (Provides read access to DynamoDB Streams.)
    3) AWSLambdaRole (Default policy for AWS Lambda service role.)
    ...
    12) AWSLambda_ReadOnlyAccess (Grants read-only access to AWS Lambda service, AWS Lambda consol ...)
    13) AWSLambda_FullAccess (Grants full access to AWS Lambda service, AWS Lambda console feature ...) ########## THIS!!!
    ...
    21) *** No policy, add permissions later ***
13
Waiting for new IAM Role to propagate to AWS regions
...............  Done
New Lambda function created
```

⚠️ **WARNING:** If you create a new IAM Role for the Lambda and add permission `AWSLambda_FullAccess`, it gets publically accessible, ergo - vulnerable. Do this only for test purposes and for shorts period of time before deleting the Lambda and the IAM Role. 

10. In cmd.exe, redeploy Lambda remotely in AWS in case of codebase changes:

```
C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond>dotnet lambda deploy-function AwsLambdaFunctionSecond --add-source https://api.nuget.org/v3/index.json
Amazon Lambda Tools for .NET Core applications (5.10.6)
Project Home: https://github.com/aws/aws-extensions-for-dotnet-cli, https://github.com/aws/aws-lambda-dotnet

Executing publish command
Deleted previous publish folder
... invoking 'dotnet publish', working folder 'C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\publish'
... dotnet publish "C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond" --output "C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\publish" --configuration "Release" --framework "net8.0" /p:GenerateRuntimeConfigurationFiles=true --runtime linux-x64 --self-contained False
... publish:   Determining projects to restore...
... publish:   All projects are up-to-date for restore.
... publish: C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\Function.cs(44,16): warning CS8603: Possible null reference return. [C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\AwsLambdaFunctionSecond.csproj]
... publish:   AwsLambdaFunctionSecond -> C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\linux-x64\AwsLambdaFunctionSecond.dll
... publish:   AwsLambdaFunctionSecond -> C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\publish\
Zipping publish folder C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\publish to C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\AwsLambdaFunctionSecond.zip
... zipping: Amazon.Lambda.APIGatewayEvents.dll
... zipping: Amazon.Lambda.Core.dll
... zipping: Amazon.Lambda.Serialization.SystemTextJson.dll
... zipping: AwsLambdaFunctionSecond.deps.json
... zipping: AwsLambdaFunctionSecond.dll
... zipping: AwsLambdaFunctionSecond.pdb
... zipping: AwsLambdaFunctionSecond.runtimeconfig.json
Created publish archive (C:\mydir\PT_Demo_AWS_Lambda\src\AwsLambdaFunctionSecond\src\AwsLambdaFunctionSecond\bin\Release\net8.0\AwsLambdaFunctionSecond.zip).
Updating code for existing function AwsLambdaFunctionSecond
```

### Execute Lambda using Function URL

11. Go to AWS Console > Choose Region > Lambda > Check available Lambdas.  
Open `AwsLambdaFunctionSecond`, which should be deployed and ready to be used.

12. Open `Configuration` tab > `Function URL` > [ Create Function URL ]

- Auth type: NONE (Lambda won't perform IAM authentication on requests to your function URL)
- Additional settings
    - Invoke mode: BUFFERED (default)
- [ Save ]

⚠️ WARNING: Using Auth type: NONE is the easiest and fastest approach for the purpose of the demo, but it should be avoided in any other case.  

Finally, go to Lambda's main page and copy `Function URL`.

13. In Postman, you can invoke the Lambda using the `Function URL` provided in the last steps:

```
https://54fakefunctionurltigrewc7istogq1.lambda-url.eu-central-1.on.aws/

Body:
{
    "Title": "MyTitle",
    "Description": "MyDescription",
    "Author": "MyAuthor",
    "CreatedDate": "2024-06-01",
    "IsInEnglish": true,
    "Priority": 1
}
```

Output:

```
2024-06-21 19/10/45
-------------------
TITLE: MYTITLE
DESCRIPTION: MYDESCRIPTION
AUTHOR: MYAUTHOR
CREATEDDATE: 2024-06-01 00/00/00
ISINENGLISH: TRUE
PRIORITY: 1
```

✅ SUCCESS!

### Delete Lambda

14. At the end of the Demo, don't forget to remove the following infrastructure from AWS:
- Lambda
- IAM Role
- IAM Policy
- IAM User Groups?

## Links
- https://docs.aws.amazon.com/lambda/latest/dg/urls-tutorial.html