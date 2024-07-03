namespace KKHS_API.OrderApp.Interface
{
    public class OrderInfoDTO
    {
        public string CustomerName { get; set; } = null!;

        public string CustomerPhone { get; set; } = null!;

        public string CustomerEmail { get; set; } = null!;

        public string? CustomerNote { get; set; }

        public string DeliveryName { get; set; } = null!;

        public string DeliveryPhone { get; set; } = null!;

        public string DeliveryAddress { get; set; } = null!;
    }
}
