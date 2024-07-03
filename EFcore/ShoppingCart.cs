using System;
using System.Collections.Generic;

namespace KKHS_API.EFcore;

public partial class ShoppingCart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ImgUrl { get; set; } = null!;

    public string Size { get; set; } = null!;

    public string Color { get; set; } = null!;

    public int Count { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatTime { get; set; }
}
