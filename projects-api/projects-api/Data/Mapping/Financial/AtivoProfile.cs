using AutoMapper;
using projects_api.Data.Dtos.Financeiro;
using projects_api.Data.Models;

namespace projects_api.Data.Mapping.Financial;

public class AtivoProfile : Profile
{
    public AtivoProfile()
    {
        CreateMap<CreateAtivoDto, Ativo>();

        CreateMap<Ativo, ReadAtivoDto>();
    }
}
