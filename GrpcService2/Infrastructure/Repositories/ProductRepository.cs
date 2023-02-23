using GrpcService2.Domain.Interfaces;
using GrpcService2.Domain.Entities;
using GrpcService2.Infrastructure.Data;
using System;
using System.Linq;

namespace GrpcService2.Infrastructure.Repositories
{
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        private readonly DemoContext _context;
        public ProductRepository(DemoContext context) : base(context)
        {
            _context = context;
        }
        public bool CheckExistByCategoryId(Guid id)
        {
            if (_context.Products.Where(m => m.CategoryId == id).Count() < 0)
                return false;
            return true;
        }
    }
}
