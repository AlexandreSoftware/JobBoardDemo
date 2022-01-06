using System.Collections.Generic;
using JobBoardServices.View;

namespace JobBoardRepository.Domain;
public class Applicant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Job> Jobs { get; set; }
    public double WageExpectation { get; set; }

}