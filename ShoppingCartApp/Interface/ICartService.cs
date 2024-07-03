using KKHS_API.EFcore;

namespace KKHS_API.ShoppingCartApp.Interface
{
    public interface ICartService
    {
        Task<Product> AddNewProcductTOcart(ProductCartDto ProductCartDto);
        Task<List<ShoppingCart>> GetCartInfo();
        Task<bool> DeleteItem(int Id);
    }
}
