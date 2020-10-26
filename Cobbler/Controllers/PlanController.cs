using System;
using System.Collections.Generic;
using Cobbler.DTOs;
using Cobbler.DTOs.Allocation;
using Cobbler.DTOs.Plan;
using Cobbler.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cobbler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly PlanService _planService;

        public PlanController(PlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public ActionResult<List<PlanDto>> GetPlans()
        {
            return Ok(_planService.GetPlans());
        }

        [HttpGet("{plaId}")]
        public ActionResult<PlanDto> GetPlan(long plaId)
        {
            return Ok(_planService.GetPlan(plaId));
        }

        [HttpPost]
        public ActionResult<PlanDto> CreatePlan(PlanBaseDto planDto)
        {
            return Ok(_planService.CreatePlan(planDto));
        }

        [HttpPut("{planId}")]
        public ActionResult<PlanDto> UpdatePlan(PlanDto planDto, long planId)
        {
            planDto.Id = planId;
            return Ok(_planService.UpdatePlan(planDto));
        }

        [HttpGet("{planId}/allocation")]
        public ActionResult<List<AllocationDto>> GetAllocations(long planId)
        {
            return Ok(_planService.GetAllocations(planId));
        }

        [HttpPost("{planId}/allocation")]
        public ActionResult<AllocationDto> CreateAllocation(long planId, AllocationDto allocationDto)
        {
            allocationDto.PlanId = planId;
            return Ok(_planService.CreateAllocation(allocationDto));
        }
        
        [HttpGet("{planId}/allocation/{allocationId}")]
        public ActionResult<AllocationDto> GetAllocation(long planId, long allocationId)
        {
            return Ok(_planService.GetAllocation(planId, allocationId));
        }

        [HttpPut("{planId}/allocation/{allocationId}")]
        public ActionResult<AllocationDto> UpdateAllocation(long planId, long allocationId, AllocationDto allocationDto)
        {
            allocationDto.PlanId = planId;
            allocationDto.Id = allocationId;
            return Ok(_planService.UpdateAllocation(allocationDto));
        }

    }
}