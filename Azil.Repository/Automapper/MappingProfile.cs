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
                .ForMember(dest => dest.id_korisnika, opt => opt.MapFrom(src => src.id_korisnika))
                .ForMember(dest => dest.korisnickoIme, opt => opt.MapFrom(src => src.KorisnickoIme))
                .ForMember(dest => dest.ime, opt => opt.MapFrom(src => src.Ime))
                .ForMember(dest => dest.prezime, opt => opt.MapFrom(src => src.Prezime))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.lozinka, opt => opt.MapFrom(src => src.Lozinka))
                .ForMember(dest => dest.admin, opt => opt.MapFrom(src => src.Admin))
                .ForMember(dest => dest.profileImg, opt => opt.MapFrom(src => src.ProfileImg))
                .ForMember(dest => dest.token, opt => opt.MapFrom(src => src.Token));

            CreateMap<Korisnici, UsersDomain>()
                .ForMember(dest => dest.id_korisnika, opt => opt.MapFrom(src => src.id_korisnika))
                .ForMember(dest => dest.KorisnickoIme, opt => opt.MapFrom(src => src.korisnickoIme))
                .ForMember(dest => dest.Ime, opt => opt.MapFrom(src => src.ime))
                .ForMember(dest => dest.Prezime, opt => opt.MapFrom(src => src.prezime))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.Lozinka, opt => opt.MapFrom(src => src.lozinka))
                .ForMember(dest => dest.Admin, opt => opt.MapFrom(src => src.admin))
                .ForMember(dest => dest.ProfileImg, opt => opt.MapFrom(src => src.profileImg))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.token));
        }
    }
}