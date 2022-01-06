using AutoMapper;
using JobBoardRepository.Domain;
using JobBoardServices.View;

namespace JobBoardServices.Profile;

public class JobProfile :AutoMapper.Profile
{
    
    public JobProfile()
    {
        CreateMap<Job, JobDTO>()
            .ForMember(x=>x.ProductId
            ,opt=>opt.MapFrom(src=>src.id))
            .ForMember(x=>x.Title
                ,opt=>opt.MapFrom(src=>src.title))
            .ForMember(x=>x.MaxPay
                ,opt=>opt.MapFrom(src=>src.maxPay))
            .ForMember(x=>x.MinPay
                ,opt=>opt.MapFrom(src=>src.minPay))
            .ForMember(x=>x.Description
                ,opt=>opt.MapFrom(src=>src.description))
            .ForMember(x=>x.SubTitle
                ,opt=>opt.MapFrom(src=>src.subTitle))
            .ReverseMap();
    }
}