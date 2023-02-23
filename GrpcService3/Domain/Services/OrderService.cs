using Grpc.Core;
using GrpcService3.Domain.Protos;
using GrpcService3.Domain.Interfaces;
using GrpcService3.Domain.Entities;
using System.Xml;
using Grpc.Net.Client;

namespace GrpcService3.Domain.Services
{
    public class OrderService : GrpcOrder.GrpcOrderBase
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderService(ILogger<OrderService> logger, IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
        }
        public override Task<OdersResponse> GetAll(OrderParamPaging request, ServerCallContext context)
        {
            var orders = _orderRepository.GetAll().ToList();
            var result = new OdersResponse();
            foreach (var item in orders)
            {
                var order = new Protos.OrderResponse { Id = item.Id.ToString() };
                result.Order.Add(order);

            }
            return Task.FromResult(result);
        }
        public override Task<OrderDetailsResponse> GetById(IdRequest request, ServerCallContext context)
        {
            var orderDetais = _orderDetailRepository.GetAll().Where(type => type.OrderId == new Guid(request.Id)).ToList();
            var result = new OrderDetailsResponse();
            foreach (var item in orderDetais)
            {
                var orderdetail = new OrderDetailResponse();
                orderdetail.OrderId = item.OrderId.ToString();
                orderdetail.ProductId = item.ProductId.ToString();
                orderdetail.Quantity = item.Quantity.Value;
                orderdetail.OrderDetailId = item.Id.ToString();
                result.OrderDetail.Add(orderdetail);
            }
            return Task.FromResult(result);
        }
        public override async Task<OrderBaseResponse> AddOrder(OrderDetailRequest request, ServerCallContext context)
        {
            try
            {
                var order = new Order()
                {
                    Id = new Guid()
                };
                await _orderRepository.CreateAsync(order);
                var orderDetail = new OrderDetail()
                {
                    ProductId = new Guid(request.ProductId),
                    OrderId = order.Id,
                    Quantity = request.Quantity
                };
                await _orderDetailRepository.CreateAsync(orderDetail);

                await _unitOfWork.CommitAsync();
                var product = new orderProductRequest()
                {
                    Id = request.ProductId,
                    Quantity = request.Quantity
                };
                var channel = GrpcChannel.ForAddress("http://localhost:5064");
                var productService = new GrpcProduct.GrpcProductClient(channel);
                var srerveReply = await productService.UpdateInventoryAsync(product);
                return await Task.FromResult(new OrderBaseResponse() { Status = true });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new OrderBaseResponse() { Status = false });
                throw;
            }
        }
        public override async Task<OrderBaseResponse> UpdateOrder(OrderDetailResponse request, ServerCallContext context)
        {
            try
            {
                var orderDetail = _orderDetailRepository.GetAll().Where(type => type.OrderId == new Guid(request.OrderId)).FirstOrDefault();
                var oldQuantity = orderDetail.Quantity.Value;
                orderDetail.ProductId = new Guid(request.ProductId);
                orderDetail.Quantity = request.Quantity;
                var result = _orderDetailRepository.Update(orderDetail);

                var product = new orderProductRequest()
                {
                    Id = request.ProductId,
                    Quantity = request.Quantity - oldQuantity
                };
                var channel = GrpcChannel.ForAddress("http://localhost:5064");
                var productService = new GrpcProduct.GrpcProductClient(channel);
                var srerveReply = await productService.UpdateInventoryAsync(product);

                await _unitOfWork.CommitAsync();
                return await Task.FromResult(new OrderBaseResponse() { Status = result });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new OrderBaseResponse() { Status = false });
                throw;
            }
        }

        public override async Task<OrderBaseResponse> DeleteOrder(IdRequest request, ServerCallContext context)
        {
            try
            {
                var order = await _orderRepository.GetById(new Guid(request.Id));
                var orderDetail = _orderDetailRepository.GetAll().Where(type => type.OrderId == order.Id).ToList();
                foreach (var item in orderDetail)
                {
                    _orderDetailRepository.Delete(item);
                }
                var result = _orderRepository.Delete(order);
                await _unitOfWork.CommitAsync();
                return await Task.FromResult(new OrderBaseResponse() { Status = result });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new OrderBaseResponse() { Status = false });
                throw;
            }
        }

        /** string OrderId = 1;
       string ProductId = 2;
       int32 Quantity = 3;
       string OrderDetailId = 4;
        * 
 //rpc DeleteOrder(IdRequest) returns(OrderBaseResponse);*/

    }
}
