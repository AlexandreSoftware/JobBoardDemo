namespace JobBoardRepository.Domain;

public class JobApplicant
{
    public Applicant ap { get; set; }
    public Job j { get; set; }
    public string status { get; set; }
}