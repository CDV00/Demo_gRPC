using Grpc.Core;
using GrpcService.Protos;
using System.Threading.Tasks;

namespace GrpcService.Services
{
    public class ProductService : Product.ProductBase
    {
        private readonly ILogger<ProductService> _logger;
        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
        }
        public override Task<ProductModel> GetProductsInformation(GetProductDetail request, ServerCallContext context)
        {
            ProductModel productDetail = new ProductModel();
            if (request.ProductId == 1)
            {
                productDetail.ProductName = "Samsung TV";
                productDetail.ProductDescription = "Smart TV";
                productDetail.ProductPrice = 35000;
                productDetail.ProductStock = 10;
            }
            else if (request.ProductId == 2)
            {
                productDetail.ProductName = "HP Laptop";
                productDetail.ProductDescription = "HP Pavilion";
                productDetail.ProductPrice = 55000;
                productDetail.ProductStock = 20;
            }
            else if (request.ProductId == 3)
            {
                productDetail.ProductName = "IPhone";
                productDetail.ProductDescription = "IPhone 12";
                productDetail.ProductPrice = 65000;
                productDetail.ProductStock = 30;
            }

            return Task.FromResult(productDetail);
        }
        public override Task<ListOfListsOfStrings> ListProduct(GetProductDetail request, ServerCallContext context)
        {
            var product = new List<ProductModel>();
            var tv = new ProductModel();
            //
            tv.ProductName = "Samsung TV";
            tv.ProductDescription = "Smart TV";
            tv.ProductPrice = 35000;
            tv.ProductStock = 10;
            product.Add(tv);
            //
            var laptop = new ProductModel();
            laptop.ProductName = "HP Laptop";
            laptop.ProductDescription = "HP Pavilion";
            laptop.ProductPrice = 55000;
            laptop.ProductStock = 20;
            product.Add(laptop);
            //
            var phone = new ProductModel();
            phone.ProductName = "IPhone";
            phone.ProductDescription = "IPhone 12";
            phone.ProductPrice = 65000;
            phone.ProductStock = 30;
            product.Add(phone);
            /*if (request.ProductId == 1)
            {
                productDetail.ProductName = "Samsung TV";
                productDetail.ProductDescription = "Smart TV";
                productDetail.ProductPrice = 35000;
                productDetail.ProductStock = 10;
            }
            else if (request.ProductId == 2)
            {
                productDetail.ProductName = "HP Laptop";
                productDetail.ProductDescription = "HP Pavilion";
                productDetail.ProductPrice = 55000;
                productDetail.ProductStock = 20;
            }
            else if (request.ProductId == 3)
            {
                productDetail.ProductName = "IPhone";
                productDetail.ProductDescription = "IPhone 12";
                productDetail.ProductPrice = 65000;
                productDetail.ProductStock = 30;
            }*/
            ListOfListsOfStrings Products = new ListOfListsOfStrings();
            Products.Product.AddRange(product);
            //a.Mo = product;
            return Task.FromResult(Products);
        }
    }
}