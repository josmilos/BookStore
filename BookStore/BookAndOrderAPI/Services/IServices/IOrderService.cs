namespace BookAndOrderAPI.Services.IServices
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderDTO>> GetSingleOrder(int id);
        Task<ServiceResponse<List<OrderDTO>>> GetAllOrders();
        Task<ServiceResponse<List<OrderDTO>>> AddOrder(OrderDTO order);
        Task<ServiceResponse<List<OrderDTO>?>> UpdateOrder(OrderDTO order);
        Task<ServiceResponse<List<OrderDTO>>> DeleteOrder(int id);
    }
}
