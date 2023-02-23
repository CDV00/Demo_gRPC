using Grpc.Core;
using GrpcService;
using GrpcService.Protos;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
        public override Task<HelloReply> SayHe(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello-" + "World"
            });
        }
    }
    public partial class Employee
    {
        public int EmpId { get; set; }
        public string Name { get; set; }

        partial void GenerateEmployeeId();

    }
    public partial class Employee
    {
        partial void GenerateEmployeeId()
        {
            this.EmpId = 1;
        }
    }
}