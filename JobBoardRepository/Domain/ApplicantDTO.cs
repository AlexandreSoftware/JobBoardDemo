using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobBoardRepository.Domain;
public class ApplicantDTO
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100,ErrorMessage = "Name needs to be between 1 and 100 characters")]
    public string Name { get; set; }
    [Required]
    public double WageExpectation { get; set; }
    public string status { get; set; } 
}