using GrpcService3.Domain.Entities;
using GrpcService3.Domain.Interfaces;
using GrpcService3.Infrastructure.Repositories;

namespace GrpcService3.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly OrderingContext _context;
        public OrderRepository(OrderingContext context) : base(context)
        {
            _context = context;
        }
    }
}
