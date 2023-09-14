using AutoMapper;
using WebAPIDemo.Dto;
using WebAPIDemo.Models;

namespace WebAPIDemo.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AccountMember, AccountMemberDto>().ReverseMap();
            CreateMap<ArtTattooService, ArtTattooServiceDto>().ReverseMap(); 
            CreateMap<ArtTattooStyle, ArtTattooStyleDto>().ReverseMap();
        }
    }
}
