using System;
using AutoMapper;
using WebApi.Api.Dto;
using WebApi.Core.Models;

namespace WebApi.Api.Map
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<MaterialVersion, MaterialVersionDto>();
            CreateMap<Material, MaterialDto>();

            CreateMap<MaterialVersionDto, MaterialVersion>();
            CreateMap<MaterialDto, Material>();
        }
    }
}
