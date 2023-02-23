﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService2.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository _CategoryRepository { get; }
        IProductRepository _ProductRepository { get; }
        IDetailRepository _DetailRepository { get; }

        Task CommitAsync();
    }
}
