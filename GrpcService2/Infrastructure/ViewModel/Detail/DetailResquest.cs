using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService2.Infrastructure.ViewModel.Detail
{
    public class DetailResquest
    {
        public Guid? Id { get; set; }
        public string DetailA { get; set; }
        public string DetailB { get; set; }
        public Guid ProductId { get; set; }
    }
}
