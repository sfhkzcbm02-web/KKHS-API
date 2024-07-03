using KKHS.Service.UserApp;
using KKHS.Service.UserApp.Service;
using KKHS_API.EFcore;
using KKHS_API.helper;
using KKHS_API.helper.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace KKHS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTservice jWTservice;

        public UserController(IUserService userService, IJWTservice jWTservice)
        {
            _userService = userService;
            this.jWTservice = jWTservice;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register (RegisterDto registerDto)
        {
            var checkDB = await _userService.IsRegister(registerDto);
            if(checkDB)
            {
                return new JsonResult(new ApiDataResult<UserInfo>()
                {
                    Success = false,
                    Message = "註冊失敗,已有人註冊此帳號密碼"
                });
            } else
            {
                var token = jWTservice.GetToken(registerDto.AccountNumber, registerDto.Password);
                var data = await _userService.Register(registerDto);
                return new JsonResult(new ApiDataResult<UserInfo>()
                {
                    Token = token,
                    Success = data,
                    Message = "註冊成功",
                });
            }
        }

        [HttpGet("login")]
        public async Task<IActionResult> LogIn(string account, string password)
        {
           return  await _userService.Login(account, password);
        }
        
        [HttpGet("testdecode")]
        public string  test (string token)
        {
            return jWTservice.GetUserIdByToken(token);
        }
    }
}
