using LocalStack.Api.Contracts.Models;
using LocalStack.Api.Domain.Services;
using LocalStack.Api.Infrastructure.Repositories.DynamoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalStack.Api.Infrastructure
{
    public static class DependenciesConfig
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAccountsService, AccountsService>();
            services.AddSingleton<IRepository<AccountV1>, AccountRepository>();
        }
    }
}
