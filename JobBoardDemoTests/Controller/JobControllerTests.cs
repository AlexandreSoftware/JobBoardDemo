using AutoMapper;
using JobBoardDemoApi.Controllers;
using JobBoardDemoApi.Controllers.Interface;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Moq;
using Xunit;

namespace JobBoardDemoTests.Controller;

public class JobControllerTests
{
    public Mock<IJobService> _js;
    public IJobController _jc;
    public JobControllerTests()
    {
        _js = new Mock<IJobService>();
        _jc = new JobController(_js.Object);
    }
    [Fact]
    public async void OnGet_NullValue_ShouldReturnNull()
    {
        //Arrange
        _js.Setup(x => x.Get()).ReturnsAsync((Job[])null);
        //Act   
        var result = await _jc.Get();
        //Assert
        Assert.Null( result.Value);
    }
    [Fact]
    public async void OnGet_emptyArray_ShouldReturnNull()
    {
        //Arrange
        _js.Setup(x => x.Get()).ReturnsAsync((Job[])null);
        //Act   
        var result = await _jc.Get();
        //Assert
        Assert.Null(result.Value);
    }
    [Fact]
    public async void OnPost_NullValue_ShouldReturnNull()
    {
        //Arrange
        _js.Setup(x => x.Post(null)).ReturnsAsync(false);
        //Act   
        var result = await _jc.Post(null);
        //Assert
        Assert.False(result.Value);   
    }
    [Fact]
    public async void OnPost_InvalidValue_ShouldReturnNull()
    {
        //Arrange
        _js.Setup(x => x.Post(null)).ReturnsAsync(false);
        //Act   
        var result = await _jc.Post(null);
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
        var obj = new Job {id = id};
        //Arrange
        _js.Setup(x => x.Post(obj)).ReturnsAsync(true);
        //Act   
        var result = await _jc.Post(obj);
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
        _js.Setup(x => x.Delete(id)).ReturnsAsync(true);
        //Act   
        var result = await _jc.Delete(id);
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
        var obj = new Job {id = id};
        //Arrange
        _js.Setup(x => x.Put(obj)).ReturnsAsync(true);
        //Act   
        var result = await _jc.Put(obj);
        //Assert
        Assert.True(result.Value);   
    }
}