namespace KKHS_API.ShoppingCartApp.Interface
{
    public class ProductCartDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }  

        public required string Color { get; set; }
        public required string Size { get; set; }
    }
}
