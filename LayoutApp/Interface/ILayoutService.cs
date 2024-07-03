using KKHS_API.EFcore;

namespace KKHS_API.LayoutApp.Interface
{
    public interface ILayoutService
    {
        Task<List<Product>> GetNewProcductList();
        Task<Product> GetProcductDetail(int id);
        Task<List<GoodsTitleList>> GetTitleList();
        Task<List<Product>> GetGoodsItemList(int id);
        Task<List<Product>> Search(string value);
        Task<List<Product>> GetOnSaleProductList();
    }
}
