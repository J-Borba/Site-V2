using AutoMapper;
using projects_api.Data.Dtos.Financeiro;
using projects_api.Data.Models;

namespace projects_api.Data.Mapping.Financial;

public class ProventoProfile : Profile
{
    public ProventoProfile()
    {
        CreateMap<CreateProventoDto, Provento>();

        CreateMap<Provento, ReadProventoDto>();
    }
}
