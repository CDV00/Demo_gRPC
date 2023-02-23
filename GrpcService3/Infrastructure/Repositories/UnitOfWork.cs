using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcService3.Domain.Interfaces;
using GrpcService3.Domain.Entities;

namespace GrpcService3.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly OrderingContext _context;

        public UnitOfWork(OrderingContext context)
        {
            _context = context;
        }

        
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
