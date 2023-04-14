using OrdersManagement.Domain.Core.Models;

namespace OrdersManagement.Domain
{
    public class Product : Entity
    {
        public Product(Guid id, string name) {
            Id = id;
            Name = name;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Order Order { get; set; }

        public Guid OrderId { get; set; }

        public string Name { get; set; }
    }
}
