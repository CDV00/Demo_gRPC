using GrpcService2.Domain.Entities;
using System;

namespace GrpcService2.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        bool CheckExistByCategoryId(Guid id);
    }
}
