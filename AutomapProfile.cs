using AutoMapper;
using KKHS.Service.UserApp;
using KKHS_API.EFcore;

namespace KKHS_API
{
    public class AutomapProfile : Profile
    {
        public AutomapProfile() 
        {
            CreateMap<RegisterDto, UserInfo>();
        }
    }
}
