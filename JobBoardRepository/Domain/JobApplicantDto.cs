namespace JobBoardRepository.Domain;

public class JobApplicantDto
{
    public ApplicantDto ap { get; set; }
    public JobDto j { get; set; }
    public string status { get; set; }
}