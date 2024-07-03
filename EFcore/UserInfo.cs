using System;
using System.Collections.Generic;

namespace KKHS_API.EFcore;

public partial class UserInfo
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreatTime { get; set; }
}
