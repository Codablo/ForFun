using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cobbler.DTOs.Plan
{
    public class PlanDto : PlanBaseDto
    {
        public long Id { get; set; }
    }
}