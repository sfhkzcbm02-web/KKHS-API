using KKHS_API.EFcore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KKHS_API.helper.JWT
{
    public class JWTservice : IJWTservice
    {
        private JWToption _JWToption;
        private readonly ApidataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JWTservice(IOptions<JWToption> options, ApidataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _JWToption = options.Value;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }       

        public string GetToken (string account, string password)
        {   
            //userService 有寫通過才觸發此方法,不用擔心null值發生問題
            var userInfo = _context.UserInfos.Where(e => e.AccountNumber == account && e.Password == password).FirstOrDefault();
            var Claims = new[]
            {
                new Claim ("userId", userInfo.UserId.ToString()),
                new Claim (ClaimTypes.Name, userInfo.UserName),
                new Claim (ClaimTypes.Role,userInfo.Role )
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWToption.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer : _JWToption.Issuer,
                audience : _JWToption.Audience,
                claims : Claims,
                expires : DateTime.Now.AddDays(1),
                notBefore : DateTime.Now,
                signingCredentials : creds
                ) ;
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }

        public string DecodeToken (string token)
        {
            var handler = new JwtSecurityTokenHandler();

            // 解碼 JWT token sample
            var jwtToken = handler.ReadJwtToken(token);
            string? userName = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            return $"答案{userName},";
        }
        public string GetUserIdByToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            // 解碼 JWT token sample
            var jwtToken = handler.ReadJwtToken(token);
            string? UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            return UserId;
        }

        public string GetTokenByHeader ()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            // 獲取 Header 中的 token
            string Token = httpContext.Request.Headers["Authorization"];
            // 假設 Authorization Header 的格式是 Bearer {token}
            if (!string.IsNullOrEmpty(Token) && Token.StartsWith("Bearer "))
            {
                // 提取 token 部分
                Token = Token.Substring("Bearer ".Length).Trim();
            }

            return Token;
        }
    }
}
