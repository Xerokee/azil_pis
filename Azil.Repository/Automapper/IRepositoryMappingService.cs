using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.Repository.Automapper
{
    public interface IRepositoryMappingService
    {
        TDestination Map<TDestination>(object source);
    }
}