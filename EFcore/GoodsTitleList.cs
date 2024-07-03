using System;
using System.Collections.Generic;

namespace KKHS_API.EFcore;

public partial class GoodsTitleList
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Part { get; set; } = null!;

    public string? Children { get; set; }
}
