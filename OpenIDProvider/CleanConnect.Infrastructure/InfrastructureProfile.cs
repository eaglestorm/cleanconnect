using System.Linq;
using AutoMapper;
using CleanConnect.Core.Dal.Record;
using CleanConnect.Core.Model.Authorization;
using CleanConnect.Core.Model.Client;
using CleanConnect.Infrastructure.Record;

namespace CleanConnect.Infrastructure
{
    public class InfrastructureProfile: Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Client, ClientRecord>();
            CreateMap<AuthenticationRequest, AuthorizationRequestRecord>()
                .ForMember(x=>x.Locales, opt=> opt.ResolveUsing(s=>s.Locales.Select(x=> x.Name)))
                .ForMember(x=>x.Prompts,opt=>opt.ResolveUsing(s=>s.Prompts.ToString()))
                .ForMember(x=>x.Scopes,opt=>opt.ResolveUsing(s=>s.Scopes.ToString()));
        }
    }
}