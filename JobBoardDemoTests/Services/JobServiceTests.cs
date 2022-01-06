using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bogus;
using FluentAssertions;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.Profile;
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
        
        JobProfile myProfile = new JobProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        m = new Mapper(configuration);
        this._jr = new Mock<IJobRepository>();
        
        this._js = new JobService(_jr.Object, m);
        this.faker = new Faker<JobDTO>()
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
    public void OnGet_ValidValue_ShouldReturnMappedObject()
    {
        //Arrange
        var data = faker.Generate(20).ToArray();
        _jr.Setup(x => x.Get()).ReturnsAsync(data);
        var mockresult = data.Select(x =>
        {
            return new Job()
            {
                id = x.ProductId,
                description = x.Description,
                title = x.Title,
                maxPay = x.MaxPay,
                minPay = x.MinPay,
                subTitle = x.SubTitle
            };
        }).ToArray();
        //Act   
        var result = _js.Get();
        //Assert
        result.Should().BeEquivalentTo(mockresult);
    }
    [Fact]
    public async void OnPost_FalseValue_ShouldReturnFalse()
    {
        //Arrange
        _jr.Setup(x => x.Post(null)).ReturnsAsync(false);
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
        var data = faker.Generate(1)[0];
        _jr.Setup(x => x.Post(It.IsAny<JobDTO>())).ReturnsAsync(true);
        var mockresult = new Job()
        {
            id = data.ProductId,
            description = data.Description,
            title = data.Title,
            maxPay = data.MaxPay,
            minPay = data.MinPay,
            subTitle = data.SubTitle
        };
        //Act   
        var result = _js.Post(mockresult);
        //Assert
        Assert.True(await result);
    }

    [Fact]
    public async void OnGetId_ValidId_ShouldReturnMappedObject()
    {
        //Arrange
        var data = faker.Generate(1)[0];
        _jr.Setup(x => x.GetId(1)).ReturnsAsync(data);
        var mockresult = new Job()
        {
            id = data.ProductId,
            description = data.Description,
            title = data.Title,
            maxPay = data.MaxPay,
            minPay = data.MinPay,
            subTitle = data.SubTitle
        };
        //Act   
        var result = _js.GetId(1);
        //Assert
        result.Should().BeEquivalentTo(mockresult);
    }
    [Fact]
    public async void OnDelete_FalseValue_ShouldReturnFalse()
    {
        //Arrange
        _jr.Setup(x => x.Delete(1)).ReturnsAsync(false);
        //Act   
        var result = _js.Delete(1);
        //Assert
        Assert.False(await result);
    }

    [Fact]
    public async void OnDelete_ValidValue_ShouldReturnTrue()
    {
        //Arrange
        var data = faker.Generate(1)[0];
        _jr.Setup(x => x.Delete(1)).ReturnsAsync(true);
        //Act   
        var result = _js.Delete(1);
        //Assert
        Assert.True(await result);
    }
    [Fact]
    public async void OnPut_FalseValue_ShouldReturnFalse()
    {
        //Arrange
        _jr.Setup(x => x.Put(It.IsAny<JobDTO>())).ReturnsAsync(false);
        //Act   
        var result = _js.Put(new Job());
        //Assert
        Assert.False(await result);
    }

    [Fact]
    public async void OnPut_ValidValue_ShouldReturnMappedObject()
    {
        //Arrange
        var data = faker.Generate(1)[0];
        _jr.Setup(x => x.Put(It.IsAny<JobDTO>())).ReturnsAsync(true);
        var mockresult = new Job()
        {
            id = data.ProductId,
            description = data.Description,
            title = data.Title,
            maxPay = data.MaxPay,
            minPay = data.MinPay,
            subTitle = data.SubTitle
        };
        //Act   
        var result = _js.Put(mockresult);
        //Assert
        Assert.True(await result);
    }

}