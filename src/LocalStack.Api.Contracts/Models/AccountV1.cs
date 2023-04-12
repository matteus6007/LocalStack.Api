using System;

namespace LocalStack.Api.Contracts.Models
{
    public class AccountV1
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
