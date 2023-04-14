namespace OrderManagementApplication.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        protected ProductViewModel()
        {
        }

        public Guid Id { get; protected set; }

        public string Name { get; set; }
    }
}
