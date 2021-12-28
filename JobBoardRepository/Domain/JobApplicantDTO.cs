using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoardRepository.Domain;

public class JobApplicantDTO
{

    public ApplicantDTO ap { get; set; }
    public JobDTO j { get; set; }
    public string status { get; set; }
}