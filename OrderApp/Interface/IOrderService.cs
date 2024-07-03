using KKHS_API.EFcore;

namespace KKHS_API.OrderApp.Interface
{
    public interface IOrderService
    {
        Task<List<OrderInfo>> GetOrderInfoList();
        Task<List<OrderInfo>> AddToOrder(OrderInfoDTO OrderInfoDTO);
        Task<List<OrderInfo>> GetOrderInfoListByToken();
    }
}
