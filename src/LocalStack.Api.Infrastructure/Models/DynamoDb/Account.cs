using System;
using Amazon.DynamoDBv2.DataModel;

namespace LocalStack.Api.Infrastructure.Models.DynamoDb
{
    [DynamoDBTable("Accounts")]
    public class Account
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public DateTime CreatedOn { get; set; }
    }
}
