using Common.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.EntityFramework
{
    public sealed class EfUnitOfWork(DbContext dbContext) : IUnitOfWork
    {

    }
}
