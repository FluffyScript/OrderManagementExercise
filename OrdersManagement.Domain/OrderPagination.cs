namespace OrdersManagement.Domain
{
    public class OrderPagination : BasePagination<Order>
    {
        public OrderPagination(int skip, int take)
            : base(i => true)
        {
            ApplyPaging(skip, take);
        }
    }
}
