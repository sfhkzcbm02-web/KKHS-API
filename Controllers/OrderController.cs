using KKHS_API.EFcore;
using KKHS_API.helper;
using KKHS_API.OrderApp.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KKHS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrderController (IOrderService orderService)
        {
            _OrderService = orderService;
        }

        [HttpGet]
        public async Task<List<OrderInfo>> GetOrderInfoList()
        {
            return await _OrderService.GetOrderInfoList();
        }

        [HttpGet("GetOrderInfoListByToken")]
        public async Task<IActionResult> GetOrderInfoListByToken()
        {
            var res = await _OrderService.GetOrderInfoListByToken();
            return new JsonResult(new ApiDataResult<List<OrderInfo>>()
            {
                Success = true,
                Message = "新增成功",
                data = res
            });
        }
    }
}
