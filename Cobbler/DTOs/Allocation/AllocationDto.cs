using System.ComponentModel.DataAnnotations;

namespace Cobbler.DTOs.Allocation
{
    public class AllocationDto
    {
        public long Id { get; set; }
        public long PlanId { get; set; }
        
        [Required(ErrorMessage = "Currency is required and must be between 1 and long.MaxValue")]
        [Range(1, long.MaxValue)]
        public long Currency { get; set; }
        public string Person { get; set; }
        public string Project { get; set; }
    }
}