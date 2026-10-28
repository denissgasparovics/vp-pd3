using Lab2.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext _db;

        public OrdersController()
        {
            _db = new OrderDbContext();
        }

        [HttpGet]
        public Order[] GetOrders()
        {
            return _db.Orders.ToArray();
        }

        [HttpGet("{id:int}")]
        public Order? GetOrder(int id)
        {
            return _db.Orders.FirstOrDefault(s => s.Id == id);
        }

        [HttpPost]
        public void Post([FromBody] Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        [HttpDelete("{id:int}")]
        public void DeleteOrder(int id)
        {
            var order = _db.Orders.FirstOrDefault(s => s.Id == id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
            }
        }

        [HttpPut("{id:int}")]
        public void UpdateOrder(int id, [FromBody] Order order)
        {
            var existing = _db.Orders.FirstOrDefault(s => s.Id == id);
            if (existing != null)
            {
                existing.OrderNumber = order.OrderNumber;
                existing.OrderDate = order.OrderDate;
                existing.Status = order.Status;

                _db.SaveChanges();
            }
        }

        [HttpGet("search")]
        public Order[] SearchOrders([FromQuery] string? number, [FromQuery] OrderStatus? status)
        {
            IEnumerable<Order> query = _db.Orders;

            if (!string.IsNullOrWhiteSpace(number))
            {
                query = query.Where(o => o.OrderNumber.ToLower().Contains(number.ToLower()));
            }

            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            return query.ToArray();
        }
    }
}
