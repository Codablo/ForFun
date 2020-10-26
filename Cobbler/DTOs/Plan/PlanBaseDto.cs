using System.ComponentModel.DataAnnotations;

namespace Cobbler.DTOs.Plan
{
    public class PlanBaseDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Currency is required and must be between 1 and long.MaxValue")]
        [Range(1, long.MaxValue)]
        public long Currency { get; set; }
        
        public string CurrencyUnit { get; set; }
    }
}