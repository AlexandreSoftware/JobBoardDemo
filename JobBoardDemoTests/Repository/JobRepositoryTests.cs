using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using JobBoardDemoApi.Controllers;
using JobBoardDemoApi.Controllers.Interface;
using JobBoardRepository;
using JobBoardRepository.Domain;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Moq;
using Xunit;

namespace JobBoardDemoTests.Controller;

public class JobRepositoryTests
{
    public Mock<DapperWrapper> _dw;
    public JobRepository _jr;
    public Faker<JobDTO> faker;
    public JobRepositoryTests()
    {
        _dw = new Mock<DapperWrapper>();
        _jr = new JobRepository(_dw.Object);
    }
    [Fact]
    public void OnGet_ValidValue_ShouldReturnObject()
    {
        //Arrange
        var data = faker.Generate(20).ToArray();
        _dw.Setup(x => x.Query<JobDTO>(It.IsAny<string>())).Returns(data);
        //Act   
        var result = _jr.Get();
        //Assert
        result.Should().BeEquivalentTo(data);
    }

    [Fact]
    public async void OnPost_FalseValue_ShouldReturnFalse()
    {
        //Arrange
        _dw.Setup(x => x.Query<JobDTO>(It.IsAny<string>())).Returns(Array.Empty<JobDTO>());
        //Act   
        var result = _jr.Post(null);
        //Assert
        Assert.False(await result);
    }

    // [Fact]
    // public async void OnPost_ValidValue_ShouldReturnMappedObject()
    // {
    //     //Arrange
    //     var data = faker.Generate(1)[0];
    //     _dw.Setup(x => x.Query<JobDTO>(It.IsAny<string>()))
    //         .ReturnsAsync<JobDTO[],JobDTO>(Array.Empty<JobDTO>());
    //     var result = await _jr.Post(data);
    //     //Assert
    //     Assert.True(result);
    // }

    [Fact]
    public void OnGetId_ValidId_ShouldReturnMappedObject()
    {
        //Arrange
        var data = faker.Generate(1);
        _dw.Setup(x => x.Query<JobDTO>(It.IsAny<string>())).Returns(data);
        //Act   
        var result = _jr.GetId(1);
        //Assert
        result.Should().BeEquivalentTo(data[0]);
    }
    // }
    // [Fact]
    // public void OnDelete_FalseValue_ShouldReturnFalse()
    // {
    //     //Arrange
    //     _jr.Setup(x => x.Delete(1)).Returns(false);
    //     //Act   
    //     var result = _jr.Delete(1);
    //     //Assert
    //     Assert.False(result);
    // }
    //
    // [Fact]
    // public void OnDelete_ValidValue_ShouldReturnTrue()
    // {
    //     //Arrange
    //     var data = faker.Generate(1)[0];
    //     _jr.Setup(x => x.Delete(1)).Returns(true);
    //     //Act   
    //     var result = _jr.Delete(1);
    //     //Assert
    //     Assert.True(result);
    // }
    // [Fact]
    // public void OnPut_FalseValue_ShouldReturnFalse()
    // {
    //     //Arrange
    //     _jr.Setup(x => x.Put(It.IsAny<JobDTO>())).Returns(false);
    //     //Act   
    //     var result = _jr.Put(new Job());
    //     //Assert
    //     Assert.False(result);
    // }
    //
    // [Fact]
    // public void OnPut_ValidValue_ShouldReturnMappedObject()
    // {
    //     ;
    //     //Arrange
    //     var data = faker.Generate(1)[0];
    //     _jr.Setup(x => x.Put(It.IsAny<JobDTO>())).Returns(true);
    //     var mockresult = new Job()
    //     {
    //         id = data.ProductId,
    //         description = data.Description,
    //         title = data.Title,
    //         maxPay = data.MaxPay,
    //         minPay = data.MinPay,
    //         subTitle = data.SubTitle
    //     };
    //     //Act   
    //     var result = _jr.Put(mockresult);
    //     //Assert
    //     Assert.True(result);
    // }
    
}