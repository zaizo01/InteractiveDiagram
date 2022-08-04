
using AutoMapper;
using DIGEIG.Api.Dto;
using DIGEIG.Domain.Entities;

namespace DIGEIG.Api.Mapper
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Sys_Tb_Institutions, Sys_Tb_InstitutionsDto >().ReverseMap();
            CreateMap<Sys_Tb_Institutions, Sys_Tb_InstitutionsWithIdDto>().ReverseMap();
            CreateMap<Sys_Tb_InstitutionsStructure, Sys_Tb_InstitutionsStructureDto>().ReverseMap(); 
            CreateMap<Sys_Tb_InstitutionsStructure, Sys_Tb_InstitutionsStructureWithIdDto>().ReverseMap();
        }
    }
}
