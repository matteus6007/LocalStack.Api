using Amazon.DynamoDBv2;
using Amazon.Runtime;
using LocalStack.Api.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalStack.Api.Infrastructure
{
    public static class AwsConfig
    {
        public static void ConfigureDynamoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var dynamoDbConfiguration = configuration.GetSection("Cloudcall:DynamoDb").Get<DynamoDbConfiguration>();

            if (!string.IsNullOrWhiteSpace(dynamoDbConfiguration.ServiceUrlOverride))
            {
                var credentials = new BasicAWSCredentials("test", "test");

                var dynamoDbConfig = new AmazonDynamoDBConfig
                {
                    ServiceURL = dynamoDbConfiguration.ServiceUrlOverride,
                    UseHttp = true
                };

                services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient(credentials, dynamoDbConfig));
            }
            else
            {
                services.AddDefaultAWSOptions(configuration.GetAWSOptions());
                services.AddAWSService<IAmazonDynamoDB>();
            }
        }
    }
}
