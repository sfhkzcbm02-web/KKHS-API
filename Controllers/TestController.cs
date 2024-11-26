using KKHS_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KKHS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        
        [HttpGet]   
        public string Test1()
        {
            return "取得資料";
        }

        [HttpGet("test2api")]
        public string Test2()
        {
            return "取得資料2";
        }

        [HttpPost]
        public ProductTest GetInfo(ProductTest Info)
        {
            ProductTest a = new ProductTest(){ 
                MerchantID = Info.MerchantID,
                MerchantTradeNo = Info.MerchantTradeNo,
                LogisticsSubType = Info.LogisticsSubType,
                CVSStoreID = Info.CVSStoreID,
                CVSStoreName = Info.CVSStoreName,
                CVSAddress = Info.CVSAddress,
                CVSTelephone = Info.CVSTelephone,
                CVSOutSide = Info.CVSOutSide,
                ExtraData = Info.ExtraData
            };
            return a;
        }
        [HttpPost("redirect-to-external")]
        public async Task<IActionResult> RedirectToExternal()
        {
            // 读取请求体
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            // 设置重定向的 URL
            var redirectUrl = $"http://localhost:5174/pay?{body}";

            // 返回重定向响应
            return Redirect(redirectUrl);
        }
    }
}
