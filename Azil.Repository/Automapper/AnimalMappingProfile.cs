using AutoMapper;
using Azil.DAL.DataModel;
using Azil.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.Repository.Automapper
{
    public class AnimalMappingProfile : Profile
    {
        public AnimalMappingProfile()
        {
            CreateMap<AnimalsDomain, KucniLjubimci>()
                .ForMember(dest => dest.id_ljubimca, opt => opt.MapFrom(src => src.IdLjubimca))
                .ForMember(dest => dest.id_udomitelja, opt => opt.MapFrom(src => src.IdUdomitelja))
                .ForMember(dest => dest.ime_ljubimca, opt => opt.MapFrom(src => src.ImeLjubimca))
                .ForMember(dest => dest.tip_ljubimca, opt => opt.MapFrom(src => src.TipLjubimca))
                .ForMember(dest => dest.opis_ljubimca, opt => opt.MapFrom(src => src.OpisLjubimca))
                .ForMember(dest => dest.udomljen, opt => opt.MapFrom(src => src.Udomljen))
                .ForMember(dest => dest.imgUrl, opt => opt.MapFrom(src => src.ImgUrl));

            CreateMap<KucniLjubimci, AnimalsDomain>()
                .ForMember(dest => dest.IdLjubimca, opt => opt.MapFrom(src => src.id_ljubimca))
                .ForMember(dest => dest.IdUdomitelja, opt => opt.MapFrom(src => src.id_udomitelja))
                .ForMember(dest => dest.ImeLjubimca, opt => opt.MapFrom(src => src.ime_ljubimca))
                .ForMember(dest => dest.TipLjubimca, opt => opt.MapFrom(src => src.tip_ljubimca))
                .ForMember(dest => dest.OpisLjubimca, opt => opt.MapFrom(src => src.opis_ljubimca))
                .ForMember(dest => dest.Udomljen, opt => opt.MapFrom(src => src.udomljen))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.imgUrl));
        }
    }
}
