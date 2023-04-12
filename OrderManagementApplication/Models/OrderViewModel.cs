namespace OrderManagementApplication.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public string DeliveryAddress { get; set; }
    }
}
