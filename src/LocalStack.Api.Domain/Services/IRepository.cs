using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalStack.Api.Domain.Services
{
    public interface IRepository<TData>
    {
        Task SaveAsync<T>(TData item);

        Task<TData> GetByIdAsync(Guid id);

        Task<List<TData>> GetAll();
    }
}
