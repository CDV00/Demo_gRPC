using GrpcService3.Domain.Interfaces;
using GrpcService3.Domain.Entities;
//using GrpcService3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService3.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;
        private readonly OrderingContext _context;
        public BaseRepository(OrderingContext context)
        {
            DbSet = context.Set<T>();
            _context = context;
        }

        public DbSet<T> Entity()
        {
            return DbSet;
        }

        public IQueryable<T> GetAll() => DbSet.AsNoTracking();

        public virtual async Task<T> GetById(Guid id)
        {
            var data = await DbSet.FindAsync(id);
            return data;
        }
        public virtual async Task CreateAsync(T _object) => await DbSet.AddAsync(_object);
        public virtual bool Update(T _object)
        {
            DbSet.Attach(_object);
            _context.Entry(_object).State = EntityState.Modified;
            return true;

        }
        public virtual bool Delete(T _object)
        {
           /* var data = DbSet.FindAsync(_object);
            if(data == null)
            {
                return false;
            }*/

            DbSet.Remove(_object);
            
            return true;

        }


    }
}
