using KKHS_API.EFcore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace KKHS.Service.UserApp
{
    public interface IUserService
    {
        Task<bool> Register(RegisterDto registerDto);
        Task<bool> IsRegister(RegisterDto registerDto);
        Task<bool> IsLogIn(string account, string password);
        Task<IActionResult> Login(string account, string password);
    }
}
