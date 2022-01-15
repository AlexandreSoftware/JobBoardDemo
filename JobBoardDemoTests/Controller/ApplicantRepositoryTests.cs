using AutoMapper;
using JobBoardDemoApi.Controllers;
using JobBoardDemoApi.Controllers.Interface;
using JobBoardRepository.Domain;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Moq;
using Xunit;

namespace JobBoardDemoTests.Controller;

public class ApplicantControllerTests
{
    public Mock<IApplicantService> _as;
    public IApplicantController _ac;
    public ApplicantControllerTests()
    {
        _as = new Mock<IApplicantService>();
        _ac = new ApplicantController(_as.Object);
    }
    [Fact]
    public async void OnGet_NullValue_ShouldReturnNull()
    {
        //Arrange
        _as.Setup(x => x.Get()).ReturnsAsync((Applicant[])null);
        //Act   
        var result = await _ac.Get();
        //Assert
        Assert.Null( result.Value);
    }
    [Fact]
    public async void OnGet_emptyArray_ShouldReturnNull()
    {
        //Arrange
        _as.Setup(x => x.Get()).ReturnsAsync((Applicant[])null);
        //Act   
        var result = await _ac.Get();
        //Assert
        Assert.Null(result.Value);
    }
    [Fact]
    public async void OnPost_NullValue_ShouldReturnNull()
    {
        //Arrange
        _as.Setup(x => x.Post(null)).ReturnsAsync(false);
        //Act   
        var result = await _ac.Post(null);
        //Assert
        Assert.False(result.Value);   
    }
    [Fact]
    public async void OnPost_InvalidValue_ShouldReturnNull()
    {
        //Arrange
        _as.Setup(x => x.Post(null)).ReturnsAsync(false);
        //Act   
        var result = await _ac.Post(null);
        //Assert
        Assert.False(result.Value);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(30)]
    [InlineData(12)]
    [InlineData(242)]
    public async void OnPost_ValidValue_ShouldReturnTrue(int id)
    {
        var obj = new Applicant {Id = id};
        //Arrange
        _as.Setup(x => x.Post(obj)).ReturnsAsync(true);
        //Act   
        var result = await _ac.Post(obj);
        //Assert
        Assert.True(result.Value);   
    }
    
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(30)]
    [InlineData(12)]
    [InlineData(242)]
    public async void OnDelete_ValidValue_ShouldReturnTrue(int id)
    {
        //Arrange
        _as.Setup(x => x.Delete(id)).ReturnsAsync(true);
        //Act   
        var result = await _ac.Delete(id);
        //Assert
        Assert.True(result.Value);   
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(30)]
    [InlineData(12)]
    [InlineData(242)]
    public async void OnPut_ValidValue_ShouldReturnTrue(int id)
    {
        var obj = new Applicant {Id = id};
        //Arrange
        _as.Setup(x => x.Put(obj)).ReturnsAsync(true);
        //Act   
        var result = await _ac.Put(obj);
        //Assert
        Assert.True(result.Value);   
    }
}
