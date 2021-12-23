using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bogus;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Moq;
using Xunit;

namespace JobBoardDemoTests.Services;

public class JobServiceTests
{
    private IJobService _js;
    public Mock<IJobRepository> _jr;
    public Faker<JobDTO> faker;
    public Mapper m ;
    public JobServiceTests()
    {
        
        var myProfile = new JobProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        m = new Mapper(configuration);
        this._jr = new Mock<IJobRepository>();
        
        this._js = new JobService(_jr.Object, m);
        this.faker = new Faker<JobDTO>()
            .RuleFor(property: x => x.id, c => c.IndexFaker)
            .RuleFor(property: x => x.title, setter: c => c.Company + " " + c.Commerce)
            .RuleFor(property: x => x.description, setter: c => c.Lorem.Word())
            .RuleFor(property: x => x.subTitle, setter: c => c.Lorem.Word())
            .RuleFor(property: x => x.maxPay, setter: c => c.Finance.Random.Int())
            .RuleFor(property: x => x.minPay, setter: (c, usr) =>
            {
                int val;
                do
                {
                    val = c.Finance.Random.Int();
                } while (val > usr.maxPay);

                return val;
            });
    }

    [Fact]
    public void OnGet_NullValue_ShouldReturnNull()
    {
        //Arrange
        _jr.Setup(x => x.Get()).Returns((JobDTO[]) null);
        //Act   
        var result = _js.Get();
        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void OnGet_ValidValue_ShouldReturnMappedObject()
    {
        //Arrange
        var data = faker.Generate(20).ToArray();
        _jr.Setup(x => x.Get()).Returns(data);
        var mockresult = data.Select(x =>
        {
            return new Job()
            {
                id = x.id,
                description = x.description,
                title = x.title,
                maxPay = x.maxPay,
                minPay = x.minPay,
                subTitle = x.subTitle
            };
        }).ToArray();
        //Act   
        var result = _js.Get();
        //Assert
        Assert.Equal(mockresult,result);
    }
    [Fact]
    public void OnPost_FalseValue_ShouldReturnFalse()
    {
        //Arrange
        _jr.Setup(x => x.Post(null)).Returns(false);
        //Act   
        var result = _js.Post(null);
        //Assert
        Assert.False(result);
    }

    [Fact]
    public void OnPost_ValidValue_ShouldReturnMappedObject()
    {
        ;
        //Arrange
        var data = faker.Generate(1)[0];
        _jr.Setup(x => x.Post(data)).Returns(true);
        var mockresult = new Job()
        {
            id = data.id,
            description = data.description,
            title = data.title,
            maxPay = data.maxPay,
            minPay = data.minPay,
            subTitle = data.subTitle
        };
        //Act   
        var result = _js.Post(mockresult);
        //Assert
        Assert.True(result);
    }
}