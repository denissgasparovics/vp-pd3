using Lab2.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly OrderDbContext _db;

        public OrderItemsController()
        {
            _db = new OrderDbContext();
        }

        [HttpGet]
        public OrderItem[] GetOrderItems()
        {
            return _db.OrderItems.ToArray();
        }

        [HttpGet("{id:int}")]
        public OrderItem? GetOrderItem(int id)
        {
            return _db.OrderItems.FirstOrDefault(s => s.Id == id);
        }

        [HttpPost]
        public void Post([FromBody] OrderItem orderItem)
        {
            _db.OrderItems.Add(orderItem);
            _db.SaveChanges();
        }

        [HttpDelete("{id:int}")]
        public void DeleteOrderItem(int id)
        {
            var orderItem = _db.OrderItems.FirstOrDefault(s => s.Id == id);
            if (orderItem != null)
            {
                _db.OrderItems.Remove(orderItem);
                _db.SaveChanges();
            }
        }

        [HttpPut("{id:int}")]
        public void UpdateOrderItem(int id, [FromBody] OrderItem orderItem)
        {
            var existing = _db.OrderItems.FirstOrDefault(s => s.Id == id);
            if (existing != null)
            {
                existing.ProductName = orderItem.ProductName;
                existing.Quantity = orderItem.Quantity;
                existing.UnitPrice = orderItem.UnitPrice;

                _db.SaveChanges();
            }
        }

        [HttpGet("{id:int}/items")]
        public OrderItem[] GetOrderItems(int id)
        {
            return _db.OrderItems.Where(s => s.OrderId == id).ToArray();
        }

        [HttpGet("search")]
        public OrderItem[] SearchOrderItems([FromQuery] string? productName)
        {
            IEnumerable<OrderItem> query = _db.OrderItems;

            if (!string.IsNullOrWhiteSpace(productName))
            {
                query = query.Where(i => i.ProductName.ToLower().Contains(productName.ToLower()));
            }

            return query.ToArray();
        }

    }
}
