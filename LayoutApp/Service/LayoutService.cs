using AutoMapper;
using KKHS_API.EFcore;
using KKHS_API.helper.JWT;
using KKHS_API.LayoutApp.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace KKHS_API.LayoutApp.Service
{
    public class LayoutService : ILayoutService
    {
        private readonly ApidataContext _context;
        private readonly IMapper _mapper;
  

        public LayoutService(ApidataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetNewProcductList ()
        {
            return await _context.Products.Where(e => e.Id <= 4).ToListAsync();
            
        }

        public async Task<List<Product>> GetOnSaleProductList()
        {
            return await _context.Products.Where(e => e.HotPrice < 600).Take(4).ToListAsync();
           
        }
        public async Task<Product> GetProcductDetail (int id) 
        {
            var result = await _context.Products.Where(e => e.Id == id).FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<List<GoodsTitleList>> GetTitleList ()
        {
            return await _context.GoodsTitleLists.ToListAsync();
        }

        public async Task<List<Product>> GetGoodsItemList(int id) 
        { 
            var resPart = await _context.GoodsTitleLists.Where(e => e.Id == id).FirstOrDefaultAsync();
            return await _context.Products.Where(e => e.Part == resPart.Part).ToListAsync();
        }
        public async Task<List<Product>> Search(string value)
        {
            var res = await _context.Products.Where(e => e.ProductName.Contains(value)).ToListAsync();
            return res;
        }
    }
}
