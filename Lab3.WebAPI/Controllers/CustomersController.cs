using Lab2.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly OrderDbContext _db;

        public CustomersController()
        {
            _db = new OrderDbContext();
        }

        [HttpGet]
        public Customer[] GetCustomers()
        {
            return _db.Customers.Include(c => c.Orders).ThenInclude(o => o.OrderItems).ToArray();
        }

        [HttpGet("{id:int}")]
        public Customer? GetCustomer(int id)
        {
            return _db.Customers.Include(c => c.Orders).ThenInclude(o => o.OrderItems).FirstOrDefault(s => s.Id == id);
        }

        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        [HttpDelete("{id:int}")]
        public void DeleteCustomer(int id)
        {
            var customer = _db.Customers.FirstOrDefault(s => s.Id == id);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
            }
        }

        [HttpPut("{id:int}")]
        public void UpdateCustomer(int id, [FromBody] Customer customer)
        {
            var existing = _db.Customers.FirstOrDefault(s => s.Id == id);
            if (existing != null)
            {
                existing.Name = customer.Name;
                existing.Email = customer.Email;
                existing.Orders = customer.Orders;

                _db.SaveChanges();
            }
        }
    }
}
