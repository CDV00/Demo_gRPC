using System;
using System.Collections.Generic;

namespace GrpcService2.Domain.Entities
{
    public partial class Detail
    {
        public Guid Id { get; set; }
        public string? DetailA { get; set; }
        public string? DetailB { get; set; }
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
