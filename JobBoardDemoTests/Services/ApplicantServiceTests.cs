using System.Linq;
using AutoMapper;
using Bogus;
using FluentAssertions;
using JobBoardRepository;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.Profile;
using JobBoardServices.Service;
using JobBoardServices.View;
using Moq;
using Xunit;

namespace JobBoardDemoTests.Services;

public class ApplicantServiceTests
{
    private IApplicantService _js;
    public Mock<IApplicantRepository> _ar;
    public Mock<IJobApplicantRepository> _jar;
    public Faker<ApplicantDTO> ApFaker;
    public Faker<JobDTO> JobFaker;
    public Mapper m ;
    public ApplicantServiceTests()
    {
        
        JobProfile myProfile = new JobProfile();
        ApplicantProfile myProfile2 = new ApplicantProfile();
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(myProfile);
            cfg.AddProfile(myProfile2);
        });
        
        m = new Mapper(configuration);
        this._jar = new Mock<IJobApplicantRepository>();
        this._ar = new Mock<IApplicantRepository>();
        this._js = new ApplicantService(_ar.Object,m,_jar.Object);
        this.ApFaker = new Faker<ApplicantDTO>()
            .RuleFor<int>(property:x=>x.Id,setter:y=>y.IndexFaker)
            .RuleFor<string>(property:x=>x.Name,setter:y=>y.Name.FullName())
            .RuleFor<double>(property:x=>x.WageExpectation,setter:y=>y.Finance.Random.Double());
        this.JobFaker = new Faker<JobDTO>()
            .RuleFor(property: x => x.ProductId, c => c.IndexFaker)
            .RuleFor(property: x => x.Title, setter: c => c.Company + " " + c.Commerce)
            .RuleFor(property: x => x.Description, setter: c => c.Lorem.Word())
            .RuleFor(property: x => x.SubTitle, setter: c => c.Lorem.Word())
            .RuleFor(property: x => x.MaxPay, setter: c => c.Finance.Random.Int())
            .RuleFor(property: x => x.MinPay, setter: (c, usr) =>
            {
                int val;
                do
                {
                    val = c.Finance.Random.Int();
                } while (val > usr.MaxPay);

                return val;
            });
    }

    [Fact]
    public async void OnGet_ValidValue_ShouldReturnMappedObject()
    {
        //Arrange
        var data = ApFaker.Generate(20).ToArray();
        var data2 = JobFaker.Generate(20).ToArray();
        _ar.Setup(x => x.Get()).ReturnsAsync(data);
        _jar.Setup(x => x.GetAppliedJobs(It.IsAny<int>())).Returns<int>(async x=>data2.Where(y=>y.ProductId==x).ToArray());
        var mockresult = data.Select(x =>
        {
            return new Applicant()
            {
                Id = x.Id,
                Name = x.Name,
                Jobs = data2.Where(y => y.ProductId == x.Id).Select(z => new Job()
                {
                    id= z.ProductId,
                    description = z.Description,
                    maxPay = z.MaxPay,
                    minPay = z.MinPay,
                    subTitle = z.SubTitle,
                    title = z.Title
                }).ToArray(),
                WageExpectation = x.WageExpectation
            };

        }).ToArray();
        //Act   
        Applicant[] result = await _js.Get();
        //Assert
        result.Should().BeEquivalentTo(mockresult);
    }
    [Fact]
    public async void OnPost_FalseValue_ShouldReturnFalse()
    {
        //Arrange
        _ar.Setup(x => x.Post(null)).ReturnsAsync(false);
        //Act   
        var result = _js.Post(null);
        //Assert
        Assert.False( await result);
    }

    [Fact]
    public async void OnPost_ValidValue_ShouldReturnMappedObject()
    {
        ;
        //Arrange
        var data = ApFaker.Generate(1)[0];
        var data2 = JobFaker.Generate(1)[0];
        _ar.Setup(x => x.Post(It.IsAny<ApplicantDTO>())).ReturnsAsync(true);
        var mockresult = 
             new Applicant()
            {
                Id = data.Id,
                Name = data.Name,
                WageExpectation = data.WageExpectation};
        //Act   
        var result = await _js.Post(mockresult);
        mockresult.Jobs = new Job[]
        {
            new Job()
            {
                id = data2.ProductId,
                description = data2.Description,
                maxPay = data2.MaxPay,
                minPay = data2.MinPay,
                subTitle = data2.SubTitle,
                title = data2.Title
            }
        };
        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void OnGetId_ValidId_ShouldReturnMappedObject()
    {
        //Arrange
        var data = ApFaker.Generate(1)[0];
        var data2 = JobFaker.Generate(5);
        _ar.Setup(x => x.GetId(1)).ReturnsAsync(data);
        _jar.Setup(x => x.GetAppliedJobs(It.IsAny<int>())).Returns<int>(async x=>data2.ToArray());
        var mockresult = new Applicant()
        {
            Id = data.Id,
            Name = data.Name,
            Jobs = data2.Select(x=>new Job()
            {
                id= x.ProductId,
                description = x.Description,
                maxPay = x.MaxPay,
                minPay = x.MinPay,
                subTitle = x.SubTitle,
                title = x.Title
            }).ToArray(),
            WageExpectation = data.WageExpectation
        };
        //Act   
        var result = await _js.GetId(1);
        //Assert
        result.Should().BeEquivalentTo(mockresult);
    }
    [Fact]
    public async void OnDelete_FalseValue_ShouldReturnFalse()
    {
        //Arrange
        _ar.Setup(x => x.Delete(1)).ReturnsAsync(false);
        //Act   
        var result = _js.Delete(1);
        //Assert
        Assert.False(await result);
    }

    [Fact]
    public async void OnDelete_ValidValue_ShouldReturnTrue()
    {
        //Arrange
        var data = ApFaker.Generate(1)[0];
        _ar.Setup(x => x.Delete(1)).ReturnsAsync(true);
        //Act   
        var result = _js.Delete(1);
        //Assert
        Assert.True(await result);
    }
    [Fact]
    public async void OnPut_FalseValue_ShouldReturnFalse()
    {
        //Arrange
        _ar.Setup(x => x.Put(It.IsAny<ApplicantDTO>())).ReturnsAsync(false);
        //Act   
        var result = _js.Put(new Applicant());
        //Assert
        Assert.False(await result);
    }

    [Fact]
    public async void OnPut_ValidValue_ShouldReturnMappedObject()
    {
        //Arrange
        var data = ApFaker.Generate(1)[0];
        var data2 = JobFaker.Generate(1)[0];
        _ar.Setup(x => x.Put(It.IsAny<ApplicantDTO>())).ReturnsAsync(true);
        var mockresult = new Applicant()
        {
            Id = data.Id,
            Name = data.Name,
            Jobs = new Job[]{new Job()
            {
                id= data2.ProductId,
                description = data2.Description,
                maxPay = data2.MaxPay,
                minPay = data2.MinPay,
                subTitle = data2.SubTitle,
                title = data2.Title
            }},
            WageExpectation = data.WageExpectation
        };
        //Act   
        var result = _js.Put(mockresult);
        //Assert
        Assert.True(await result);
    }
}