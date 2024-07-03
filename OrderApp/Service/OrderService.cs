using KKHS_API.EFcore;
using KKHS_API.helper.JWT;
using KKHS_API.OrderApp.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace KKHS_API.OrderApp.Service
{
    public class OrderService : IOrderService
    {
        private readonly ApidataContext _context;
        private readonly IJWTservice _JWTservice;

        public OrderService(ApidataContext context, IJWTservice JWTservice)
        {
            _context = context;
            _JWTservice = JWTservice;
        }

        public async Task<List<OrderInfo>> GetOrderInfoList ()
        {
            return await _context.OrderInfos.ToListAsync();
        }

        public async Task<List<OrderInfo>> GetOrderInfoListByToken()
        {
            var token = _JWTservice.GetTokenByHeader();
            var userID = Convert.ToInt32(_JWTservice.GetUserIdByToken(token));
            return await _context.OrderInfos.Where( e => e.UserId == userID ).ToListAsync();
        }

        public async Task<List<OrderInfo>> AddToOrder(OrderInfoDTO OrderInfoDTO)
        {
            var token = _JWTservice.GetTokenByHeader();
            var userID = Convert.ToInt32(_JWTservice.GetUserIdByToken(token));
            var List = await _context.ShoppingCarts.Where(e => e.UserId == userID).ToListAsync();
            if(List.Count > 0)
            {
                foreach (var item in List)
                {
                    OrderInfo data = new OrderInfo();
                    data.UserId = userID;
                    data.ProductId = item.ProductId;
                    data.ProductName = item.ProductName;
                    data.ImgUrl = item.ImgUrl;
                    data.Size = item.Size;
                    data.Color = item.Color;
                    data.Count = item.Count;
                    data.Price = item.Price;
                    data.CustomerName = OrderInfoDTO.CustomerName;
                    data.CustomerPhone = OrderInfoDTO.CustomerPhone;
                    data.CustomerEmail = OrderInfoDTO.CustomerEmail;
                    data.CustomerNote = OrderInfoDTO.CustomerNote;
                    data.DeliveryName = OrderInfoDTO.DeliveryName;
                    data.DeliveryPhone = OrderInfoDTO.DeliveryPhone;
                    data.DeliveryAddress = OrderInfoDTO.DeliveryAddress;
                    await _context.AddAsync(data);
                    _context.SaveChanges();
                }
                _context.ShoppingCarts.RemoveRange(List);
                _context.SaveChanges();
            }
            return await GetOrderInfoList();
        }
    }
}
