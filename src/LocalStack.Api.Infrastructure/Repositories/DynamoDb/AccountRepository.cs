using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using LocalStack.Api.Contracts.Models;
using LocalStack.Api.Domain.Services;
using LocalStack.Api.Infrastructure.Models.DynamoDb;

namespace LocalStack.Api.Infrastructure.Repositories.DynamoDb
{
    public class AccountRepository : IRepository<AccountV1>
    {
        private readonly IAmazonDynamoDB _client;

        public AccountRepository(IAmazonDynamoDB client)
        {
            _client = client;
        }

        public async Task SaveAsync<T>(AccountV1 item)
        {
            var account = new Account
            {
                Id = item.Id.ToString(),
                Name = item.Name,
                CreatedOn = item.CreatedOn
            };

            using (var context = new DynamoDBContext(_client))
            {
                await context.SaveAsync(account);
            }
        }

        public async Task<AccountV1> GetByIdAsync(Guid id)
        {
            using (var context = new DynamoDBContext(_client))
            {
                var account = await context.LoadAsync<Account>(id.ToString());

                return account == null ? null : MapFromAccount(account);
            }
        }

        public async Task<List<AccountV1>> GetAll()
        {
            using (var context = new DynamoDBContext(_client))
            {
                // DO NOT do this in production
                var accounts = await context.ScanAsync<Account>(new List<ScanCondition>()).GetRemainingAsync();

                return accounts.Select(MapFromAccount).ToList();
            }
        }

        private static AccountV1 MapFromAccount(Account account)
        {
            return new AccountV1
            {
                Id = new Guid(account.Id),
                Name = account.Name,
                CreatedOn = account.CreatedOn
            };
        }
    }
}
