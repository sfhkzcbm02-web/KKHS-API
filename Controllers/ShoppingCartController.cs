using KKHS_API.EFcore;
using KKHS_API.helper;
using KKHS_API.OrderApp.Interface;
using KKHS_API.OrderApp.Service;
using KKHS_API.ShoppingCartApp.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KKHS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ApidataContext _context;
        private readonly IOrderService _OrderService;

        public ShoppingCartController(ICartService cartService, ApidataContext context, IOrderService orderService)
        {
            _cartService = cartService;
            _context = context;
            _OrderService = orderService;
        }

        [HttpPost("AddTOcart")]
        public async Task<IActionResult> AddTOcart(ProductCartDto ProductCartDto)
        {
            var res = await _cartService.AddNewProcductTOcart(ProductCartDto);

            return new JsonResult(new ApiDataResult<Product>()
            {
                Success = true,
                Message = "新增成功",
                data = res
            });
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetCart")]
        public async Task<IActionResult> GetInfo()
        {
            var res = await _cartService.GetCartInfo();
            return new JsonResult(new ApiDataResult<List<ShoppingCart>>()
            {
                Success = true,
                Message = "新增成功",
                data = res
            });

        }

        [HttpPut("DeleteItem")]
        public async Task<bool> Delete (int Id)
        {
            return await _cartService.DeleteItem(Id);
        }

        [HttpPost("SummitOrder")]
        public async Task<List<OrderInfo>> AddToOrder(OrderInfoDTO OrderInfoDTO)
        {
            return await _OrderService.AddToOrder(OrderInfoDTO);
        }
    }
}
