using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService2.Infrastructure.ViewModel.Category
{
    public class CategoryResquest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
