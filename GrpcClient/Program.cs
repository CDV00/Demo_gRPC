using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // See https://aka.ms/new-console-template for more information
            Console.WriteLine("Press any key to Start");
            Console.ReadLine();
            using var channel = GrpcChannel.ForAddress("http://localhost:5243");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });


            Console.WriteLine("Press any key to Exit");
            Console.ReadLine();
        }
    }
}


