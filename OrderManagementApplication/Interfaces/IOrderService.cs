using OrderManagementApplication.Models;

namespace OrderManagementApplication
{
    public interface IOrderService : IDisposable
    {
        Task Create(OrderViewModel orderViewModel);
        Task<IEnumerable<OrderViewModel>> GetAll();
        Task<IEnumerable<OrderViewModel>> GetAll(int skip, int take);
        Task<OrderViewModel> GetById(Guid id);
        Task Update(OrderViewModel orderViewModel);
        Task Cancel(Guid id);
    }
}
