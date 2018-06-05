using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Controllers
{
    public class TaxRequestDto
    {
        [Required]
        public EmploymentType  EmploymentType{ get; set; }
        [Required]
        [Range(0, 100000000)]
        public decimal? Salary { get; set; }
        public int? DaysPerYear { get; set; }
        public bool? IncludesSuper { get; set; }
    }
}