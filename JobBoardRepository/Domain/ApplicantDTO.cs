using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobBoardRepository.Domain;
public class ApplicantDTO
{
    [Key]
    public int id;
    [Required]
    public string name;
}