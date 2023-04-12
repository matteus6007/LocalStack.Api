using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalStack.Api.Contracts.Models;

namespace LocalStack.Api.Domain.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IRepository<AccountV1> _repository;

        public AccountsService(IRepository<AccountV1> repository)
        {
            _repository = repository;
        }

        public async Task SaveAsync(AccountV1 account)
        {
            await this._repository.SaveAsync<AccountV1>(account);
        }

        public async Task<AccountV1> GetById(Guid id)
        {
            return await this._repository.GetByIdAsync(id);
        }

        public async Task<List<AccountV1>> GetAll()
        {
            return await this._repository.GetAll();
        }
    }
}
