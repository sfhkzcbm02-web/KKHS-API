using Azure.Core;
using KKHS_API.EFcore;
using KKHS_API.helper.JWT;
using KKHS_API.LayoutApp.Interface;
using KKHS_API.ShoppingCartApp.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;

namespace KKHS_API.ShoppingCartApp.Service
{
    public class CartService : ICartService
    {
        private readonly ApidataContext _context;
        private readonly ILayoutService _layoutService;
        private readonly IJWTservice _jWTservice;

        public CartService (ApidataContext context, ILayoutService layoutService, IJWTservice jWTservice)
        {
            _context = context;
            _layoutService = layoutService;
            _jWTservice = jWTservice;
        }   

        public async Task<List<ShoppingCart>> GetCartInfo()
        {
            var token = _jWTservice.GetTokenByHeader();
            var UserId = Convert.ToInt32(_jWTservice.GetUserIdByToken(token));
            return await _context.ShoppingCarts.Where(e => e.UserId == UserId).ToListAsync();
        }
        
        public async Task<Product> AddNewProcductTOcart (ProductCartDto ProductCartDto)
        {
            var token = _jWTservice.GetTokenByHeader();
            var ProductInfo = await _layoutService.GetProcductDetail(ProductCartDto.ProductId);
            ShoppingCart ShoppingCart = new ShoppingCart() {
                ProductId = ProductInfo.Id,
                UserId = Convert.ToInt32(_jWTservice.GetUserIdByToken(token)) ,
                ProductName = ProductInfo.ProductName,
                ImgUrl = ProductInfo.ImgUrl,
                Size = ProductCartDto.Size,
                Color = ProductCartDto.Color,
                Price = ProductInfo.HotPrice,
                Count = ProductCartDto.Count
            };
            await _context.AddAsync(ShoppingCart);
            _context.SaveChanges();
            return ProductInfo;
        }

        public async Task<bool> DeleteItem (int Id)
        {
            var Item = await _context.ShoppingCarts.Where(e => e.Id == Id).FirstOrDefaultAsync();
            _context.ShoppingCarts.Remove(Item);
            _context.SaveChanges();
            return true;
        }
    }
}
