using AutoMapper;
using KKHS_API.EFcore;
using KKHS_API.helper;
using KKHS_API.LayoutApp.Interface;
using KKHS_API.LayoutApp.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace KKHS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayoutController : ControllerBase
    {
        private readonly ApidataContext _context;
        private readonly IMapper _mapper;
        private readonly ILayoutService _layoutService;


        public LayoutController(ApidataContext context, IMapper mapper, ILayoutService layoutService)
        {
            _context = context;
            _mapper = mapper;
            _layoutService = layoutService;
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetList")]
        public async Task<List<Product>> GetProducts()
        {

            return await _layoutService.GetNewProcductList();
        }
        [HttpGet("GetOnSaleProductList")]
        public async Task<List<Product>> GetOnSalePList()
        {

            return await _layoutService.GetOnSaleProductList();
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProcduct(int id)
        {
            var result = await _layoutService.GetProcductDetail(id);
            return result;
        }

        [Route("GetTitleList")]
        [HttpGet]
        public async Task<IActionResult> GetGoodsTitleList()
        {
            var res = await _layoutService.GetTitleList();
            if( res != null)
            {
                return new JsonResult(new ApiDataResult<List<GoodsTitleList>>()
                {
                    Success = true,
                    Message = "獲取成功",
                    data = res
                });
            } else
            {
                return new JsonResult(new ApiDataResult<List<GoodsTitleList>>()
                {
                    Success = false,
                    Message = "獲取失敗",
                });
            }
        }

        [HttpGet("GetGoodsItemList/{id}")]
        public async Task<IActionResult> GetGoodsItemList(int id)
        {
            var res = await _layoutService.GetGoodsItemList(id);
            if (res != null)
            {
                return new JsonResult(new ApiDataResult<List<Product>>()
                {
                    Success = true,
                    Message = "獲取成功",
                    data = res
                });
            }
            else
            {
                return new JsonResult(new ApiDataResult<List<Product>>()
                {
                    Success = false,
                    Message = "獲取失敗",
                });
            }
        }

        [HttpGet("SearchItem")]
        public async Task<IActionResult> SearchItem(string value)
        {
            var res = await _layoutService.Search(value);
            if(res.Count() != 0)
            {
                return new JsonResult(new ApiDataResult<List<Product>>()
                {
                    Success = true,
                    Message = "獲取成功",
                    data = res
                }) ;
            }
            else
            {
                return new JsonResult(new ApiDataResult<List<Product>>()
                {
                    Success = false,
                    Message = "獲取失敗,無資料",
                    data = []
                });
            }
        }

    }
 }

