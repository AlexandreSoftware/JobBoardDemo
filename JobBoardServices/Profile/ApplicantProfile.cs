using AutoMapper;
using JobBoardRepository.Domain;

namespace JobBoardServices.Profile;

public class ApplicantProfile:AutoMapper.Profile
{
    public ApplicantProfile()
    {
        CreateMap<Applicant, ApplicantDTO>().ReverseMap();
    }
}