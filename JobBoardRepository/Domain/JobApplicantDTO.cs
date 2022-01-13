using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoardRepository.Domain;

public class JobApplicantDTO
{

    public int ApplicantId { get; set; }
    public int JobId { get; set; }
    public string Status { get; set; }
}