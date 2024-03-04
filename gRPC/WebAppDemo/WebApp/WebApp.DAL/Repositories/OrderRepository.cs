using Microsoft.EntityFrameworkCore;
using WebApp.Common.DTOs.Order;
using WebApp.Common.Protos;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Models;

namespace WebApp.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepository;

        public OrderRepository(AppDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync() =>
            await _context.Orders.AsNoTracking()
                .Select(x => new OrderDto
                {
                    Id = x.Id,
                    Status = x.Status,
                    CustomerId = x.CustomerId,
                    ProductsId = x.OrderProducts.Select(p => p.ProductId).ToList(),
                })
                .ToListAsync();

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id) =>
            await _context.Orders.AsNoTracking()
                .Select(x => new OrderDto
                {
                    Id = x.Id,
                    Status = x.Status,
                    CustomerId = x.CustomerId,
                    ProductsId = x.OrderProducts.Select(p => p.ProductId).ToList(),
                })
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<OrderDto?> PostNewOrderAsync(CreateOrderDto orderDto)
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Pending,
                CustomerId = orderDto.CustomerId
            };

            await _context.Orders.AddAsync(order);
            await SaveAllProductsForOrderIdAsync(order, orderDto);
            await _context.SaveChangesAsync();

            OrderDto response = new()
            {
                Id = order.Id,
                CustomerId = orderDto.CustomerId,
                ProductsId = orderDto.ProductsId,
            };

            return response;
        }

        public async Task UpdateOrderStatusAsync(Guid id, OrderStatus status)
        {
            Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order == null) return;

            order.Status = status;
            await _context.SaveChangesAsync();
        }

        private async Task SaveAllProductsForOrderIdAsync(Order order, CreateOrderDto orderDto)
        {
            foreach (Guid productId in orderDto.ProductsId)
            {
                var product = await _productRepository.GetProductByIdAsync(productId);

                if (product is not null)
                {
                    OrderProduct orderProduct = new()
                    {
                        ProductId = product.Id,
                        OrderId = order.Id,
                    };

                    await _context.OrderProducts.AddAsync(orderProduct);
                }
            }
        }
    }
}
