using AutoMapper;
using CleanConnect.Core.Messages;

namespace CleanConnect.Web
{
    public class WebProfile: Profile
    {
        public WebProfile()
        {
            CreateMap<AuthenticationRequestDto, ProcessRequestMessage>();
        }
    }
}