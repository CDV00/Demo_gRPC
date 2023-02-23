using GrpcService2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService2.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
