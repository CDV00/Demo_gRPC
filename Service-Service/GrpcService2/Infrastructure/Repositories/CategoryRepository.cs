using GrpcService2.Domain.Interfaces;
using GrpcService2.Domain.Entities;
using GrpcService2.Infrastructure.Data;
using GrpcService2.Infrastructure.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService2.Infrastructure.Repositories
{
    public class CategoryRepository: BaseRepository<Category>,ICategoryRepository
    {
        private readonly DemoContext _context;
        public CategoryRepository(DemoContext context):base(context)
        {
            _context = context;
        }

        public async Task<CategoryReponse> GetCategory()
        {
            try
            {
                var categoryss = _context.Categories.AsEnumerable();
                IQueryable productss = _context.Products;
                IQueryable detailss= _context.Details;

                /*var Result = (from ca in categoryss
                              join po in productss ca.Id equals po.)*/

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
