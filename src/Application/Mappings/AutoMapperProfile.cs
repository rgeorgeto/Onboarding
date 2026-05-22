using AutoMapper;
using Domain.Aggregates;
using Domain.Entities;
using Domain.Enums;
using Application.Modules.Queries.ObterProgresso;
using Application.Modules.Queries.ObterModulos;
using Application.Modules.Queries.ObterConquistas;

namespace Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Modulo, ModuloDto>()
            .ForMember(d => d.Tipo, o => o.MapFrom(s => s.Tipo.ToString()));

        CreateMap<Modulo, ModuloProgressoDto>()
            .ForMember(d => d.Tipo, o => o.MapFrom(s => s.Tipo.ToString()))
            .ForMember(d => d.Status, o => o.Ignore())
            .ForMember(d => d.DataConclusao, o => o.Ignore());

        CreateMap<Conquista, ConquistaDto>()
            .ForMember(d => d.Desbloqueada, o => o.Ignore());

        CreateMap<ProgressoProfissional, ProgressoDto>()
            .ForMember(d => d.Modulos, o => o.Ignore())
            .ForMember(d => d.Conquistas, o => o.Ignore())
            .ForMember(d => d.ModulosConcluidos, o => o.Ignore())
            .ForMember(d => d.TotalModulos, o => o.Ignore())
            .ForMember(d => d.PercentualConclusao, o => o.Ignore());
    }
}