using GrpcService2.Domain.Entities;
using System;

namespace GrpcService2.Domain.Interfaces
{
    public interface IDetailRepository : IRepository<Detail>
    {
        Guid FindByProductId(Guid id);
    }
}
