using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using AutoFixture.Xunit2;
using LocalStack.Api.Contracts.Models;
using LocalStack.Api.Domain.Services;
using LocalStack.Api.Infrastructure.Repositories.DynamoDb;
using Shouldly;
using Xunit;

namespace LocalStack.Api.Infrastructure.Integration.Tests.Repositories
{
    public class AccountRepositoryTests
    {
        private const string TableName = "Accounts";

        private readonly IAmazonDynamoDB _client;
        private readonly IRepository<AccountV1> _sut;

        public AccountRepositoryTests()
        {
            var credentials = new BasicAWSCredentials("test", "test");

            var dynamoDbConfig = new AmazonDynamoDBConfig
            {
                ServiceURL = "http://localhost:4566",
                UseHttp = true
            };

            _client = new AmazonDynamoDBClient(credentials, dynamoDbConfig);

            _sut = new AccountRepository(_client);
        }

        [Theory]
        [AutoData]
        public async Task SaveAsync_WhenAccountDoesNotExist_ThenSaveIsSuccessful(AccountV1 account)
        {
            // Act
            await _sut.SaveAsync<AccountV1>(account);

            // Assert
            var result = await ThenAccountExists(account);

            result.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public async Task GetByIdAsync_WhenAccountExists_ThenShouldReturnAccount(AccountV1 account)
        {
            // Arrange
            await GivenAccountExists(account);

            // Act
            var result = await _sut.GetByIdAsync(account.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(account.Id);
            result.Name.ShouldBe(account.Name);
            result.CreatedOn.ShouldBe(account.CreatedOn, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [AutoData]
        public async Task GetByIdAsync_WhenAccountDoesNotExist_ThenShouldReturnNull(Guid id)
        {
            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            result.ShouldBeNull();
        }

        private async Task GivenAccountExists(AccountV1 account)
        {
            var properties = new Dictionary<string, AttributeValue>
            {
                { nameof(account.Id), new AttributeValue(account.Id.ToString()) },
                { nameof(account.Name), new AttributeValue(account.Name) },
                { nameof(account.CreatedOn), new AttributeValue(account.CreatedOn.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ")) }
            };

            var request = new PutItemRequest(TableName, properties);

            await _client.PutItemAsync(request);
        }

        private async Task<GetItemResponse> ThenAccountExists(AccountV1 account)
        {
            var key = new Dictionary<string, AttributeValue>
            {
                { nameof(account.Id), new AttributeValue(account.Id.ToString()) }
            };

            return await _client.GetItemAsync(TableName, key);
        }
    }
}
