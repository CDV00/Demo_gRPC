using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService3.Domain.Interfaces
{
    public interface IUnitOfWork
    {

        Task CommitAsync();
    }
}
