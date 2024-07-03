namespace KKHS_API.helper.JWT
{
    public interface IJWTservice
    {
        public string GetToken(string account, string password);
        public string DecodeToken(string token);
        public string GetUserIdByToken(string token);
        public string GetTokenByHeader();
    }
}
