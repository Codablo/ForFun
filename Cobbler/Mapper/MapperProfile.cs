using AutoMapper;
using Cobbler.DAO;
using Cobbler.DTOs.Allocation;
using Cobbler.DTOs.Plan;

namespace Cobbler.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Plan, PlanDto>();
            CreateMap<PlanDto, Plan>();
            CreateMap<PlanBaseDto, Plan>();

            CreateMap<AllocationDto, Allocation>();
            CreateMap<Allocation, AllocationDto>();
        }        
    }
}