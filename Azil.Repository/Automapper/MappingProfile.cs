using AutoMapper;
using Azil.DAL.DataModel;
using Azil.Model;

namespace Azil.Repository.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsersDomain, Korisnici>()
                .ForMember(dest => dest.id_korisnika, opt => opt.MapFrom(src => src.IdKorisnika))
                .ForMember(dest => dest.ime, opt => opt.MapFrom(src => src.Ime))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.lozinka, opt => opt.MapFrom(src => src.Lozinka))
                .ForMember(dest => dest.admin, opt => opt.MapFrom(src => src.Admin));

            CreateMap<Korisnici, UsersDomain>()
                .ForMember(dest => dest.IdKorisnika, opt => opt.MapFrom(src => src.id_korisnika))
                .ForMember(dest => dest.Ime, opt => opt.MapFrom(src => src.ime))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.Lozinka, opt => opt.MapFrom(src => src.lozinka))
                .ForMember(dest => dest.Admin, opt => opt.MapFrom(src => src.admin));
        }
    }
}