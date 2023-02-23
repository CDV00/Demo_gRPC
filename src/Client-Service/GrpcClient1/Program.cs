using Grpc.Net.Client;
using GrpcClient1;
var message = new HelloRequest
{
};
var channel = GrpcChannel.ForAddress("http://localhost:5243");
var client = new Greeter.GreeterClient(channel);
var srerveReply = await client.SayHeAsync(message);

var clients = new Product.ProductClient(channel);
var product = new GetProductDetail
{
    ProductId = 2
};
var serverReplyProduct = await clients.ListProductAsync(product);
foreach(var item in serverReplyProduct.Product)
{
    Console.WriteLine($"{item.ProductName} | {item.ProductDescription} | {item.ProductPrice} | {item.ProductStock}");
}
Console.WriteLine(serverReplyProduct.Product.Count);
Console.WriteLine(srerveReply.Message);
Console.ReadLine();
