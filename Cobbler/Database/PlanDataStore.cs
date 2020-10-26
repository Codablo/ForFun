using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cobbler.DAO;

namespace Cobbler.Database
{
    public class PlanDataStore
    {
        private long _nextPlanId = 1;
        private long _nextAllocationId = 1;
        private Dictionary<long, Plan> Plans { get; } = new Dictionary<long, Plan>();
        private Dictionary<long, Allocation> Allocations { get; } = new Dictionary<long, Allocation>();

        public IEnumerable<Plan> GetPlans()
        {
            return Plans.Values;
        }

        public Plan GetPlan(long planId)
        {
            var planExists = Plans.ContainsKey(planId);
            return planExists ? Plans[planId] : null;
        }
        
        public Plan CreatePlan(Plan plan)
        {
            plan.Id = _nextPlanId;
            _nextPlanId++;
            Plans.Add(plan.Id, plan);
            return plan;
        }

        public Plan UpdatePlan(Plan plan)
        {
            Plans[plan.Id] = plan;
            return plan;
        }

        public IEnumerable<Allocation> GetAllocations(long planId)
        {
            return Allocations.Values.Where(a => a.PlanId == planId);
        }

        public Allocation GetAllocation(long planId, long allocationId)
        {
            return Allocations.Values.FirstOrDefault(a => a.Id == allocationId && a.PlanId == planId);
        }

        public Allocation CreateAllocation(Allocation allocation)
        {
            allocation.Id = _nextAllocationId;
            _nextAllocationId++;
            Allocations[allocation.Id] = allocation;
            return allocation;
        }

        public Allocation UpdateAllocation(Allocation allocation)
        {
            var allocationExists = Allocations.ContainsKey(allocation.Id);
            return allocationExists ? Allocations[allocation.Id] : null;
        }

        public long GetAllocatedSum(long planId)
        {
            return Allocations.Values.Where(a => a.PlanId == planId).Sum(a => a.Currency);
        }

    }
}