using AutoMapper;
using Azil.DAL.DataModel;
using Azil.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.Repository.Automapper
{
    public class RepositoryMappingService : IRepositoryMappingService
    {
        public Mapper mapper;

        public RepositoryMappingService()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Korisnici, UsersDomain>(); //ruta baza - GUI
                    cfg.CreateMap<UsersDomain, Korisnici>(); //ruta GUI - baza
                });
            mapper = new Mapper(config);
        }
        public TDestination Map<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }
    }
}