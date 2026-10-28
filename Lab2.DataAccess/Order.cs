using System.ComponentModel.DataAnnotations;

namespace Lab2.DataAccess
{
    public enum OrderStatus
    {
        Shipped,
        Delivered,
        Cancelled
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public required string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new(); 
    }
}
