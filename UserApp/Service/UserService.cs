using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KKHS_API.EFcore;
using KKHS_API.helper;
using KKHS_API.helper.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KKHS.Service.UserApp.Service
{
    public class UserService : IUserService
    {
        private readonly ApidataContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTservice jWTservice;

        public UserService(ApidataContext context,IMapper mapper, IJWTservice jWTservice)
        {
            _context = context;
            _mapper = mapper;
            this.jWTservice = jWTservice;   
        }
        public async Task<bool> Register(RegisterDto registerDto)
        {

            var result = _mapper.Map<RegisterDto, UserInfo>(registerDto);
            await _context.AddAsync(result);
            _context.SaveChanges();
            return true;
        }
        public async Task<bool> IsRegister (RegisterDto registerDto)
        {
            var DBaccount = await _context.UserInfos.Where(m => m.AccountNumber == registerDto.AccountNumber && m.Password == registerDto.Password).FirstOrDefaultAsync(); 
            if (DBaccount != null)
            {
                return true; 
            }
            return false;
        }

        public async Task<bool> IsLogIn (string account, string password )
        {
            var DBInfo =  await _context.UserInfos.Where(m => m.AccountNumber == account && m.Password == password).FirstOrDefaultAsync();
            if (DBInfo != null)
            {
                return true;
            }
            return false;
        }

        public async Task<IActionResult> Login (string account, string password)
        {
            if (await IsLogIn(account, password))
            {
                var Info = await _context.UserInfos.Where(m => m.AccountNumber == account && m.Password == password).FirstOrDefaultAsync();
                return new JsonResult(new ApiDataResult<UserInfo>()
                {
                    Success = true,
                    Message = "登入成功",
                    data = Info ,
                    Token = jWTservice.GetToken(account, password)
                });
            } else
            {
                return new JsonResult(new ApiDataResult<UserInfo>()
                {
                    Success = false,
                    Message = "登入失敗",
                }
                );
            }
            
        }

        
    }
}
