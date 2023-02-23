using AutoMapper;
using GrpcService2.Domain.Interfaces;
using GrpcService2.Domain.Entities;
using Grpc.Core;
using GrpcService2.Infrastructure.ViewModel.Reponse;
using GrpcService2.Infrastructure.ViewModel.Product;
using NetTopologySuite.Index.HPRtree;
using Microsoft.EntityFrameworkCore;

namespace GrpcService2.Domain.Services
{
    public class grpcProductService : GrpcProduct.GrpcProductBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ILogger<grpcProductService> _logger;
        private readonly IProductRepository _productRepository;

        public grpcProductService(IUnitOfWork unitOfWork,
                                  IMapper mapper,
                                  IProductRepository productRepository,
                                ILogger<grpcProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
        }



        public override Task<response> SayHello(request request, ServerCallContext context)
        {
            return Task.FromResult(new response
            {
                Message = "Hello " + request.Name
            });
        }
        public override Task<ListDataProduct> GetAll(ParamPaging request, ServerCallContext context)
        {
            var products = _productRepository.GetAll().AsTracking().ToList();
            var result = new ListDataProduct();
            foreach (var item in products)
            {
                var product = new DataProduct();
                product.Id = item.Id.ToString();
                product.Name = item.Name;
                product.CategoryId = item.CategoryId.ToString();
                product.Inventory = item.Inventory.Value;
                result.Products.Add(product);
            }
            return Task.FromResult(result);
        }
        public override async Task<DataProduct> GetById(idRequest request, ServerCallContext context)
        {
            var product = await _productRepository.GetById(new Guid(request.Id));
            var result = new DataProduct();

            result.Id = product.Id.ToString();
            result.Name = product.Name;
            result.CategoryId = product.CategoryId.ToString();
            result.Inventory = product.Inventory.Value;

            return await Task.FromResult(result);
        }
        public override async Task<BaseResponse> UpdateInventory(orderProductRequest request, ServerCallContext context)
        {
            try
            {
                var product = await _productRepository.GetById(new Guid(request.Id));
                product.Inventory = product.Inventory.Value - request.Quantity;

                var result = _productRepository.Update(product);
                await _unitOfWork.CommitAsync();

                return await Task.FromResult(new BaseResponse() { Status = result });
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse() { Status = false });
                throw;
            }
            
        }
        public override async Task<BaseResponse> UpdateProduct(DataProduct request, ServerCallContext context)
        {
            try
            {
                var product = await _productRepository.GetById(new Guid(request.Id));
                product.CategoryId = new Guid(request.CategoryId);
                product.Name = request.Name;
                product.Inventory = request.Inventory;
                var result = _productRepository.Update(product);
                await _unitOfWork.CommitAsync();

                return await Task.FromResult(new BaseResponse() { Status = result });
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse() { Status = false });
                throw;
            }

        }
        public override async Task<BaseResponse> DeleteProduct(idRequest request, ServerCallContext context)
        {
            try
            {
                var product = await _productRepository.GetById(new Guid(request.Id));
                var result = _productRepository.Delete(product);
                await _unitOfWork.CommitAsync();

                return await Task.FromResult(new BaseResponse() { Status = result });
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse() { Status = false });
                throw;
            }

        }
        public override async Task<BaseResponse> AddProduct(DataProduct request, ServerCallContext context)
        {
            try
            {
                var product = new Product()
                {
                    CategoryId = new Guid(request.CategoryId),
                    Name = request.Name,
                    Inventory = request.Inventory
                };
                await _productRepository.CreateAsync(product);
                await _unitOfWork.CommitAsync();

                return await Task.FromResult(new BaseResponse() { Status = true });
            }
            catch (Exception)
            {
                return await Task.FromResult(new BaseResponse() { Status = false });
                throw;
            }

        }

    }
}