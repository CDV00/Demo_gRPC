using GrpcService3.Domain.Interfaces;
using GrpcService3.Domain.Entities;
using System;
namespace GrpcService3.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}

