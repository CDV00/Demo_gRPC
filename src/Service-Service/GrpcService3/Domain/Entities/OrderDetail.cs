using System;
using System.Collections.Generic;

namespace GrpcService3.Domain.Entities
{
    public partial class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
