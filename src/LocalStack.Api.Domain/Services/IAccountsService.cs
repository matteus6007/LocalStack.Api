using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalStack.Api.Contracts.Models;

namespace LocalStack.Api.Domain.Services
{
    public interface IAccountsService
    {
        Task SaveAsync(AccountV1 account);

        Task<AccountV1> GetById(Guid id);

        Task<List<AccountV1>> GetAll();
    }
}
