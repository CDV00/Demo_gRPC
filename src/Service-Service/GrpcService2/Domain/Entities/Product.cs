using System;
using System.Collections.Generic;

namespace GrpcService2.Domain.Entities
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid CategoryId { get; set; }
        public int? Inventory { get;set; }
        public virtual Category Category { get; set; } = null!;
        public virtual Detail? Detail { get; set; }
    }
}
