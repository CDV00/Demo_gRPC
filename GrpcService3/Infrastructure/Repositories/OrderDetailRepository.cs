using GrpcService3.Domain.Entities;
using GrpcService3.Domain.Interfaces;

namespace GrpcService3.Infrastructure.Repositories
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly OrderingContext _context;
        public OrderDetailRepository(OrderingContext context) : base(context)
        {
            _context = context;
        }
    }
}
