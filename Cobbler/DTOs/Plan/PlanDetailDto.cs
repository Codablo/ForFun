using System.Collections.Generic;
using Cobbler.DTOs.Allocation;

namespace Cobbler.DTOs.Plan
{
    public class PlanDetailDto : PlanDto
    {
        public List<AllocationDto> Allocations;
    }
}