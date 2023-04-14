using OrdersManagement.Domain.Core.Models;

namespace OrdersManagement.Domain
{
    public class Order : Entity
    {
        public Order(
            Guid id,
            string name,
            string deliveryAddress)
        {
            Id= id;
            Name= name;
            DeliveryAddress= deliveryAddress;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Order() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public string Name { get; set; }

        public string DeliveryAddress { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}