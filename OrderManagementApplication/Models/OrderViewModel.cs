namespace OrderManagementApplication.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DeliveryAddress { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set;}
    }
}
