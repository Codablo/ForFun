using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cobbler.DAO;
using Cobbler.Database;
using Cobbler.DTOs;
using Cobbler.DTOs.Allocation;
using Cobbler.DTOs.Plan;
using Cobbler.Exceptions;

namespace Cobbler.Services
{
    public class PlanService
    {
        private readonly PlanDataStore _planDataStore;
        private readonly IMapper _mapper;
        private readonly PlanDataContext _planRepository;

        public PlanService(PlanDataStore planDataStore, IMapper mapper, PlanDataContext planRepository)
        {
            _planDataStore = planDataStore;
            _mapper = mapper;
            _planRepository = planRepository;
        }
        
        public List<PlanDto> GetPlans()
        {
            var plans = _planDataStore.GetPlans();
            return plans.Select(p => _mapper.Map<PlanDto>(p)).ToList();
        }

        public PlanDto GetPlan(long planId)
        {
            var existingPlan = _planDataStore.GetPlan(planId);
            if (existingPlan == null)
            {
                throw new RecordNotFoundException($"A plan with id {planId} was not found.");
            }
            return _mapper.Map<PlanDto>(existingPlan);
        }

        public PlanDto CreatePlan(PlanBaseDto planBaseDto)
        {
            var insertedPlan = _planDataStore.CreatePlan(_mapper.Map<Plan>(planBaseDto));
            return _mapper.Map<PlanDto>(insertedPlan);
        }

        public PlanDto UpdatePlan(PlanDto planDto)
        {
            var existingPlan = _planDataStore.GetPlan(planDto.Id);
            if (existingPlan == null)
            {
                throw new RecordNotFoundException($"A plan with id {planDto.Id} was not found.");
            }
            
            if (existingPlan.Currency > planDto.Currency)
            {
                var currentAllocationTotal = GetCurrentTotalAllocation(planDto.Id);
                if (currentAllocationTotal > planDto.Currency)
                {
                    throw new BusinessLogicException($"Unable to lower plan funds below current allocations of {currentAllocationTotal}.");
                }
            }

            var newPlan = _planDataStore.UpdatePlan(_mapper.Map<Plan>(planDto));
            return _mapper.Map<PlanDto>(newPlan);
        }

        public List<AllocationDto> GetAllocations(long planId)
        {
            var allocations = _planDataStore.GetAllocations(planId);
            return allocations.Select(a => _mapper.Map<AllocationDto>(a)).ToList();
        }

        public AllocationDto CreateAllocation(AllocationDto allocationDto)
        {
            var plan = _planDataStore.GetPlan(allocationDto.PlanId);
            if (plan == null)
            {
                throw new RecordNotFoundException($"Unable to find plan with id {allocationDto.PlanId}.");
            }

            if (allocationDto.Person == null && allocationDto.Project == null)
            {
                throw new BusinessLogicException("Person and Project can not both be null");
            }

            allocationDto.Person ??= "Any Person";
            allocationDto.Project ??= "Any Project";
            
            var currentAllocationSum = GetCurrentTotalAllocation(allocationDto.PlanId);
            var maxAllowedAllocation = plan.Currency - currentAllocationSum;
            if (maxAllowedAllocation < allocationDto.Currency)
            {
                throw new BusinessLogicException($"{allocationDto.Currency} is more than the remaining allotment of {maxAllowedAllocation}.");
            }

            var newAllocation = _planDataStore.CreateAllocation(_mapper.Map<Allocation>(allocationDto));
            return _mapper.Map<AllocationDto>(newAllocation);
        }
        
        public AllocationDto GetAllocation(long planId, long allocationId)
        {
            var allocation = _planDataStore.GetAllocation(planId, allocationId);
            return _mapper.Map<AllocationDto>(allocation);
        }

        public AllocationDto UpdateAllocation(AllocationDto allocationDto)
        {
            var plan = _planDataStore.GetPlan(allocationDto.PlanId);
            if (plan == null)
            {
                throw new RecordNotFoundException($"Unable to find plan with id {allocationDto.PlanId}.");
            }

            var existingAllocation = _planDataStore.GetAllocation(allocationDto.PlanId, allocationDto.Id);
            if (existingAllocation == null)
            {
                throw new RecordNotFoundException($"Unable to find allocation with id {allocationDto.Id}.");
            }
            
            if (allocationDto.Person == null && allocationDto.Project == null)
            {
                throw new BusinessLogicException("Person and Project can not both be null");
            }

            allocationDto.Person ??= "Any Person";
            allocationDto.Project ??= "Any Project";
            
            var currentAllocationSum = GetCurrentTotalAllocation(allocationDto.PlanId);
            var maxAllowedAllocation = plan.Currency - currentAllocationSum + existingAllocation.Currency;
            if (maxAllowedAllocation < allocationDto.Currency)
            {
                throw new BusinessLogicException($"{allocationDto.Currency} is more than the allotment of {maxAllowedAllocation} which does not include the funds from allocation id {existingAllocation.Id}.");
            }

            var newAllocation = _planDataStore.CreateAllocation(_mapper.Map<Allocation>(allocationDto));
            return _mapper.Map<AllocationDto>(newAllocation);
        }

        private long GetCurrentTotalAllocation(long planId)
        {
            return _planDataStore.GetAllocatedSum(planId);
        }
    }
}